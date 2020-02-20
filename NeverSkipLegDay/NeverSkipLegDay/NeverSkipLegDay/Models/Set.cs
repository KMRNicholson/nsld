using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NeverSkipLegDay.Models
{
    public class Set
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ExerciseID { get; set; }
        public int? Reps { get; set; }
        public decimal? Weight { get; set; }
        public DateTime Date { get; set; }
    }
}
