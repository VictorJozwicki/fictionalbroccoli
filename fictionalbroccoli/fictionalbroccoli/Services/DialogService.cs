using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace fictionalbroccoli.Services
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {
        }

        public async Task ShowMessage(string title, string message, Action<bool> callback)
        {
            var res = await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "VALIDER",
                "ANNULER");
            if (callback != null)
            {
                callback(res);
            }
        }
    }
}
