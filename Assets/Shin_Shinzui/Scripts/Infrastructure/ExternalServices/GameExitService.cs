using Shin_Shinzui.Scripts.Application.Interfaces;
using UnityEngine;

namespace Shin_Shinzui.Scripts.Infrastructure.ExternalServices
{
    public class GameExitService : IGameExitService
    {
        /// <summary>
        /// ゲーム終了処理
        /// </summary>
        public void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                UnityEngine.Application.Quit();
            #endif
        }
    }
}