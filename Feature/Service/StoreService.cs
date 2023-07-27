using SalesTaxes.Problem.Feature.Interfaces;
using SalesTaxes.Problem.Feature.Models;
namespace SalesTaxes.Problem.Feature.Service
{
    public class StoreService : IStoreService
    {
        private readonly IProblemSolvingService _problemSolvingService;
        public StoreService(IProblemSolvingService problemSolvingService)
        {
            this._problemSolvingService = problemSolvingService;
        }
        public List<Product> Products = new List<Product>();
        public List<string> ProductsExempts = new List<string>();
        public List<string> ProductsImported = new List<string>();
        private decimal taxPercent = 0.10m;
        private decimal importPercent = 0.05m;
        
        public int GetTotalProductsInBasket() => Products.Count;
        public int GetTotalProductsExempts() => ProductsExempts.Count;
        public int GetTotalProductsImported() => ProductsImported.Count;

        public bool ReadMedical()
        {
            Console.WriteLine("Please type a medical, books, food keyword");
            ProductsExempts.Add((Console.ReadLine() ?? "").ToUpper());
            return true;
        }

        public bool ReadImported()
        {
            Console.WriteLine("Please type a import keyword");
            ProductsImported.Add((Console.ReadLine() ?? "").ToUpper());
            return true;
        }

        public bool ReadBasket()
        {
            Console.WriteLine("Please type an item for your basket");
            string? basketItem = Console.ReadLine();
            Products.Add(_problemSolvingService.getProductFromString(basketItem));
            return true;
        }

        public bool WriteResult()
        {
            var productsGroup = GroupProducts();
            productsGroup.ForEach((item) => {
                string quantity = item.Quantity > 1 ? $"{item.Total} ({item.Quantity} @ {item.Price + (item.Tax / item.Quantity)})" : $"{item.Total}";
                Console.WriteLine($"{item.Description} : {quantity}");
            });
            WriteTotalAndTax();
            Console.ReadLine();
            return true;
        }

        private void CalculateTax()
        {
            Products.ForEach((item) => {
                decimal finalTaxPercent = item.isInTheList(ProductsImported) && item.isInTheList(ProductsExempts) ? importPercent :
                item.isInTheList(ProductsImported) ? taxPercent + importPercent
                : item.isInTheList(ProductsExempts) ? 0 : taxPercent;

                item.Tax = (decimal)((item.Price * item.Quantity) * finalTaxPercent);
                item.Tax = Round(item.Tax);
                item.Total = (item.Price * item.Quantity) + item.Tax;
            });
        }
        private List<Product> GroupProducts()
        {
            CalculateTax(); ;
            return Products.GroupBy(x => new { x.Price, x.Description }).Select(x => new Product
            {
                Tax = x.Sum(c => c.Tax),
                Quantity = x.Sum(c => c.Quantity),
                Total = x.Sum(c => c.Total),
                Price = x.Key.Price,
                Description = x.Key.Description,

            }).ToList();
        }

        private void WriteTotalAndTax()
        {
            decimal Tax = Products.Sum(c => c.Tax);
            decimal Total = Products.Sum(c => c.Total);
            Console.WriteLine($"Sales Tax: {Tax}");
            Console.WriteLine($"Total: {Total}");
        }

        private decimal Round(decimal value)
        {
            var ceiling = Math.Ceiling(value * 20);
            if (ceiling == 0) return 0;
            return ceiling / 20;
        }
    }
}
