using SalesTaxes.Problem.Feature.Enums;
using SalesTaxes.Problem.Feature.Interfaces;

namespace SalesTaxes.Problem.Feature.Service
{
    public class MenuService: IMenuService
    {
        private const string option1 = "1) Register keywords for products exempt from basic sales tax example: pills";
        private const string option2 = "2) Register keywords for import (import tax) example: imported";
        private const string option3 = "3) Add product to your shopping basket";
        private const string option4 = "4) Show result";
        private const string option5 = "5) Finish";
        private const string menu = $"{option1}\n{option2}\n{option3}\n{option4}\n{option5}";
        public readonly List<string> options = new List<string>() { MenuOptions.Medical.ToString("D"), MenuOptions.Imported.ToString("D"), MenuOptions.Basket.ToString("D"), MenuOptions.ShowResult.ToString("D"), MenuOptions.Finish.ToString("D") };
        private readonly IStoreService _storeService;
        public MenuService(IStoreService storeService)
        {
            _storeService = storeService;
        }
      
        public void ShowMenu()
        {
            Console.WriteLine(menu);
            ShowSubMenu();
        }


        private void ShowSubMenu()
        {
            int totalProductsInBasket = _storeService.GetTotalProductsInBasket();
            int totalProductsExempts = _storeService.GetTotalProductsExempts();
            int totalProductsImported = _storeService.GetTotalProductsImported();
            Console.WriteLine("##############################################################################");
            Console.WriteLine($"Products in the basket\t\tkeywords for exempt\t\tkeywords for imports");
            Console.WriteLine($"{totalProductsInBasket}\t\t\t\t{totalProductsExempts}\t\t\t\t{totalProductsImported}");
            Console.WriteLine("##############################################################################");
        }


        public MenuOptions ValidateOption(string? option)
        {
            if (!options.Contains(option)) throw new Exception("Wrong option");
            return (MenuOptions)short.Parse(option);
        }
    }
}
