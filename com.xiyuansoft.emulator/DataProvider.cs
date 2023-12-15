using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace com.xiyuansoft.emulator
{
    public class DataProvider
    {
        UdpClient UdpClient = new UdpClient(
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8123)
            );
        IPEndPoint targetEndPoint
            = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8124);

        public DataProvider()
        {
            UdpClient.Connect(targetEndPoint);
        }

        public void send(Dictionary<string, string> msgDic)
        {
            if(msgDic==null || msgDic.Count == 0)
            {
                return;
            }

            string msg = null;
            string tempStr = "";
            foreach(string key in msgDic.Keys)
            {
                tempStr = key + "=" + msgDic[key];
                if (msg != null)
                {
                    msg += ",";
                }
                msg += tempStr;
            }
            send(msg);
        }
        public void send(string msg)
        {
            // send data
            byte[] sendBytes = Encoding.UTF8.GetBytes(msg);
            UdpClient.Send(sendBytes, sendBytes.Length);
        }
    }
}
