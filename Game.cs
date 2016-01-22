using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class Game : MonoBehaviour {

	private List<Button> allButtons = new List<Button>();
	private static int health;
	private static Light sun = null;
	private static Game self = null;
	private static AudioSource warning = null;
	private static CameraShake shake = null;
	private static bool gameOver = false;
	private static ParticleSystem particle = null;

	void Start() {
		self = this;
		sun = GameObject.Find ("Sun").GetComponent<Light> ();
		warning = GameObject.Find ("Warning").GetComponent<AudioSource> ();
		particle = GameObject.Find ("Particle").GetComponent<ParticleSystem> ();
		shake = Camera.main.GetComponent<CameraShake> ();
		StartCoroutine (GameRoutine ());
		health = 10;
	}

	public static void TakeDamage(Vector3 pos ) {
		particle.Play ();
		particle.transform.position = pos; 
		print ("Ouch!");
		health--;
		if (health == 5 ) {
			self.StartCoroutine(Red ());
			shake.DoShake();
			warning.Play();
		}
		if (health <= 0) {
			gameOver = true;
			self.StartCoroutine(GameOver ());
			print ("Game Over");
		}
	}
	
	public static IEnumerator Red() { 
		float t = 0.2f;
		while(true) {
			HOTween.To (sun,t,"color",Color.red);
			yield return new WaitForSeconds (t);
			if(gameOver)break;
			HOTween.To (sun,t,"color",Color.white);
			yield return new WaitForSeconds (t);
		}
	}

	public static IEnumerator GameOver() {
		shake.StopShake ();
		HOTween.To (sun,1,"color",Color.black);
		yield return new WaitForSeconds (5);
		Application.LoadLevel (Application.loadedLevel);
	}

	public static void Score() {
		print ("Score");
	}

	public void AddButton(Button b) {
		allButtons.Add (b);
	}

	public IEnumerator GameRoutine() {
		yield return new WaitForSeconds(2);
		foreach (Button b in allButtons) {
			b.Activate();
			yield return new WaitForSeconds(1);
		}
	}
}
