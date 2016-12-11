using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Entity
{
    public class MonitorEntity
    {
        public string ID { set; get; }
        public string EQUIPMENT_TYPE_ID { get; set; }
        public string EQUIPMENT_TYPE_NAME { get; set; }
        public string EQUIPMENT_MODEL_ID { get; set; }
        public string EQUIPMENT_MODEL_NAME { get; set; }
        public string COMMUNICATION_NO { get; set; }
        public string Name { set; get; }        
        public string POSITION { set; get; }
        public string JOIN_TIME { get; set; }
        public string JOINER { get; set; }
        public string ADDRESS_NO { get; set; }
        public string STATE { get; set; }
        public bool IsAlarm { set; get; }
        public double Value { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public string LatLong { get; set; }
        public string Create_ID { set; get; }
        public string Updata_ID { set; get; }
    }

}