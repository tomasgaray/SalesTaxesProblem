using SalesTaxes.Problem.Feature.Models;

namespace SalesTaxes.Problem.Feature.Interfaces
{
    public interface IProblemSolvingService
    {
        public Product getProductFromString(string? description);
        public string getItemName(string description);
    }
}
