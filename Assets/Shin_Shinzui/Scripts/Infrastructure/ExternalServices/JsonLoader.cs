using System;
using Cysharp.Threading.Tasks;
using Shin_Shinzui.Scripts.Application.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Shin_Shinzui.Scripts.Infrastructure.ExternalServices
{
    public class JsonLoader : IJsonLoader
    {
        /// <summary>
        /// Jsonフォーマットの文字列をAddressableからロードして、指定された型にデシリアライズする
        /// </summary>
        /// <param name="addressableJsonFileKey">JsonファイルをAddressableから呼び出すためのアドレス</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async UniTask<T> LoadJsonAsync<T>(string addressableJsonFileKey)
        {
            if (string.IsNullOrEmpty(addressableJsonFileKey))
            {
                Debug.LogError("addressableJsonFileKey is null or empty");
                return default;
            }
            
            var handle = Addressables.LoadAssetAsync<TextAsset>(addressableJsonFileKey);
            TextAsset jsonFile = await handle.Task.AsUniTask();

            if (jsonFile == null)
            {
                Debug.LogError($"Failed to load TextAsset from Addressables: {addressableJsonFileKey}");
                return default;
            }

            string json = jsonFile.text;
            
            if (json.TrimStart().StartsWith("["))
            {
                json = "{\"Items\":" + json + "}";
                return JsonUtility.FromJson<Wrapper<T>>(json).Items;
            }
            
            return JsonUtility.FromJson<T>(json);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T Items;
        }
    }
}