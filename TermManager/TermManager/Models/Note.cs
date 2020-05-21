using System;
using SQLite;

namespace TermManager.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int NoteId { get; set; }
        public int CourseId { get; set; }
        public string NoteMessage { get; set; }
    }
}
