using Targets;
using UnityEngine;

namespace Targets
{
    public class CrystalBonus : BaseDamageableObject, IBonusable
    {
        [SerializeField] private GameObject _pointLight;

        private void Start()
        {
            PrepareObject();
        }

        protected override void PrepareObject()
        {
            base.PrepareObject();
            _pointLight.SetActive(true);
        }

        public override void BreakTarget()
        {
            base.BreakTarget();
            _pointLight.SetActive(false);

            GetBonus();
        }

        public void GetBonus()
        {
            //+ammo to player + health to player
        }
    }
}
