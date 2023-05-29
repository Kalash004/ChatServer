using System;
using System.Collections.Generic;
using System.Linq;
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
            listener = new TcpListener(System.Net.IPAddress.Any,port);
            listener.Start();
            isRunning = true;
            ServerLoop();
        }

        private void ServerLoop()
        {
            Console.WriteLine("Server Started");
            while (isRunning)
            {
                TcpClient client = listener.AcceptTcpClient();
                Circuit circuit = new Circuit(client);
                runtimeCircuits.Add(circuit);
                Thread t = new Thread();
                t.Start();
                threads.Add(t);
            }
        }
    }
}
