using Nike.Models;
using System;

namespace Nike.Commands
{
    public class CreateProductCommand : ICommand
    {
        private readonly Product _product;
        private readonly Action<Product> _saveAction;
        private Product _createdProduct;

        public CreateProductCommand(Product product, Action<Product> saveAction)
        {
            _product = product;
            _saveAction = saveAction;
        }

        public void Execute()
        {
            // Generate product code
            var prCode = new Random();
            _product.ProductCode = $"PR{prCode.Next(5000, 7000)}";
            _product.NgayNhapHang = DateTime.Now;

            _saveAction(_product);
            _createdProduct = _product;
        }

        public void Undo()
        {
            if (_createdProduct != null)
            {
                // Logic để undo tạo sản phẩm (nếu cần)
            }
        }
    }
}