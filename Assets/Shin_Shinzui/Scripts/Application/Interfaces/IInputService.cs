using R3;

namespace Shin_Shinzui.Scripts.Application.Interfaces
{
    public interface IInputService
    {
        // 決定・進む操作
        Observable<Unit> OnSubmit { get; }
        
        // キャンセル・戻る操作
        Observable<Unit> OnCancel { get; }
    }
}