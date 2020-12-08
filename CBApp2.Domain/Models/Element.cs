using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace CBApp2.Domain.Models
{
    [Table("Elements")]
    public class Element
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public Week Week { get; set; } //= new List<Week>();

        public override string ToString()
        {
            return Name;
        }
    }
}
