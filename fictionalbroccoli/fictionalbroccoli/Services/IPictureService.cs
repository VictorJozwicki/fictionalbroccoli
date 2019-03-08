using System;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace fictionalbroccoli.Services
{
    public interface IPictureService
    {
        Task<MediaFile> TakeFromCamera();
        Task<MediaFile> PickPhotoAsync();
        Task<MediaFile> PickOrTakePhotoAsync();
    }
}
