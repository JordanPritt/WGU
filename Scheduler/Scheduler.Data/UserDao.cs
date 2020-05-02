using Scheduler.Data.Models;
using System.Linq;
using System;

namespace Scheduler.Data
{
    public static class UserDao
    {
        /// <summary>
        /// Creates a User object
        /// </summary>
        /// <param name="name">string representation of name.</param>
        /// <param name="password">string representation of password.</param>
        /// <param name="createdBy">string representation of who created the user.</param>
        /// <returns>A true value if user gets created, else it's a false value.</returns>
        public static bool CreateUser(string name, string password, string createdBy)
        {
            using (Context context = new Context())
            {
                try
                {
                    User user = new User
                    {
                        UserName = name,
                        Password = password,
                        Active = 1,
                        CreateDate = DateTime.Now,
                        CreatedBy = createdBy,
                        LastUpdate = DateTime.Now,
                        LastUpdateBy = createdBy
                    };

                    context.User.Add(user);
                    context.SaveChanges();

                    return true;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Couldn't add user." + err);

                    return false;
                }
            }
        }

        /// <summary>
        /// Login a user based on given credentials. 
        /// </summary>
        /// <param name="username">String representation of given username.</param>
        /// <param name="password">String representation of given password.</param>
        /// <returns>A string, which is empty if false.</returns>
        public static string LoginUser(string username, string password)
        {
            try
            {
                string id;

                using (Context db = new Context())
                {
                    id = db.User.Single(u => u.UserName == username && u.Password == password).UserId.ToString();
                }

                return id;
            }
            catch (Exception err)
            {
                Console.WriteLine("coudn't login user: " + err);

                return "";
            }
        }

        /// <summary>
        /// Gets an ID of a given username.
        /// </summary>
        /// <param name="name">A string representation of a given username.</param>
        /// <returns>An ID as an int or -1 if not found.</returns>
        public static int GetUserIdByName(string name)
        {
            try
            {
                int id = 0;

                using (Context db = new Context())
                {
                    id = db.User.Single(u => u.UserName == name).UserId;
                }

                return id;
            }
            catch
            {
                return -1;
            }
        }
    }
}
