using Cysharp.Threading.Tasks;

namespace Shin_Shinzui.Scripts.Application.Interfaces
{
    public interface ISceneLoader
    {
        public UniTask LoadSceneAsync(string addressableKey);
    }
}