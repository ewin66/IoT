using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyJoin
{
    public class ParkingOccupyEntity
    {
        public string UpdateTime { set; get; }
        public int Vacant { set; get; }
        public int Occupy { set; get; }
    }
}