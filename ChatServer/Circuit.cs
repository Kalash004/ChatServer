using ChatServer.Commands;
using ChatServer.Interfaces;
using OneOf;
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
        private Dictionary<string, AbstractCommand> commands = new Dictionary<string, AbstractCommand>();

        public Circuit(Client client, TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            this.client = client;
            CreateCommands(client, tcpClient);
            reader = new StreamReader(tcpClient.GetStream(), Encoding.UTF8);
            writer = new StreamWriter(tcpClient.GetStream(), Encoding.UTF8);
        }

        private void CreateCommands(Client client, TcpClient tcpClient)
        {
            commands.Add("help", new HelpCommand(this.commands));
        }

        public IEnumerator<bool> Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                try
                {
                    ClientFunction();
                }
                catch (Exception e)
                {
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
            string? data = null;
            string? returnData = null;
            bool isConnected = true;
            SendMessage("You joined the server");
            while (isConnected)
            {
                data = reader.ReadLine();
                returnData = ReadAndExecuteCommand(data);
                SendMessage(returnData);
            }
        }

        private string? ReadAndExecuteCommand(string data)
        {
            if (data == null) return "";
            if (!commands.ContainsKey(data)) return $"Command {data} was not found, use command \"help\" for list of commands";
            OneOf<string,bool> result = ExecuteCommand(commands[data]);
            string? returnData = null;
            bool executed;
            if (result.TryPickT0(out returnData, out executed)) return returnData;
            if (executed) return $"Command {data} was executed successfuly";
            return $"Command {data} was NOT executed";
        }


        /// <summary>
        /// Executes command depending on the string from client
        /// </summary>
        /// <param name="command">command to execute</param>
        /// <returns>One of (string, bool) case: bool => returns if command was executed or not. case: string => text to return to the client </returns>
        private OneOf<string, bool> ExecuteCommand(AbstractCommand command)
        {
            string? retrunData = null;
            try
            {
                command.Execute(null);
                if (command.Result != null)
                {
                    retrunData = command.Result.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception occurred {e.Message}");
                return false;
            }
            if (retrunData != null) return retrunData;
            return true;
        }

        private void SendMessage(string message)
        {
            writer.WriteLine(message);
        }
    }
}
