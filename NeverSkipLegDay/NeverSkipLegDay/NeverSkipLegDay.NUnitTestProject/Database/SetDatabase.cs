using System.Collections.Generic;
using System.Linq;
using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.NUnitTestProject.Database
{
    public class SetDatabase : ISetDal
    {
        public List<Set> Sets { get; set; }
        public SetDatabase()
        {
            Sets = new List<Set>
            {
                new Set(){ Id = 1, ExerciseId = 2, Reps = 8, Weight = 110 },
                new Set(){ Id = 2, ExerciseId = 2, Reps = 5, Weight = 120 },
                new Set(){ Id = 3, ExerciseId = 3, Reps = 12, Weight = 90 },
                new Set(){ Id = 4, ExerciseId = 1, Reps = 3, Weight = 150 },
                new Set(){ Id = 5, ExerciseId = 1, Reps = 1, Weight = 160 },
                new Set(){ Id = 6, ExerciseId = 3, Reps = 15, Weight = 20 }
            };
        }

        public List<Set> GetSets()
        {
            return Sets;
        }

        public Set GetSet(int id)
        {
            return Sets.Where(s => s.Id == id).ToList().FirstOrDefault();
        }

        public List<Set> GetSetsByExerciseId(int exerciseId)
        {
            return Sets.Where(s => s.ExerciseId == exerciseId).ToList();
        }

        public int SaveSet(Set model)
        {
            if (model.Id != 0)
            {
                Sets.ForEach(s => { 
                    if (s.Id == model.Id)
                    {
                        s.Reps = model.Reps;
                        s.Weight = model.Weight;
                    }
                });
                return 1;
            }
            else
            {
                model.Id = Sets.Last().Id + 1;
                Sets.Add(model);
                return Sets.Find(s => s == model) != null ? 1 : 0;
            }
        }

        public int DeleteSet(Set model)
        {
            Sets.Remove(model);
            return Sets.Find(s => s == model) == null ? 1 : 0;
        }
    }
}
