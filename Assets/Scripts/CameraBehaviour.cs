using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
		public Transform target;
		public float distance = 3;
		public float heigth = 0;
		public float damping = 5;
		public bool smoothRotation = true;
		public float rotationDamping = 10;
		public bool lockRotation;
		// Use this for initialization
		void Start ()
		{
	
		}

		void FixedUpdate ()
		{
				//Vector3 wantedPosition = target.TransformPoint (new Vector3 (0, this.heigth, 0));
				Vector3 wantedPosition = target.TransformPoint (new Vector3 (0, 0, - this.distance));
				wantedPosition = new Vector3 (wantedPosition.x, this.transform.position.y, wantedPosition.z);
				this.transform.position = Vector3.Lerp (this.transform.position, wantedPosition, this.damping);
		}
	
		// Update is called once per frame
		void Update ()
		{
				/*
				if (this.smoothRotation) {
						Quaternion wantedRotation = Quaternion.LookRotation (target.position - this.transform.position, target.up);
						this.transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
				} else {
						transform.LookAt (target, target.up);
				}
				if (lockRotation) {
						this.transform.localRotation = Quaternion.Euler (Vector3.zero);
				}*/
		}
}

