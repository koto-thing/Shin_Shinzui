using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shin_Shinzui.Scripts.View
{
    public class ConfirmationWindowView : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI MessageText;
        [SerializeField] public Button ProceedButton;
        [SerializeField] public Button CancelButton;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        /// <summary>
        /// Prefabインスタンス化後に呼ぶ初期化メソッド
        /// </summary>
        /// <param name="message">表示するメッセージ</param>
        /// <param name="onProceed">「はい」押下時のコールバック</param>
        /// <param name="onCancel">「いいえ」押下時のコールバック</param>
        public void Initialize(string message, Action onProceed, Action onCancel = null)
        {
            MessageText.text = message;

            ProceedButton.OnClickAsObservable()
                .Take(1)
                .Subscribe(_ =>
                {
                    onProceed?.Invoke();
                    Close();
                })
                .AddTo(_disposables);

            CancelButton.OnClickAsObservable()
                .Take(1)
                .Subscribe(_ =>
                {
                    onCancel?.Invoke();
                    Close();
                })
                .AddTo(_disposables);
        }

        private void Close()
        {
            _disposables.Dispose();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}