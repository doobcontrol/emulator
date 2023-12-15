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

namespace sampleDataProvider
{
    public partial class Form1 : Form
    {
        DataProvider dataProvider = new DataProvider();
        public Form1()
        {
            InitializeComponent();

            textBox1.Text = "35.6C";
            textBox2.Text = "1344mm";
            textBox3.Text = "测试文件";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> msgDic = new Dictionary<string, string>();
            msgDic.Add("温度", textBox1.Text);
            msgDic.Add("距离", textBox2.Text);
            msgDic.Add("文件", textBox3.Text);
            dataProvider.send(msgDic);
        }
    }
}
