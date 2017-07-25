using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace GetLocationExe
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void ShowData()
        {
            ArrayList records = new ArrayList();
            StreamReader sr = new StreamReader(GlobalConfig.filePath, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                records.Add(line);
            }
            sr.Close();
            for (int i = 0; i < records.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                string[] record = records[i].ToString().Split('|');
                lvi.Text = record[0];
                lvi.SubItems.Add(record[1]);
                lvi.SubItems.Add(record[2]);
                lvi.SubItems.Add(record[3]);
                lvi.SubItems.Add(record[4]);
                listView1.Items.Add(lvi);
            }

        }
    }
}
