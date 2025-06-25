using Infrastructure.Services;
using MessagePipe;
using R3;
using Zenject;
using static Messages.Messages;

namespace Player
{
    public class PlayerStats : Service
    {
        [Inject] private readonly IPublisher<PlayerDead> _playerDead;

        [Inject] private readonly ISubscriber<PlayerDamage> _playerDamage;
        [Inject] private readonly ISubscriber<HitTarget> _hitTarget;
        [Inject] private readonly ISubscriber<PlayerShoot> _playerShoot;
        [Inject] private readonly ISubscriber<ReloadStats> _reloadStats;

        public ReactiveProperty<int> Ammo { get; } = new();
        public ReactiveProperty<int> Score { get; } = new();
        public ReactiveProperty<int> Health { get; } = new();

        [Inject]
        private void Construct()
        {
            _playerDamage.Subscribe(_ =>
            {
                Health.Value--;

                if (Health.Value <= 0) _playerDead.Publish(new());

            }).AddTo(Disposables);

            _playerShoot.Subscribe(_ => Ammo.Value--).AddTo(Disposables);

            _hitTarget.Subscribe(_ =>
            {
                Score.Value++;
                Ammo.Value++;

            }).AddTo(Disposables);

            InitializeValues();

            _reloadStats.Subscribe(_ => InitializeValues()).AddTo(Disposables);
        }

        private void InitializeValues()
        {
            Ammo.Value = 10;
            Score.Value = 0;
            Health.Value = 2;
        }
    }
}
