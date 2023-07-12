using UndoAssessment.ViewModels;
using Xamarin.Forms;

namespace UndoAssessment.Views
{
    public partial class UserDataCollectPage : ContentPage
    {
        public UserDataCollectPage()
        {
            InitializeComponent();
            this.BindingContext = new UserDataCollectViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            if (!(this.BindingContext is UserDataCollectViewModel model))
            {
                return false;
            }

            if(model.BackButtonCommand.CanExecute(this))
            {
                model.BackButtonCommand.Execute(this);
            }

            return base.OnBackButtonPressed();
        }
    }
}