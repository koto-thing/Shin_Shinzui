using System;
using Cysharp.Threading.Tasks;
using R3;
using Shin_Shinzui.Scripts.Application.Interfaces;
using Shin_Shinzui.Scripts.Application.UseCases;
using Shin_Shinzui.Scripts.View;
using VContainer.Unity;

namespace Shin_Shinzui.Scripts.Presentation
{
    public class TitleSceneButtonPresenter : IInitializable, IDisposable
    {
        private readonly SceneTransitionUseCase _sceneTransitionUseCase;
        private readonly ExitGameUseCase _exitGameUseCase;
        private readonly TitleSceneMainPanelView _titleSceneMainPanelView;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public TitleSceneButtonPresenter(
            SceneTransitionUseCase sceneTransitionUseCase,
            ExitGameUseCase exitGameUseCase,
            TitleSceneMainPanelView titleSceneMainPanelView
            )
        {
            _sceneTransitionUseCase = sceneTransitionUseCase;
            _exitGameUseCase = exitGameUseCase;
            _titleSceneMainPanelView = titleSceneMainPanelView;
        }

        public void Initialize()
        {
            _titleSceneMainPanelView.AnimateIn();
            
            _titleSceneMainPanelView.OnGameStartButtonClicked
                .Subscribe(_ => _sceneTransitionUseCase.TransitionToIntroductionScene())
                .AddTo(_disposables);
            
            _titleSceneMainPanelView.OnExitButtonClicked
                .Subscribe(_ => _exitGameUseCase.ExecuteAsync().Forget())
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}