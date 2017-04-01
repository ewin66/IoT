using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyJoin
{
    public class ParkingStateEntity
    {
        private string wpseid;
        private string parkingname;
        private string state;
        private DateTime updatetime;
        private string battery;
        private string rssi;
        private DateTime changeTime;
        private string toolTip;
        public ParkingStateEntity()
        { }
        public string WPSD_ID
        {
            get { return wpseid; }
            set { wpseid = value; }
        }
        public string PARKINGNAME { get { return parkingname; } set { parkingname = value; } }
        public string STATE { get { return state; } set { state = value; } }
        public DateTime UPDATETIME { get { return updatetime; } set { updatetime = value; } }

        public string RSSI { get { return rssi; } set { rssi = value; } }

        public string Battery { get { return battery; } set { battery = value; } }
        public DateTime ChangeTime { get { return changeTime; } set { changeTime = value; } }

        public string ToolTip { get { return toolTip; } set { toolTip = value; } }
    }
}