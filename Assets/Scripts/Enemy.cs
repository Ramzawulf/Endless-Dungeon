using UnityEngine;
using System.Collections;

public enum ShootingDirection
{
		front,
		up
}


public class Enemy : MonoBehaviour
{
		public GameObject bullet;
		public Transform front;
		public Transform shootingPoint;
		public float shootingSpeed;
		public Audio audio;
		ShootingDirection shootingDirection;	
		private float lastShoot;
		// Use this for initialization
		void Start ()
		{
				this.lastShoot = Time.time;
				this.shootingSpeed = 1f;
				this.shootingDirection = ShootingDirection.front;
				this.transform.Rotate (new Vector3 (90, 0, 0));
		}
	
		// Update is called once per frame
		void Update ()
		{
				/*
				Vector3 direction = front.position - this.transform.position;
				RaycastHit hit;
				Debug.DrawRay (this.transform.position, direction, Color.blue);
				
				if (Physics.Linecast (this.transform.position, front.position, out hit)) {
						if (hit.collider.name == "Enemy(Clone)" || hit.collider.name == "Brick") {
								if (this.shootingDirection != ShootingDirection.up) {
										this.transform.Rotate (new Vector3 (0, 90, 0));
								} else {
										this.shootingDirection = ShootingDirection.up;
								}
						}
								
				}*/

				if (Time.time > (this.lastShoot + this.shootingSpeed)) {
						this.transform.Rotate (new Vector3 (0, 90, 0));
						this.shoot ();
						this.lastShoot = Time.time;
				}
		}

		private void shoot ()
		{
				Vector3 direction = this.front.position - this.transform.position;
				GameObject GmObj = (GameObject)GameObject.Instantiate (this.bullet, this.shootingPoint.position, Quaternion.identity);
				GmObj.GetComponent<Bullet> ().direction = direction;	
				this.audio.playShoot ();
		}
}
