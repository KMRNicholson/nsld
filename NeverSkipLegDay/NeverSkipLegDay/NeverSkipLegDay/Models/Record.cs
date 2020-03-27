using SQLite;

namespace NeverSkipLegDay.Models
{
    /*
     * Class which defines the behavior and properties of the Record model, and entity in the database.
     */
    public class Record
    {
        #region attributes
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        #endregion
    }
}
