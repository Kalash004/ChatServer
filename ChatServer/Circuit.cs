using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Circuit
    {
        private Client client;
        private TcpClient tcpClient;
        private StreamReader reader;
        private StreamWriter writer;
        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        public Circuit(Client client, TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            this.client = client;
        }

        public IEnumerator<bool> Run()
        {
            reader = new StreamReader(tcpClient.GetStream(), Encoding.UTF8);
            writer = new StreamWriter(tcpClient.GetStream(), Encoding.UTF8);
            bool isRunning = true;
            while (isRunning) 
            {
                try
                {
                    ClientFunction();
                } catch (Exception e) {
                    Console.WriteLine(e.ToString());
                    SendMessage($"Error happened, your circuit was closed. ErrCode: {e.Message}");
                    isRunning = false;
                }
                yield return isRunning;
            }
            yield return isRunning;
        }

        private void ClientFunction()
        {
            string? dataWrite = null;
            string? dataRecieved = null;
            SendMessage("You joined the server");
        }

        private void SendMessage(string message)
        {
            writer.WriteLine(message);

        }
    }
}
