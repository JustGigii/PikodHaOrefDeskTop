using Microsoft.Win32;
using Newtonsoft.Json;
using PikodAorfLayout.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using IWshRuntimeLibrary;
using Forms = System.Windows.Forms;
using PikodAorfLayout.data;
using System.Net.Sockets;
using System.Threading;

namespace PikodAorfLayout
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {   
        public static string JsonPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "PikodHaoref", "config.json");
        public static TcpClient tcpClient;
        public static bool isAppOpen;
        private readonly Forms.NotifyIcon _notifyIcon;
        private Thread runreconnect;
        public App()
        {
            _notifyIcon = new Forms.NotifyIcon();
            runreconnect = new Thread(TryToReconect);
            
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            connection();
            
            bool isfirst = false;
            isAppOpen = true;
            runreconnect.Start();
            //AddTostartUp();
            InitializeNotifyIcon();
            if (!System.IO.File.Exists(JsonPath))
            {         
            if (!Directory.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "PikodHaoref")))
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "PikodHaoref"));
            string newtext = "{\"ChoiseAlarm\":\"allcheak\",\"choise\":{},\"Size\": \"big\"}";
                System.IO.File.WriteAllText(JsonPath, newtext);
                isfirst=true;
            }
            loadstartupjson();
            if (isfirst)
            {
                startupadder();
                Setting settingwin = new Setting();
                settingwin.Show();
            }
            base.OnStartup(e);
        }

        async void TryToReconect()
        {
            while(isAppOpen) 
            {
                if (tcpClient == null || !tcpClient.Connected ) connection();
                else if (!tcpClient.Connected) tcpClient = null;
                await Task.Delay(75);
            }
        }
        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
        private static void AddTostartUp()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string str = Forms.Application.ExecutablePath;
            key.SetValue("PikodHaoref", str);
          
        }
        private void InitializeNotifyIcon()
        {
            using (MemoryStream iconStream = new MemoryStream(System.IO.File.ReadAllBytes("icon.ico")))
            {
                _notifyIcon.Icon = new System.Drawing.Icon(iconStream);
            }
            _notifyIcon.Visible = true;
            _notifyIcon.Text= "התראות פיקוד העורף";
            _notifyIcon.DoubleClick += _notifyIcon_Click;
            _notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Setting",Image.FromFile("image/Settings.png"), _notifyIcon_Click);
            _notifyIcon.ContextMenuStrip.Items.Add("exit", Image.FromFile("image/exit.png"), _notifyIcon_Exit);
        }
        private void loadstartupjson()
        {
            string jsonData = System.IO.File.ReadAllText(App.JsonPath);
            Config.config = JsonConvert.DeserializeObject<Config>(jsonData);
            if(Config.config.ChoiseAlarm == "DistrictsCheck") 
            {
                jsonData = System.IO.File.ReadAllText("data/areas_data.json");
                var districts = JsonConvert.DeserializeObject<List<Districts>>(jsonData);
                Choise.choiselist = new Choise();
                foreach (var item in Config.config.choise.Items)
                {
                    var distcirct = districts.Find(e => e.name == item);

                    foreach (var city in distcirct.cities)
                    {
                        Choise.choiselist.AddChoise(city);
                    }
                }
            }
            else
            {
            Choise.choiselist = Config.config.choise;
            }
            

        }
        private void startupadder()
        {
            WshShell wshShell = new WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut;
            string startUpFolderPath =
              Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Create the shortcut
            shortcut =
              (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(
                startUpFolderPath + "\\" +
                Forms.Application.ProductName + ".lnk");

            shortcut.TargetPath = Forms.Application.ExecutablePath;
            shortcut.WorkingDirectory = Forms.Application.StartupPath;
            shortcut.Description = "pikoad haoref Appliction";
            shortcut.IconLocation = Forms.Application.StartupPath + @"\icon.ico";
            shortcut.Save();
        }

        private void _notifyIcon_Exit(object? sender, EventArgs e)
        {
          Application.Current.Shutdown();
        }
        private void _notifyIcon_Click(object? sender, EventArgs e)
        {
            Setting settingwin = new Setting();
            settingwin.Show();
        }
        private void connection()
        {
            string serverIp = "129.159.128.159";
            //string serverIp = "127.0.0.1";
            short serverPort = 8080;
            try
            {
                tcpClient = new TcpClient(serverIp, serverPort);
            }
            catch { tcpClient = null; }

        }
    }
}
