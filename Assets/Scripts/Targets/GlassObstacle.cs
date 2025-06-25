using MessagePipe;
using static Messages.Messages;
using Zenject;
using UnityEngine;
using Player;

namespace Targets
{
    public class GlassObstacle : BaseDamageableObject, IDangerable
    {
        [Inject] private readonly IPublisher<PlayerDamage> _playerDamage;

        [SerializeField] private float _damageCount = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover player))
            {
                GiveDamage();
                BreakTarget();
            }
        }

        public void GiveDamage()
        {
            _playerDamage.Publish(new(_damageCount));
        }
    }
}
