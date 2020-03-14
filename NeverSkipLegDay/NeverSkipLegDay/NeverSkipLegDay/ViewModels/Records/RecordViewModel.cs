using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class RecordViewModel : BaseViewModel
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
        private int? _weight;
        public int? Weight
        {
            get { return _weight; }
            set
            {
                SetValue(ref _weight, value);
                OnPropertyChanged(nameof(Weight));
            }
        }

        public RecordViewModel() { }

        public RecordViewModel(Record record)
        {
            Id = record.Id;
            ExerciseId = record.ExerciseId;
            Reps = record.Reps;
            Weight = record.Weight;
        }
    }
}
