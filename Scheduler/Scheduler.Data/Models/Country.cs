using System;
using System.Collections.Generic;

namespace Scheduler.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
        }

        public int CountryId { get; set; }
        public string Country1 { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public virtual ICollection<City> City { get; set; }
    }
}
