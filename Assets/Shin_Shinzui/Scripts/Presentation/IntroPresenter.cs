using System;
using Cysharp.Threading.Tasks;
using R3;
using Shin_Shinzui.Scripts.Application.Interfaces;
using Shin_Shinzui.Scripts.Application.UseCases;
using Shin_Shinzui.Scripts.View;
using VContainer.Unity;

namespace Shin_Shinzui.Scripts.Presentation
{
    public class IntroPresenter : IInitializable, IDisposable
    {
        private readonly SceneTransitionUseCase _sceneTransitionUseCase;
        private readonly LoadIntroMessageUseCase _loadIntroMessageUseCase;
        private readonly IInputService _inputService;
        private readonly SequenceTextView _sequenceTextView;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private bool _isCompleted = false;

        public IntroPresenter(
            SceneTransitionUseCase sceneTransitionUseCase,
            LoadIntroMessageUseCase loadIntroMessageUseCase,
            IInputService inputService,
            SequenceTextView sequenceTextView
        )
        {
            _sceneTransitionUseCase = sceneTransitionUseCase;
            _loadIntroMessageUseCase = loadIntroMessageUseCase;
            _inputService = inputService;
            _sequenceTextView = sequenceTextView;
        }

        public async void Initialize()
        {
            // 左クリックで現在のメッセージをスキップ（ViewのTweenを即座に完了させる）
            _inputService.OnSubmit
                .Subscribe(_ => _sequenceTextView.SkipCurrentMessage())
                .AddTo(_disposables);

            // Escでイントロ全体をスキップ（シーン遷移へ）
            _inputService.OnCancel
                .Subscribe(_ => CompleteIntro())
                .AddTo(_disposables);

            // JSONの読み込み
            await _loadIntroMessageUseCase.LoadIntroJsonAsync();
            
            // 最初のメッセージを表示開始
            ShowNextMessage();
        }

        private void ShowNextMessage()
        {
            if (_isCompleted) 
                return;

            // キューにメッセージが残っているとき
            if (_loadIntroMessageUseCase.IntroMessages.Count > 0)
            {
                // 順にメッセージを表示
                var messageData = _loadIntroMessageUseCase.IntroMessages.Dequeue();
                _sequenceTextView.ShowMessage(
                    messageData.Message, 
                    messageData.DisplayDuration, 
                    onComplete: ShowNextMessage // 完了したら次を呼ぶ
                );
            }
            // すべてのメッセージを表示し終えたとき
            else
            {
                CompleteIntro();
            }
        }

        private void CompleteIntro()
        {
            if (_isCompleted) 
                return;
            _isCompleted = true;

            _sceneTransitionUseCase.TransitionToTutorialScene();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
