using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
namespace ALERT 
{
    public partial class Form1 : Form   
    {
        bool b = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ahtung();
            timer1.Interval = 60000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ahtung();
        }

        private async void ahtung ()
        {
            using (var httpClient = new HttpClient())
            {
                //https----------------------------------------
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var json = await httpClient.GetStringAsync("https://alarmmap.online/assets/json/_alarms/siren.json");
                //---------------------------------------------
                List<Item> items;
                items = JsonConvert.DeserializeObject<List<Item>>(json);
                int i;
                for (i = 0; i <= items.Count - 1; i++)
                {
                    var tmp = items[i];
                    string tmp_dist = tmp.district;
                    // string tmp_time = tmp.start;
                    //-------------------------------------------------------------
                    //if (tmp_dist == "Київська_область")
                    //if (tmp_dist == "Луганська_область")
                    if (tmp_dist == "м_Київ")
                  
                    {
                        pictureBox1.Image = Image.FromFile("ahtung.jpg");
                   
                        this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        //-----media
                        if (b == true)
                        {
                            WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
                            player.URL = "alert.mp3";
                            player.controls.play();
                            b = false;
                            break;
                        }
                        //----------
                        return;
                    }
                    else
                    {
                        pictureBox1.Image = Image.FromFile("noahtung.jpg");
                        b = true;
                        this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    }
                }
            }
        }
    }
    public class Item
    {
        public string district;
        public string start;
    }

}
