namespace Nike.DecoraterPatterm.CommandPatterm
    public interface IProductCommand
    {
        void Execute();
        void Undo();
    }
}