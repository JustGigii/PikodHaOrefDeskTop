
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
        private Choise allcity;
        private Choise choiselist;
        private Config config;
        private bool onload;
        public Setting()
        {
            onload = false;
            choiselist = new Choise();
            choosedistricts = new List<Districts>();
            allcity = new Choise();
            InitializeComponent();
            clearbox.Visibility = Visibility.Collapsed;
            loadDistrictjson();
            loadautocomplte();
            config = new Config(Config.config);
            selectionradio(Config.config.ChoiseAlarm, false);
            var a = Choise.choiselist;
        }
        private void loadDistrictjson()
        {
            string jsonData = File.ReadAllText("data/areas_data.json");
            districts = JsonConvert.DeserializeObject<List<Districts>>(jsonData);
        }

        void loadautocomplte()
        {
            foreach (var district in districts)
            {
                foreach (var choise in district.cities)
                {
                    allcity.AddChoise(choise);
                }
            }
        }
        private void selectionradio(string ChoiseAlarm, bool isfirst)
        {
            switch (ChoiseAlarm)
            {
                case "allcheak":
                    config.ChoiseAlarm = "allcheak";
                    allcheak.IsChecked = true;
                    DistrictsPanel.Visibility = Visibility.Hidden;
                    autoTextBox.Visibility = Visibility.Hidden;
                    clearlist();
                    break;
                case "DistrictsCheck":
                    config.ChoiseAlarm = "DistrictsCheck";
                    DistrictsCheck.IsChecked = true;
                    autoTextBox.Visibility = Visibility.Hidden;
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

                    DistrictsPanel.Visibility = Visibility.Visible;
                    break;
                case "cityCheck":
                    config.ChoiseAlarm = "cityCheck";
                    cityCheck.IsChecked = true;
                    DistrictsPanel.Visibility = Visibility.Hidden;
                    autoTextBox.Visibility = Visibility.Visible;
                    if (config.choise is null) config.choise = new Choise();
                    else
                    {

                        foreach (var item in config.choise.Items)
                        {
                            choiselist.AddChoise(item);
                        }
                    }
                    break;
            }
            popDistricts();
            onload = true;
        }

        private void clearlist()
        {
            choiselist.Clear();
            choosedistricts.Clear();
            Config.config.choise.Clear();
            loadDistrictjson();
        }

        void popDistricts()
        {
            districtscontrol.DataContext = new ObservableCollection<Districts>(districts);
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
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f7bd59"));
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Grid panel)
            {//#f7bd59
                panel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffab45"));
            }
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffab45"));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Choise.choiselist = choiselist;
            Config.config = config;
            string json = JsonConvert.SerializeObject(config);
            MessageBox.Show("הבחירה שלך נשמרה", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);


            File.WriteAllText(App.JsonPath, json, Encoding.UTF8);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radio && onload)
            {
                selectionradio(radio.Name, true);
                clearbox.Visibility = (radio.Name.Equals("cityCheck")) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private void OpenAutoSuggestionBox()
        {
            try
            {
                // Enable.  
                this.autoListPopup.Visibility = Visibility.Visible;
                this.autoListPopup.IsOpen = true;
                this.autoList.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                // Info.  
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }



        #region Close Auto Suggestion box method  

        /// <summary>  
        ///  Close Auto Suggestion box method  
        /// </summary>  
        private void CloseAutoSuggestionBox()
        {
            try
            {
                // Enable.  
                this.autoListPopup.Visibility = Visibility.Collapsed;
                this.autoListPopup.IsOpen = false;
                this.autoList.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                // Info.  
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }

        #endregion

        #region Auto Text Box text changed the method  

        /// <summary>  
        ///  Auto Text Box text changed method.  
        /// </summary>  
        /// <param name="sender">Sender parameter</param>  
        /// <param name="e">Event parameter</param>  
        private void AutoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                // Verification.  
                if (string.IsNullOrEmpty(this.autoTextBox.Text))
                {
                    // Disable.  
                    this.CloseAutoSuggestionBox();

                    // Info.  
                    return;
                }
                if (this.autoList is null || this.autoTextBox.Text == "...הכנס שם ישוב") return;
                // Settings.  
                this.autoList.ItemsSource = this.allcity.Items.Where(p => p.Contains(this.autoTextBox.Text)).ToList();
                // Enable.  
                this.OpenAutoSuggestionBox();

            }
            catch (Exception ex)
            {
                // Info.  
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }

        #endregion

        #region Auto list selection changed method  

        /// <summary>  
        ///  Auto list selection changed method.  
        /// </summary>  
        /// <param name="sender">Sender parameter</param>  
        /// <param name="e">Event parameter</param>  
        private void AutoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Verification.  
                if (this.autoList.SelectedIndex <= -1)
                {
                    // Disable.  
                    this.CloseAutoSuggestionBox();

                    // Info.  
                    return;
                }

                // Disable.  
                this.CloseAutoSuggestionBox();

                // Settings.  
                this.autoTextBox.Text = "...הכנס שם ישוב";
                if (!choiselist.Items.Contains(this.autoList.SelectedItem.ToString()))
                    choiselist.AddChoise(this.autoList.SelectedItem.ToString());
                choose.DataContext = new ObservableCollection<string>(choiselist.Items);
                config.choise.AddChoise(this.autoList.SelectedItem.ToString());
                this.autoList.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                // Info.  
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(ex);
            }
        }

        #endregion

        private void autoTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox text)
            {
                if (text.Text == "...הכנס שם ישוב")
                {
                    text.Text = "";
                }
            }
        }

        private void autoTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox text)
            {
                text.Text = "...הכנס שם ישוב";
            }
        }

        private void Button_Click_clear(object sender, RoutedEventArgs e)
        {
            choiselist.Clear();
            Config.config.choise.Clear();
            autoTextBox.Text = "...הכנס שם ישוב";
            popDistricts();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }

}
