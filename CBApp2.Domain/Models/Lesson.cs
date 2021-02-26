using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace CBApp2.Domain.Models
{
    [Table("Lessons")]
    public class Lesson
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public byte Number { get; set; }
        public string Subject { get; set; }
        public string Room { get; set; }

        public int DayId { get; set; }
        public Day Day { get; set; }

        public override string ToString()
        {
            return Subject;
        }
    }
}
