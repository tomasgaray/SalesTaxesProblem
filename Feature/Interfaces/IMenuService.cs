using SalesTaxes.Problem.Feature.Enums;
namespace SalesTaxes.Problem.Feature.Interfaces
{
    public interface IMenuService
    {
        public void ShowMenu();
        public MenuOptions ValidateOption(string? option);
    }
}
