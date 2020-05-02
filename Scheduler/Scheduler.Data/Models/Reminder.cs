using System;
using System.Collections.Generic;

namespace Scheduler.Data.Models
{
    public partial class Reminder
    {
        public int ReminderId { get; set; }
        public DateTime ReminderDate { get; set; }
        public int SnoozeIncrement { get; set; }
        public int SnoozeIncrementTypeId { get; set; }
        public int AppointmentId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Remindercol { get; set; }
    }
}
