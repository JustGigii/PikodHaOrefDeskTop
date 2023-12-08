using Microsoft.VisualBasic;
using PikodAorfLayout.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.IO;

namespace PikodAorfLayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DateTime startTime;
        float massgesize;
        short massgefont;
        public MainWindow()
        {
            Name = "MainWindow";
            InitializeComponent();

            Topmost = true;
            Visibility = Visibility.Hidden;
            ChanceSize(Config.config.Size);

            // System.Threading.Thread thread = (tcpClient is object) ? new System.Threading.Thread(GetFromServer): new System.Threading.Thread(CheakDirecJson); 
            System.Threading.Thread thread = new System.Threading.Thread(lodingConnection);
            thread.Start();
            //  var a = Choise.choiselist;

        }
        private void ChanceSize(string size)
        {
            float scale =1;
            switch (size)
            {
                case "big":
                    scale = 1;
                    break;
                case "mid":
                    scale = 0.75f;
                    break;
                case "small":
                    scale = 0.60f;
                    break;
            }

            image.Height = 121* scale;
            Width = 500* scale;
            massgesize =(55 * scale);
            massgefont = (short)(36 * scale);
            Left = System.Windows.SystemParameters.WorkArea.Width - Width;
            Height = System.Windows.SystemParameters.WorkArea.Height;
        }
        private async void lodingConnection()
        {
            while (App.isAppOpen)
            {
                if (App.tcpClient is Object && App.tcpClient.Connected) await GetFromServer();
                else await CheakDirecJson();
            }
        }

        public async void LoadExmple()
        {
            if (Visibility == Visibility.Visible) Visibility= Visibility.Hidden;

            ChanceSize(Config.config.Size);
            List<Alert> exmplelist = new List<Alert>();
            for (int i = 0; i < 20; i++)
            {
                Alert exmple = new Alert() { data = "דוגמא" };
                exmplelist.Add(exmple);
            }
             await popdetails(exmplelist);
            Visibility = Visibility.Visible;
            await Task.Delay(3000).ContinueWith(async _ =>
            {
                await this. Dispatcher.Invoke(async () =>
                {
                    Visibility = Visibility.Hidden;
                    await popdetails(exmplelist);
                });

            }
            );
        }


        //######################################## show with server connection####################################### 
        private async Task popdetails(List<Alert> showlist)
        {
            ObservableCollection<massge> show = new ObservableCollection<massge>();
            if(showlist != null)
            foreach (var item in showlist) show.Add(new massge(item.data, massgesize, massgefont));
            await this.Dispatcher.Invoke(async () =>
            {
                massege.DataContext = show;
            });
        }
        private async Task GetFromServer()
        {
            try
            {

                string response = RecvieAlert();
                if (response == string.Empty) 
                {
                    await hideScreen();
                    await popdetails(null);
                }
                List<Alert> alertlist = (response != string.Empty) ? JsonSerializer.Deserialize<List<Alert>>(response) : null;  
                alertlist = Filter(alertlist);
                if (alertlist.Count > 0)
                {
                    await this.Dispatcher.Invoke(async () =>
                    {
                        Visibility = Visibility.Visible;
                    });
                    await popdetails(alertlist);
                }
                else
                {
                    await hideScreen();
                    popdetails(null);

                }
                await Task.Delay(20).ConfigureAwait(false);
            }
            catch (System.Text.Json.JsonException ex)
            {

            }
            catch (Exception ex)
            {
                await hideScreen();
            }

        }

        private async Task hideScreen()
        {
            if(Visibility != Visibility.Hidden)
            await this.Dispatcher.Invoke(async () =>
            {
                Visibility = Visibility.Hidden;
            });
        }

        private string RecvieAlert()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead;


                try
                {
                    // Set a timeout of, for example, 5000 milliseconds (5 seconds)
                    App.tcpClient.GetStream().ReadTimeout = 4000;

                    do
                    {
                        bytesRead = App.tcpClient.GetStream().Read(buffer, 0, buffer.Length);
                        memoryStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead == buffer.Length);
                }
                catch (IOException ex)
                {
                    // Handle the timeout exception here
                    return string.Empty;
                }

                string response = Encoding.UTF8.GetString(memoryStream.ToArray());
                return response;
            }

        }
        private List<Alert> Filter(List<Alert> data)
        {
            List<Alert> releventData = new List<Alert>();
            if (data == null) return releventData;
            foreach (var alert in data)
            {
                if (releventData.Any(e => e.data == alert.data)) continue;

                if (Choise.choiselist.Items.Count > 0)
                {

                    if (Choise.choiselist.Items.Contains(alert.data))
                        releventData.Add(alert);
                }
                else
                {
                    releventData.Add(alert);
                }

            }

            return releventData;
        }
        //######################################################### Conect direct to json ##################################################################

        private async Task CheakDirecJson()
        {
            bool isRun = true;
            startTime = DateTime.Now;
            await Popdata();

            if (App.tcpClient != null && App.tcpClient.Connected) { isRun = false; }
            if (DateTime.Now - startTime > TimeSpan.FromSeconds(4))
            {
                await Popdata();
            }
            else
            {
                await hideScreen();
                popdetails(null);
            }

            Thread.Sleep(TimeSpan.FromSeconds(1));

        }
        /// <summary>
        /// this function is the logic form get the json 
        /// in the way when few people send requst
        /// he mannger not to get ban
        /// </summary>
        /// <returns> show list for alert. </returns>
        public async Task Popdata()
        {
            List<Alert> data = await LoadJsonAsync();
            List<Alert> releventData = FilterWithTimer(data);
            if (releventData.Count() > 0)
            {
                int loop = 7;
                if (releventData.Count > 7)
                    loop = 15;
                for (int i = 0; i < loop; i++)
                {
                    if (i > 0) data = await LoadJsonAsync();
                    releventData = FilterWithTimer(data);
                    if (releventData.Count() == 0)
                    {
                        i = loop;
                        break;
                    }
                    releventData.Sort();
                    await this.Dispatcher.Invoke(async () =>
                    {
                        Visibility = Visibility.Visible;
                    });
                    popdetails(releventData);

                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }
            }
            else
            {
                await hideScreen();
                popdetails(null);
            }



        }

        /// <summary>
        /// This is old filter when I don't use server
        /// this filter cheak time 
        /// </summary>
        private List<Alert> FilterWithTimer(List<Alert> data)
        {
            List<Alert> releventData = new List<Alert>();
            if (data == null) return releventData;
            foreach (var alert in data)
            {
                // if (DateTime.Now - DateTime.Parse(alert.alertDate) < TimeSpan.FromHours(24))
                if (DateTime.Now - DateTime.Parse(alert.alertDate) < TimeSpan.FromMinutes(1))
                {
                    if (releventData.Any(e => e.data == alert.data)) continue;

                    if (Choise.choiselist.Items.Count > 0)
                    {

                        if (Choise.choiselist.Items.Contains(alert.data))
                            releventData.Add(alert);
                    }
                    else
                    {
                        releventData.Add(alert);
                    }
                }
            }

            return releventData;
        }

        // before ItemsControl

        //public TextBlock CreateTextBlock(string text)
        //{
        //    var newblock = new TextBlock();

        //    newblock.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/massage.png")));
        //    newblock.Text = "בדיקה";
        //    newblock.Foreground = Brushes.White;
        //    newblock.FontFamily = new FontFamily("Hobo Std");
        //    newblock.Width = 500;
        //    newblock.Text = text + "  ";
        //    newblock.Margin = new Thickness(0, 0, 0, 3);
        //    newblock.TextAlignment = TextAlignment.Right;
        //    newblock.FontSize = 36;
        //    return newblock;
        //}
        private async Task<List<Alert>> LoadJsonAsync()
        {
            HttpClient httpClient = new HttpClient();
            // URL of the JSON data
            string url = "https://www.oref.org.il/WarningMessages/History/AlertsHistory.json";
            try
            {
                // Fetching the data
                string response = await httpClient.GetStringAsync(url);

                // If your JSON represents an object, you can define a class and deserialize to it.
                // Here I'm using dynamic for simplicity, but in a real-world scenario, it's better to deserialize to a specific class.
                List<Alert> data = JsonSerializer.Deserialize<List<Alert>>(response);
                return data;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // MessageBox.Show("Error fetching data: " + ex.Message);
            }
            return null;
        }
    }
}
