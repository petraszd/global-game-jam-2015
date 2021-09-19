using UnityEngine;
using System.Collections;

public class JellyController : MonoBehaviour {
  public Sprite[] ActiveSprites;

  private bool isDying;

  void Start ()
  {
    isDying = false;
  }

  void OnTriggerEnter2D(Collider2D other) {
    string otherTag = other.gameObject.tag;
    if (!isDying && (otherTag == "Player" || otherTag == "PlayerHead")) {
      OnPlayerTouched();
    }
  }

  void OnPlayerTouched () {
    isDying = true;
    gameObject.tag = "DyingJelly";
    InvokeRepeating("SetRandomSprite", 0.0f, 0.05f);
    Invoke("DestroyItself", 1.0f);

    GetComponent<AudioSource>().Play();
  }

  void SetRandomSprite () {
    var newSprite = ActiveSprites[Random.Range(0, ActiveSprites.Length)];

    SpriteRenderer sr = (SpriteRenderer) renderer;
    sr.sprite = newSprite;
  }

  void DestroyItself () {
    Destroy(gameObject);
  }
}
