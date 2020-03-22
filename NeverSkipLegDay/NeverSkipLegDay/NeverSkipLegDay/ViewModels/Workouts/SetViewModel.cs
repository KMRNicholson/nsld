using System;

using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * Class which defines the ViewModel for the Set model. This is used for displaying sets,
     * as well as mapping values from the view to the model/database, through the binded ViewModel.
     */
    public class SetViewModel : BaseViewModel
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
                OnPropertyChanged(nameof(_reps));
            }
        }
        public decimal Weight
        {
            get { return _weight; }
            set
            {
                SetValue(ref _weight, value);
                OnPropertyChanged(nameof(_weight));
            }
        }
        #endregion

        #region constructors
        public SetViewModel() { }

        public SetViewModel(Set set)
        {
            if (set == null)
                throw new ArgumentNullException(nameof(set));

            Id = set.Id;
            ExerciseId = set.ExerciseId;
            Reps = set.Reps;
            Weight = set.Weight;
        }
        #endregion
    }
}
