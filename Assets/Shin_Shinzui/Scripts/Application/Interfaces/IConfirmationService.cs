using Cysharp.Threading.Tasks;

namespace Shin_Shinzui.Scripts.Application.Interfaces
{
    public interface IConfirmationService
    {
        UniTask<bool> ConfirmAsync(string message);
    }
}
