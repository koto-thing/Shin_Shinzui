using Shin_Shinzui.Scripts.Application.Interfaces;
using Shin_Shinzui.Scripts.Application.UseCases;
using Shin_Shinzui.Scripts.Infrastructure.ExternalServices;
using Shin_Shinzui.Scripts.Presentation;
using Shin_Shinzui.Scripts.Presentation.Services;
using Shin_Shinzui.Scripts.View;
using VContainer;
using VContainer.Unity;

namespace Shin_Shinzui.Scripts.DI
{
    public class TitleLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Infrastructure
            builder.Register<IConfirmationWindowFactory, ConfirmationWindowFactory>(Lifetime.Scoped);
            
            // Presentation Services
            builder.Register<IConfirmationService, ConfirmationService>(Lifetime.Scoped);

            // UseCases
            builder.Register<ExitGameUseCase>(Lifetime.Scoped);
            builder.Register<SceneTransitionUseCase>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<TitleSceneButtonPresenter>();
            builder.RegisterComponentInHierarchy<TitleSceneMainPanelView>();
        }
    }
}