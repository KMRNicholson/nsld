using SQLite;

namespace NeverSkipLegDay.Models
{
    /*
     * Class which defines the behavior and properties of the Food model, and entity in the database.
     */
    public class Food
    {
        #region attributes
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MealId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Fat { get; set; }
        public decimal Prot { get; set; }
        public decimal Carb { get; set; }
        public decimal Cal { get; set; }
        #endregion
    }
}
