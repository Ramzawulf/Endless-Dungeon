using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Saw : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				//this.transform.Rotate (new Vector3 (90, 0, 0));

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void activateSaw ()
		{
				this.GetComponent<Renderer>().enabled = true;
				this.GetComponent<Collider>().enabled = true;
		}

		void OnCollisionEnter (Collision col)
		{
				if (col.gameObject.tag == "Player") {
						print ("hero hit");
				}
		}

		void OnTriggerEnter (Collider col)
		{
				if (col.gameObject.tag == "Player") {
						col.gameObject.GetComponent<HeroBehaviour> ().DamageCharacter ();
				}
		}
}
