using SQLite;

namespace NeverSkipLegDay.Models
{
    public class Record
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int? Reps { get; set; }
        public decimal? Weight { get; set; }
    }
}
