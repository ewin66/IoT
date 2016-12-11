using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Entity
{
    public class MonitorDataEntity
    {
        public string ID { set; get; }

        public string Name { set; get; }

        public double Value { get; set; }
    }
}