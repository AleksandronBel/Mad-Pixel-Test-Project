using UnityEngine;

namespace CameraHandler
{
    public class RailCamera : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        private readonly Vector3 _offset = new(0, 1.5f, -4.5f);

        private void Update()
        {
            transform.position = _player.position + _offset;
        }
    }
}
