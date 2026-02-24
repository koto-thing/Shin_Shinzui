using Shin_Shinzui.Scripts.Application.Interfaces;
using Shin_Shinzui.Scripts.Application.UseCases;
using Shin_Shinzui.Scripts.Infrastructure.ExternalServices;
using Shin_Shinzui.Scripts.Presentation;
using Shin_Shinzui.Scripts.View;
using VContainer;
using VContainer.Unity;

namespace Shin_Shinzui.Scripts.DI
{
    public class IntroLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Infrastructure
            builder.Register<IJsonLoader, JsonLoader>(Lifetime.Scoped);
            builder.Register<IInputService, IntroInputService>(Lifetime.Scoped);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Scoped);
            
            // UseCases
            builder.Register<LoadIntroMessageUseCase>(Lifetime.Scoped);
            builder.Register<SceneTransitionUseCase>(Lifetime.Scoped);
            
            // View
            builder.RegisterComponentInHierarchy<SequenceTextView>();
            
            // Presenter (Entry Point)
            builder.RegisterEntryPoint<IntroPresenter>();
        }
    }
}