#region

using Assets.Scripts;
using UnityEngine;

#endregion

namespace Assets
{
    public class DestroyingWall : MonoBehaviour
    {
        public Audio Audio;
        public float Speed = 2f;
        public LayerMask WhatToDestroy;

        private void Start()
        {
        }

        // Update is called once per frame
        public void Update()
        {
            Audio.playRocks();
            transform.Translate(new Vector3(1, 0, 0)*Speed*Time.deltaTime);
        }

        public void OnTriggerEnter(Collider col)
        {
            var gmObj = col.gameObject;
            if (gmObj.tag == "Player")
            {
                gmObj.GetComponent<HeroBehaviour>().DamageCharacter(float.MaxValue);
            }
        }
    }
}