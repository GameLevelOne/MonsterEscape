using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

	public delegate void FadeInFinished();
	public delegate void FadeOutFinished();
	public delegate void FadeOutGameOverFinished();
	public static event FadeInFinished OnFadeInFinished;
	public static event FadeOutFinished OnFadeOutFinished;
	public static event FadeOutGameOverFinished OnFadeOutGameOverFinished;

	Image faderImage;

	float fadeSpeed = 1f;
	float fadeTimer = 0f;

	void Awake(){
		faderImage = GetComponent<Image>();
		faderImage.gameObject.SetActive(true);
	}

	void Start(){
		FadeIn();
	}

	public void FadeIn (){
		faderImage.gameObject.SetActive(true);
		StartCoroutine(DoFade(true));
	}

	public void FadeOut(){
		faderImage.gameObject.SetActive(true);
		StartCoroutine(DoFade(false));
	}

	public void FadeOutGameOver(){
		faderImage.gameObject.SetActive(true);
		StartCoroutine(DoFadeGameOver());
	}

	IEnumerator DoFade (bool fadeIn)
	{
		while (fadeTimer < 1f) {
			fadeTimer += Time.deltaTime * fadeSpeed;
			if (fadeIn) {
				faderImage.color = Color.Lerp (Color.black, Color.clear, fadeTimer);
			} else {
				faderImage.color = Color.Lerp (Color.clear, Color.black, fadeTimer);
			}
			yield return null;
		}

		fadeTimer=0f;
		if (fadeIn) {
			if(OnFadeInFinished !=null)
				OnFadeInFinished ();
			gameObject.SetActive (false);
		} else {
			if(OnFadeOutFinished !=null)
				OnFadeOutFinished();
		}
	}

	IEnumerator DoFadeGameOver ()
	{
		while (fadeTimer < 2f) {
			fadeTimer += Time.deltaTime *fadeSpeed;
			Color dim = new Color(0,0,0,0.5f);
			faderImage.color = Color.Lerp (Color.clear, dim, fadeTimer);
			yield return null;
		}
		fadeTimer=0f;
		OnFadeOutGameOverFinished();
	}
}
