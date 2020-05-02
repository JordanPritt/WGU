using System;
using System.Collections.Generic;

namespace Scheduler.Data.Models
{
    public partial class City
    {
        public City()
        {
            Address = new HashSet<Address>();
        }

        public int CityId { get; set; }
        public string City1 { get; set; }
        public int CountryId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
