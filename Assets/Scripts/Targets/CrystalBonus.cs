using MessagePipe;
using Zenject;
using static Messages.Messages;

namespace Targets
{
    public class CrystalBonus : BaseDamageableObject, IBonusable
    {
        [Inject] private readonly IPublisher<HitTarget> _hitTarget;

        private void Start()
        {
            PrepareObject();
        }

        public override void BreakTarget()
        {
            base.BreakTarget();

            GetBonus();
        }

        public void GetBonus()
        {
            _hitTarget.Publish(new());
        }
    }
}
