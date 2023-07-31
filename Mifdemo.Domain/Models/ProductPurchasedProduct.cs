namespace Mifdemo.Domain.Models
{
    public class ProductPurchasedProduct
    {
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int PurchasedProductId { get; set; }
        public PurchasedProductModel PurchasedProduct { get; set; }
    }
}