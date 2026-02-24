using Cysharp.Threading.Tasks;

namespace Shin_Shinzui.Scripts.Application.Interfaces
{
    public interface IJsonLoader
    {
        UniTask<T> LoadJsonAsync<T>(string addressableJsonFileKey);
    }
}