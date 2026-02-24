using Cysharp.Threading.Tasks;
using Shin_Shinzui.Scripts.Application.Interfaces;

namespace Shin_Shinzui.Scripts.Application.UseCases
{
    public class ExitGameUseCase
    {
        private readonly IGameExitService _gameExitService;
        private readonly IConfirmationService _confirmationService;

        public ExitGameUseCase(
            IGameExitService gameExitService,
            IConfirmationService confirmationService
            )
        {
            _gameExitService = gameExitService;
            _confirmationService = confirmationService;
        }

        /// <summary>
        /// 確認後にゲームを終了する
        /// </summary>
        public async UniTask ExecuteAsync()
        {
            bool isConfirmed = await _confirmationService.ConfirmAsync("ゲームを終了しますか？");
            if (isConfirmed)
            {
                _gameExitService.ExitGame();
            }
        }
    }
}