using ChatServer.Commands;

namespace ChatServer.Interfaces
{
    public interface ICommand
    {
        void Execute(object? parameter);
    }
}