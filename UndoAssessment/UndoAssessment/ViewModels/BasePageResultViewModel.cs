using System;
using System.Threading.Tasks;
using UndoAssessment.EventHandlers;
using Xamarin.Forms;

namespace UndoAssessment.ViewModels
{
    public class BasePageResultViewModel : BaseViewModel
    {
        public event EventHandler<PageResultEventArg> OnPageResultEvent;

        protected async Task<T> ShowWithResultAsync<T>(string pageName)
        {
            TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();

            void PageResult(object sender, PageResultEventArg args)
            {
                if(args.Result == null)
                {
                    taskCompletionSource.SetResult(default);

                    return;
                }

                var viewModel = sender as BasePageResultViewModel;
                viewModel.OnPageResultEvent -= PageResult;

                taskCompletionSource.SetResult((T)args.Result);
            }

            void PageNavigated(object sender, ShellNavigatedEventArgs args)
            {
                if (!(Shell.Current.CurrentPage.BindingContext is BasePageResultViewModel model))
                {
                    return;
                }

                Shell.Current.Navigated -= PageNavigated;
                model.OnPageResultEvent += PageResult;
            }

            Shell.Current.Navigated += PageNavigated;

            await Shell.Current.GoToAsync(pageName);

            return await taskCompletionSource.Task;
        }

        protected virtual Task SetPageResultAsync<T>(T data)
        {
            this.OnPageResultEvent.Invoke(this, new PageResultEventArg(data));

            return Task.CompletedTask;
        }
    }
}
