using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mifdemo.Domain.Interface.RepositoryInterface;
using Mifdemo.Infrastructure.Data;
using Mifdemo.Infrastructure.Repository;

namespace Mifdemo.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var con = configuration.GetConnectionString("default");
            services.AddDbContext<Context>(options => {
                options.UseMySql(con, ServerVersion.AutoDetect(con));
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFamilyRepository, FamilyRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IPurchasedProductRepository, PurchasedProductRepository>();
            services.AddScoped<IIdentifierRepository, IdentifierRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<ITokenManagerRepository, TokenManagerRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();

            return services;
            
        }
    }
}