using Microsoft.EntityFrameworkCore;
using TeacherAssistant.Models;

namespace TeacherAssistant.Data
{
    public class TeacherAssistantContext : DbContext
    {
        public TeacherAssistantContext (DbContextOptions<TeacherAssistantContext> options) : base(options)
        { }

        public DbSet<Student> Students { get; set; }
    }
}
