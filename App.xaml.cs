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
using Forms = System.Windows.Forms;

namespace PikodAorfLayout
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {   
        public static string JsonPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "PikodHaoref", "config.json");
       
        private readonly Forms.NotifyIcon _notifyIcon;
        public App()
        {
            _notifyIcon = new Forms.NotifyIcon();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            AddTostartUp();
            InitializeNotifyIcon();
            if (!File.Exists(JsonPath))
            {         
            if (!Directory.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "PikodHaoref")))
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "PikodHaoref"));
            string newtext = "{\"ChoiseAlarm\":\"allcheak\",\"choise\":{}}";
            File.WriteAllText(JsonPath, newtext);
            }
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
        private static void AddTostartUp()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string str = Assembly.GetExecutingAssembly().Location;
            key.SetValue("Camaleone", str);
        }
        private void InitializeNotifyIcon()
        {
            _notifyIcon.Visible = true;
            _notifyIcon.Icon = new System.Drawing.Icon("icon.ico");
            _notifyIcon.Text= "התראות פיקוד העורף";
            _notifyIcon.DoubleClick += _notifyIcon_Click;
            _notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Setting",Image.FromFile("image/Settings.png"), _notifyIcon_Click);
            _notifyIcon.ContextMenuStrip.Items.Add("exit", Image.FromFile("image/exit.png"), _notifyIcon_Exit);
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
    }
}
