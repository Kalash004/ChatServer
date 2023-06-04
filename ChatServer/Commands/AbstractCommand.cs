using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatServer.Interfaces;

namespace ChatServer.Commands
{
    public abstract class AbstractCommand:ICommand
    {
        private string? description;
        private string? result;
        public string? Description { get => description; set => description = value; }
        public string? Result { get => result; set => result = value; }

        public abstract bool CanExecute(object? parameter);
        public abstract void Execute(object? parameter);
    }
}
