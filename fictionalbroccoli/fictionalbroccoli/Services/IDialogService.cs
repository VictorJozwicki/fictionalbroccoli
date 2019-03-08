using System;
using System.Threading.Tasks;

namespace fictionalbroccoli.Services
{
    public interface IDialogService
    {
        Task ShowMessage(
            string title,
            string message,
            Action<bool> callback
            );

        Task chooseBetween(string title, string[] buttons, Action<string> callback);
    }
}
