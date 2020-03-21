using System;

using NeverSkipLegDay.Models;

namespace NeverSkipLegDay.ViewModels
{
    public class WorkoutViewModel : BaseViewModel
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

        public WorkoutViewModel() { }

        public WorkoutViewModel(Workout workout)
        {
            if (workout == null)
                throw new ArgumentNullException(nameof(workout));

            Id = workout.Id;
            Name = workout.Name;
        }
    }
}
