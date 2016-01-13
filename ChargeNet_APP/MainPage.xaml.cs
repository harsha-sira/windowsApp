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
using System.Collections;
using System.Threading;
using Microsoft.Phone.Net.NetworkInformation;
using System.IO;
using Windows.Storage;
using System.Runtime.Serialization.Json;
using System.IO.IsolatedStorage;


namespace ChargeNet_APP
{


    public partial class MainPage : PhoneApplicationPage
    {
        public static int num = 0;
        public static int warningFlagNoInternet = 0;
        public static String[,] array = new String[100, 7];
        bool isConnected = NetworkInterface.GetIsNetworkAvailable();
        IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
        String filename = "offlinedata.txt";
        bool isJsonloaded = false;
        //offline data loading not working 
        public void loadData()
        {
            try {
                StreamReader sr = new StreamReader(new IsolatedStorageFileStream(filename, FileMode.Open, myIsolatedStorage));
                string json = sr.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<Location>>(json);
                num = 0;

                foreach (var item in items)
                {
                    if (item.workingStatus.Equals("FREE"))
                    {
                        array[num, 1] = "FREE NORMAL";
                    }
                    else if (item.workingStatus.Equals("FREE_L3"))
                    {
                        array[num, 1] = "FREE FAST";
                    }
                    else if (item.workingStatus.Equals("IN_PROGRESS"))
                    {
                        array[num, 1] = "CHARGING NORMAL";
                    }
                    else if (item.workingStatus.Equals("IN_PROGRESS_L3"))
                    {
                        array[num, 1] = "CHARGING FAST";
                    }
                    else if (item.workingStatus.Equals("NA"))
                    {
                        array[num, 1] = "SOCKETS";
                    }
                    array[num, 0] = item.gpsLocation;
                    array[num, 2] = item.locationName;
                    array[num, 3] = item.longitude;
                    array[num, 4] = item.latitude;
                    array[num, 5] = item.chargerReference;
                    num++;
                    Debug.WriteLine("cache" + item.chargerReference);
                }
                isJsonloaded = true;
                if (isJsonloaded)
                {
                    mapBtn.Visibility = Visibility.Visible;
                    LocationBtn.Visibility = Visibility.Visible;
                    loadingText.Visibility = Visibility.Collapsed;
                    progressbar.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occured ! Please start the application");
                Application.Current.Terminate();
            }

           

            
        }
        // Constructor
        public MainPage()
        {

            InitializeComponent();
            Debug.WriteLine(isConnected);
            if (!isConnected)
            {
              // MessageBox.Show("No internet connection is avaliable. Please connect to internet");
               
                loadData();
                

            }
            else
            {
                loadjsonfrominternet();   
            }


           
            
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
               
                var list = JsonConvert.DeserializeObject<List<Location>>(e.Result);
                string jsonCache = JsonConvert.SerializeObject(list);
                Debug.WriteLine(jsonCache);
                //writing to offlinedata.txt for caching 
                if(myIsolatedStorage.FileExists(filename)){
                    
                       StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(filename, FileMode.Truncate, myIsolatedStorage));
                       writeFile.WriteLine(jsonCache);
                       writeFile.Close();
                }
               
                
                num = 0;
                foreach (var item in list)
                {
                    if (item.workingStatus.Equals("FREE"))
                    {
                        array[num, 1] = "FREE NORMAL";
                    }
                    else if (item.workingStatus.Equals("FREE_L3"))
                    {
                        array[num, 1] = "FREE FAST";
                    }
                    else if (item.workingStatus.Equals("IN_PROGRESS"))
                    {
                        array[num, 1] = "CHARGING NORMAL";
                    }
                    else if (item.workingStatus.Equals("IN_PROGRESS_L3"))
                    {
                        array[num, 1] = "CHARGING FAST";
                    }
                    else if (item.workingStatus.Equals("NA"))
                    {
                        array[num, 1] = "SOCKETS";
                    } 
                    array[num, 0] = item.gpsLocation;
                    array[num, 2] = item.locationName;
                    array[num, 3] = item.longitude;
                    array[num, 4] = item.latitude;
                    array[num, 5] = item.chargerReference;
                    num++;
                    Debug.WriteLine(item.chargerReference);
                }
                isJsonloaded = true;
                if (isJsonloaded)
                {
                    mapBtn.Visibility = Visibility.Visible;
                    LocationBtn.Visibility = Visibility.Visible;
                    loadingText.Visibility = Visibility.Collapsed;
                    progressbar.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Network error");
                MessageBox.Show("Please go back and load the page again");
                loadData();
            }

           
        }

        public void loadjsonfrominternet()
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            wc.DownloadStringAsync(new Uri("http://54.149.108.224/getActiveChargingPointLocations"));

        }

        private void mapBtn_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.Navigate(new Uri("/MapPage.xaml", UriKind.Relative));
        }

        private void LocationBtn_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/LocationPage.xaml", UriKind.Relative));
        }

    }   
}