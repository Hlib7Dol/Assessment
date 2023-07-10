using UndoAssessment.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UndoAssessment.Views
{
    public partial class UserDataCollectPage : ContentPage
    {
        public UserDataCollectPage()
        {
            InitializeComponent();
            this.BindingContext = new UserDataCollectViewModel();
        }
    }
}