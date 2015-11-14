#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Pooling
{
    public class BlockBehaviour : MonoBehaviour
    {
        private bool _crumbling;

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag != "Player") return;
            if (_crumbling)
                Physics.IgnoreCollision(other.collider, GetComponent<Collider>(), true);
        }

        public void Crumble()
        {
            var rb = gameObject.GetComponent<Rigidbody>();
            _crumbling = true;
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
    }
}