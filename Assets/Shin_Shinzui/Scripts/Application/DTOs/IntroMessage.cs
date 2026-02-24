using System;

namespace Shin_Shinzui.Scripts.Application.DTOs
{
    [Serializable]
    public class IntroMessageList
    {
        public IntroMessage[] Messages;
    }
    
    [Serializable]
    public class IntroMessage
    {
        public string Message;
        public float DisplayDuration;
    }
}