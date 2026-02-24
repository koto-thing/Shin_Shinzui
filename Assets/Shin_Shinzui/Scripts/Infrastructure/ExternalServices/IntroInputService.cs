using System;
using R3;
using Shin_Shinzui.Scripts.Application.Interfaces;
using UnityEngine.InputSystem;

namespace Shin_Shinzui.Scripts.Infrastructure.ExternalServices
{
    public class IntroInputService : IInputService, IDisposable
    {
        private readonly InputSystem_Actions _inputSystemActions;
        
        private readonly Subject<Unit> _onSubmit = new Subject<Unit>();
        private readonly Subject<Unit> _onCancel = new Subject<Unit>();
        
        public Observable<Unit> OnSubmit => _onSubmit;
        public Observable<Unit> OnCancel => _onCancel;

        public IntroInputService()
        {
            _inputSystemActions = new InputSystem_Actions();
            
            // InputSystemのIntroActionマップを使用
            _inputSystemActions.IntroAction.Next.performed += OnNextPerformed;
            _inputSystemActions.IntroAction.Skip.performed += OnSkipPerformed;
            
            _inputSystemActions.Enable();
        }

        private void OnNextPerformed(InputAction.CallbackContext _) => _onSubmit.OnNext(Unit.Default);
        private void OnSkipPerformed(InputAction.CallbackContext _) => _onCancel.OnNext(Unit.Default);

        public void Dispose()
        {
            if (_inputSystemActions != null)
            {
                _inputSystemActions.IntroAction.Next.performed -= OnNextPerformed;
                _inputSystemActions.IntroAction.Skip.performed -= OnSkipPerformed;
                _inputSystemActions.Dispose();
            }
            
            _onSubmit.OnCompleted();
            _onCancel.OnCompleted();
            _onSubmit.Dispose();
            _onCancel.Dispose();
        }
    }
}