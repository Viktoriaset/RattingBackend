using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ratting.Aplication.Battle;
using Ratting.Application.Battle;
using Ratting.Application.Common.Behaviors;
using Ratting.Application.MatchMaking;

namespace Ratting.Aplication
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<MatchMakingConfiguration>();
            services.AddTransient<BattleRoomsController>();
            services.AddTransient<BattleCreateService>();
            services.AddTransient<MatchMakingService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
