using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour {
  public Sprite[] Sprites;

  private float speed;

	void Start ()
  {
    SpriteRenderer sr = (SpriteRenderer) renderer;
    sr.sprite = Sprites[Random.Range(0, Sprites.Length)];

    transform.position += new Vector3(
        Random.Range(-1.0f, 1.0f),
        0.0f, 0.0f);

    speed = Random.Range(8.0f, 16.0f);
	}

  void Update ()
  {
    transform.position += Vector3.up * Time.deltaTime * speed;

    Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
    if (screenPos.y > Screen.height) {
      Destroy(gameObject);
    }
  }
}
