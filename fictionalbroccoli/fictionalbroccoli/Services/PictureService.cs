using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Services;
using Xamarin.Forms;

namespace fictionalbroccoli.Services
{
    public class PictureService : IPictureService
    {
        public Image CurrentImage = new Image();

        IPageDialogService _pageDialogService;

        public PictureService(
            IPageDialogService pageDialogService
            )
        {
            _pageDialogService = pageDialogService;
        }

        public async Task<MediaFile> PickOrTakePhotoAsync()
        {
            string[] Buttons = new string[2];
            Buttons[0] = "Gallerie";
            Buttons[1] = "Prendre";
            MediaFile file;

            var res = await _pageDialogService.DisplayActionSheetAsync(
               "Que voulez vous faire ?",
               "Annuler",
               "Quitter",
               Buttons
               );
               
                if (res == "Gallerie")
                {
                    file = await PickPhotoAsync();
                } 
                else
                {
                    file = await TakeFromCamera();
                }

            return file;
        }

        public async Task<MediaFile> PickPhotoAsync()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    Debug.WriteLine("No camera available.");
                    return null;
                }

                MediaFile file = await CrossMedia.Current.PickPhotoAsync();
                return file;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error  == ", ex.Message);
                return null;
            }
        }

        public async Task<MediaFile> TakeFromCamera()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    Debug.WriteLine("No camera available.");
                    return null;
                }
                    
                    var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Registrations",
                        Name = "myphoto.jpg",
                        PhotoSize = PhotoSize.Medium,
                        CompressionQuality = 92,
                    };

                    MediaFile file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                    return file;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error  == ", ex.Message);
                return null;
            }
        }
    }
}
