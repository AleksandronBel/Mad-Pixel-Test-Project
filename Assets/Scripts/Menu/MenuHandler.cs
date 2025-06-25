using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Menu
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private string gameSceneName = "GameScene";

        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitGameButton;

        private void Start()
        {
            SubscribeAllButtons();
        }

        private void SubscribeAllButtons()
        {
            _startGameButton.OnClickAsObservable().Subscribe(_ => StartGame()).AddTo(this);
            _settingsButton.OnClickAsObservable().Subscribe(_ => OpenSettingsPanel()).AddTo(this);
            _exitGameButton.OnClickAsObservable().Subscribe(_ => ExitGame()).AddTo(this);
        }

        private async void StartGame()
        {
            await SceneManager.LoadSceneAsync(gameSceneName);
        }

        private void OpenSettingsPanel()
        {
        }
        
        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
