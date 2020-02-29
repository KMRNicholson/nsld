using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NeverSkipLegDay.Models
{
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
