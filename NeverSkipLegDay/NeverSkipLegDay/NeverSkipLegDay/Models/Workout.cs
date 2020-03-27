using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NeverSkipLegDay.Models
{
    /*
     * Class which defines the behavior and properties of the Workout model and entity in the database.
     */
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
