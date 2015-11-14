using UnityEngine;
using System.Collections;

public class Elevation : MonoBehaviour
{

		public Brick bottomBrick, middleBrick, upperBrick;

		public int height;

		public Vector3 position{ get { return this.transform.position; } }
		
		void Start ()
		{
				this.height = 3;
		}
	
		
		void Update ()
		{
	
		}

		public Elevation setHeight (int height)
		{
				switch (height) {
				case 0:
						this.bottomBrick.deactivate ();
						this.middleBrick.deactivate ();
						this.upperBrick.deactivate ();
						this.height = height;
						break;
				case 1:
						this.middleBrick.deactivate ();
						this.upperBrick.deactivate ();
						this.height = height;
						break;
				case 2:
						this.upperBrick.deactivate ();
						this.height = height;
						break;
				default:
						break;

				}
				return this;
		}

}