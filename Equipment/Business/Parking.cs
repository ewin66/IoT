﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBO;
using Common;
using System.Data;

namespace Business
{
    public class Parking
    {
        private static string updateParkingStateSQL = @"Update ParkingState Set STATE='{0}',UPDATETIME=now() Where WPSD_ID = '{1}';";

        private static string InsertParkingHistoryWithRssiSQL = @"INSERT INTO ParkingHistory(WPSD_ID,STATE,UPDATETIME)VALUE('{0}','{1}', now());";

        private static string InsertParkingHistorySQL = @"INSERT INTO ParkingHistory(WPSD_ID,STATE,UPDATETIME,RSSI,Battery)VALUE('{0}','{1}', now(),{2},{3});";

        private static string UpdateParkingHistorySQL = @"Update ParkingHistory Set UPDATETIME=now(),Times=Times+1 Where ID = {0}";

        //private static string GetParkingHistorySQL = @"Select * From ParkingHistory Where WPSD_ID = '{0}' Order By UPDATETIME Desc Limt 2";

        private static string GetParkingHistorySQL = @"Select * From ParkingHistory 
Where WPSD_ID = '{0}' 
Order By UPDATETIME Desc LIMIT 2;";

        private static string updateParkingStateWithRSSISQL = @"Update ParkingState Set ChangeTime=if(STATE='{0}' and ChangeTime!=null, ChangeTime ,now()), STATE='{0}',UPDATETIME=now(),RSSI={1},Battery={2} Where WPSD_ID = '{3}';";


        private static string getParkingStateSQL = @"Select * From parkingstate
Where PARKINGLOT_ID = '{0}' ORDER BY PARKINGNAME Desc;";

        private static string AddParkingSQL = @"INSERT INTO ParkingState (
	PARKINGLOT_ID,
  PARKINGNAME,
  WPSD_ID,
  STATE,
  UPDATETIME,
	HEIGHT,
	WIDTH,
	LOCATIONX,
	LOCATIONY
)VALUE('{0}','{1}','{2}','0',now(),{3},{4},{5},{6});";

        private static string UpdateParkingSQL = @"Update ParkingState Set 
PARKINGNAME='{0}',HEIGHT={1},WIDTH={2},LOCATIONX={3},LOCATIONY={4} 
Where WPSD_ID = '{5}';";

        private static string GetParkingSQL = @"Select PARKINGNAME from ParkingState Where WPSD_ID = '{0}';";

        private static string InsertParkingOccupySQL = @"Insert Into ParkingOccupy(PARKINGLOT_ID,UpdateTime,Vacant,Occupy)
select PARKINGLOT_ID,now(),max(case STATE when '1' then Number else 0 end) as Vacant,
max(case STATE when '2' then Number else 0 end) as Occupy from
(select PARKINGLOT_ID,STATE,count(STATE) as Number from parkingstate 
where PARKINGLOT_ID='SMDS001' 
group by STATE,PARKINGLOT_ID) A GROUP BY PARKINGLOT_ID";

        private static string InsertRepeatParkingOccupySQL = @"Insert Into ParkingOccupy(PARKINGLOT_ID, UpdateTime, Vacant, Occupy)
Value('{0}',now(),{1},{2})";

        private static string UpdateParkingOccupySQL = @"Update ParkingOccupy set UpdateTime = now(),Times = Times+1 where ID= {0}";

        private static string GetParkingOccupySQL = @"Select * from ParkingOccupy where 1=1 @@@ order by UpdateTime LIMIT 720";

        private static string GetRepeatParkingOccupySQL = @"select -1 as ID,PARKINGLOT_ID,null as UpdateTime,max(case STATE when '1' then Number else 0 end) as Vacant,
max(case STATE when '2' then Number else 0 end) as Occupy from
(select PARKINGLOT_ID,STATE,count(STATE) as Number from parkingstate 
where PARKINGLOT_ID='{0}' 
group by STATE,PARKINGLOT_ID) A GROUP BY PARKINGLOT_ID
UNION ALL
(Select ID,PARKINGLOT_ID,UpdateTime,Vacant,Occupy 
From parkingoccupy 
Where PARKINGLOT_ID='{0}' 
Order By UpdateTime DESC LIMIT 2)";

        public bool UpdateParkingState(string WPSD_ID, string STATE)
        {
            string sql = string.Empty;
            bool re = false;
            sql = string.Format(updateParkingStateSQL,STATE, WPSD_ID);
            string strResoult = DBConnect.ExecuteSql(sql);
            if (strResoult == "0")
            {
                ReturnValue resoult = new ReturnValue();
                sql = string.Format(GetParkingHistorySQL, WPSD_ID);
                resoult = DBConnect.Select(sql);
                //查询最新两条数据
                if (resoult.NoError)
                {
                    if (resoult.Count >= 2)
                    {
                        DataTable tb = resoult.ResultDataSet.Tables[0];
                        string State1 = tb.Rows[0]["STATE"].ToString();
                        string State2 = tb.Rows[1]["STATE"].ToString();
                        if (STATE == State1)
                        {
                            //一致 
                            //是否最后两天条数据一致
                            if (State1 == State2)
                            {
                                //一致 
                                //更新第二条（时间、Times）
                                string ID = tb.Rows[0]["ID"].ToString();
                                sql = string.Format(UpdateParkingOccupySQL, ID);
                                strResoult = DBConnect.ExecuteSql(sql);
                                if (strResoult == "0")
                                {
                                    re = true;
                                }
                            }
                            else
                            {
                                //不一致 
                                //添加一条相同数据
                                sql = string.Format(InsertParkingHistoryWithRssiSQL, WPSD_ID, STATE);
                                strResoult = DBConnect.ExecuteSql(sql);
                                if (strResoult == "0")
                                {
                                    re = true;
                                }
                            }
                        }
                        else
                        {
                            //不一致 
                            //添加一条新数据
                            sql = string.Format(InsertParkingHistoryWithRssiSQL, WPSD_ID, STATE);
                            strResoult = DBConnect.ExecuteSql(sql);
                            if (strResoult == "0")
                            {
                                re = true;
                            }
                        }
                    }
                }
                else
                {
                    sql = string.Format(InsertParkingHistoryWithRssiSQL, WPSD_ID, STATE);
                    strResoult = DBConnect.ExecuteSql(sql);
                    if (strResoult == "0")
                    {
                        re = true;
                    }
                }
            }
            return re;
        }

        public bool UpdateParkingState(string WPSD_ID,string RSSI, string Voltage, string STATE)
        {
            string sql = string.Empty;
            bool re = false;
            sql = string.Format(updateParkingStateWithRSSISQL
                , STATE, RSSI, Voltage, WPSD_ID);
            string strResoult = DBConnect.ExecuteSql(sql);
            if (strResoult == "0")
            {
                ReturnValue resoult = new ReturnValue();
                sql = string.Format(GetParkingHistorySQL,WPSD_ID);
                resoult = DBConnect.Select(sql);
                //查询最新两条数据
                if (resoult.NoError)
                {
                    if (resoult.Count >= 2)

                    {
                        DataTable tb = resoult.ResultDataSet.Tables[0];

                        string State1 = tb.Rows[0]["STATE"].ToString();
                        string Rssi1 = tb.Rows[0]["RSSI"].ToString();
                        string Battery1 = tb.Rows[0]["Battery"].ToString();

                        string State2 = tb.Rows[1]["STATE"].ToString();
                        string Rssi2 = tb.Rows[1]["RSSI"].ToString();
                        string Battery2 = tb.Rows[1]["Battery"].ToString();

                        if (RSSI == Rssi1 && Voltage == Battery1 && STATE == State1)
                        {
                            //一致 
                            //是否最后两天条数据一致
                            if (State1 == State2 && Rssi1 == Rssi2 && Battery1 == Battery2)
                            {
                                //一致 
                                //更新第二条（时间、Times）
                                string ID = tb.Rows[0]["ID"].ToString();
                                sql = string.Format(UpdateParkingOccupySQL, ID);
                                strResoult = DBConnect.ExecuteSql(sql);
                                if (strResoult == "0")
                                {
                                    re = true;
                                }
                            }
                            else
                            {
                                //不一致 
                                //添加一条相同数据
                                sql = string.Format(InsertParkingHistorySQL, WPSD_ID, STATE, RSSI, Voltage);
                                strResoult = DBConnect.ExecuteSql(sql);
                                if (strResoult == "0")
                                {
                                    re = true;
                                }
                            }
                        }
                        else
                        {
                            //不一致 
                            //添加一条新数据
                            sql = string.Format(InsertParkingHistorySQL, WPSD_ID, STATE, RSSI, Voltage);
                            strResoult = DBConnect.ExecuteSql(sql);
                            if (strResoult == "0")
                            {
                                re = true;
                            }
                        }
                    }
                    else
                    {
                        sql = string.Format(InsertParkingHistorySQL, WPSD_ID, STATE, RSSI, Voltage);
                        strResoult = DBConnect.ExecuteSql(sql);
                        if (strResoult == "0")
                        {
                            re = true;
                        }
                    }
                }
            }
            return re;
        }

        public bool UpdateParkingOccupy(string parkingID)
        {
            string sql = string.Empty;
            ReturnValue resoult = new ReturnValue();
            sql = string.Format(GetRepeatParkingOccupySQL, parkingID);
            resoult = DBConnect.Select(sql);
            bool re = false;
            //查询最新两条数据
            if (resoult.NoError)
            {
                DataTable tb = resoult.ResultDataSet.Tables[0];
                string Vacant0=string.Empty;
                string Occupy0 = string.Empty;

                string Vacant1 = string.Empty;
                string Occupy1 = string.Empty;

                string Vacant2 = string.Empty;
                string Occupy2 = string.Empty;
                if (resoult.Count >0)
                {
                    Vacant0 = tb.Rows[0]["Vacant"].ToString();
                    Occupy0 = tb.Rows[0]["Occupy"].ToString();
                }
                if (resoult.Count > 1)
                {
                    Vacant1 = tb.Rows[1]["Vacant"].ToString();
                    Occupy1 = tb.Rows[1]["Occupy"].ToString();
                }
                if (resoult.Count >= 3)
                {
                    Vacant2 = tb.Rows[2]["Vacant"].ToString();
                    Occupy2 = tb.Rows[2]["Occupy"].ToString();
                    //最后一条是否与当前数据一致
                    if (Vacant0 == Vacant1 && Occupy0 == Occupy1)
                    {
                        //一致 
                        //是否与第一第二条数据一致
                        if (Vacant1 == Vacant2 && Occupy1 == Occupy2)
                        {
                            //一致 
                            //更新第二条（时间、Times）
                            string ID = tb.Rows[1]["ID"].ToString();
                            sql = string.Format(UpdateParkingOccupySQL, ID );
                            string strResoult = DBConnect.ExecuteSql(sql);
                            if (strResoult == "0")
                            {
                                re = true;
                            }
                        }
                        else
                        {
                            //不一致 
                            //添加一条相同数据
                            sql = string.Format(UpdateParkingHistorySQL, parkingID, Vacant0, Occupy0);
                            string strResoult = DBConnect.ExecuteSql(sql);
                            if (strResoult == "0")
                            {
                                re = true;
                            }
                        }
                    }
                    else
                    {
                        //不一致 
                        //添加一条新数据
                        sql = string.Format(InsertRepeatParkingOccupySQL, parkingID, Vacant0, Occupy0);
                        string strResoult = DBConnect.ExecuteSql(sql);
                        if (strResoult == "0")
                        {
                            re = true;
                        }
                    }
                }
                else
                {
                    sql = string.Format(InsertRepeatParkingOccupySQL, parkingID, Vacant0, Occupy0);
                    string strResoult = DBConnect.ExecuteSql(sql);
                    if (strResoult == "0")
                    {
                        re = true;
                    }
                }
            }
            return re;
        }

        public bool AddorUpdateParking(string ParkingLot_ID, string WPSD_ID, string parkingName, int height, int width, int X, int Y)
        {
            string sql = string.Empty;
            ReturnValue resoult = new ReturnValue();
            sql = string.Format(GetParkingSQL, WPSD_ID);
            resoult = DBConnect.Select(sql);

            if (resoult.NoError)
            {
                if (resoult.Count > 0)
                {
                    sql = string.Format(UpdateParkingSQL, parkingName, height, width, X, Y, WPSD_ID);
                    string strResoult = DBConnect.ExecuteSql(sql);
                    if (strResoult == "0")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    sql = string.Format(AddParkingSQL, ParkingLot_ID, parkingName, WPSD_ID, height, width, X, Y);
                    string strResoult = DBConnect.ExecuteSql(sql);
                    if (strResoult == "0")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public bool UpdateParking(string WPSD_ID, bool STATE)
        {
            if (STATE)//
            {
                return UpdateParkingState(WPSD_ID, "1");
            }
            else
            {
                return UpdateParkingState(WPSD_ID, "2");
            }
        }

        public bool UpdateParking(string WPSD_ID,string RSSI,string Voltage, bool STATE)
        {
            if (STATE)//
            {
                return UpdateParkingState(WPSD_ID, RSSI,Voltage, "1");
            }
            else
            {
                return UpdateParkingState(WPSD_ID, RSSI, Voltage, "2");
            }
        }

        public ReturnValue GetParkingState(string PARKINGLOT_ID)
        {
            return DBConnect.Select(string.Format(getParkingStateSQL, PARKINGLOT_ID));
        }

        public ReturnValue GetParkingOccupy(string PARKINGLOT_ID)
        {
            string where = "";
            if (PARKINGLOT_ID.Length > 0)
            {
                where = " and PARKINGLOT_ID='" + PARKINGLOT_ID + "'";
            }
            return DBConnect.Select(GetParkingOccupySQL.Replace("@@@",where));
        }
    }
}