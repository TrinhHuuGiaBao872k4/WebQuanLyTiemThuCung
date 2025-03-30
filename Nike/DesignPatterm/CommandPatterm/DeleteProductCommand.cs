using Nike.Models;
using System;

namespace Nike.Commands
{
    public class DeleteProductCommand : ICommand
    {
        private readonly Product _product;
        private readonly Action<Product> _deleteAction;

        public DeleteProductCommand(Product product, Action<Product> deleteAction)
        {
            _product = product;
            _deleteAction = deleteAction;
        }

        public void Execute()
        {
            _deleteAction(_product);
        }

        public void Undo()
        {
            // Có thể implement undo nếu cần
        }
    }
}