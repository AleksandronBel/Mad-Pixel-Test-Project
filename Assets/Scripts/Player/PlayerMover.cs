using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Rigidbody _rb;

        private void Awake() => _rb = GetComponent<Rigidbody>();
        private void FixedUpdate() => _rb.velocity = Vector3.forward * speed;
    }
}
