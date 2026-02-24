using Shin_Shinzui.Scripts.Application.Interfaces;
using Shin_Shinzui.Scripts.Application.UseCases;
using Shin_Shinzui.Scripts.Infrastructure.ExternalServices;
using VContainer;
using VContainer.Unity;

namespace Shin_Shinzui.Scripts.DI
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            builder.Register<GameExitService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}
