using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeverSkipLegDay.Models.DAL;
using SQLite;

namespace NeverSkipLegDay.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int? GetRepsTotal(ISetDal setDal)
        {
            List<Set> sets = setDal.GetSetsByExerciseId(this.Id);

            return sets.Select(x=>x.Reps).Sum();
        }

        public int GetSetsTotal(ISetDal setDal)
        {
            return setDal.GetSetsByExerciseId(this.Id).Count;
        }
    }
}
