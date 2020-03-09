using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class RecordViewModel : BaseViewModel
    {
        public int Id { get; set; }
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
            Name = record.Name;
            Reps = record.Reps;
            Weight = record.Weight;
        }
    }
}
