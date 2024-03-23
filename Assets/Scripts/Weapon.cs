using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Prototype
{
    public enum WeaponType
    {
        Axe,
        Crutch
    }

    public class Weapon : MonoBehaviour
    {
        public WeaponType Type;
        public GameObject Owner;
        public int damage;
        public TrailRenderer Trail;
        private Collider m_HitBox;

        private Transform m_Transform;

        private TweenerCore<Vector3, Vector3, VectorOptions> m_DeactivateTween;

        private void Awake()
        {
            m_Transform = transform;
            m_HitBox = GetComponent<Collider>();
            m_DeactivateTween = m_Transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
                ActivateTrail(false);
            });
            ActivateTrail(false);
            m_DeactivateTween.Pause();
        }

        public void ActivateTrail(bool activate)
        {
            Trail.gameObject.SetActive(activate);
            Trail.Clear();
        }

        public void EnableHitBox(bool enable)
        {
            m_HitBox.enabled = enable;
        }

        public void HideWeapon()
        {
            m_DeactivateTween.Restart();           
        }

        public void ShowWeapon()
        {
            m_DeactivateTween.Restart();
            m_DeactivateTween.Pause();

            float activateDuration = 0.2f;
            m_Transform.DOScale(Vector3.one, activateDuration).SetEase(Ease.InSine).OnComplete(() => {
                ActivateTrail(true);
            });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Owner == other.gameObject)
                return;

            if (other.TryGetComponent<FarmableObject>(out var farmable))
            {
                if (farmable.RequiredWeapon == Type)
                {
                    other.GetComponent<HealthData>().RemoveHealth(damage);
                }
            }
        }
    }
}
