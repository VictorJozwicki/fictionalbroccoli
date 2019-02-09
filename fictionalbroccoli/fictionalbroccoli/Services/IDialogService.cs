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
    }
}
