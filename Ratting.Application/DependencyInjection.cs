using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Ratting.Application.Battle;
using Ratting.Application.Common.Behaviors;
using Ratting.Application.MatchMaking;

namespace Ratting.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddSingleton<BattleCreateService>();
            services.AddSingleton<MatchMakingConfiguration>();
            services.AddSingleton<MatchMakingService>();
        }
    }
}
