using System;

using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * Class which defines the ViewModel for the Record model. This is used for displaying records,
     * as well as mapping values from the view to the model/database, through the binded ViewModel.
     */
    public class RecordViewModel : BaseViewModel
    {
        #region private properties
        private int _reps;
        private decimal _weight;
        #endregion

        #region public properties
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int Reps
        {
            get { return _reps; }
            set
            {
                SetValue(ref _reps, value);
                OnPropertyChanged(nameof(Reps));
            }
        }
        public decimal Weight
        {
            get { return _weight; }
            set
            {
                SetValue(ref _weight, value);
                OnPropertyChanged(nameof(Weight));
            }
        }
        #endregion

        #region constructors
        public RecordViewModel() { }

        public RecordViewModel(Record record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            Id = record.Id;
            ExerciseId = record.ExerciseId;
            Reps = record.Reps;
            Weight = record.Weight;
        }
        #endregion
    }
}
