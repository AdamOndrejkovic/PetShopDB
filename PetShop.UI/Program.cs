using System;
using Microsoft.Extensions.DependencyInjection;
using PetShop.Core.IServices;
using PetShop.Domain.IRepositories;
using PetShop.Domain.Services;
using PetShop.SQL;

namespace PetShop.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IPrinter, Printer>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IPetTypeService, PetTypeService>();
            /*serviceCollection.AddScoped<IPetRepository, PetDb>();
            serviceCollection.AddScoped<IPetTypeRepository, PetDb>();*/

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var printer = serviceProvider.GetRequiredService<IPrinter>();
            printer.StartUi();

        }
    }
}