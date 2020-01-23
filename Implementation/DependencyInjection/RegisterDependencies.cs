using Implementation.Components;
using Implementation.Repositories;
using Implementation.Workflows;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Implementation.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class RegisterDependencies
    {
        public static void ConfigureServiceImplementationDependenciesInjection(this IServiceCollection services)
        {
            //-- Workflows
            services.AddTransient<ILoginWorkflow, LoginWorkflow>();
            services.AddTransient<IValidateTokenWorkflow, ValidateTokenWorkflow>();

            //-- Repositories
            services.AddTransient<IUserRepository, UserRepository>();

            //-- Components
            services.AddTransient<ITokenManagerComponent, TokenManagerComponent>();
        }
    }
}
