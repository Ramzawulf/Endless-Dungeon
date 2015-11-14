using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour
{
		public AudioSource[] death;
		public AudioSource[] explosion;
		public AudioSource[] jump;
		public AudioSource[] shoot;
		public AudioSource[] rocks;
		public AudioSource intro;

		private int lastPlayed;


		public void playDeath ()
		{
				int rgn = Random.Range (0, death.Length);
				if (death.Length > 0) {
						death [rgn].Play ();
				}
		}

		public void playExplosion ()
		{
				int rgn = Random.Range (0, explosion.Length);
				if (explosion.Length > 0) {
						explosion [rgn].Play ();
				}
		}

		public void playJump ()
		{
				int rgn = Random.Range (0, jump.Length);
				if (jump.Length > 0) {
						jump [rgn].Play ();
				}
		}

		public void playShoot ()
		{
				int rgn = Random.Range (0, shoot.Length);
				if (shoot.Length > 0) {
						shoot [rgn].Play ();
				}
		}

		public void playRocks ()
		{
				int rgn = Random.Range (0, this.rocks.Length);
				if (this.rocks.Length > 0) {
						if (!this.rocks [this.lastPlayed].isPlaying) {
								this.rocks [rgn].Play ();
								this.lastPlayed = rgn;
						}
				}
		}

		public void playIntro ()
		{
				intro.Play ();
		}
}
