using Microsoft.Extensions.DependencyInjection;
using SalesTaxes.Problem.Feature.Interfaces;
using SalesTaxes.Problem.Feature.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IJobservice, JobService>()
            .AddTransient<IProblemSolvingService, ProblemSolvingService>()
            .AddSingleton<IStoreService, StoreService>()
            .AddSingleton<IMenuService, MenuService>()
            .BuildServiceProvider();

        var job = serviceProvider.GetService<IJobservice>();
        job.Run();

    }
}