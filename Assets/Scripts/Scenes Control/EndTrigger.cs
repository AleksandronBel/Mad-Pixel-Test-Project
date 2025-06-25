using MessagePipe;
using UnityEngine;
using static Messages.Messages;
using Zenject;
using Player;

namespace ScenesControl
{
    public class EndTrigger : MonoBehaviour
    {
        [Inject] private readonly IPublisher<PlayerWin> _playerWin;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover player))
                _playerWin.Publish(new());
        }
    }
}
