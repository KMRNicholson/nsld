using System.Threading.Tasks;

using Xamarin.Forms;

namespace NeverSkipLegDay.ViewModels
{
    public class PageService : IPageService
    {
        
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, ok, cancel).ConfigureAwait(false);
        }

        public async Task DisplayAlert(string title, string message, string ok)
        {
            await MainPage.DisplayAlert(title, message, ok).ConfigureAwait(false);
        }

        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync().ConfigureAwait(false);
        }

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page).ConfigureAwait(false);
        }
        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}
