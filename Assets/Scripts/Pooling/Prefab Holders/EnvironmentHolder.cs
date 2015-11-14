#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Pooling.Prefab_Holders
{
    internal class EnvironmentHolder : MonoBehaviour
    {
        public static EnvironmentHolder Ctrl;
        private int _continuousPits;
        private float _index;
        public GameObject Height0;
        public GameObject Height1Plain;
        public GameObject Height2Plain;
        public GameObject Height3Plain;
        public GameObject Roof;

        public void Awake()
        {
            if (Ctrl == null)
                Ctrl = this;
            else if (Ctrl != this)
                Destroy(gameObject);
        }

        public GameObject GetRandom()
        {
            var v = Random.value;
            _index += 0.001f;

            if (v <= 0.25f)
                return Height3Plain;
            if (v <= 0.5f)
                return Height2Plain;
            if (v <= 0.75f)
                return Height1Plain;
            return Height0;
        }

        public GameObject Next()
        {
            while (true)
            {
                var v = Random.value;
                //var v =  Mathf.PerlinNoise(_index, _index);
                _index += 0.001f;
                if (v <= 0.25f)
                {
                    _continuousPits = 0;
                    return Height3Plain;
                }

                if (v <= 0.5f)
                {
                    return Height2Plain;
                }

                if (v <= 0.75f)
                {
                    _continuousPits = 0;
                    return Height1Plain;
                }

                if (_continuousPits < 3)
                {
                    _continuousPits++;
                    return Height0;
                }
            }
        }
    }
}