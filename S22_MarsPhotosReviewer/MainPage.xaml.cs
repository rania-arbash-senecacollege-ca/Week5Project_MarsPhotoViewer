using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace S22_MarsPhotosReviewer
{
    public partial class MainPage : ContentPage
    {
        List<MarsPhoto> listOfPhotos = new List<MarsPhoto>();
        NetworkingManager networkingManager = new NetworkingManager();
        List<string> rovers = new List<string>();
        string selectedRover = "Opportunity";
        string selectedDate = "2005-6-3";
        List<string> images_ids = new List<string>();
        public MainPage()
        {
            InitializeComponent();
            rovers.Add("Curiosity");
            rovers.Add("Opportunity");
            rovers.Add("Spirit");

            roverpicker.ItemsSource = rovers;
        }

        override async protected void OnAppearing() {
          var listOfPhotos =  await networkingManager.getMarsPhotos("Spirit", "2005-6-3");
         //  await DisplayAlert("MarsPhotos", listOfPhotos.Count.ToString(), "OK");
        }


       async void roverpicker_SelectedIndexChanged(System.Object sender,
            System.EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex == 0)
                selectedRover = "Curiosity";
            else if (picker.SelectedIndex == 1)
                selectedRover = "Opportunity";
            else
                selectedRover = "Spirit";


             listOfPhotos = await networkingManager.getMarsPhotos(selectedRover, selectedDate);
            updateImageIDs();

        }

       async void datepicker_DateSelected(System.Object sender,
            Xamarin.Forms.DateChangedEventArgs e)
        {
            //'yyyy-mm-dd'.

            selectedDate = e.NewDate.Year.ToString() + "-" + e.NewDate.Month.ToString() + "-" + e.NewDate.Day.ToString();
             listOfPhotos = await networkingManager.getMarsPhotos(selectedRover, selectedDate);
            updateImageIDs();
        }


        public void updateImageIDs()
        {
            if (listOfPhotos.Count != 0)
            {
                images_ids = new List<string>();
                for (int i = 0; i < listOfPhotos.Count; i++)
                    images_ids.Add(listOfPhotos[i].id.ToString());
            }
            else
            {
                images_ids = new List<string>();
                
            }
            imagesIDs.ItemsSource = null;
            imagesIDs.ItemsSource = images_ids;
        }

        void imagesIDs_SelectedIndexChanged(System.Object sender,
            System.EventArgs e)
        {
            var picker = (Picker)sender;
            image.Source = listOfPhotos[picker.SelectedIndex].image_url;
        }
    }
}
