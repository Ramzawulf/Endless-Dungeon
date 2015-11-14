using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class floorComponents
{
		int length = 0;
		int numPits = 0;
		Vector3 start;
		List<int> elevations;
		List<FloorBrick> bricks;

		public floorComponents (int l, int nOP, Vector3 reference)
		{
				this.length = l;
				this.numPits = Mathf.Min (nOP, l);
				this.initElevation ();
				this.start = reference;
				this.bricks = new List<FloorBrick> ();
				this.initComponents ();
		}
		public floorComponents (Vector3 reference, List<int> elevation)
		{
				this.length = elevation.Count;
				this.start = reference;
				this.elevations = elevation;
				this.bricks = new List<FloorBrick> ();
				this.initComponents ();
		}

		private void initComponents ()
		{
				for (int i = 0; i < this.length; i++) {
						this.bricks.Add (new FloorBrick (this.start + new Vector3 (i, 0, 0), this.elevations [i]));
				}
		}

		private void initElevation ()
		{
				int assignedPits = 0;
				List<int> tempElevation = new List<int> ();
				for (int i = 0; i < this.length; i++) {
						if (assignedPits < this.numPits) {
								tempElevation.Add (0);
								assignedPits++;
						} else {
								tempElevation.Add (Mathf.CeilToInt (Random.Range (1, 4)));
						}
				}
				this.elevations = tempElevation;
				this.shuffleElevation ();
		}

		private void shuffleElevation ()
		{
				int rgn = 0;
				List<int> tmpList = new List<int> ();
				while (this.elevations.Count > 0) {
						rgn = Mathf.CeilToInt (Random.Range (0, this.elevations.Count));
						tmpList.Add (this.elevations [rgn]);
						this.elevations.RemoveAt (rgn);
				}
				this.elevations = tmpList;
		}
}
