#region

using Assets.Scripts;
using UnityEngine;

#endregion

namespace Assets
{
    public class Destroyer : MonoBehaviour
    {
        // Use this for initialization
        public void Start()
        {
        }

        // Update is called once per frame
        public void Update()
        {
            var newPosition = transform.position;
            newPosition.x = HeroBehaviour.Ctrl.transform.position.x;
            newPosition.z = HeroBehaviour.Ctrl.transform.position.z;
            transform.position = newPosition;
        }

        public void OnTriggerEnter(Collider col)
        {
            Destruction(col);
        }

        public void OnTriggerStay(Collider col)
        {
            Destruction(col);
        }

        public static void Destruction(Collider collider)
        {
            if (collider.gameObject.tag == "Floor" || collider.gameObject.tag == "Roof")
            {
                Destroy(collider.gameObject);
            }

            else if (collider.gameObject.tag == "Player")
            {
                HeroBehaviour.Ctrl.DamageCharacter(float.MaxValue);
            }
        }
    }
}