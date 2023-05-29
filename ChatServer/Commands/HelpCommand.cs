using ChatServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Commands
{
    public class HelpCommand : AbstractCommand
    {
        public HelpCommand(Dictionary<string,AbstractCommand> commands)
        {

        }
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public HelpCommand getCommand()
        {
            return this;
        }
    }
}
