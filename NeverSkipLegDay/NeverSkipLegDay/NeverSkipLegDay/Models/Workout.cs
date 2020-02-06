using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NeverSkipLegDay.Models
{
    class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
