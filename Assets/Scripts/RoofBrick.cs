using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoofBrick
{
		private List<GameObject> bricks;
		private int elevation = 1;
		GameObject prefab;
		public RoofBrick (Vector3 position, int elevation)
		{
				this.bricks = new List<GameObject> ();
				this.elevation = elevation;
				this.prefab = (GameObject)Resources.Load ("Prefabs/RoofBrick");
			
				for (int i = 0; i < Mathf.Abs(this.elevation); i++) {
						GameObject.Instantiate (this.prefab, position - new Vector3 (0, i, 0), Quaternion.identity);
				}
		}
}
