using System.Threading.Tasks;
using CommonHelpers.Collections;
using CommonHelpers.Common;
using Workouts.Portable.Common;
using Workouts.Portable.Models;
using Xamarin.Forms;

namespace Workouts.Portable.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private Exercise selectedExercise;

        public ViewModel()
        {
            Exercises = new ObservableRangeCollection<Exercise>();
            GoToViewCommand = new Command<ViewType>(GoToView);
        }

        public INavigationHandler NavigationHandler { get; set; }

        public ObservableRangeCollection<Exercise> Exercises { get; set; }

        public Exercise SelectedExercise
        {
            get => selectedExercise;
            set
            {
                if (SetProperty(ref selectedExercise, value) && selectedExercise != null)
                {
                    GoToView(ViewType.Details);
                }
            }
        }
        
        public Command<ViewType> GoToViewCommand { get; set; }

        private void GoToView(ViewType obj)
        {
            NavigationHandler.LoadView(obj);
        }

        public async Task LoadExercisesAsync()
        {
            IsBusy = true;

            var result = await App.ApiService.GetAllExercisesAsync();

            Exercises.AddRange(result);

            IsBusy = false;
        }
    }
}
