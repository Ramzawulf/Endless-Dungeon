#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Pooling
{
    public class BaseBehaviour : MonoBehaviour
    {
        public static BaseBehaviour ctrl;
        private bool _destroyed;
        private float _startingTime;
        public float TimeToLive;

        public void Awake()
        {
            if (ctrl == null)
                ctrl = this;
            else if (ctrl != this)
                Destroy(gameObject);
        }

        public void Start()
        {
            _startingTime = Time.time;
            TimeToLive = EnvironmentManager.Ctrl.CrumbleDelay;
        }

        public void Update()
        {
            if (_startingTime + TimeToLive < Time.time)
            {
                if (_destroyed) return;

                foreach (Transform child in transform)
                {
                    var rb = child.gameObject.GetComponent<Rigidbody>();
                    if (rb != null) rb.useGravity = true;
                }
                Destroy(this, 2f);
                _destroyed = true;
            }
        }
    }
}