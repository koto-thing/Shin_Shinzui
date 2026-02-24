using Shin_Shinzui.Scripts.Application.Interfaces;

namespace Shin_Shinzui.Scripts.Application.UseCases
{
    public class SceneTransitionUseCase
    {
        private readonly ISceneLoader _sceneLoader;
        
        public SceneTransitionUseCase(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void TransitionToTitleScene()
        {
            _sceneLoader.LoadSceneAsync("TitleScene");
        }

        public void TransitionToIntroductionScene()
        {
            _sceneLoader.LoadSceneAsync("IntroScene");
        }

        public void TransitionToTutorialScene()
        {
            _sceneLoader.LoadSceneAsync("TutorialScene");
        }
    }
}