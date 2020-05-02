using System;
using System.Collections.Generic;

namespace Scheduler.Data.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int AddressId { get; set; }
        public sbyte Active { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
