using Factories;
using Projectile;
using UnityEngine;
using Zenject;

namespace Targets
{
    public class CrystalBonus : MonoBehaviour, IDamageable, IBreakable, IBonusable
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
            var fracture = _gameFactory.Instantiate(_fracturePrefab, null); //использовать пул

            fracture.transform.position = transform.position;
            fracture.transform.rotation = Quaternion.identity;

            _breakSfx.PlayOneShot(_breakSfx.clip);

            GetBonus();
            Destroy(gameObject);
        }

        public void GetBonus()
        {
            //+ammo to player + health to player
        }
    }
}
