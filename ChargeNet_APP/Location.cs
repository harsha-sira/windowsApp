using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargeNet_APP
{
    class Location
    {
        //    public String chargenetReffrence { get; set; }
       // private String Status;
        public String gpsLocation { get; set; }
        public String workingStatus
        {
            get;
            set;
        } 
        public String locationName { get; set; }
        public Uri image { get; set; }
        public String longitude { get ; set ;}
        public String latitude { get; set;}
        public String chargerReference { get; set; }

        public Location(String longitude,String latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
            
        }

    }

   
}
