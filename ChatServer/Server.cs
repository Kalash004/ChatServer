using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Server
    {
        private TcpListener listener;
        private bool isRunning;
        private List<Thread> threads = new List<Thread>();
        private List<Circuit> runtimeCircuits = new List<Circuit>();

        public Server(int port)
        {
            listener = new TcpListener(System.Net.IPAddress.Any, port);
            listener.Start();
            isRunning = true;
            ServerLoop();
        }

        private void ServerLoop()
        {
            Console.WriteLine("Server Started");
            while (isRunning)
            {
                Console.WriteLine("I am listening for connections on " + IPAddress.Parse(((IPEndPoint)listener.LocalEndpoint).Address.ToString()) + " on port number " + ((IPEndPoint)listener.LocalEndpoint).Port.ToString());
                TcpClient client = listener.AcceptTcpClient();
                Circuit circuit = new Circuit(new Client(), client);
                runtimeCircuits.Add(circuit);
                Thread t = new Thread(() => { circuit.Run(); });
                t.Start();
                threads.Add(t);
            }
        }
    }
}
