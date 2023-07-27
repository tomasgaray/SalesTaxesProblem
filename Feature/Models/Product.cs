namespace SalesTaxes.Problem.Feature.Models
{
    public class Product
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public Product()
        {
            this.Quantity = 0;
            this.Price = 0;
            this.Tax = 0;
            this.Total = 0;
            this.Description = string.Empty;
        }

        public bool isInTheList(List<string> products)
        {
            var descriptionToList = Description.Split(" ").Select(x => x.ToUpper()).ToList();
            return descriptionToList.Exists(c => products.Any(x => x.Contains(c)));
        }
    }
}
