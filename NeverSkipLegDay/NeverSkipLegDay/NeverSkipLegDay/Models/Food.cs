using SQLite;

namespace NeverSkipLegDay.Models
{
    public class Food
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MealId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public int? Fat { get; set; }
        public int? Prot { get; set; }
        public int? Carb { get; set; }
        public int? Cal { get; set; }

    }
}
