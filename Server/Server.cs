using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PikodHaorefServer
{
    class Server
    {
        private TcpListener tcpListener;
        private List<TcpClient> clients = new List<TcpClient>();

        public Server(string ipAddress, int port)
        {
            tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
        }

        public void Start()
        {
            tcpListener.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                clients.Add(client);
                Console.WriteLine("new client: "+ remoteIpEndPoint.Address+":"+ remoteIpEndPoint.Port);
                // Handle each client in a separate thread
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(client);
            }
        }

        private void HandleClient(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                    break;

                // Convert the bytes to a string
                string clientMessage = Encoding.UTF8.GetString(message, 0, bytesRead);
                Console.WriteLine($"Received: {clientMessage}");

                // Broadcast the message to all clients
                Broadcast(clientMessage, tcpClient);
            }

            // Remove the client from the list when disconnected
            clients.Remove(tcpClient);
            tcpClient.Close();
        }

        public void Broadcast(string message, TcpClient excludeClient = null)
        {
            foreach (TcpClient client in clients)
            {
                if (client != excludeClient)
                {
                    NetworkStream clientStream = client.GetStream();
                    byte[] broadcastMessage = Encoding.UTF8.GetBytes(message);
                    clientStream.Write(broadcastMessage, 0, broadcastMessage.Length);
                    clientStream.Flush();
                }
            }
        }

        public void Stop()
        {
            tcpListener.Stop();
        }
    }

}
