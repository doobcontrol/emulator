using com.xiyuansoft.emulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static com.xiyuansoft.emulator.DataReceiver;

namespace sampleDataMainApp
{
    public partial class Form1 : Form
    {
        DataReceiver dataReceiver = new DataReceiver();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataReceiver.dicDataMsghandler += dataReceiver_dicDataMsg;
            dataReceiver.startReceiver();
        }

        private void dataReceiver_dataMsg(object sender, DataMsgEventArgs e)
        {
            textBox1.AppendText(e.DataMsg + "\r\n");
        }

        private void dataReceiver_dicDataMsg(object sender, DicDataMsgEventArgs e)
        {
            Dictionary<string, string> msgDic = e.DicDataMsg;

            string msg = null;
            string tempStr = "";
            foreach (string key in msgDic.Keys)
            {
                tempStr = key + "：" + msgDic[key] + "\r\n";
                if (msg != null)
                {
                    msg += ",";
                }
                msg += tempStr;
            }

            textBox1.AppendText(msg);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataReceiver.stop();
        }
    }
}
