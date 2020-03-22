using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class SetViewModel : BaseViewModel
    {
        public int Id { get; set; }
        private int _exerciseId;
        public int ExerciseId
        {
            get { return _exerciseId; }
            set
            {
                SetValue(ref _exerciseId, value);
                OnPropertyChanged(nameof(ExerciseId));
            }
        }

        private int _reps;
        public int Reps
        {
            get { return _reps; }
            set
            {
                SetValue(ref _reps, value);
                OnPropertyChanged(nameof(_reps));
            }
        }

        private decimal _weight;
        public decimal Weight
        {
            get { return _weight; }
            set
            {
                SetValue(ref _weight, value);
                OnPropertyChanged(nameof(_weight));
            }
        }

        public SetViewModel() { }

        public SetViewModel(Set set)
        {
            Id = set.Id;
            ExerciseId = set.ExerciseId;
            Reps = set.Reps;
            Weight = set.Weight;
        }
    }
}
