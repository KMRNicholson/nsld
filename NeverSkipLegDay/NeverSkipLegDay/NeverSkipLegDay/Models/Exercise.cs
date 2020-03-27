using System;
using System.Linq;

using SQLite;

using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Models
{
    /*
     * Class which defines the behavior and properties of the Exercise model, and entity in the database.
     */
    public class Exercise
    {
        #region attributes
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        #endregion

        #region public methods
        //Method which gets the total reps for the exercise using the exercise id to get all sets for the exercise and summing the reps.
        public int GetRepsTotal(ISetDal setDal)
        {
            if (setDal == null)
                throw new ArgumentNullException(nameof(setDal));

            return setDal.GetSetsByExerciseId(this.Id).Select(x=>x.Reps).Sum();
        }

        //Method which gets the total sets for the exercise using the exercise id to get all sets for the exercise and summing the sets.
        public int GetSetsTotal(ISetDal setDal)
        {
            if (setDal == null)
                throw new ArgumentNullException(nameof(setDal));

            return setDal.GetSetsByExerciseId(this.Id).Count;
        }
        #endregion
    }
}
