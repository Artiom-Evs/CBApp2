using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace CBApp2.Domain.Models
{
    [Table("Weeks")]
    public class Week
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public long CreateDate { get; set; }
        public List<Day> Days { get; set; } = new List<Day>();

        public int ElementId { get; set; }
        public Element Element { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}
