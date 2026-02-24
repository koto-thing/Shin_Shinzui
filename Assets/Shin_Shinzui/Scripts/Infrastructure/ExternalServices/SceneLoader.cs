using Cysharp.Threading.Tasks;
using Shin_Shinzui.Scripts.Application.Interfaces;
using UnityEngine.AddressableAssets;

namespace Shin_Shinzui.Scripts.Infrastructure.ExternalServices
{
    public class SceneLoader : ISceneLoader
    {
        private bool _isLoading;

        /// <summary>
        /// シーンをロードする
        /// </summary>
        /// <param name="addressableKey">ロードするシーンのAddressableのKey</param>
        public async UniTask LoadSceneAsync(string addressableKey)
        {
            if (_isLoading) 
                return;

            _isLoading = true;
            await Addressables.LoadSceneAsync(addressableKey).ToUniTask();
            _isLoading = false;
        }
    }
}