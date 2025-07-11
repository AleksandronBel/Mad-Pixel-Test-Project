﻿using Projectile;
using UnityEngine;

namespace Targets
{
    public class BaseDamageableObject : MonoBehaviour, IDamageable, IBreakable
    {
        [SerializeField] private GameObject _mainObjectObject;
        [SerializeField] private float _hitPoints = 1f;
        [SerializeField] private AudioSource _breakSfx;
        [SerializeField] private ParticleSystem _breakParticleSystem;
        [SerializeField] private BoxCollider _boxColliderTrigger;

        private void Start()
        {
            PrepareObject();
        }

        protected virtual void PrepareObject()
        {
            _mainObjectObject.SetActive(true);
            _boxColliderTrigger.enabled = true;
        }

        public virtual void TakeDamage(float dmg)
        {
            _hitPoints -= dmg;
            if (_hitPoints <= 0)
                BreakTarget();
        }

        public virtual void BreakTarget()
        {
            _mainObjectObject.SetActive(false);
            _boxColliderTrigger.enabled = false;

            _breakSfx.pitch = Random.Range(0.8f, 1.3f);
            _breakSfx.PlayOneShot(_breakSfx.clip);
            _breakParticleSystem.Play();
        }
    }
}
