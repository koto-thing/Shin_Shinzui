using Cysharp.Threading.Tasks;
using ObservableCollections;
using Shin_Shinzui.Scripts.Application.DTOs;
using Shin_Shinzui.Scripts.Application.Interfaces;

namespace Shin_Shinzui.Scripts.Application.UseCases
{
    public class LoadIntroMessageUseCase
    {
        private readonly IJsonLoader _jsonLoader;
        
        private const string INTRO_MESSAGE_JSON_KEY = "IntroText";

        private ObservableQueue<IntroMessage> _introMessages = new ObservableQueue<IntroMessage>();
        public ObservableQueue<IntroMessage> IntroMessages => _introMessages;
        
        public LoadIntroMessageUseCase(IJsonLoader jsonLoader)
        {
            _jsonLoader = jsonLoader;
        }

        /// <summary>
        /// イントロ部分のメッセージテキストを読み込む
        /// </summary>
        public async UniTask LoadIntroJsonAsync()
        {
            var result = await _jsonLoader.LoadJsonAsync<IntroMessageList>(INTRO_MESSAGE_JSON_KEY);
            _introMessages.Clear();

            if (result is { Messages: not null })
            {
                foreach (var message in result.Messages)
                {
                    _introMessages.Enqueue(message);
                }
            }
        }
    }
}