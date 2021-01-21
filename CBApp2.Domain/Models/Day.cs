using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace CBApp2.Domain.Models
{
    [Table("Days")]
    public class Day
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public byte Number { get; set; }
        public string Name { get; set; }
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public int WeekId { get; set; }
        public Week Week { get; set; }

        public override string ToString()
        {
            return DateTime.Parse(Name).ToString("dddd")[0].ToString().ToUpper() 
                + DateTime.Parse(Name).ToString("dddd")
                .Substring(1, DateTime.Parse(Name).ToString("dddd")
                .Length - 1) + ", " + DateTime.Parse(Name).ToString("dd MMMM");
        }
    }
}
