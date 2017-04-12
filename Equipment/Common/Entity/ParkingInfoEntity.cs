using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Entity
{
    public class ParkingInfoEntity
    {
        public ParkingInfoEntity()
        { }
        #region Model
        private string _parkingid;
        private string _parkingname;
        private string _lat;
        private string _lon;
        private string _position;
        private string _serveraddress;
        private string _manager;
        private string _phonenum;
        private string _email;
        private int? _totlenum;
        private int? _freenum;
        private string _feescale;
        private DateTime? _updatedatatime;
        private DateTime? _createdatatime;
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
        public string ParkingName
        {
            set { _parkingname = value; }
            get { return _parkingname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lat
        {
            set { _lat = value; }
            get { return _lat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lon
        {
            set { _lon = value; }
            get { return _lon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Position
        {
            set { _position = value; }
            get { return _position; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ServerAddress
        {
            set { _serveraddress = value; }
            get { return _serveraddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Manager
        {
            set { _manager = value; }
            get { return _manager; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNum
        {
            set { _phonenum = value; }
            get { return _phonenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TotleNum
        {
            set { _totlenum = value; }
            get { return _totlenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FreeNum
        {
            set { _freenum = value; }
            get { return _freenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FeeScale
        {
            set { _feescale = value; }
            get { return _feescale; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateDataTime
        {
            set { _updatedatatime = value; }
            get { return _updatedatatime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDataTime
        {
            set { _createdatatime = value; }
            get { return _createdatatime; }
        }
        #endregion Model
    }
}
