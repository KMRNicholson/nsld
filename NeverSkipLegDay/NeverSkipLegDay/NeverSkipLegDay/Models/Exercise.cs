using System;
using System.Linq;

using SQLite;

using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.Models
{
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
        public int GetRepsTotal(ISetDal setDal)
        {
            if (setDal == null)
                throw new ArgumentNullException(nameof(setDal));

            return setDal.GetSetsByExerciseId(this.Id).Select(x=>x.Reps).Sum();
        }
        public int GetSetsTotal(ISetDal setDal)
        {
            if (setDal == null)
                throw new ArgumentNullException(nameof(setDal));

            return setDal.GetSetsByExerciseId(this.Id).Count;
        }
        #endregion
    }
}
