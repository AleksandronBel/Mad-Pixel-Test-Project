using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScenesControl
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private string _gameSceneName = "GameScene";

        [SerializeField] private Image _backgroundFadeImage;

        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitGameButton;

        private void Start()
        {
            SubscribeAllButtons();
            StartFadeAnimation();
        }

        private void StartFadeAnimation()
        {
            _backgroundFadeImage.gameObject.SetActive(true);
            _backgroundFadeImage.color = new Color(0f, 0f, 0f, 1f); 
            _backgroundFadeImage.DOFade(0f, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _backgroundFadeImage.gameObject.SetActive(false); 
            });
        }

        private void SubscribeAllButtons()
        {
            _startGameButton.OnClickAsObservable().Subscribe(_ => StartGame()).AddTo(this);
            _exitGameButton.OnClickAsObservable().Subscribe(_ => ExitGame()).AddTo(this);
        }

        private void StartGame()
        {
            _backgroundFadeImage.gameObject.SetActive(true);
            _backgroundFadeImage.DOFade(1f, 1f).SetEase(Ease.Linear).OnComplete(async () =>
            {
                await SceneManager.LoadSceneAsync(_gameSceneName);
            });
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
