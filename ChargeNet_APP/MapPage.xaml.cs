using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ChargeNet_APP.Resources;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location; // Provides the GeoCoordinate class.
using Windows.Devices.Geolocation; //Provides the Geocoordinate class.
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows.Controls.Primitives;
using Windows.UI.Popups;
using Microsoft.Phone.Net.NetworkInformation;


namespace ChargeNet_APP
{
    public partial class MapPage : PhoneApplicationPage
    {
        bool socetesON = false;
        int[] soctetNumArray = new int[100];
        int numberOfSocets =0 ;
        bool pushtapped = false;
        MapOverlay[] mapOverlay = new MapOverlay[100];
        MapLayer myLayer = new MapLayer();
        Popup p = new Popup();
        double longitudeRecieved, latitudeRecieved;
        bool isConnected = NetworkInterface.GetIsNetworkAvailable();
        
        public MapPage()
        {
            InitializeComponent();
            if (!isConnected && MainPage.warningFlagNoInternet!=2)
            {
                MessageBox.Show("No internet connection is avaliable. Please connect to internet\nLoad the page again");
                MainPage.warningFlagNoInternet++;
              //  Application.Current.Terminate();

            }
            else if(isConnected)
            {
                MainPage objectMainpage = new MainPage();
                objectMainpage.loadjsonfrominternet();
                objectMainpage = null;
            }

            loadLocationOnMap();
            ShowMyLocationOnTheMap();


        }

        public void socketsprint()
        {
            Uri imageplug = new Uri("/images/plug.png", UriKind.RelativeOrAbsolute);
            
            for (int i = 0; i < numberOfSocets; i++)
            {
                var image = new Image();
                image.Width = 50;
                image.Height = 50;
                image.Opacity = 100;
                image.Source = new BitmapImage(imageplug);
                image.Name = MainPage.array[soctetNumArray[i], 2] + "\n" + MainPage.array[soctetNumArray[i], 0];
                image.Tap += pushtap_Tap;
                mapOverlay[soctetNumArray[i]] = new MapOverlay();
                mapOverlay[soctetNumArray[i]].Content = image;
                mapOverlay[soctetNumArray[i]].GeoCoordinate = new GeoCoordinate(Double.Parse(MainPage.array[soctetNumArray[i], 4]), Double.Parse(MainPage.array[soctetNumArray[i], 3]));
                mapOverlay[soctetNumArray[i]].PositionOrigin = new Point(0, 1);
                
                
                myLayer.Add(mapOverlay[soctetNumArray[i]]);
                
            }
        }

        public void loadLocationOnMap()
        {
            
            int numberOfLocations= MainPage.num;
          
            Uri imagebusy =new Uri("/images/level-2-busy-marker-pin.png", UriKind.RelativeOrAbsolute) ;
            Uri imagefree =new Uri("/images/level-2-free-marker-pin.png", UriKind.RelativeOrAbsolute) ;
            Uri imageSuperfree = new Uri("/images/super-charger-free-marker-pin.png",UriKind.RelativeOrAbsolute);
            Uri imageSuperbusy = new Uri("/images/super-charger-busy-marker-pin.png", UriKind.RelativeOrAbsolute);
            
            for (int i = 0; i < numberOfLocations; i++)
            {
                BitmapImage a;
                
                if (MainPage.array[i, 1].Equals("FREE NORMAL"))
                {
                    a = new BitmapImage(imagefree);
                }else if(MainPage.array[i,1].Equals("FREE FAST")){
                    a = new BitmapImage(imageSuperfree);
                }
                else if (MainPage.array[i, 1].Equals("CHARGING FAST"))
                {
                    a = new BitmapImage(imageSuperfree);
                }
                else if (MainPage.array[i, 1].Equals("CHARGING NORMAL"))
                {
                    a = new BitmapImage(imagebusy);
                }
                else
                {
                    soctetNumArray[numberOfSocets++] = i ;
                    continue;

                }

                var image = new Image();
                image.Width = 50;
                image.Height = 50;
                image.Opacity = 100;
                image.Source = a;
                image.Name = MainPage.array[i, 2]+"\n"+MainPage.array[i,0];

                image.Tap += pushtap_Tap;
                mapOverlay[i] = new MapOverlay();
                
                mapOverlay[i].Content = image;
                mapOverlay[i].GeoCoordinate = new GeoCoordinate(Double.Parse(MainPage.array[i,4]),Double.Parse(MainPage.array[i,3]));
                mapOverlay[i].PositionOrigin = new Point(0, 1);
                
                myLayer.Add(mapOverlay[i]); 
             

            }
            mapWithMyLocation.Layers.Add(myLayer);
        }

        public void pushtap_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
               
            Image im = (Image)sender;
            MessageBox.Show(im.Name);
            
        }
        public void removeSockets()
        {
            for (int i = 0; i < numberOfSocets; i++)
            {
                myLayer.Remove(mapOverlay[soctetNumArray[i]]);
            }
        }

        private async void ShowMyLocationOnTheMap()
        {
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Red);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;
            
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;

            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            mapWithMyLocation.Layers.Add(myLocationLayer);
            
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {

            if (!socetesON)
            {
                socketsprint();
                socetesON = true;
            }
            
                
        }

        void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
           
            if (socetesON)
            {
                removeSockets();
                socetesON = false;
            }
        }

        private void mapWithMyLocation_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (pushtapped)
            {
                p.IsOpen = false;
            }
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string strCode = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("selectedItem", out strCode))
            { 
                Debug.WriteLine(strCode);
                Debug.WriteLine(strCode.Substring(strCode.IndexOf(',')+1));

                latitudeRecieved =Convert.ToDouble(strCode.Substring(strCode.IndexOf(',') + 1));
                longitudeRecieved = Convert.ToDouble(strCode.Substring(0,strCode.IndexOf(',')));
                mapWithMyLocation.Center = new GeoCoordinate(latitudeRecieved, longitudeRecieved);
                mapWithMyLocation.ZoomLevel = 16;
               
                
            }
        }
       

    }
}