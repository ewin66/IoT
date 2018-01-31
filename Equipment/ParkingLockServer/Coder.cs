using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ParkingLockServer
{
    public class Coder
    {
        /// <summary> 
        /// 编码方式 
        /// </summary> 
        private EncodingMothord _encodingMothord;

        public EncodingMothord EncodingMothord1
        {
            get { return _encodingMothord; }
            set { _encodingMothord = value; }
        }
        public Coder()
        {

        }
        public Coder(EncodingMothord encodingMothord)
        {
            _encodingMothord = encodingMothord;
        }

        public enum EncodingMothord
        {
            Default = 0,
            Unicode,
            UTF8,
            ASCII,
            Obj,
        }

        /// <summary> 
        /// 通讯数据解码 
        /// </summary> 
        /// <param name="dataBytes">需要解码的数据</param> 
        /// <returns>编码后的数据</returns> 
        public virtual string GetEncodingString(byte[] dataBytes, int start, int size)
        {
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetString(dataBytes, start, size);
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetString(dataBytes, start, size);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetString(dataBytes, start, size);
                    }
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetString(dataBytes, start, size);
                    }
                default:
                    {
                        throw (new Exception("未定义的编码格式"));
                    }
            }
        }
        /// <summary>
        /// obj 对象，可以是字节流，可以是已经序列化的对象
        /// </summary>
        /// <param name="datBytes">字节流</param>
        /// <param name="start">开始位置</param>
        /// <param name="size">字节长度</param>
        /// <returns></returns>
        public object Deserilize(MemoryStream msStream)
        {
            object obj = null;
            if (msStream == null)
            {
                return null;
            }
            MemoryStream ms = msStream;
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                obj = formatter.Deserialize(ms);   //有时候会出现"缺少根元素"这样的错误
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }

            ms.Close();
            return obj;
        }



        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="Result">二进制数组</param>
        /// <param name="Result">是否保存成功</param>
        public bool SaveFile(FileStream stream, byte[] Result)
        {
            try
            {
                //保存文件
                FileStream fs = stream;
                fs.Write(Result, 0, Result.Length);
            }
            catch (Exception ee)
            {
                throw new ApplicationException(ee.Message);
            }
            return true;

        }

        /// <summary> 
        /// 数据编码 
        /// </summary> 
        /// <param name="datagram">需要编码的报文</param> 
        /// <returns>编码后的数据</returns> 
        public virtual byte[] GetTextBytes(string datagram)
        {
            byte[] rbyte = new byte[Encoding.UTF8.GetBytes(datagram).Length];
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        Encoding.Default.GetBytes(datagram, 0, datagram.Length, rbyte, 0);
                        return rbyte;
                    }
                case EncodingMothord.Unicode:
                    {
                        Encoding.Unicode.GetBytes(datagram, 0, datagram.Length, rbyte, 0);
                        return rbyte;
                    }
                case EncodingMothord.UTF8:
                    {
                        Encoding.UTF8.GetBytes(datagram, 0, datagram.Length, rbyte, 0);
                        return rbyte;
                    }
                case EncodingMothord.ASCII:
                    {
                        Encoding.ASCII.GetBytes(datagram, 0, datagram.Length, rbyte, 0);
                        return rbyte;
                    }
                default:
                    {
                        throw (new Exception("未定义的编码格式"));
                    }
            }

        }

        /// <summary>
        /// 获得文件的二进制流
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="stream">一次读取的长度</param>
        /// <returns>二进制数组成</returns>
        public virtual byte[] GetFileBytes(FileStream stream, int len)
        {
            FileStream fs = stream;
            byte[] byteBuffer = new byte[len];
            if (stream != null)
            {
                try
                {
                    fs.Read(byteBuffer, 0, len);
                }
                catch (Exception ee)
                {
                    throw new ApplicationException(ee.Message);
                }
            }
            return byteBuffer;
        }

        /// <summary>
        /// 序列化obj对象
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <returns>字节数组</returns>
        public virtual byte[] GetObjectBytes(object obj)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                byte[] data = ms.GetBuffer();
                byte[] objData = new byte[data.Length];
                data.CopyTo(objData, 0);
                return objData;
            }
            catch
            {
                throw (new Exception("obj对象序列化出错！"));
            }
            finally
            {
                ms.Close();
            }
        }
    }
}
