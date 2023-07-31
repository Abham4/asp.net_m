using Microsoft.Extensions.DependencyInjection;
using Mifdemo.Application.Service;
using Mifdemo.Domain.Interface.ServiceInterface;

namespace Mifdemo.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IPurchasedProductService, PurchasedProductService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IIdentifierService, IdentifierService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<ITokenManagerService, TokenManagerService>();
            services.AddScoped<IBranchService, BranchService>();

            return services;
        }
    }
}