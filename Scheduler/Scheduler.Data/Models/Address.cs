using System;
using System.Collections.Generic;

namespace Scheduler.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            Customer = new HashSet<Customer>();
        }

        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
    }
}
