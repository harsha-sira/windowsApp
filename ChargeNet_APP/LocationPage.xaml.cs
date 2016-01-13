using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System.Diagnostics;
using ChargeNet_APP;
using System.Threading;
using Microsoft.Phone.Net.NetworkInformation;

namespace ChargeNet_APP
{


    public partial class LocationPage : PhoneApplicationPage
    {
        List<Location> stationList = new List<Location>();
        bool isConnected = NetworkInterface.GetIsNetworkAvailable();

        public LocationPage()
        {
            InitializeComponent();

            if (!isConnected && MainPage.warningFlagNoInternet!=2)
            {
                MessageBox.Show("No internet connection is avaliable. Please connect to internet\nLoad the page again");
                MainPage.warningFlagNoInternet++;
               // Application.Current.Terminate();

            }
            else if (isConnected)
            {
                MainPage objectMainpage = new MainPage();
                objectMainpage.loadjsonfrominternet();
                objectMainpage = null;
            }

            this.myAddressBook.ItemsSource = this.GetLocationGroups();
        
            
           
        }

        private static IEnumerable<Location> GetstationList()
        {
            List<Location> stationList = new List<Location>();

            for (int i = 0; i < MainPage.num; i++)
            {
                String status = MainPage.array[i, 1];
                Uri imageIcon;
                if (status.Contains("FREE"))
                {
                    imageIcon = new Uri(@"/Images/avail.png", UriKind.Relative);

                }
                else if (status.Contains("CHARGING"))
                {
                    imageIcon = new Uri(@"/Images/use.png", UriKind.Relative);

                }
                else
                {
                    imageIcon = new Uri(@"/Images/plug.png", UriKind.Relative);
                }
                 
               
              stationList.Add(new Location( MainPage.array[i, 3], MainPage.array[i, 4]) { gpsLocation = MainPage.array[i, 0], workingStatus = MainPage.array[i, 1], locationName = MainPage.array[i, 2], chargerReference = MainPage.array[i, 5], image = imageIcon });
                
            }

            return stationList;
        }

        private void LLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myAddressBook.SelectedItem == null)
                return;

            String longtitudeTapped = (myAddressBook.SelectedItem as Location).longitude;
            String latitudeTapped = (myAddressBook.SelectedItem as Location).latitude;
            
            Debug.WriteLine( (myAddressBook.SelectedItem as Location).locationName + "\n");

           
            NavigationService.Navigate(new Uri("/MapPage.xaml?selectedItem="+longtitudeTapped+","+latitudeTapped, UriKind.Relative));
            
            myAddressBook.SelectedItem = null;
        }
        private List<Group<Location>> GetLocationGroups()
        {
            
            IEnumerable<Location> stationList = GetstationList();
            return GetItemGroups(stationList, c => c.workingStatus);
        }

        private static List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item) into g
                                              orderby g.Key
                                              select new Group<T>(g.Key, g);
            return groupList.ToList();
        }

       

    }
}