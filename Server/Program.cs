using System.Text.Json;

namespace PikodHaorefServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = (args.Length == 0)? new Server("127.0.0.1", 8888) : new Server(args[0], int.Parse(args[1]));
            JsonHandler handler = new JsonHandler(server);
            server.Start();
        }



      
    }
}