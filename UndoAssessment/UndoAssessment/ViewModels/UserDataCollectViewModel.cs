using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using UndoAssessment.Models;
using Xamarin.Forms;

namespace UndoAssessment.ViewModels
{
    public class UserDataCollectViewModel : BasePageResultViewModel
    {
        private const string AgeFieldEmptyValidation = "User age field cannot be empty";
        private const string AgeFieldMustBeIntegersValidation = "Age must consist only of integers";
        private const string NameFieldEmptyValidation = "User name field cannot be empty";

        public ICommand SubmitCommand { get; }
        public ICommand BackButtonCommand { get; }

        private string _userName;
        public string UserName
        {
            get => this._userName;
            set => this.SetProperty(ref this._userName, value);
        }

        public string _userAge;
        public string UserAge
        {
            get => this._userAge;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    this.UserAgeValidation = AgeFieldEmptyValidation;
                    this.IsUserAgeValidationVisible = true;
                }
                else if (!int.TryParse(value, out _))
                {
                    this.UserAgeValidation = AgeFieldMustBeIntegersValidation;
                    this.IsUserAgeValidationVisible = true;
                }
                else
                {
                    this.IsUserAgeValidationVisible = false;
                    this.SetProperty(ref this._userAge, value);
                }
            }
        }

        private string _userNameValidation;
        public string UserNameValidation
        {
            get => this._userNameValidation;
            set => this.SetProperty(ref this._userNameValidation, value);
        }

        private string _userAgeValidation;
        public string UserAgeValidation
        {
            get => this._userAgeValidation;
            set => this.SetProperty(ref this._userAgeValidation, value);
        }

        private bool _isUserNameValidationVisible;
        public bool IsUserNameValidationVisible
        {
            get => this._isUserNameValidationVisible;
            set => this.SetProperty(ref this._isUserNameValidationVisible, value);
        }

        private bool _isUserAgeValidationVisible;
        public bool IsUserAgeValidationVisible
        {
            get => this._isUserAgeValidationVisible;
            set => this.SetProperty(ref this._isUserAgeValidationVisible, value);
        }

        public UserDataCollectViewModel()
        {
            this.SubmitCommand = new AsyncCommand(SubmitAsync);
            this.BackButtonCommand = new AsyncCommand(BackAsync);
        }

        public async Task SubmitAsync()
        {
            if(string.IsNullOrWhiteSpace(this.UserName))
            {
                this.UserNameValidation = NameFieldEmptyValidation;
                this.IsUserNameValidationVisible = true;                

                return;
            }
            else
            {
                this.IsUserNameValidationVisible = false;
            }

            if (this.IsUserAgeValidationVisible)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(this.UserAge))
            {
                this.IsUserAgeValidationVisible = true;
                this.UserAgeValidation = AgeFieldEmptyValidation;

                return;
            }
            else
            {
                this.IsUserAgeValidationVisible = false;
            }

            var parseSuccess = int.TryParse(this.UserAge, out int userAge);
            var userData = new UserData
            {
                UserName = this.UserName,
                UserAge = userAge
            };

            if(parseSuccess)
            {
                userData.UserAge = userAge;
            }
            else
            {
                return;
            }

            await base.SetPageResultAsync(userData);

            await Shell.Current.GoToAsync("..");
        }

        private async Task BackAsync()
        {
            await base.SetPageResultAsync(default(UserData));
        }
    }
}
