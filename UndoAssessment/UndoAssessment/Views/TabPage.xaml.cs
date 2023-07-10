using UndoAssessment.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UndoAssessment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : ContentPage
    {
        public TabPage()
        {
            InitializeComponent();
            this.BindingContext = new TabViewModel();
        }
    }
}