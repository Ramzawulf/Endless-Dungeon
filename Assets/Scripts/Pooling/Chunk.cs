#region

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Pooling.Prefab_Holders;
using UnityEngine;

#endregion

namespace Assets.Scripts.Pooling
{
    public class Chunk : MonoBehaviour
    {
        public List<GameObject> Blocks;
        public int Height;

        public void Start()
        {
            Blocks = new List<GameObject>();
            var blockParent = transform.Find("Blocks").gameObject;
            foreach (Transform child in blockParent.transform)
            {
                var go =
                    Instantiate(AssetHolder.Ctrl.BlockPrefab, child.transform.position, Quaternion.identity) as
                        GameObject;
                Blocks.Add(go);
                go.transform.SetParent(transform);
            }
        }

        public void Crumble()
        {
            StartCoroutine(CrumbleBlocks());
        }

        protected IEnumerator CrumbleBlocks()
        {
            foreach (var blockGo in Blocks)
            {
                if (blockGo == null)
                    break;

                var b = blockGo.GetComponent<BlockBehaviour>();

                //if (b != null)
                b.Crumble();

                yield return new WaitForSeconds(0.1f);
            }
        }

        public void OnCollisionEnter(Collision col)
        {

        }
    }
}