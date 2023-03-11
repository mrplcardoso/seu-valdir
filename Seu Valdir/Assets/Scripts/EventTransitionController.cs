using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTransitionController : MonoBehaviour
{
	public List<Sprite> spritesTransitions;
	public SpriteRenderer renderer;
	public string sceneName;
	public float speed;

	private void Start()
	{
		renderer.color = Color.clear;
		renderer.sprite = null;
		StartCoroutine(ShowSprites());
	}

	IEnumerator ShowSprites()
	{
		yield return new WaitForSeconds(1f);
		for(int i = 0; i < spritesTransitions.Count; ++i)
		{
			renderer.sprite = spritesTransitions[i];
			yield return FadeSprite(renderer, Color.white, speed);
			yield return new WaitForSeconds(2f);
			yield return FadeSprite(renderer, Color.clear, speed);
			yield return new WaitForSeconds(0.2f);
		}
		SceneManager.LoadScene(sceneName);
	}

	IEnumerator FadeSprite(SpriteRenderer sp, Color target, float speed = 1f)
	{
		float t = 0;
		Color c = sp.color;
		while(t < 1.01f)
		{
			sp.color = Color.Lerp(c, target, t);
			t += Time.deltaTime * speed;
			yield return null;
		}
		sp.color = target;
	}
}
