using UnityEngine;
using System.Collections;
using Holoville.HOTween;

[RequireComponent(typeof(AudioSource))]
public class ButtonPress : Button {
	[SerializeField] TextMesh keyText = null;
	[SerializeField] AudioClip clip1 = null;
	[SerializeField] AudioClip clip2 = null;

	private bool canPress = false;
	private bool pressed = false;

	void Start () {
		keyText.text = mappedKeys [0].ToString();
		this.renderer.material.color = Color.green;
		this.StartCoroutine (FlowRoutine ());
	}

	private void Test() {
		this.gameObject.SetActive (true);
	}

	void Update() {

		if (Input.GetKeyDown (mappedKeys [0])) {
			Pressed();
		}
	}

	void Pressed() {
		if (canPress) { 
			canPress = false;
			pressed = true;
			audio.clip = clip2;
			audio.Play();
			this.renderer.material.color = Color.green;
			StopCoroutine("Blink");
			Game.Score();
		}
		else {
			Game.TakeDamage(transform.position);
		}
	}

	IEnumerator FlowRoutine() {
		this.renderer.material.color = Color.green;
		yield return new WaitForSeconds (Random.Range(3.5f,15.5f));
		StartCoroutine (Blink ());
	}

	IEnumerator Blink() {
		audio.clip = clip1;
		audio.Play();
		canPress = true;
		int i = 20;
		while (i > 0) {
			i--;
			HOTween.To (this.renderer.material,0.1f,"color",Color.red);
			yield return new WaitForSeconds(0.1f);
			HOTween.To (this.renderer.material,0.1f,"color",new Color(0.5f,0,0,1));
			yield return new WaitForSeconds(0.1f);
			if(pressed) break;
		}
		if (!pressed) {
			Game.TakeDamage(transform.position);
		}
		pressed = false;
		canPress = false;
		StartCoroutine (FlowRoutine());
	}
}
