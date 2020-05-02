using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Scheduler.Data.Models;
using System;
using Scheduler.Data;

namespace Scheduler.Test
{
    [TestClass]
    public class AppointmentDaoTest
    {
        [TestMethod]
        public void GetAppointmentsInCurrentWeekTest()
        {
            List<Appointment> appts = AppointmentDao.GetAppointmentsInCurrentWeek();

            if (appts.Count > 0)
            {
                Assert.IsTrue(appts.Count > 0);
            }
        }

        [TestMethod]
        public void GetAllAppointmentsTest()
        {
            List<Appointment> AllAppointments = AppointmentDao.GetAppointments();
        }

        [TestMethod]
        public void GetAppointmentById()
        {
            Appointment appt = AppointmentDao.GetAppointmentById(1);
            if (appt != null)
            {
                Assert.IsInstanceOfType(appt, typeof(Appointment));
            }
            else
            {
                Assert.IsNull(appt);
            }
        }

        [TestMethod]
        public void CreateUpdateSaveDeleteAppointmentTest()
        {
            int customerId = 1;
            int userId = 0;
            string title = "Test Appointment";
            string description = "Test Descriptiton";
            string location = "Test Location";
            string contact = "Test contact";
            string type = "Test Type";
            string url = "Test.url.com";
            DateTime start = DateTime.UtcNow;
            DateTime end = DateTime.UtcNow;
            end.AddDays(1);
            DateTime createdDate = DateTime.Now;
            string createdBy = "system";
            DateTime modifiedDate = DateTime.Now;
            string modifiedBy = "system";

            Appointment appt = AppointmentDao.CreateNewAppointment(customerId,
                                                                   userId,
                                                                   title,
                                                                   description,
                                                                   location,
                                                                   contact,
                                                                   type,
                                                                   url,
                                                                   start,
                                                                   end,
                                                                   createdDate,
                                                                   createdBy,
                                                                   modifiedDate,
                                                                   modifiedBy);
            Assert.IsInstanceOfType(appt, typeof(Appointment));
            Assert.IsTrue(appt.Title == title);
            Assert.IsTrue(appt.CreateDate == createdDate);

            bool saved = AppointmentDao.SaveAppointment(appt);
            Assert.IsTrue(saved == true);

            appt.Title = "Updated Test Title";
            bool update = AppointmentDao.UpdateAppointment(appt);
            Assert.IsTrue(update == true);

            bool delete = AppointmentDao.DeleteAppointment(appt);
            Assert.IsTrue(delete == true);
        }
    }
}
