using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBO;
using Common;
using Common.Entity;
using System.Data;
using MySql.Data.MySqlClient;

namespace Business
{
    public class Parking
    {
        #region SQL

        private static string updateParkingStateSQL = @"Update ParkingState Set ChangeTime=if(STATE ='{0}' and not isnull(ChangeTime), ChangeTime ,now()), STATE='{0}',UPDATETIME=now() Where WPSD_ID = '{1}';";

        private static string updateParkingStateWithRSSISQL = @"Update ParkingState Set ChangeTime=if(STATE ='{0}' and not isnull(ChangeTime), ChangeTime ,now()), STATE='{0}',UPDATETIME=now(),RSSI={1},Battery={2} Where WPSD_ID = '{3}';";

        private static string InsertParkingHistorySQL = @"INSERT INTO ParkingHistory(WPSD_ID,STATE,UPDATETIME)VALUE('{0}','{1}', now());";

        private static string InsertParkingHistoryWithRssiSQL = @"INSERT INTO ParkingHistory(WPSD_ID,STATE,UPDATETIME,RSSI,Battery)VALUE('{0}','{1}', now(),{2},{3});";

        private static string UpdateParkingHistorySQL = @"Update ParkingHistory Set UPDATETIME=now(),Times=Times+1 Where ID = {0}";

        //private static string GetParkingHistorySQL = @"Select * From ParkingHistory Where WPSD_ID = '{0}' Order By UPDATETIME Desc Limt 2";

        private static string GetParkingHistorySQL = @"Select * From ParkingHistory 
Where WPSD_ID = '{0}' 
Order By UPDATETIME Desc LIMIT 2;";

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

        private static string GetParkingOccupySQL = @"select * from (Select * from ParkingOccupy where 1=1 @@@ order by UpdateTime desc LIMIT 720) A order by UpdateTime";

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

        private static string InsertAutoinoutInfoSQL = @"";

        #endregion

        #region Method

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
                            //是否最后两条数据一致
                            if (State1 == State2)
                            {
                                //一致 
                                //更新最后一条数据（时间、Times）
                                string ID = tb.Rows[0]["ID"].ToString();
                                sql = string.Format(UpdateParkingHistorySQL, ID);
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
                                sql = string.Format(InsertParkingHistorySQL, WPSD_ID, STATE);
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
                            sql = string.Format(InsertParkingHistorySQL, WPSD_ID, STATE);
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
                    sql = string.Format(InsertParkingHistorySQL, WPSD_ID, STATE);
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
                                sql = string.Format(UpdateParkingHistorySQL, ID);
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
                                sql = string.Format(InsertParkingHistoryWithRssiSQL, WPSD_ID, STATE, RSSI, Voltage);
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
                            sql = string.Format(InsertParkingHistoryWithRssiSQL, WPSD_ID, STATE, RSSI, Voltage);
                            strResoult = DBConnect.ExecuteSql(sql);
                            if (strResoult == "0")
                            {
                                re = true;
                            }
                        }
                    }
                    else
                    {
                        sql = string.Format(InsertParkingHistoryWithRssiSQL, WPSD_ID, STATE, RSSI, Voltage);
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
        public bool AddParkingInfo(ParkingInfoEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Insert into parkinginfo(");
            strSql.Append("ParkingID,ParkingName,Lat,Lon,Position,ServerAddress,Manager,PhoneNum,Email,TotleNum,FreeNum,FeeScale,UpdateDataTime,CreateDataTime)");
            strSql.Append(" values (");
            strSql.Append("@ParkingID,@ParkingName,@Lat,@Lon,@Position,@ServerAddress,@Manager,@PhoneNum,@Email,@TotleNum,@FreeNum,@FeeScale,now(),now())");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ParkingID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@ParkingName", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Lat", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Lon", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Position", MySqlDbType.VarChar,200),
                    new MySqlParameter("@ServerAddress", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Manager", MySqlDbType.VarChar,50),
                    new MySqlParameter("@PhoneNum", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Email", MySqlDbType.VarChar,50),
                    new MySqlParameter("@TotleNum", MySqlDbType.Int32,11),
                    new MySqlParameter("@FreeNum", MySqlDbType.Int32,11),
                    new MySqlParameter("@FeeScale", MySqlDbType.VarChar,100)};
            parameters[0].Value = model.ParkingID;
            parameters[1].Value = model.ParkingName;
            parameters[2].Value = model.Lat;
            parameters[3].Value = model.Lon;
            parameters[4].Value = model.Position;
            parameters[5].Value = model.ServerAddress;
            parameters[6].Value = model.Manager;
            parameters[7].Value = model.PhoneNum;
            parameters[8].Value = model.Email;
            parameters[9].Value = model.TotleNum;
            parameters[10].Value = model.FreeNum;
            parameters[11].Value = model.FeeScale;

            string strResoult = DBConnect.ExecuteSql(strSql.ToString(), parameters);
            if (strResoult == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AutoIn(AutoinoutInfoEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into autoinoutinfo(");
            strSql.Append("ParkingID,DoorIn,DoorOut,CarNo,InTime,OutTime,State,TimeLong,TotalCost,PreCost,FinalCost,Model,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@ParkingID,@DoorIn,null,@CarNo,now(),null,'0',null,null,null,null,null,now())");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ParkingID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DoorIn", MySqlDbType.VarChar,50),
                    new MySqlParameter("@CarNo", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.ParkingID;
            parameters[1].Value = model.DoorIn;
            parameters[2].Value = model.CarNo;

            string strResoult = DBConnect.ExecuteSql(strSql.ToString(), parameters);
            if (strResoult == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AutoOut(AutoinoutInfoEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            string ID = string.Empty;
            strSql.Append("select ID from autoinoutinfo ");
            strSql.Append(" Where ParkingID=@ParkingID and  CarNo=@CarNo and State='0' LIMIT 1");
            MySqlParameter[] parameters1 = {
                    new MySqlParameter("@ParkingID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@CarNo", MySqlDbType.VarChar,50) };
            parameters1[0].Value = model.ParkingID;
            parameters1[1].Value = model.CarNo;
            ReturnValue resoult = DBConnect.Select(strSql.ToString(), parameters1);
            if (!resoult.NoError)
            {
                return false;
            }
            else
            {
                if (resoult.Count > 0)
                {
                    //修改
                    ID = resoult.ResultDataSet.Tables[0].Rows[0]["ID"].ToString();
                    strSql.Clear();
                    strSql.Append("update autoinoutinfo set ");
                    strSql.Append("DoorOut=@DoorOut,");
                    strSql.Append("OutTime=Now(),");
                    strSql.Append("State='1',");
                    strSql.Append("TimeLong=TIMESTAMPDIFF(MINUTE,InTime,Now()),");
                    strSql.Append("TotalCost=@TotalCost,");
                    strSql.Append("PreCost=@PreCost,");
                    strSql.Append("FinalCost=@TotalCost-@PreCost,");
                    strSql.Append("Model=@Model,");
                    strSql.Append("UpdateTime=Now()");
                    strSql.Append(" Where ID=@ID");
                    MySqlParameter[] parameters2 = {
                    new MySqlParameter("@ID", MySqlDbType.Int32),
                    new MySqlParameter("@DoorOut", MySqlDbType.VarChar,50),
                    new MySqlParameter("@TotalCost", MySqlDbType.Float),
                    new MySqlParameter("@PreCost", MySqlDbType.Float),
                    new MySqlParameter("@Model", MySqlDbType.VarChar,50)};
                    parameters2[0].Value = ID;
                    parameters2[1].Value = model.DoorOut;
                    parameters2[2].Value = model.TotalCost;
                    parameters2[3].Value = model.PreCost;
                    parameters2[4].Value = model.Model;
                    string strResoult = DBConnect.ExecuteSql(strSql.ToString(), parameters2);
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
                    //添加
                    strSql.Clear();
                    strSql.Append("insert into autoinoutinfo(");
                    strSql.Append("ParkingID,DoorIn,DoorOut,CarNo,InTime,OutTime,State,TimeLong,TotalCost,PreCost,FinalCost,Model,UpdateTime)");
                    strSql.Append(" values (");
                    strSql.Append("@ParkingID,null,@DoorOut,@CarNo,null,now(),'2',null,null,null,null,null,now())");
                    MySqlParameter[] parameters3 = {
                    new MySqlParameter("@ParkingID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DoorOut", MySqlDbType.VarChar,50),
                    new MySqlParameter("@CarNo", MySqlDbType.VarChar,50)};
                    parameters3[0].Value = model.ParkingID;
                    parameters3[1].Value = model.DoorIn;
                    parameters3[2].Value = model.CarNo;

                    string strResoult = DBConnect.ExecuteSql(strSql.ToString(), parameters3);
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
        }

        #endregion
    }
}
