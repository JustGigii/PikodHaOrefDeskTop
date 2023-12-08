using Newtonsoft.Json;

namespace PikodHaorefServer.ClassModel
{

    internal class Alert : IComparable
    {
        public string alertDate { get; set; }
        public string title { get; set; }
        public string data { get; set; }
        public int category { get; set; }

        [JsonConstructor]
        public Alert(string alertDate, string title, string data, int category)
        {
            this.alertDate = alertDate;
            this.title = title;
            this.data = data;
            this.category = category;
        }
        public int CompareTo(object? obj)
        {
            Alert other = obj as Alert;
            return data.CompareTo(other.data);
        }
    }
}