using Nike.Models;
using System;

namespace Nike.DesignPattern.CommandPattern
{
    public class UpdateProductCommand : IProductCommand
    {
        private readonly Product _originalProduct;
        private readonly Product _newProduct;
        private readonly Action<Product> _updateAction;

        public UpdateProductCommand(Product originalProduct, Product newProduct, Action<Product> updateAction)
        {
            _originalProduct = originalProduct;
            _newProduct = newProduct;
            _updateAction = updateAction;
        }

        public void Execute()
        {
            // Copy data từ newProduct sang originalProduct
            _originalProduct.ProductName = _newProduct.ProductName;
            _originalProduct.CatalogId = _newProduct.CatalogId;
            _originalProduct.PriceOld = _newProduct.PriceOld;
            _originalProduct.ProductSale = _newProduct.ProductSale;
            _originalProduct.UnitPrice = CalculateUnitPrice(_newProduct);
            _originalProduct.NgayNhapHang = DateTime.Now;
            
            _updateAction(_originalProduct);
        }

        public void Undo()
        {
            // Có thể implement undo nếu cần
        }

        private double? CalculateUnitPrice(Product product)
        {
            return product.ProductSale != null 
                ? product.PriceOld - (product.PriceOld * int.Parse(product.ProductSale)) / 100 
                : product.PriceOld;
        }
    }
}