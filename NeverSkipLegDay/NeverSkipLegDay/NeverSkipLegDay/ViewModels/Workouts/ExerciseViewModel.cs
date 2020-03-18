using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay.ViewModels
{
    public class ExerciseViewModel : BaseViewModel
    {
        private SetDal _setDal;
        public int Id { get; set; }
        private int _workoutId;
        public int WorkoutId
        {
            get { return _workoutId; }
            set
            {
                SetValue(ref _workoutId, value);
                OnPropertyChanged(nameof(WorkoutId));
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
                OnPropertyChanged(nameof(Name));
            }
        }

        private int? _reps;
        public int? Reps
        {
            get { return _reps; }
            set
            {
                SetValue(ref _reps, value);
                OnPropertyChanged(nameof(Reps));
            }
        }

        private int _sets;
        public int Sets
        {
            get { return _sets; }
            set
            {
                SetValue(ref _sets, value);
                OnPropertyChanged(nameof(Sets));
            }
        }

        public ExerciseViewModel() { }

        public ExerciseViewModel(Exercise exercise)
        {
            Id = exercise.Id;
            WorkoutId = exercise.WorkoutId;
            Name = exercise.Name;

            _setDal = new SetDal(new SQLiteDB());

            Reps = exercise.GetRepsTotal(_setDal);
            Sets = exercise.GetSetsTotal(_setDal);
        }
    }
}
