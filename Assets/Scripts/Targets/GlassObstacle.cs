using UnityEngine;
using Projectile;
using Zenject;
using Factories;

namespace Targets
{
    public class GlassObstacle : MonoBehaviour, IDamageable, IBreakable
    {
        [SerializeField] private float _hitPoints = 1f;
        [SerializeField] private FractureItemView _fracturePrefab;
        [SerializeField] private AudioSource _breakSfx;

        private GameFactory _gameFactory;

        [Inject]
        private void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void TakeDamage(float dmg)
        {
            _hitPoints -= dmg;
            if (_hitPoints <= 0)
                BreakTarget();
        }

        public void BreakTarget()
        {
            var fracture = _gameFactory.Instantiate(_fracturePrefab, null);

            fracture.transform.position = transform.position;
            fracture.transform.rotation = Quaternion.identity;

            _breakSfx.PlayOneShot(_breakSfx.clip);
            Destroy(gameObject);
        }
    }
}
