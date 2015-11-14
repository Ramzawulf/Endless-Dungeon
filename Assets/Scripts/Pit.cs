using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Pit : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerExit (Collider col)
		{
				if (col.tag == "Player") {
						HeroBehaviour hb = col.GetComponent<HeroBehaviour> ();
						hb.DamageCharacter (float.MaxValue);
				}
		}

}
