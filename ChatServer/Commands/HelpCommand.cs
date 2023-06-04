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
        private Dictionary<string, AbstractCommand> _help = new Dictionary<string, AbstractCommand>();

        public HelpCommand(Dictionary<string,AbstractCommand> commands)
        {

        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            foreach (KeyValuePair<string, AbstractCommand> pair in _help)
            {
                this.Result = this.Result + $"\n{pair.Key.ToString()} : {pair.Value.Description}";
            }
        }

        public HelpCommand getCommand()
        {
            return this;
        }

   
    }
}
