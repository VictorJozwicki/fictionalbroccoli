using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace fictionalbroccoli.Services
{
    public class PictureService : IPictureService
    {
        public Image CurrentImage = new Image();

        public PictureService()
        {

        }

        public async Task<MediaFile> TakeFromCamera()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    //await DisplayAlert("No Camera ", "No camera available. ", "OK");
                    Debug.WriteLine("No camera available.");
                    return null;
                }
                else
                {
                    var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Registrations",
                        //Name = $"{DateTime.UtcNow}.jpg",
                        Name = "myphoto.jpg",
                    };

                    MediaFile file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                    Debug.WriteLine(file);

                    Debug.WriteLine(ImageSource.FromStream(file.GetStream));

                    if (file != null)
                        return file;
                    //CurrentImage.Source = ImageSource.FromStream(file.GetStream);


                    Debug.WriteLine("File Location", file.Path);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error  == ", ex.Message);
                return null;
            }
        }
    }
}
