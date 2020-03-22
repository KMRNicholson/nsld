using NeverSkipLegDay.Models;
using NeverSkipLegDay.Models.DAL;
using System;

namespace NeverSkipLegDay.ViewModels
{
    /*
     * Class which defines the ViewModel for the Exercise model. This is used for displaying exercises,
     * as well as mapping values from the view to the model/database, through the binded ViewModel.
     */
    public class ExerciseViewModel : BaseViewModel
    {
        #region private properties
        private readonly SetDal _setDal;
        private string _name;
        #endregion

        #region public properties
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public int Reps { get; private set; }
        public int Sets { get; private set; }
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
                OnPropertyChanged(nameof(Name));
            }
        }
        #endregion

        #region constructors
        public ExerciseViewModel() { }

        public ExerciseViewModel(Exercise exercise)
        {
            if (exercise == null)
                throw new ArgumentNullException(nameof(exercise));

            _setDal = new SetDal(new SQLiteDB());

            Id = exercise.Id;
            WorkoutId = exercise.WorkoutId;
            Name = exercise.Name;
            Reps = exercise.GetRepsTotal(_setDal);
            Sets = exercise.GetSetsTotal(_setDal);
        }
        #endregion
    }
}
