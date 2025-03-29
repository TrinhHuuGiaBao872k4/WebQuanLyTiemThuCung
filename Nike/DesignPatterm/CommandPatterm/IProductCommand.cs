namespace Nike.Commands
{
    public interface IProductCommand
    {
        void Execute();
        void Undo();
    }
}