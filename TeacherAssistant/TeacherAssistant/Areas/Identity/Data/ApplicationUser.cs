using System;
using Microsoft.AspNetCore.Identity;

namespace TeacherAssistant
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateJoined { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
