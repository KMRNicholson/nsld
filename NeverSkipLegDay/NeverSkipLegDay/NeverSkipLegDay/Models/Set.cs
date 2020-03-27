using SQLite;

namespace NeverSkipLegDay.Models
{
    /*
     * Class which defines the behavior and properties of the Set model, and entity in the database.
     */
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "<Pending>")]
    public class Set
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
    }
}
