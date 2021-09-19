using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
  SpriteRenderer spriteRenderer;

  bool isFading = false;

	void Start ()
  {
    spriteRenderer = (SpriteRenderer) renderer;
    Invoke("StartFading", 0.5f);
    Invoke("RemoveFromScene", 1.5f);
	}

	void Update ()
  {
    if (isFading) {
      Color newColor = spriteRenderer.color;
      newColor.a = Mathf.Clamp(newColor.a - Time.deltaTime * 2.0f, 0.0f, 1.0f);
      spriteRenderer.color = newColor;
    }
	}

  void StartFading ()
  {
    isFading = true;
  }

  void RemoveFromScene ()
  {
    Destroy(gameObject);
  }
}
