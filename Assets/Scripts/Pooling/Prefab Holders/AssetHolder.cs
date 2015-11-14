#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Pooling.Prefab_Holders
{
    public class AssetHolder : MonoBehaviour
    {
        public static AssetHolder Ctrl;
        public GameObject BlockPrefab;
        public GameObject ChunkPrefab;
        public GameObject HeroPrefab;
        public GameObject BodyExplosionParticleSystem;
        public GameObject JumpParticleSystem;

        public void Awake()
        {
            if (Ctrl == null)
                Ctrl = this;
            else if (Ctrl != this)
                Destroy(gameObject);
        }
    }
}