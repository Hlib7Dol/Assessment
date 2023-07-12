using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using UndoAssessment.Models;
using UndoAssessment.Services;
using UndoAssessment.Views;
using Xamarin.Forms;

namespace UndoAssessment.ViewModels
{
    public class TabViewModel : BasePageResultViewModel
    {
        private readonly IRequestService _requestService;

        private const string SuccessUrl = "/success";
        private const string FailUrl = "/fail";

        public ICommand FailCommand { get; }
        public ICommand SuccessCommand { get; }
        public ICommand OpenUserDataCollectorPageCommand { get; }

        private bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get => this._isButtonVisible;
            set => this.SetProperty(ref this._isButtonVisible, value);
        }

        private string _userAge;
        public string UserAge
        {
            get => this._userAge;
            set => this.SetProperty(ref this._userAge, value);
        }

        private string _userName;
        public string UserName
        {
            get => this._userName;
            set => this.SetProperty(ref this._userName, value);
        }

        public TabViewModel() 
        {
            this._requestService = DependencyService.Resolve<IRequestService>();
            this.FailCommand = new AsyncCommand(ExecuteFailRequestAsync);
            this.SuccessCommand = new AsyncCommand(ExecuteSuccessRequestAsync);
            this.OpenUserDataCollectorPageCommand = new AsyncCommand(OpenUserDataCollectorPageAsync);
            this.IsButtonVisible = true;
        }

        public async Task ExecuteFailRequestAsync()
        {
            var response = await this._requestService.Get<ResponseItem>(FailUrl);

            var message = $"{response.Message} at {response.Date}";
            var alertData = this.PrepareShowData(response.ErrorCode.ToString(), message);

            await this.ShowAlertAsync(alertData.title, alertData.message);
        }

        public async Task ExecuteSuccessRequestAsync()
        {
            var response = await this._requestService.Get<ResponseItem>(SuccessUrl);

            var message = $"{response.Message} at {response.Date}";
            var alertData = this.PrepareShowData(response.ErrorCode.ToString(), message);

            await this.ShowAlertAsync(alertData.title, alertData.message);
        }

        public async Task OpenUserDataCollectorPageAsync()
        {
            var userData = await base.ShowWithResultAsync<UserData>(nameof(UserDataCollectPage));
            if (userData == null)
            {
                return;
            }

            IsButtonVisible = false;
            this.UserName = userData.UserName;
            this.UserAge = userData.UserAge.ToString();
        }

        private (string title, string message) PrepareShowData(string titleData, string messageData)
        {
            return ($"Error code - {titleData}", messageData);
        }

        private async Task ShowAlertAsync(string alertTitle, string alertMessage)
        {
            await Shell.Current.DisplayAlert(alertTitle, alertMessage, "Cancel");
        }
    }
}
