namespace PikodHaorefServer
{
    internal class Alert : IComparable
    {
        public string alertDate { get; set; }
        public string title { get; set; }
        public string data { get; set; }
        public int category { get; set; }

        public int CompareTo(object? obj)
        {
            Alert other = obj as Alert;
            return this.data.CompareTo(other.data);
        }
    }
}