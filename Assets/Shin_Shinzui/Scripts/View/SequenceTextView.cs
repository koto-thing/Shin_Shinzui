using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Shin_Shinzui.Scripts.View
{
    public class SequenceTextView : MonoBehaviour
    {
        [SerializeField] public CanvasGroup UICanvasGroup;
        
        [SerializeField] public TextMeshProUGUI MessageText;

        private Sequence _sequence;
        
        /// <summary>
        /// メッセージをフェードで表示する
        /// </summary>
        public void ShowMessage(string message, float displayDuration = 2.0f, Action onComplete = null)
        {
            if (_sequence != null) 
                _sequence.Kill();
            
            UICanvasGroup.alpha = 0.0f;
            MessageText.text = message;
            
            _sequence = DOTween.Sequence();
            
            _sequence
                .Append(UICanvasGroup.DOFade(1.0f, 0.5f))
                .AppendInterval(displayDuration)
                .Append(UICanvasGroup.DOFade(0.0f, 0.5f))
                .OnComplete(() => onComplete?.Invoke());
        }

        /// <summary>
        /// 現在の表示を中断して即座に完了させる
        /// </summary>
        public void SkipCurrentMessage()
        {
            if (_sequence != null && _sequence.IsActive())
            {
                _sequence.Complete(); // DOTweenの完了（OnCompleteが呼ばれる）
            }
        }
    }
}