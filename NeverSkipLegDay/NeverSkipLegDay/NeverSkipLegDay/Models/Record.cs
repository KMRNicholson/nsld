using SQLite;

namespace NeverSkipLegDay.Models
{
    public class Record
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int? Reps { get; set; }
        public int? Weight { get; set; }
    }
}
