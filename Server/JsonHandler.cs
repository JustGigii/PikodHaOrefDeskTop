using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PikodHaorefServer
{
    internal class JsonHandler
    {
        DateTime _startTime;
        bool _isClose;
        Thread thread;
        Server _server;
        public JsonHandler(Server server)
        {
            _isClose = false;
            thread = new Thread(cheakjson);
            thread.Start();
            _server = server;

        }
        private async void cheakjson()
        {
            _startTime = DateTime.Now;
            while (!_isClose)
            {
                var alerts = Filter(await LoadJsonAsync());
                alerts.Sort();
                if (alerts != null && alerts.Count > 0)
                {
                    _server.Broadcast(JsonConvert.SerializeObject(alerts));
                }
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
        }
        private async Task<Alert[]> LoadJsonAsync()
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
                Alert[] data = System.Text.Json.JsonSerializer.Deserialize<Alert[]>(response);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("can't accses json: " + ex.Message);
                // Handle exceptions
                // MessageBox.Show("Error fetching data: " + ex.Message);
            }

        }
        private List<Alert> Filter(Alert[] data)
        {
            List<Alert> releventData = new List<Alert>();
            if (data == null) return releventData;
            foreach (var alert in data)
            {
                  if (DateTime.Now - DateTime.Parse(alert.alertDate) < TimeSpan.FromHours(24))
               // if (DateTime.Now - DateTime.Parse(alert.alertDate) < TimeSpan.FromMinutes(1))
                {
                    releventData.Add(alert);
                }
            }

            return releventData;
        }


        public void Close()
        {
            this._isClose = true;
            thread.Abort();
        }
    }
}
