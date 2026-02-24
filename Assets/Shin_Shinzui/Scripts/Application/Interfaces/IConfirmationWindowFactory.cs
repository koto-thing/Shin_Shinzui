using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Shin_Shinzui.Scripts.Application.Interfaces
{
    public interface IConfirmationWindowFactory
    {
        UniTask<GameObject> CreateAsync();
    }
}

