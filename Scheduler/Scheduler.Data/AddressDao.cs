using System;
using System.Linq;
using Scheduler.Data.Models;
using System.Collections.Generic;

namespace Scheduler.Data
{
    public static class AddressDao
    {
        /// <summary>
        /// Get's all the addresses saved in the DB.
        /// </summary>
        /// <returns>A list of all addresses as objects.</returns>
        public static List<string> GetAddresses()
        {
            List<string> addresses = new List<string>();

            try
            {
                using (Context db = new Context())
                {
                    addresses = db.Address.Select(a => a.Address1).ToList();
                }
            }
            catch
            {
                return null;
            }

            return addresses;
        }

        /// <summary>
        /// Gets an address based off of text value of address1
        /// </summary>
        /// <param name="address">The address tet to search on.</param>
        /// <returns>An address object or -1 if not found.</returns>
        public static int GetAddressIdByString(string address)
        {
            try
            {
                int id;
                using (Context db = new Context())
                {
                    id = db.Address.Where(a => a.Address1 == address).Select(a => a.AddressId).Single();
                    return id;
                }
            }
            catch
            {
                return -1;
            }
        }

        public static Dictionary<string, int> GetAllCities()
        {
            Dictionary<string, int> cities = new Dictionary<string, int>();

            try
            {
                using (Context db = new Context())
                {
                    cities = db.City.Select(c => new { c.City1, c.CityId }).ToDictionary(c => c.City1, c => c.CityId);
                }

                return cities;
            }
            catch (Exception err)
            {
                throw new Exception($"Couldn't retrieve all cities because: {err}");
            }
        }

        public static Address CreateAddress(string address1,
                                            string address2,
                                            int cityId,
                                            string zipCode,
                                            string phone,
                                            DateTime createdDate,
                                            DateTime updatedDate,
                                            string createdBy,
                                            string updatedBy)
        {
            Address newAddress = new Address
            {
                Address1 = address1,
                Address2 = address2,
                CityId = cityId,
                PostalCode = zipCode,
                Phone = phone,
                CreateDate = createdDate,
                CreatedBy = createdBy,
                LastUpdate = updatedDate,
                LastUpdateBy = updatedBy
            };

            return newAddress;
        }

        public static bool SaveNewAddress(Address address)
        {
            try
            {
                using (Context db = new Context())
                {
                    db.Address.Add(address);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Address GetAddressByValues(string txtAddress, int cityId, string zip)
        {
            try
            {
                using (Context db = new Context())
                {
                    Address address = db.Address.Where(c => c.Address1 == txtAddress && c.CityId == cityId && c.PostalCode == zip).Single();
                    return address;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
