using System.Globalization;
using System.Windows.Forms;
using System.Threading;
using Scheduler.Data;
using System;

namespace Scheduler
{
    public partial class LoginForm : Form
    {
        public CultureInfo local;
        public static string username;
        private string password;

        public LoginForm()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            cultureInfo.ClearCachedData();
            local = new CultureInfo(cultureInfo.Name);

            InitializeComponent();
            SetLoginScreenLanguage();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                username = UserNameText.Text;
                password = PasswordText.Text;

                //int num;
                bool login = int.TryParse(UserDao.LoginUser(username, password), out int num);

                if (login == true)
                {
                    Dashboard dash = new Dashboard();
                    Logic.Logger.LogUserActivity(username);
                    dash.FormClosed += new FormClosedEventHandler(Dashboard_FormClosed);
                    dash.Show();
                    Hide();
                }
                else
                {
                    ShowErrorMessage();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void ShowErrorMessage(string ex = "")
        {
            if (ex != "")
            {
                MessageBox.Show($"Sorry an unexpected error occured: {ex}");
            }
            if (local.TwoLetterISOLanguageName.ToLower() == "it")
            {
                MessageBox.Show("Spiacenti, la password o il nome utente non sono corretti. Si prega di riprovare.");
            }
            else if (local.TwoLetterISOLanguageName.ToLower() == "us")
            {
                MessageBox.Show("Sorry, either the password or username was incorrect. Please try again.");
            }
            else
            {
                MessageBox.Show("Sorry, either the password or username was incorrect. Please try again.");
            }
        }

        private void SetLoginScreenLanguage()
        {
            if (local.TwoLetterISOLanguageName.ToLower() == "it")
            {
                UserNameLbl.Text = "Nome utente";
                PasswordLbl.Text = "Parola D'ordine ";
                LoginButton.Text = "Accesso";
                this.Text = "Accedi";
            }
            else
            {
                UserNameLbl.Text = "User Name";
                PasswordLbl.Text = "Password";
                LoginButton.Text = "Login";
                this.Text = "Login";
            }
        }
    }
}
