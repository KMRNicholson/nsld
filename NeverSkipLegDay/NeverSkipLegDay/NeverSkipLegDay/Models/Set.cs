using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NeverSkipLegDay.Models
{
    public class Set
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int? Reps { get; set; }
        public decimal? Weight { get; set; }
    }
}
