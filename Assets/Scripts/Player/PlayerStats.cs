using Infrastructure.Services;
using MessagePipe;
using R3;
using System;
using Zenject;
using static Messages.Messages;

namespace Player
{
    public class PlayerStats : Service
    {
        [Inject] private readonly ISubscriber<PlayerDamage> _playerDamage;
        [Inject] private readonly ISubscriber<AddScore> _addScore;
        [Inject] private readonly ISubscriber<PlayerShoot> _playerShoot;

        [Inject]
        private void Construct()
        {
            _playerDamage.Subscribe(_ => Health.Value--).AddTo(Disposables);
            _addScore.Subscribe(_ => Score.Value++).AddTo(Disposables);
            _playerShoot.Subscribe(_ => Ammo.Value--).AddTo(Disposables);
        }

        public ReactiveProperty<int> Ammo { get; } = new(25);
        public ReactiveProperty<int> Score { get; } = new(0);
        public ReactiveProperty<int> Health { get; } = new(3);
    }
}
