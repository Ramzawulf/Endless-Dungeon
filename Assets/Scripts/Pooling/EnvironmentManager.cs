#region

using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Pooling.Prefab_Holders;
using UnityEngine;

#endregion

namespace Assets.Scripts.Pooling
{
    public class EnvironmentManager : MonoBehaviour
    {
        public static EnvironmentManager Ctrl;
        private List<Chunk> _chunkList;
        private float _lastCrumble;
        private List<RoofBehaviour> _roofList;
        public int BaseSize;
        public float CrumbleDelay;
        public Vector3 RoofOffset;
        public Color LightColor;
        public float LightIntensity;
        public float LightRange;

        public void Awake()
        {
            if (Ctrl == null)
                Ctrl = this;
            else if (Ctrl != this)
                Destroy(gameObject);
        }

        public void Start()
        {
            Init();
        }

        public void Update()
        {
            if (_lastCrumble + GameController.Ctrl.ChunkLifeTime < Time.time)
            {
                _chunkList[0].Crumble();
                _chunkList.RemoveAt(0);
                Push();
                _roofList[0].Crumble();
                _roofList.RemoveAt(0);
                PushRoof();
                _lastCrumble = Time.time;
            }
        }

        private void Init()
        {
            _lastCrumble = Time.time + CrumbleDelay;
            _chunkList = new List<Chunk>();
            _roofList = new List<RoofBehaviour>();
            var go = BuildBaseFloor(transform.position);
            _chunkList.Add(go.GetComponent<Chunk>());

            for (var i = 1; i < BaseSize; i++)
            {
                InitialPush();
            }
        }

        private void InitialPush()
        {
            var position = _chunkList.Last().transform.position + Vector3.right;
            var tmp = BuildBaseFloor(position);
            _chunkList.Add(tmp.GetComponent<Chunk>());

            var tmpRoof = BuildBaseRoof(position + RoofOffset);
            _roofList.Add(tmpRoof.GetComponent<RoofBehaviour>());
        }

        private void PushRoof()
        {
            var position = _roofList.Last().transform.position + Vector3.right;
            var tmp = BuildRoof(position);
            _roofList.Add(tmp.GetComponent<RoofBehaviour>());
        }

        private GameObject BuildRoof(Vector3 position)
        {
            var go = Instantiate(EnvironmentHolder.Ctrl.Roof, position, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            return go;
        }

        private GameObject BuildBaseFloor(Vector3 at)
        {
            var prefab = EnvironmentHolder.Ctrl.Height1Plain;

            var go = Instantiate(prefab, at, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            return go;
        }

        private GameObject BuildBaseRoof(Vector3 position)
        {
            var tmp = BuildRoof(position);
            tmp.transform.SetParent(transform);
            return tmp;
        }

        public void Push()
        {
            var position = _chunkList.Last().transform.position + Vector3.right;
            var tmp = BuildFloor(_chunkList.Last().transform.position + Vector3.right);
            _chunkList.Add(tmp.GetComponent<Chunk>());
        }

        private GameObject BuildFloor(Vector3 at)
        {
            var prefab = EnvironmentHolder.Ctrl.Next();

            var go = Instantiate(prefab, at, Quaternion.identity) as GameObject;
            if (go != null)
                go.transform.SetParent(transform);
            return go;
        }
    }
}