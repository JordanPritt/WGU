using System;
using System.Collections.Generic;

namespace Scheduler.Data.Models
{
    public partial class User
    {
        public User()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public sbyte Active { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
