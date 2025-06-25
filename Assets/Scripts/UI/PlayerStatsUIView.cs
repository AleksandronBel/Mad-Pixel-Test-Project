using Cysharp.Threading.Tasks;
using Player;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PlayerStatsUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthCountText;
        [SerializeField] private TMP_Text bulletsCountText;
        [SerializeField] private TMP_Text scoreText;

        private PlayerStats _playerStats;

        [Inject]
        private void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;

            playerStats.Health.Subscribe(UpdateHealthCount).AddTo(this);
            playerStats.Ammo.Subscribe(UpdateBulletsCount).AddTo(this);
            playerStats.Score.Subscribe(UpdateScoreCount).AddTo(this);
        }

        private void UpdateHealthCount(int health)
        {
            healthCountText.text = $"Health: {health}";
        }

        private void UpdateBulletsCount(int bullets)
        {
            bulletsCountText.text = $"Bullets: {bullets}";
        }

        private void UpdateScoreCount(int score)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}
