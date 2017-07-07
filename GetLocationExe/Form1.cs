using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GetLocationExe
{
    public partial class Form1 : Form
    {
        public string ip = "";
        public string CountryName = "";
        public string RegionName = "";
        public string City = "";

        public Form1()
        {
            InitializeComponent();
        }

        public static string GetExtenalIpAddress()
        {
            String url = "http://hijoyusers.joymeng.com:8100/test/getNameByOtherIp";
            string IP = "";
            try
            {
                //从网址中获取本机ip数据    
                System.Net.WebClient client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.Default;
                string str = client.DownloadString(url);
                client.Dispose();

                if (!str.Equals(""))
                    IP = str;
            }
            catch (Exception)
            {
                MessageBox.Show("Get this PC's IP failed !", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return IP;
        }

        private void showLocation(string ip)
        {
            string strURL = "http://www.freegeoip.net/xml/" + ip;   //网址URL
            //通过GetElementsByTagName获取标签结点集合
            XmlDocument doc = new XmlDocument();
            try
            {
                //Xml文档
                doc.Load(strURL);
            }
            catch
            {
                MessageBox.Show("Illegal IP！");
            }
            //加载strURL指定XML数据
            XmlNodeList nodeLstCity = doc.GetElementsByTagName("City"); //获取标签
            //通过SelectSingleNode匹配匹配第一个节点
            XmlNode root = doc.SelectSingleNode("Response");
            if (root != null)
            {
                CountryName = (root.SelectSingleNode("CountryName")).InnerText;
                RegionName = (root.SelectSingleNode("RegionName")).InnerText;
                City = (root.SelectSingleNode("City")).InnerText;
            }
            if (CountryName == "" && RegionName == "" && City == "")
            {
                MessageBox.Show("Locate your IP failed !", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            textBox2.Text = "Country:  " + CountryName + "\r\n\r\nProvince: " + RegionName + "\r\n\r\nCity:     " + City;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ip = textBox1.Text;
            IPAddress ipStr;
            if (IPAddress.TryParse(ip, out ipStr))
            {
                showLocation(ip);
            }
            else
            {
                MessageBox.Show("Illegal IP !","Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox2.Text = "";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ip = GetExtenalIpAddress();
            if (ip == "")
            {
                MessageBox.Show("Can't get this PC's IP !", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                textBox1.Text = ip;
                showLocation(ip);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString(("yyyy-MM-dd HH:mm"));
        }
    }
}
