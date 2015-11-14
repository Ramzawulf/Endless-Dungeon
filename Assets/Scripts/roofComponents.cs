using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class roofComponents
{
		int length;
		Vector3 start;
		List<int> elevations;
//		List<RoofBrick> ceiling;
		List<RoofBrick> bricks;
		public roofComponents (int l, Vector3 reference)
		{
				this.length = l;
				this.start = reference;
				//			this.ceiling = new List<RoofBrick> ();
				this.bricks = new List<RoofBrick> ();
		}

		public roofComponents (Vector3 reference, List<int> elevations)
		{
				this.length = elevations.Count;
				this.elevations = elevations;
				this.start = reference;
//				this.ceiling = new List<RoofBrick> ();
				this.bricks = new List<RoofBrick> ();
				this.initCeiling ();
				this.initComponents ();
		}

		private void initComponents ()
		{
				for (int i = 0; i < this.length; i++) {
						this.bricks.Add (new RoofBrick (this.start + new Vector3 (i, 0, 0), -this.elevations [i]));
				}
		}

		private void initCeiling ()
		{
				for (int i = 0; i < this.length; i++) {
						this.bricks.Add (new RoofBrick (this.start + new Vector3 (i, -1, 0), 1));
				}
		}
}
