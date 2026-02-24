using Cysharp.Threading.Tasks;
using Shin_Shinzui.Scripts.Application.Interfaces;
using Shin_Shinzui.Scripts.View;
using UnityEngine;

namespace Shin_Shinzui.Scripts.Presentation.Services
{
    public class ConfirmationService : IConfirmationService
    {
        private readonly IConfirmationWindowFactory _factory;

        public ConfirmationService(IConfirmationWindowFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// ユーザーに確認ダイアログを表示し、ユーザーの選択を待つ
        /// </summary>
        /// <param name="message">表示する文字列</param>
        /// <returns></returns>
        public async UniTask<bool> ConfirmAsync(string message)
        {
            var go = await _factory.CreateAsync();

            // Canvasを見つけて親に設定する
            var canvas = Object.FindAnyObjectByType<Canvas>();
            if (canvas != null)
            {
                go.transform.SetParent(canvas.transform, false);
            }

            // 中央に配置するための座標リセット
            var rectTransform = go.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = Vector2.zero;
            }

            var view = go.GetComponent<ConfirmationWindowView>();

            if (view == null)
            {
                Debug.LogError("ConfirmationWindowView component not found on the instantiated object.");
                return false;
            }

            var tcs = new UniTaskCompletionSource<bool>();

            view.Initialize(
                message: message,
                onProceed: () => tcs.TrySetResult(true),
                onCancel: () => tcs.TrySetResult(false)
            );

            return await tcs.Task;
        }
    }
}
