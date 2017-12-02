using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIBGAnimation : MonoBehaviour {
	public Screen[] screens;
	public int startingScreen;
	public int target;
	public int source;
	public float fadeTime = 0.1f;
	public float moveTime = 0.5f;
	public float animationTimer;
	
	void Awake() {
		target = startingScreen;
		source = startingScreen;
		animationTimer = fadeTime * 2 + moveTime + 1;
	}
	
	public void GoToScreen(int id) {
		animationTimer = 0;
		source = target;
		target = id;
	}
	
	void Update() {
		screens[source].fade.blocksRaycasts = false;
		screens[target].fade.blocksRaycasts = true;
		if (animationTimer == 0) {
			screens[source].fade.alpha = 1;
			screens[target].fade.alpha = 0;
		}
		animationTimer += Time.deltaTime;
		if (animationTimer <= fadeTime) {
			screens[source].fade.alpha = 1 - animationTimer / fadeTime;
		} else if (animationTimer > fadeTime) {
			screens[source].fade.alpha = 0;
		}
		if (animationTimer > fadeTime && animationTimer <= fadeTime + moveTime) {
			transform.position = Vector3.Lerp(screens[source].rect.position,screens[target].rect.position,(animationTimer - fadeTime) / moveTime);
			LerpSize((animationTimer - fadeTime) / moveTime);
		} else if (animationTimer > fadeTime + moveTime) {
			transform.position = screens[target].rect.position;
			LerpSize(1);
		}
		if (animationTimer <= fadeTime * 2 + moveTime && animationTimer > fadeTime + moveTime) {
			screens[target].fade.alpha = (animationTimer - moveTime - fadeTime) / fadeTime;
		} else if (animationTimer > fadeTime * 2 + moveTime) {
			screens[target].fade.alpha = 1;
		}
	}
	
	public void LerpSize(float frac) {
		Vector2 sourceSize = screens[source].rect.rect.size;
		Vector2 targetSize = screens[target].rect.rect.size;
		Vector2 mySize = Vector2.Lerp(sourceSize,targetSize,frac);
		GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,mySize.x);
		GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,mySize.y);
	}
	
	[System.Serializable]
	public class Screen {
		public RectTransform rect;
		public CanvasGroup fade;
	}
}
