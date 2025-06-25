using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Bootsrappers
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private string menuSceneName = "MenuScene";

        private async void Awake()
        {
            Application.targetFrameRate = 60;

            await SceneManager.LoadSceneAsync(menuSceneName);
        }
    }
}
