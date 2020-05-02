using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Scheduler.Data;

namespace Scheduler
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            SetDefaults();

            StartDateDatePicker.Format = DateTimePickerFormat.Custom;
            StartDateDatePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            StartDateUpdateDatePicker.Format = DateTimePickerFormat.Custom;
            StartDateUpdateDatePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";

            EndDateDatePicker.Format = DateTimePickerFormat.Custom;
            EndDateDatePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            EndDateUpdateDatePicker.Format = DateTimePickerFormat.Custom;
            EndDateUpdateDatePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
        }

        private void SetDefaults()
        {
            var weekData = AppointmentDao.GetAppointmentsInCurrentWeek()
                .Select(a => new { a.Title, Start = a.Start.ToLocalTime(), End = a.End.ToLocalTime() });
            var week = new BindingSource();
            week.DataSource = weekData;

            var monthData = AppointmentDao.GetAppointmentsInCurrentMonth()
                .Select(a => new { a.Title, Start = a.Start.ToLocalTime(), End = a.End.ToLocalTime() });
            var month = new BindingSource();
            month.DataSource = monthData;

            EditAddressComboBox.DataSource = AddressDao.GetAddresses();
            RenameTextBox.Text = "";
            ApptInCurrentWeek.DataSource = week;
            ApptInCurrentMonth.DataSource = month;

            Dictionary<string, int> cities = AddressDao.GetAllCities();
            CitySelectBox.DataSource = new BindingSource(cities, null);
            CitySelectBox.DisplayMember = "Key";
            CitySelectBox.ValueMember = "Value";

            Dictionary<string, int> customers = CustomerDao.GetAllCustomers();
            EditCustomerComboBox.DataSource = new BindingSource(customers, null);
            EditCustomerComboBox.DisplayMember = "Key";
            EditCustomerComboBox.ValueMember = "Value";

            CustomerAptComboBox.DataSource = new BindingSource(customers, null);
            CustomerAptComboBox.DisplayMember = "Key";
            CustomerAptComboBox.ValueMember = "Value";

            CustomerUpdateComboBox.DataSource = new BindingSource(customers, null);
            CustomerUpdateComboBox.DisplayMember = "Key";
            CustomerUpdateComboBox.ValueMember = "Value";

            Dictionary<string, int> appts = AppointmentDao.GetAllAppointments();
            if (appts.Count > 0)
            {
                AppointmentsComboBox.DataSource = new BindingSource(appts, null);
                AppointmentsComboBox.DisplayMember = "Key";
                AppointmentsComboBox.ValueMember = "Value";
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ResetAddCustomerButton_Click(object sender, EventArgs e)
        {
            CustomerNameTextBox.Text = null;
            CitySelectBox.SelectedIndex = 0;
            Address1TextBox.Text = null;
            Address2TextBox.Text = null;
            PhoneTextBox.Text = null;
            ZipTextBox.Text = null;
        }

        private void AddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = CustomerNameTextBox.Text;
                int addressId = AddressDao.GetAddressIdByString(Address1TextBox.Text);

                // create the address if one doesn't exist
                if (addressId == -1)
                {
                    var newAddress = AddressDao.CreateAddress(Address1TextBox.Text,
                                                              Address2TextBox.Text,
                                                              Convert.ToInt32(CitySelectBox.SelectedValue),
                                                              ZipTextBox.Text,
                                                              PhoneTextBox.Text,
                                                              DateTime.Now,
                                                              DateTime.Now,
                                                              LoginForm.username,
                                                              LoginForm.username);
                    bool added = AddressDao.SaveNewAddress(newAddress);

                    if (added)
                    {
                        addressId = AddressDao.GetAddressIdByString(Address1TextBox.Text);
                    }
                }

                if (customerName.Trim() != "" && addressId > 0)
                {
                    var customer = CustomerDao.CreateCustomer(customerName, addressId, LoginForm.username);
                    bool created = CustomerDao.SaveNewCustomer(customer);

                    // clear out options
                    CustomerNameTextBox.Text = null;
                    Address1TextBox.Text = null;
                    Address2TextBox.Text = null;
                    PhoneTextBox.Text = null;
                    ZipTextBox.Text = null;

                    if (created == true)
                    {
                        SetDefaults();
                        MessageBox.Show($"Success! You have successfully added {customerName}.");
                    }
                    else
                    {
                        MessageBox.Show($"Something seems to have gone wrong, {customerName} was not created.");
                    }
                }
                else
                {
                    MessageBox.Show("Please make sure both a name and address are selected.");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Couldn't create customer because: " + err);
            }
        }

        private void CustomerCreateTab_Click(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void AddressLabel_Click(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void ResetEditCustomerButton_Click(object sender, EventArgs e)
        {
            RenameTextBox.Text = null;
            SetDefaults();
        }

        private void DeleteCustomerButton_Click(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt32(EditCustomerComboBox.SelectedValue);

            DialogResult delete = MessageBox.Show($"Are you sure  you want to delete customer: {EditCustomerComboBox.SelectedText.ToString()}?", "Delete Customer", MessageBoxButtons.YesNo);

            if (delete.ToString().ToLower() == "yes")
            {
                var customer = CustomerDao.GetCustomerById(customerId);
                CustomerDao.DeleteCustomer(customer);

                SetDefaults();
            }
        }

        private void ApplyChangesButton_Click(object sender, EventArgs e)
        {
            string updatedName = RenameTextBox.Text;
            int customerId = Convert.ToInt32(EditCustomerComboBox.SelectedValue);
            int updatedAddress = AddressDao.GetAddressIdByString(EditAddressComboBox.SelectedValue.ToString());

            if (updatedName.Trim() != "" && updatedAddress >= 0 && customerId >= 0)
            {
                Data.Models.Customer cust = CustomerDao.GetCustomerById(customerId);
                var modifiedCustomer = CustomerDao.ApplyCustomerChanges(cust, updatedName, updatedAddress, 1, DateTime.Now, LoginForm.username);
                var updated = CustomerDao.UpdateCustomer(modifiedCustomer);

                if (updated == true)
                {
                    MessageBox.Show($"Success! {updatedName} was updated.");
                    SetDefaults();
                }
                else
                {
                    MessageBox.Show($"Sorry, {updatedName} was not updated properly.");
                }
            }
        }

        private void AddApptButton_Click(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt32(CustomerAptComboBox.SelectedValue);
            int userId = UserDao.GetUserIdByName(LoginForm.username);
            bool saved = false;
            DateTime start = DateTimeOffset.Parse(StartDateDatePicker.Text).UtcDateTime;
            DateTime end = DateTimeOffset.Parse(EndDateDatePicker.Text).UtcDateTime; ;

            if (start.Hour >= 9 && start.Hour <= 17 && end.Hour >= 9 && end.Hour <= 17)
            {
                var newAppt = AppointmentDao.CreateNewAppointment(customerId,
                                                                  userId,
                                                                  TitleApptTextBox.Text,
                                                                  DescriptionText.Text,
                                                                  LocationText.Text,
                                                                  ContactText.Text,
                                                                  TypeText.Text,
                                                                  UrlText.Text,
                                                                  start,
                                                                  end,
                                                                  DateTime.UtcNow,
                                                                  LoginForm.username,
                                                                  DateTime.UtcNow,
                                                                  LoginForm.username);
                saved = AppointmentDao.SaveAppointment(newAppt);
            }
            else
            {
                MessageBox.Show("Please select a time within business hours.");
                return;
            }

            if (saved == true)
            {
                MessageBox.Show("Appointment was saved successfully!");
                SetDefaults();
            }
            else if (saved == false)
            {
                MessageBox.Show("Appointment wasn't able to save.");
            }
        }

        private void DeleteApptButton_Click(object sender, EventArgs e)
        {
            int appointmentId = Convert.ToInt32(AppointmentsComboBox.SelectedValue);
            var appt = AppointmentDao.GetAppointmentById(appointmentId);
            bool deleted = AppointmentDao.DeleteAppointment(appt);

            if (deleted == true)
            {
                MessageBox.Show("Deleted appointment successfully.");
                SetDefaults();
            }
            else
            {
                MessageBox.Show("Appointment wasn't able to be deleted.");
            }
        }

        private void UpdateApptButton_Click(object sender, EventArgs e)
        {
            int apptId = Convert.ToInt32(AppointmentsComboBox.SelectedValue);
            int customerId = Convert.ToInt32(CustomerUpdateComboBox.SelectedValue);
            int userId = UserDao.GetUserIdByName(LoginForm.username);
            DateTime start = DateTimeOffset.Parse(StartDateUpdateDatePicker.Text).UtcDateTime;
            DateTime end = DateTimeOffset.Parse(EndDateUpdateDatePicker.Text).UtcDateTime; ;

            var newAppt = AppointmentDao.CreateNewAppointment(customerId,
                                                              userId,
                                                              TitleUpdateText.Text,
                                                              DescriptionUpdateText.Text,
                                                              LocationUpdateText.Text,
                                                              ContactUpdateText.Text,
                                                              TypeUpdateText.Text,
                                                              UrlUpdateText.Text,
                                                              start,
                                                              end,
                                                              DateTime.UtcNow,
                                                              LoginForm.username,
                                                              DateTime.UtcNow,
                                                              LoginForm.username);
            newAppt.AppointmentId = apptId;
            bool update = AppointmentDao.UpdateAppointment(newAppt);

            if (update == true)
            {
                MessageBox.Show("Appointment was updated successfully!");
                SetDefaults();
            }
            else if (update == false)
            {
                MessageBox.Show("Appointment wasn't able to update.");
            }
        }

        private void ResetApptButton_Click(object sender, EventArgs e)
        {
            TitleApptTextBox.Text = null;
            DescriptionText.Text = null;
            LocationText.Text = null;
            TypeText.Text = null;
            ContactText.Text = null;
            UrlText.Text = null;
            StartDateDatePicker.Value = DateTime.Now;
            EndDateDatePicker.Value = DateTime.Now;
            CustomerAptComboBox.SelectedValue = 0;
        }

        private void ResetApptUpdateButton_Click(object sender, EventArgs e)
        {
            TitleUpdateText.Text = null;
            DescriptionUpdateText.Text = null;
            LocationUpdateText.Text = null;
            TypeUpdateText.Text = null;
            ContactUpdateText.Text = null;
            UrlUpdateText.Text = null;
            StartDateUpdateDatePicker.Value = DateTime.Now;
            EndDateUpdateDatePicker.Value = DateTime.Now;
            CustomerUpdateComboBox.SelectedValue = 0;
        }
    }
}
