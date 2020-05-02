using System;
using System.Linq;
using Scheduler.Data.Models;
using System.Collections.Generic;

namespace Scheduler.Data
{
    /// <summary>
    /// DAO for appointments data.
    /// </summary>
    public class AppointmentDao
    {
        /// <summary>
        /// Get's a single appointment by ID.
        /// </summary>
        /// <returns>An Appointment object or null.</returns>
        public static Appointment GetAppointmentById(int id)
        {
            try
            {
                using (Context db = new Context())
                {
                    //using a lambda for linq query
                    Appointment appt = db.Appointment.Single(a => a.AppointmentId == id);
                    return appt;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get's a single appointment by ID.
        /// </summary>
        /// <returns>An Appointment object or null.</returns>
        public static List<Appointment> GetAppointmentsInCurrentWeek()
        {
            try
            {
                List<Appointment> appt;

                using (Context db = new Context())
                {
                    var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                    var week = cal.GetWeekOfYear(DateTime.Now, 0, 0);

                    appt = db.Appointment
                        .Where(a => cal.GetWeekOfYear(a.Start, 0, 0) == week)
                        .ToList();
                    return appt;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get's a single appointment by ID.
        /// </summary>
        /// <returns>An Appointment object or null.</returns>
        public static List<Appointment> GetAppointmentsInCurrentMonth()
        {
            try
            {
                List<Appointment> appt;

                using (Context db = new Context())
                {
                    var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                    var month = cal.GetMonth(DateTime.Now);

                    appt = db.Appointment
                        .Where(a => cal.GetMonth(a.Start) == month)
                        .ToList();
                    return appt;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a list of all the appointments stored.
        /// </summary>
        /// <returns>Either a list of all the appoinments or null if there aren't any.</returns>
        public static List<Appointment> GetAppointments()
        {
            try
            {
                List<Appointment> appts = new List<Appointment>();

                using (Context db = new Context())
                {
                    appts = db.Appointment.Select(a => a).ToList();
                }

                return appts;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the appointments stored.
        /// </summary>
        /// <returns>A dictionary of the appointments.</returns>
        public static Dictionary<string, int> GetAllAppointments()
        {
            Dictionary<string, int> appts = new Dictionary<string, int>();

            try
            {
                using (Context db = new Context())
                {
                    // using lambda for linq query
                    appts = db.Appointment.Select(a => new { a.Title, a.AppointmentId }).ToDictionary(a => a.Title, a => a.AppointmentId);
                }

                return appts;
            }
            catch (Exception err)
            {
                throw new Exception($"Couldn't retrieve all cities because: {err}");
            }
        }

        /// <summary>
        /// Creates a new appointment object.
        /// </summary>
        /// <returns>An Appointment object.</returns>
        public static Appointment CreateNewAppointment(int customerId,
                                                       int userId,
                                                       string title,
                                                       string description,
                                                       string location,
                                                       string contact,
                                                       string type,
                                                       string url,
                                                       DateTime start,
                                                       DateTime end,
                                                       DateTime createdDate,
                                                       string createdBy,
                                                       DateTime modifiedDate,
                                                       string modifiedBy)
        {
            try
            {

                Appointment appt = new Appointment()
                {
                    CustomerId = customerId,
                    UserId = userId,
                    Title = title,
                    Description = description,
                    Location = location,
                    Contact = contact,
                    Type = type,
                    Url = url,
                    Start = start,
                    End = end,
                    CreateDate = createdDate,
                    CreatedBy = createdBy,
                    LastUpdate = modifiedDate,
                    LastUpdateBy = modifiedBy
                };

                return appt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't create appointment becuase: {ex}");
            }
        }

        /// <summary>
        /// Saves an appointment to the DB.
        /// </summary>
        /// <param name="appt">An Appointment object to be saved.</param>
        /// <returns>Boolean true if saved or boolean false if not.</returns>
        public static bool SaveAppointment(Appointment appt)
        {
            try
            {
                using (Context db = new Context())
                {
                    db.Appointment.Add(appt);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Updates an appointment to the DB.
        /// </summary>
        /// <param name="appt">An Appointment object to be updated.</param>
        /// <returns>Boolean true if updated or boolean false if not.</returns>
        public static bool UpdateAppointment(Appointment appt)
        {
            try
            {
                using (Context db = new Context())
                {
                    db.Appointment.Update(appt);
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes an appointment to the DB.
        /// </summary>
        /// <param name="appt">An Appointment object to be deleted.</param>
        /// <returns>Boolean true if deleted or boolean false if not.</returns>
        public static bool DeleteAppointment(Appointment appt)
        {
            try
            {
                using (Context db = new Context())
                {
                    db.Appointment.Remove(appt);
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
