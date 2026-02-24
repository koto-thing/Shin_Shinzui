using Cysharp.Threading.Tasks;
using Shin_Shinzui.Scripts.Application.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Shin_Shinzui.Scripts.Infrastructure.ExternalServices
{
    public class ConfirmationWindowFactory : IConfirmationWindowFactory
    {
        private const string PREFAB_KEY = "ConfirmationPanel";

        /// <summary>
        /// 確認画面を表示する
        /// </summary>
        /// <returns>確認画面のオブジェクト</returns>
        public async UniTask<GameObject> CreateAsync()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(PREFAB_KEY);
            await handle.ToUniTask();
            return Object.Instantiate(handle.Result);
        }
    }
}
