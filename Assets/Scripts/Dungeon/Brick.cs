using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SawCollection
{
		public Saw left;
		public Saw up;
		public Saw right;
		public Saw down;
}

public class Brick : MonoBehaviour
{
		public GameObject sawprefab;
		private SawCollection saws;
		public const string SAW_LEFT = "left";
		public const string SAW_UP = "up";
		public const string SAW_RIGHT = "right";
		public const string SAW_DOWN = "down";
		public const string SAW_ALL = "all";
		
		void Start ()
		{
			
		}
	
		
		void Update ()
		{
				
		}

		public Brick deactivate ()
		{
				this.gameObject.SetActive (false);
				return this;
		}

		public Brick activate ()
		{
				this.gameObject.SetActive (true);
				return this;
		}

		public Brick activateSaw (string sawPosition)
		{
				GameObject createdSaw;
				switch (sawPosition) {
				case Brick.SAW_LEFT:
						createdSaw = Instantiate (this.sawprefab, this.transform.position + new Vector3 (0, -0.5f, 0), Quaternion.identity) as GameObject;
						createdSaw.transform.Rotate (90, 90, 90);
						this.saws.left = createdSaw.GetComponent<Saw> ();
						break;

				case Brick.SAW_UP:
						createdSaw = Instantiate (this.sawprefab, this.transform.position + new Vector3 (0.5f, 0, 0), Quaternion.identity) as GameObject;
						createdSaw.transform.Rotate (0, 90, 90);
						this.saws.up = createdSaw.GetComponent<Saw> ();
						break;

				case Brick.SAW_RIGHT:
						createdSaw = Instantiate (this.sawprefab, this.transform.position + new Vector3 (0, 0.5f, 0), Quaternion.identity) as GameObject;
						this.saws.right = createdSaw.GetComponent<Saw> ();
						createdSaw.transform.Rotate (90, 90, 90);
						break;

				case Brick.SAW_DOWN:
						createdSaw = Instantiate (this.sawprefab, this.transform.position + new Vector3 (-0.5f, 0, 0), Quaternion.identity) as GameObject;
						createdSaw.transform.Rotate (0, 90, 90);
						this.saws.down = createdSaw.GetComponent<Saw> ();
						break;

				case Brick.SAW_ALL:
						this.activateSaw (Brick.SAW_LEFT);
						this.activateSaw (Brick.SAW_UP);
						this.activateSaw (Brick.SAW_RIGHT);
						this.activateSaw (Brick.SAW_DOWN);
						break;
				}
				return this;
		}

		public Brick deactivateSaw (string sawPosition)
		{/*
				switch (sawPosition) {
				case Brick.SAW_LEFT:
						//this.sawLeft.SetActive (false);
						this.sawLeft.collider.enabled = false;
						this.sawLeft.renderer.enabled = false;
						break;
				case Brick.SAW_UP:
						//this.sawUp.SetActive (false);
						this.sawUp.collider.enabled = false;
						this.sawUp.renderer.enabled = false;
						break;
				case Brick.SAW_RIGHT:
						//this.sawRight.SetActive (false);
						this.sawRight.collider.enabled = false;
						this.sawRight.renderer.enabled = false;
						break;
				case Brick.SAW_DOWN:
						//this.sawDown.SetActive (false);
						this.sawDown.collider.enabled = false;
						this.sawDown.renderer.enabled = false;
						break;
				case Brick.SAW_ALL:
						this.deactivateSaw (Brick.SAW_LEFT);
						this.deactivateSaw (Brick.SAW_UP);
						this.deactivateSaw (Brick.SAW_RIGHT);
						this.deactivateSaw (Brick.SAW_DOWN);
						break;
				}
				*/
				return this;
		}
}
