using R3;
using System;
using Zenject;

namespace Player
{
    public class PlayerService
    {
        public ReactiveProperty<int> Ammo { get; } = new(25);
        public ReactiveProperty<int> Score { get; } = new(0);
        public ReactiveProperty<int> Health { get; } = new(3);
    }
}
