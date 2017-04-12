using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Entity
{
    public class AutoinoutInfoEntity
    {
        public AutoinoutInfoEntity()
        { }
        #region Model
        private int _id;
        private string _parkingid;
        private string _doorIn;
        private string _doorOut;
        private string _carno;
        private DateTime _intime;
        private DateTime? _outtime;
        private string _state;
        private int? _timelong;
        private string _totalcost;
        private string _precost;
        private double _finalcost;
        private string _model;
        private DateTime? _updatetime;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParkingID
        {
            set { _parkingid = value; }
            get { return _parkingid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DoorIn
        {
            set { _doorIn = value; }
            get { return _doorIn; }
        }

        public string DoorOut
        {
            set { _doorOut = value; }
            get { return _doorOut; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime InTime
        {
            set { _intime = value; }
            get { return _intime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutTime
        {
            set { _outtime = value; }
            get { return _outtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TimeLong
        {
            set { _timelong = value; }
            get { return _timelong; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember(IsRequired = true, Order = 1)]
        public string TotalCost
        {
            set { _totalcost = value; }
            get { return _totalcost; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember(IsRequired = true, Order = 2)]
        public string PreCost
        {
            set { _precost = value; }
            get { return _precost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double FinalCost
        {
            set { _finalcost = value; }
            get { return _finalcost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Model
        {
            set { _model = value; }
            get { return _model; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model
    }
}
