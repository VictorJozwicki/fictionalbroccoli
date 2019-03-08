using System;
using System.Threading.Tasks;
using Prism.Services;
using Xamarin.Forms;

namespace fictionalbroccoli.Services
{
    public class DialogService : IDialogService
    {
        IPageDialogService _pageDialogService;
        public DialogService(IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
        }

        public async Task ShowMessage(string title, string message, Action<bool> callback)
        {
            var res = await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "VALIDER",
                "ANNULER");
            callback?.Invoke(res);
        }

        public async Task chooseBetween(string title, string[] buttons, Action<string> callback)
        {
            var res = await _pageDialogService.DisplayActionSheetAsync(
                title,
                "Annuler",
                "Quitter",
                buttons
                );
            callback?.Invoke(res);
        }
    }
}
