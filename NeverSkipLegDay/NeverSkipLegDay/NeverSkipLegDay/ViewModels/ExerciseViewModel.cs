using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class ExerciseViewModel : BaseViewModel
    {
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

        public ExerciseViewModel() { }

        public ExerciseViewModel(Exercise exercise)
        {
            Id = exercise.Id;
            WorkoutId = exercise.WorkoutId;
            Name = exercise.Name;
        }
    }
}
