using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GetLocationExe
{
    public partial class Form1 : Form
    {
        public string ip = "";
        public string URLorIP = "";
        public string CountryName = "";
        public string RegionName = "";
        public string CityName = "";

        public Form1()
        {
            InitializeComponent();
        }

        public static string GetExtenalIpAddress()
        {
            string IP = "";
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.Default;
                string str = client.DownloadString(GlobalConfig.GetExtenalIpURL);
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

        public static string URLToIP(string URL)
        {
            string URLIP = "";
            try
            {
                IPAddress[] addres = Dns.GetHostAddresses(URL);
                URLIP = addres[0].ToString();
            }
            catch
            {
                MessageBox.Show("IP/URL resolution failed!", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return URLIP;
        }

        private void showLocation(string ip)
        {
            string strURL = GlobalConfig.IPLocateURL + ip;   //IP Locate URL
            XmlDocument doc = new XmlDocument();
            try
            {
                //Get Xml info 
                doc.Load(strURL);
            }
            catch
            {
                MessageBox.Show("Illegal IP！", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Load strURL XML data
            XmlNodeList nodeLstCity = doc.GetElementsByTagName("City"); //Get the tag
            //Using SelectSingleNode match the first node 
            XmlNode root = doc.SelectSingleNode("Response");
            if (root != null)
            {
                CountryName = (root.SelectSingleNode("CountryName")).InnerText;
                RegionName = (root.SelectSingleNode("RegionName")).InnerText;
                CityName = (root.SelectSingleNode("City")).InnerText;
            }
            if (CountryName == "" && RegionName == "" && CityName == "")
            {
                MessageBox.Show("Locate your IP failed !", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            textBox2.Text = "Country:  " + CountryName + "\r\n\r\nProvince: " + RegionName + "\r\n\r\nCity:     " + CityName;
            SaveToFile();
        }

        private void SaveToFile()
        {
            FileStream fs = null;
            
            string URLorIPToSave = URLorIP == "" ? ip : URLorIP;
            string RegionToSave = RegionName == "" ? "unknow" : RegionName;
            string CityToSave = CityName == "" ? "unknow" : CityName;
            string recored = DateTime.Now.ToString() + "|" + URLorIPToSave + "|" + CountryName + "|" + RegionToSave + "|" + CityToSave + "\r\n";
            Encoding encoder = Encoding.UTF8;
            byte[] bytes = encoder.GetBytes(recored);
            try
            {
                fs = File.OpenWrite(GlobalConfig.filePath);
                //Append to the history file.
                fs.Position = fs.Length;
                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception)
            {
                MessageBox.Show("Open recored file failed!", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                fs.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains('.'))
            {
                MessageBox.Show("Please enter IP/URL correctly!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            URLorIP = textBox1.Text;
            ip = URLToIP(URLorIP);
            if (ip != "")
            {
                textBox1.Text = ip;
                showLocation(ip);
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
            ip = "";
            URLorIP = "";
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
            ip = "";
            URLorIP = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            ip = "";
            URLorIP = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            f2.ShowData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString(("yyyy-MM-dd HH:mm"));
        }
    }
}
