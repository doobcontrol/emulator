using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace com.xiyuansoft.emulator
{
    public class DataReceiver
    {
        IPEndPoint targetEndPoint
            = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8123);
        UdpClient UdpClient = new UdpClient(
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8124)
            );

        public EventHandler<DataMsgEventArgs> dataMsghandler;
        public EventHandler<DicDataMsgEventArgs> dicDataMsghandler;

        bool stopReceive = false;
        public void startReceiver()
        {
            _ = Task.Run(new Action(
                async () =>
                {
                    while (!stopReceive)
                    {
                        await Task.Run(new Action(
                            () =>
                            { while (UdpClient.Available == 0) ; }
                            ));

                        byte[] receivedBytes = UdpClient.Receive(ref targetEndPoint);
                        string receivedString =
                            Encoding.UTF8.GetString(receivedBytes);

                        if (dataMsghandler != null)
                        {
                            dataMsghandler(this, new DataMsgEventArgs(receivedString));
                        }
                        if (dicDataMsghandler != null)
                        {
                            dicDataMsghandler(this, new DicDataMsgEventArgs(receivedString));
                        }
                    }
                }
                ));
        }
    
        public void stop()
        {
            stopReceive = true;
        }

        public class DataMsgEventArgs : EventArgs
        {
            private readonly string dataMsg;

            public DataMsgEventArgs(string dataMsg)
            {
                this.dataMsg = dataMsg;
            }

            public string DataMsg
            {
                get { return dataMsg; }
            }
        }

        public class DicDataMsgEventArgs : EventArgs
        {
            private readonly Dictionary<string, string> dicDataMsg;

            public DicDataMsgEventArgs(string dataMsg)
            {
                dicDataMsg = new Dictionary<string, string>();

                string[] dicItemsArr = dataMsg.Split(',');
                foreach(string dicItem in dicItemsArr)
                {
                    string[] itemArr = dicItem.Split('=');
                    dicDataMsg.Add(itemArr[0], itemArr[1]);
                }
            }

            public Dictionary<string, string> DicDataMsg
            {
                get { return dicDataMsg; }
            }
        }
    }
}
