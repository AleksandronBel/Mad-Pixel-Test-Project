using MessagePipe;
using static Messages.Messages;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using R3;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

namespace ScenesControl
{
    public class GameSceneHandler : MonoBehaviour
    {
        [Inject] private readonly ISubscriber<PlayerDead> _playerDead;
        [Inject] private readonly ISubscriber<PlayerWin> _playerWin;
        [Inject] private readonly IPublisher<ReloadStats> _reloadStats;

        [SerializeField] private string _menuSceneName = "MenuScene";

        [Header("Image for fade")]
        [SerializeField] private Image _backgroundFadeImage;

        [Header("In menu button during game")]
        [SerializeField] private Button _inMenuButton;

        [Header("After Win")]
        [SerializeField] private Button _inMenuPlayerWinButton;
        [SerializeField] private CanvasGroup _winGamePanel;

        [Header("After Lose")]
        [SerializeField] private CanvasGroup _gameOverPanel;
        [SerializeField] private Button _inMenuGameOverButton;
        [SerializeField] private Button _restartButton;

        private CancellationTokenSource _cts;

        [Inject]
        private void Construct()
        {
            _playerDead.Subscribe(_ => GameOver()).AddTo(this);
            _playerWin.Subscribe(_ => PlayerWin()).AddTo(this);

            _inMenuPlayerWinButton.OnClickAsObservable().Subscribe(_ => BackInMenu()).AddTo(this);
            _inMenuButton.OnClickAsObservable().Subscribe(_ => BackInMenuDuringGame()).AddTo(this);

            _inMenuGameOverButton.OnClickAsObservable().Subscribe(_ => BackInMenu()).AddTo(this);
            _restartButton.OnClickAsObservable().Subscribe(_ => RestarGame()).AddTo(this);
        }

        private void Start()
        {
            StartFadeAnimation();
        }

        private void BackInMenuDuringGame()
        {
            _backgroundFadeImage.gameObject.SetActive(true);
            _backgroundFadeImage.DOFade(1f, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                BackInMenu();
            });
        }

        private void StartFadeAnimation()
        {
            _backgroundFadeImage.gameObject.SetActive(true);
            _backgroundFadeImage.color = new Color(0f, 0f, 0f, 1f);
            _backgroundFadeImage.DOFade(0f, 2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _backgroundFadeImage.gameObject.SetActive(false);
            });
        }

        private void GameOver()
        {
            _gameOverPanel.gameObject.SetActive(true);
            _gameOverPanel.alpha = 0f;

            SetStateInteractableObject(_gameOverPanel, false);

            _gameOverPanel.DOFade(1f, 2f).OnComplete(() =>
            {
                SetStateInteractableObject(_gameOverPanel, true);
                Time.timeScale = 0f;
            });
        }

        private void SetStateInteractableObject(CanvasGroup objectToManupulate, bool state)
        {
            objectToManupulate.interactable = state;
            objectToManupulate.blocksRaycasts = state;
        }

        private void PlayerWin()
        {
            _winGamePanel.gameObject.SetActive(true);
            _winGamePanel.alpha = 0f;

            SetStateInteractableObject(_winGamePanel, false);

            _winGamePanel.DOFade(1f, 2f).OnComplete(() =>
            {
                SetStateInteractableObject(_winGamePanel, true);
                Time.timeScale = 0f;
            });
        }

        private void PrepareForRestart()
        {
            Time.timeScale = 1f;
            _reloadStats.Publish(new());
        }

        private void RestarGame()
        {
            PrepareForRestart();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void BackInMenu()
        {
            PrepareForRestart();
            SceneManager.LoadScene(_menuSceneName);
        }

        private void OnDestroy()
        {
            _gameOverPanel?.DOKill();
        }
    }
}
