
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using PikodAorfLayout.Class;
using PikodAorfLayout.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PikodAorfLayout
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        private List<Districts> districts;
        private List<Districts> choosedistricts;
        private Choise choiselist;
        private Config config;
        private bool onload;
        public Setting()
        {
            onload = false;
            choiselist =   Choise.choiselist;
            choosedistricts = new List<Districts>();
            InitializeComponent();
            loadDistrictjson();
            loadstartupjson();


        }
        private void loadDistrictjson()
        {
            string jsonData = File.ReadAllText("data/areas_data.json");
            districts = JsonConvert.DeserializeObject<List<Districts>>(jsonData);
        }
        private void loadstartupjson()
        {
            string jsonData = File.ReadAllText(App.JsonPath);
            config = JsonConvert.DeserializeObject<Config>(jsonData);
            selectionradio(config.ChoiseAlarm, false);

        }

        private void selectionradio(string ChoiseAlarm, bool isfirst)
        {
            switch (ChoiseAlarm)
            {
                case "allcheak":
                    config.ChoiseAlarm = "allcheak";
                    allcheak.IsChecked = true;
                    DistrictsPanel.Visibility = Visibility.Hidden;
                    config.choise.Clear();
                    break;
                case "DistrictsCheck":
                    config.ChoiseAlarm = "DistrictsCheck";
                    DistrictsCheck.IsChecked = true;
                    choosedistricts.Clear();
                    if (isfirst) config.choise = new Choise();
                    else
                    {

                        foreach (var item in config.choise.Items)
                        {
                            var distcirct = districts.Find(e => e.name == item);
                            
                            foreach (var city in distcirct.cities)
                            {
                                choiselist.AddChoise(city);

                            }
                            choosedistricts.Add(distcirct);
                            districts.Remove(distcirct);
                        }
                    }
                    popDistricts();
                    DistrictsPanel.Visibility = Visibility.Visible;

                    break;
            }
            onload = true;
        }

        void popDistricts()
        {
            districtscontrol.DataContext =new ObservableCollection<Districts>(districts);
            districtscontrolchoose.DataContext = new ObservableCollection<Districts>(choosedistricts);
            choose.DataContext = new ObservableCollection<string>(choiselist.Items);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        private void Grid_MouseLeftButtonDownAdd(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid panel && panel.DataContext is Districts district)
            {
                foreach (var item in district.cities)
                {
                    choiselist.AddChoise(item);
                }
                config.ChoiseAlarm = "DistrictsCheck";
                choosedistricts.Add(district);
                districts.Remove(district);
                config.choise.AddChoise(district.name);
                popDistricts();
            }
        }
        private void Grid_MouseLeftButtonDownDelete(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid panel && panel.DataContext is Districts district)
            {
                foreach (var item in district.cities)
                {
                    choiselist.RemoveChoise(item);
                }
                choosedistricts.Remove(district);
                districts.Add(district);
                config.choise.RemoveChoise(district.name);
                popDistricts();
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Grid panel)
            {
                panel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f7bd59"));
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Grid panel)
            {//#f7bd59
                panel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffab45"));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(config);


            File.WriteAllText(App.JsonPath, json, Encoding.UTF8);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radio && onload)
            {
                selectionradio(radio.Name, true);

            }
        }
    }
}
