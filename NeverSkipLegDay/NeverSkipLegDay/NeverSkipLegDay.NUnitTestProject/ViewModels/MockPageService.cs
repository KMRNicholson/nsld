using NeverSkipLegDay.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NeverSkipLegDay.NUnitTestProject.ViewModels
{
    public class MockPageService : IPageService
    {
        public Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return Task<bool>.Factory.StartNew(()=>true);
        }

        public Task DisplayAlert(string title, string message, string ok)
        {
            return Task.Factory.StartNew(() => true);
        }

        public Task<Page> PopAsync()
        {
            return Task<Page>.Factory.StartNew(() => new Page());
        }

        public Task PushAsync(Page page)
        {
            return Task.Factory.StartNew(() => true);
        }
    }
}
