using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Bullet : MonoBehaviour
{
		public Vector3 direction;
		private float speed;
		private float startingTime;
		private float timeToLive;
		private bool isShot;


		// Use this for initialization
		void Start ()
		{
				this.speed = 0.12f;
				this.timeToLive = 3;
				this.isShot = false;
				//this.direction = new Vector3 (-1, 0, 0);
				Destroy (this.gameObject, this.timeToLive);
		}

		void FixedUpdate ()
		{
				this.transform.position += this.direction * speed;
				
		}


		void Update ()
		{
		}

		public void shootTowards (Vector3 direction)
		{
				this.direction = direction;
				this.isShot = true;
		}
	

		void OnCollisionEnter (Collision col)
		{
				GameObject gmObj = col.gameObject;

				if (gmObj.tag == "Player") {
						gmObj.GetComponent <HeroBehaviour> ().DamageCharacter ();
						Destroy (this.gameObject);
				}
				if (gmObj.tag == "Enemy") {
						Physics.IgnoreCollision (col.collider, this.GetComponent<Collider>());
				} else if (gmObj.tag == "Floor") {
						Destroy (this.gameObject);
				}

		}
}
