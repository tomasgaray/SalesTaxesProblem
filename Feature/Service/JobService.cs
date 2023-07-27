using SalesTaxes.Problem.Feature.Enums;
using SalesTaxes.Problem.Feature.Interfaces;


namespace SalesTaxes.Problem.Feature.Service
{
    public class JobService : IJobservice
    {
        private readonly IStoreService _storeService;
        private readonly IMenuService _menuService;
        public JobService(IStoreService storeService, IMenuService menuService)
        {
            _storeService = storeService;
            _menuService = menuService;
        }
        bool isOpen = true;
        public void Run()
        {
            while (isOpen)
            {
                try
                {
                    _menuService.ShowMenu();
                    MenuOptions option = _menuService.ValidateOption(Console.ReadLine());
                    Console.Clear();

                    if (option != MenuOptions.Finish)
                    {
                        var funcs = new Func<bool>[] { _storeService.ReadMedical, _storeService.ReadImported, _storeService.ReadBasket, _storeService.WriteResult };
                        funcs[((int)option) - 1]();
                        Console.Clear();
                    }
                    else isOpen = false;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.Write($"{ex.Message}\n\n");
                }
            }
        }
    }
}
