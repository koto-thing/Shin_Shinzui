using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Shin_Shinzui.Scripts.View
{
    public class TitleSceneMainPanelView : MonoBehaviour
    {
        [Header("MainPanelCanvasGroup")]
        [SerializeField] public CanvasGroup CanvasGroup_Static;
        [SerializeField] public CanvasGroup CanvasGroup_Dynamic;
        
        [Header("MainPanelButton")]
        [SerializeField] public Button GameStartButton;
        [SerializeField] public Button OptionButton;
        [SerializeField] public Button CreditButton;
        [SerializeField] public Button ExitButton;
        
        public Observable<Unit> OnGameStartButtonClicked => GameStartButton.OnClickAsObservable();
        public Observable<Unit> OnOptionButtonClicked => OptionButton.OnClickAsObservable();
        public Observable<Unit> OnCreditButtonClicked => CreditButton.OnClickAsObservable();
        public Observable<Unit> OnExitButtonClicked => ExitButton.OnClickAsObservable();

        /// <summary>
        /// フェードインアニメーション
        /// </summary>
        public void AnimateIn()
        {
            const float duration = 0.8f;
            const float slideDistance = 50f;
            const float staggerDelay = 0.08f;
            var ease = Ease.OutCubic;

            // ロゴ部分のフェードイン
            CanvasGroup_Static.alpha = 0;
            CanvasGroup_Static.DOFade(1.0f, duration).SetEase(ease);
            CanvasGroup_Static.transform.DOLocalMoveX(0, duration)
                .From(-slideDistance, isRelative: true)
                .SetEase(ease);

            // 各ボタンの順次フェードイン
            Button[] buttons = { GameStartButton, OptionButton, CreditButton, ExitButton };
            
            // 親グループの透明度も一応管理
            CanvasGroup_Dynamic.alpha = 0;
            CanvasGroup_Dynamic.DOFade(1.0f, duration).SetEase(ease).SetDelay(0.1f);

            for (int i = 0; i < buttons.Length; i++)
            {
                var buttonTransform = buttons[i].transform;
                float delay = 0.15f + (i * staggerDelay);

                // 各ボタンを左からスライド
                buttonTransform.DOLocalMoveX(0, duration)
                    .From(-slideDistance, isRelative: true)
                    .SetEase(ease)
                    .SetDelay(delay);
            }
        }
    }
}