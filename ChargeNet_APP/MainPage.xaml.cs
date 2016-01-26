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
        public static bool flag =true;
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

                if (!myIsolatedStorage.FileExists(filename))
                {

                    StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(filename, FileMode.CreateNew, myIsolatedStorage));
                    writeFile.WriteLine("[{\"chargerReference\":\"8\",\"longitude\":\"79.889851\",\"latitude\":\"6.860892\",\"gpsLocation\":\"Piliyandala Road, Kohuwela\",\"workingStatus\":\"FREE\",\"locationName\":\"SPKCHG2\"},{\"chargerReference\":\"C0001\",\"longitude\":\"79.861156\",\"latitude\":\"6.929267\",\"gpsLocation\":\"Trace Expert City, Maradana, 0115 551 551\",\"workingStatus\":\"IN_PROGRESS\",\"locationName\":\"Trace\"},{\"chargerReference\":\"S0003\",\"longitude\":\"79.879895\",\"latitude\":\"6.914149\",\"gpsLocation\":\"Cotta Rd, Colombo 10\",\"workingStatus\":\"FREE\",\"locationName\":\"SPKCHG1\"},{\"chargerReference\":\"C0003\",\"longitude\":\"80.3846977\",\"latitude\":\"6.7158094\",\"gpsLocation\":\"RTS Petrolium Rathnapura\",\"workingStatus\":\"FREE\",\"locationName\":\"RTS Petrolium\"},{\"chargerReference\":\"C0004\",\"longitude\":\"79.861156\",\"latitude\":\"6.929267\",\"gpsLocation\":\"Trace Expert City, Maradana, 0115 551 551\",\"workingStatus\":\"FREE\",\"locationName\":\"Trace\"},{\"chargerReference\":\"T0001\",\"longitude\":\"79.860615\",\"latitude\":\"6.929651\",\"gpsLocation\":\"Trace Expert City, Maradana, 0115 551 551\",\"workingStatus\":\"FREE\",\"locationName\":\"Trace II\"},{\"chargerReference\":\"C0005\",\"longitude\":\"79.8983181\",\"latitude\":\"7.0346611\",\"gpsLocation\":\"Negombo-Colombo Main Rd Welesara\",\"workingStatus\":\"IN_PROGRESS\",\"locationName\":\"Wasana Trading\"},{\"chargerReference\":\"P0001\",\"longitude\":\"79.897876\",\"latitude\":\"7.040059\",\"gpsLocation\":\"No. 310, Negombo Road, Welisara, Ragama,077-2239900, 011-2241333\",\"workingStatus\":\"NA\",\"locationName\":\"Wasana 001\"},{\"chargerReference\":\"P0002\",\"longitude\":\"79.897876\",\"latitude\":\"7.040059\",\"gpsLocation\":\"No.291, Negombo Road, Welisara, Ragama, 077-3833834, 011-2239900\",\"workingStatus\":\"NA\",\"locationName\":\"Wasana Speed Service Station\"},{\"chargerReference\":\"P0003\",\"longitude\":\"79.897876\",\"latitude\":\"7.040059\",\"gpsLocation\":\"No. 305, Negombo Road, Welisara, Ragama, 077-7626217\",\"workingStatus\":\"NA\",\"locationName\":\"S R B Traders\"},{\"chargerReference\":\"P0004\",\"longitude\":\"79.890176\",\"latitude\":\"7.086880\",\"gpsLocation\":\"No. 275, Negombo Road, Tudella, Jaela, 071-6822709, 011-2231075\",\"workingStatus\":\"NA\",\"locationName\":\"W M M Fernando & Sons\"},{\"chargerReference\":\"P0006\",\"longitude\":\"79.850910\",\"latitude\":\"7.227844\",\"gpsLocation\":\"No. 53/2/A, Colombo Road, Kattuwa, Negombo, 077-7580380, 031-2277950\",\"workingStatus\":\"NA\",\"locationName\":\"Rising Motors\"},{\"chargerReference\":\"P0007\",\"longitude\":\"79.859008\",\"latitude\":\"7.264351\",\"gpsLocation\":\"No. 07, Negombo Road, Kochchikade, 077-2392522\",\"workingStatus\":\"NA\",\"locationName\":\"View Bright Service Center\"},{\"chargerReference\":\"P0010\",\"longitude\":\"79.987373\",\"latitude\":\"7.110987\",\"gpsLocation\":\"No. 285 C, Bulathkade Junction, Asgiriya, Gampaha, 077-0772888\",\"workingStatus\":\"NA\",\"locationName\":\"Ranathunga Motor Traders\"},{\"chargerReference\":\"P0015\",\"longitude\":\"80.093481\",\"latitude\":\"7.144064\",\"gpsLocation\":\"No. 403 B, Negombo Road, Nittambuwa, 071-2252563, 033-3555077\",\"workingStatus\":\"NA\",\"locationName\":\"Kubeeda Wheel Alignment Center\"},{\"chargerReference\":\"P0016\",\"longitude\":\"80.023765\",\"latitude\":\"7.152335\",\"gpsLocation\":\"Marapola, Veyangoda, 077-3664246, 033-2289888\",\"workingStatus\":\"NA\",\"locationName\":\"Jayasingha Auto Engineers\"},{\"chargerReference\":\"P0017\",\"longitude\":\"80.151744\",\"latitude\":\"7.192835\",\"gpsLocation\":\"No. 61/A/8, Kandy Road, Weweldeniya, 077-2097533, 033-2284482\",\"workingStatus\":\"NA\",\"locationName\":\"Lanka Filling Station\"},{\"chargerReference\":\"P0018\",\"longitude\":\"80.133884\",\"latitude\":\"7.291703\",\"gpsLocation\":\"No. 02, Pohonnaruwa, Mirigama\",\"workingStatus\":\"NA\",\"locationName\":\"Lanka Filling Station (2)\"},{\"chargerReference\":\"P0019\",\"longitude\":\"80.001589\",\"latitude\":\"7.092631\",\"gpsLocation\":\"No. 23, Mary Bisho Mawatha, Gampaha, 077-7266167, 033-2231810\",\"workingStatus\":\"NA\",\"locationName\":\"K O G Enterprises\"},{\"chargerReference\":\"P0020\",\"longitude\":\"79.999523\",\"latitude\":\"7.091385\",\"gpsLocation\":\"No. 09, Yakkala Road, Gampaha, 077-1925694, 033-2222443\",\"workingStatus\":\"NA\",\"locationName\":\"Senevirathna & Sons\"},{\"chargerReference\":\"P0021\",\"longitude\":\"79.999523\",\"latitude\":\"7.091385\",\"gpsLocation\":\"No. 09, Yakkala Road, Gampaha, 077-1925694, 033-2222443\",\"workingStatus\":\"NA\",\"locationName\":\"Senevirathna & Sons\"},{\"chargerReference\":\"P0025\",\"longitude\":\"80.040286\",\"latitude\":\"6.915771\",\"gpsLocation\":\"No. 1613, Avissawella Road, Ranala, Kaduwela, 077-5357057, 011-2415485\",\"workingStatus\":\"NA\",\"locationName\":\"Miyurasa Hotel & Rest\"},{\"chargerReference\":\"P0026\",\"longitude\":\"80.128982\",\"latitude\":\"6.940872\",\"gpsLocation\":\"No. 121/1, High Level Road, Pahala Kosgama, Kosgama, 071-5965765, 036-2254120\",\"workingStatus\":\"NA\",\"locationName\":\"Mahagedara Hotel\"},{\"chargerReference\":\"P0027\",\"longitude\":\"80.091062\",\"latitude\":\"6.942884\",\"gpsLocation\":\"Opposite Linea Aqua, Kapugoda, Hanwella, 071-4892198, 011-2406907\",\"workingStatus\":\"NA\",\"locationName\":\"Red Chilee Restaurant\"},{\"chargerReference\":\"P0028\",\"longitude\":\"79.938972\",\"latitude\":\"6.877312\",\"gpsLocation\":\"No. 287/E/7, Hokandara Road, Thalawatugoda, 077-7356630, 011-2774147\",\"workingStatus\":\"NA\",\"locationName\":\"Cool  Zoon Auto Mart\"},{\"chargerReference\":\"P0029\",\"longitude\":\"79.901807\",\"latitude\":\"6.884712\",\"gpsLocation\":\"No. 549/B, Kotte Road, Petakotte,\\\\n 077-4308863, 076-7455144\",\"workingStatus\":\"NA\",\"locationName\":\"Right Automobiles\"},{\"chargerReference\":\"P0030\",\"longitude\":\"79.963818\",\"latitude\":\"6.891333\",\"gpsLocation\":\"No. 456, Athurugiriya Road, Hokandara Road, Arangala, 077-7780527, 011-2185585\",\"workingStatus\":\"NA\",\"locationName\":\"S P Filling Station\"},{\"chargerReference\":\"P0031\",\"longitude\":\"79.946871\",\"latitude\":\"6.846355\",\"gpsLocation\":\"072-7746025, 011-2746749\",\"workingStatus\":\"NA\",\"locationName\":\"Machan â?? Pannipitiya\"},{\"chargerReference\":\"T0002\",\"longitude\":\"79.860615\",\"latitude\":\"6.929651\",\"gpsLocation\":\"Trace Expert City, Maradana, 0115 551 551\",\"workingStatus\":\"IN_PROGRESS\",\"locationName\":\"Trace II\"},{\"chargerReference\":\"T0003\",\"longitude\":\"79.860615\",\"latitude\":\"6.929651\",\"gpsLocation\":\"Trace Expert City, Maradana, 0115 551 551\",\"workingStatus\":\"IN_PROGRESS\",\"locationName\":\"Trace II\"},{\"chargerReference\":\"C0006\",\"longitude\":\"79.912932\",\"latitude\":\"7.101143\",\"gpsLocation\":\"No. 08, Gampaha Road, Ekala, Jaela, 077-2975238, 011-2236928\",\"workingStatus\":\"IN_PROGRESS\",\"locationName\":\"Lora Lanka Filling Station\"},{\"chargerReference\":\"P0041\",\"longitude\":\"80.228396\",\"latitude\":\"6.068212\",\"gpsLocation\":\"No. 500/C, Kahaduwa Watta Junction, Hiribura Road, Galle, 077-4275776\",\"workingStatus\":\"NA\",\"locationName\":\"Apex Health Care\"},{\"chargerReference\":\"P0042\",\"longitude\":\"80.228112\",\"latitude\":\"6.067754\",\"gpsLocation\":\"No. 34, Godakanda Road, Karapitiya, Galle, 077-7612609, 091-2226047\",\"workingStatus\":\"NA\",\"locationName\":\"Athula Auto Traders\"},{\"chargerReference\":\"P0043\",\"longitude\":\"80.229920\",\"latitude\":\"6.068646\",\"gpsLocation\":\"Namal Filling Station,071-4922382 091-2244819\",\"workingStatus\":\"NA\",\"locationName\":\"Namal Filling Station\"},{\"chargerReference\":\"P0044\",\"longitude\":\"80.388679\",\"latitude\":\"6.035579\",\"gpsLocation\":\"Akuressa Road, Paragoda, Imaduwa, Galle, 077-7184663\",\"workingStatus\":\"NA\",\"locationName\":\"Ranjika Stores\"},{\"chargerReference\":\"P0045\",\"longitude\":\"80.432735\",\"latitude\":\"6.020242\",\"gpsLocation\":\"Kadaduwage Watta, kokmaduwa, Weligama, 077-7184663\",\"workingStatus\":\"NA\",\"locationName\":\"Dilmini Stores\"},{\"chargerReference\":\"P0046\",\"longitude\":\"80.526646\",\"latitude\":\"5.944867\",\"gpsLocation\":\"No. 445, Galle Road, Pamburana, Matara, 077-7328336, 041-2229016\",\"workingStatus\":\"NA\",\"locationName\":\"Sujith Auto Electricals\"},{\"chargerReference\":\"P0048\",\"longitude\":\"81.331023\",\"latitude\":\"6.413178\",\"gpsLocation\":\"Kataragama, 076-7253395, 047-2235217\",\"workingStatus\":\"NA\",\"locationName\":\"Hotel Chamila\"},{\"chargerReference\":\"P0049\",\"longitude\":\"81.279733\",\"latitude\":\"6.284959\",\"gpsLocation\":\"Molakepupathana, Tissamaharama,071-8440962, 047-2239599\",\"workingStatus\":\"NA\",\"locationName\":\"Amaraweera Industries\"},{\"chargerReference\":\"P0050\",\"longitude\":\"81.082544\",\"latitude\":\"6.116694\",\"gpsLocation\":\"Ratalawewa, 2nd Road, Miriggawila, Hambantota, 071-3364677\",\"workingStatus\":\"NA\",\"locationName\":\"W K Transport (Pvt) Ltd\"},{\"chargerReference\":\"P0052\",\"longitude\":\"81.083413\",\"latitude\":\"6.117007\",\"gpsLocation\":\"No. 22, Miriggawila, Hambantota, 077-4610000, 047-2222323\",\"workingStatus\":\"NA\",\"locationName\":\"Mr. Dilan Gunawardana\"},{\"chargerReference\":\"P0053\",\"longitude\":\"81.084775\",\"latitude\":\"6.116900\",\"gpsLocation\":\"Port Access, Hambantota, 077-6404088\",\"workingStatus\":\"NA\",\"locationName\":\"Chamila Motors\"},{\"chargerReference\":\"P0054\",\"longitude\":\"81.028540\",\"latitude\":\"6.122794\",\"gpsLocation\":\"Tangalle Road, Ambalantota, 077-7909778, 047-2223401\",\"workingStatus\":\"NA\",\"locationName\":\"Ruhunu Lanka IOC\"},{\"chargerReference\":\"P0055\",\"longitude\":\"81.030128\",\"latitude\":\"6.122832\",\"gpsLocation\":\"Tissa Road, Walava, Ambalnatota, 077-4610000 \",\"workingStatus\":\"NA\",\"locationName\":\"Senumi Automobiles\"},{\"chargerReference\":\"P0040\",\"longitude\":\"79.983455\",\"latitude\":\"6.482196\",\"gpsLocation\":\"No. 13, Galle Road, Polkatuwa, Beruwala,077-3580113, 034-2276185\",\"workingStatus\":\"NA\",\"locationName\":\"B M C Guest House\"},{\"chargerReference\":\"P0038\",\"longitude\":\"80.068317\",\"latitude\":\"6.718010\",\"gpsLocation\":\"No. 479, Panadura Road, Horana, 071-9199696\",\"workingStatus\":\"NA\",\"locationName\":\"Horana EV Center\"},{\"chargerReference\":\"P0035\",\"longitude\":\"79.907762\",\"latitude\":\"6.720122\",\"gpsLocation\":\"No. 101, Main Street, Panadura, 071-9199696, 038-2246333\",\"workingStatus\":\"NA\",\"locationName\":\"Panadura EV Center\"},{\"chargerReference\":\"P0037\",\"longitude\":\"80.044441\",\"latitude\":\"6.541775\",\"gpsLocation\":\"No. 19 B, Near the Southern Highway, Siripura, Dodangoda, 071-8209055, 034-2280292\",\"workingStatus\":\"NA\",\"locationName\":\"Ranaweera Trade Center\"},{\"chargerReference\":\"P0068\",\"longitude\":\"80.202102\",\"latitude\":\"7.230545\",\"gpsLocation\":\"No. 246, Kandy Road, Warakapola, 077-6747084, 035-2267383\",\"workingStatus\":\"NA\",\"locationName\":\"Warakapola Tyre Service\"},{\"chargerReference\":\"P0069\",\"longitude\":\"80.396643\",\"latitude\":\"7.256798\",\"gpsLocation\":\"Kandy Road, Mangalagama, Molagoda, Kegalle, 077-2351223, 035-2228961\",\"workingStatus\":\"NA\",\"locationName\":\"Tilanis Family Restaruant\"},{\"chargerReference\":\"P0070\",\"longitude\":\"80.533068\",\"latitude\":\"7.591145\",\"gpsLocation\":\"No. 204, Kandy Road, Meepitiya, Kegalle, 077-6588561, 035-2221567\",\"workingStatus\":\"NA\",\"locationName\":\"Prasanna Motor Engineers\"},{\"chargerReference\":\"P0074\",\"longitude\":\"80.555019\",\"latitude\":\"7.265794\",\"gpsLocation\":\"Colombo Road, Pilimathalawa, 071-8286234, 071-3692008\",\"workingStatus\":\"NA\",\"locationName\":\"Unic Service Station\"},{\"chargerReference\":\"P0077\",\"longitude\":\"80.593007\",\"latitude\":\"7.190151\",\"gpsLocation\":\"No. 138, Kandy Road, Weligalla, Gelioya, 077-7276797, 081-5624114\",\"workingStatus\":\"NA\",\"locationName\":\"Sun Auto Electricals\"},{\"chargerReference\":\"P0078\",\"longitude\":\"80.583944\",\"latitude\":\"7.142447\",\"gpsLocation\":\"Nuwaraeliya Road, Store Field, Gampola, 077-6800783, 081-2350078\",\"workingStatus\":\"NA\",\"locationName\":\"The Kichan CafÃ©\"},{\"chargerReference\":\"C0007\",\"longitude\":\"79.865905\",\"latitude\":\"6.885615\",\"gpsLocation\":\"Havelock Town\",\"workingStatus\":\"IN_PROGRESS\",\"locationName\":\"Laughs - Havelock Town\"},{\"chargerReference\":\"C0009\",\"longitude\":\"80.720008\",\"latitude\":\"6.673274\",\"gpsLocation\":\"Naluwela, Balangoda. 0771441520\",\"workingStatus\":\"IN_PROGRESS\",\"locationName\":\"CHAMENRO Bake House\"},{\"chargerReference\":\"C0008\",\"longitude\":\"79.860615\",\"latitude\":\"6.929651\",\"gpsLocation\":\"Trace Expert City, Maradana, 0115 551 551\",\"workingStatus\":\"FREE\",\"locationName\":\"Trace II\"},{\"chargerReference\":\"F0001\",\"longitude\":\"79.861748\",\"latitude\":\"6.929057\",\"gpsLocation\":\"Maradana, 011551551\",\"workingStatus\":\"FREE_L3\",\"locationName\":\"Trace Fast Charger\"},{\"chargerReference\":\"F0002\",\"longitude\":\"79.861748\",\"latitude\":\"6.929057\",\"gpsLocation\":\"Maradana, 011551551\",\"workingStatus\":\"IN_PROGRESS_L3\",\"locationName\":\"Trace Fast Charger\"},{\"chargerReference\":\"C0010\",\"longitude\":\"79.849625\",\"latitude\":\"6.917416\",\"gpsLocation\":\"Araliya carpark, Galle Road, Kollupitiya\",\"workingStatus\":\"IN_PROGRESS\",\"locationName\":\"Crescat \"},{\"chargerReference\":\"C0011\",\"longitude\":\"79.849136\",\"latitude\":\"6.917587\",\"gpsLocation\":\"Araliya carpark, Galle Road, Kollupitiya\",\"workingStatus\":\"FREE\",\"locationName\":\"Crescat II\"},{\"chargerReference\":\"C0012\",\"longitude\":\"79.884849\",\"latitude\":\"6.853597\",\"gpsLocation\":\"Speed Motors, Nedimala\",\"workingStatus\":\"FREE\",\"locationName\":\"Speed Motors\"},{\"chargerReference\":\"F0003\",\"longitude\":\"79.906237\",\"latitude\":\"6.833119\",\"gpsLocation\":\"198, Kesbewa Rd, Werahera, 0776521987\",\"workingStatus\":\"IN_PROGRESS_L3\",\"locationName\":\"E-Car City\"}]");
                    writeFile.Close();
                    Debug.WriteLine("writing 1st time");
                }

                StreamReader sr = new StreamReader(new IsolatedStorageFileStream(filename, FileMode.Open, myIsolatedStorage));
                string json = sr.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<Location>>(json);
                num = 0;

                foreach (var item in items)
                {
                    if (item.chargerReference.Equals("F0001"))
                        continue;

                    if (item.workingStatus.Equals("FREE"))
                    {
                        array[num, 1] = "FREE LEVEL2";
                    }
                    else if (item.workingStatus.Equals("FREE_L3"))
                    {
                        array[num, 1] = "FREE FAST";
                    }
                    else if (item.workingStatus.Equals("IN_PROGRESS"))
                    {
                        array[num, 1] = "CHARGING LEVEL2";
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
            
            if (!isConnected)
            {
              // MessageBox.Show("No internet connection is avaliable. Please connect to internet");
               
                loadData();
                flag = false;

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
                if (myIsolatedStorage.FileExists(filename))
                {

                    StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(filename, FileMode.Truncate, myIsolatedStorage));
                    writeFile.WriteLine(jsonCache);
                    writeFile.Close();
                }
                else
                {
                    StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(filename, FileMode.CreateNew, myIsolatedStorage));
                    writeFile.WriteLine(jsonCache);
                    writeFile.Close();
                }
               
                
                num = 0;
                foreach (var item in list)
                {
                    if (item.chargerReference.Equals("F0001"))
                        continue;
                        
                    if (item.workingStatus.Equals("FREE"))
                    {
                        array[num, 1] = "FREE LEVEL2";
                    }
                    else if (item.workingStatus.Equals("FREE_L3"))
                    {
                        array[num, 1] = "FREE FAST";
                    }
                    else if (item.workingStatus.Equals("IN_PROGRESS"))
                    {
                        array[num, 1] = "CHARGING LEVEL2";
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