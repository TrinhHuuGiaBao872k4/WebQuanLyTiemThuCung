namespace Nike.DesignPattern.CommandPattern
{
    public interface IProductCommand
    {
        void Execute();
        void Undo();
    }
}