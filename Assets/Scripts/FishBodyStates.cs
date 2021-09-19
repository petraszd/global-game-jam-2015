using UnityEngine;
using System.Collections;

public class FishBodyStates : MonoBehaviour {
  public Sprite Normal;
  public Sprite NearBy;
  public Sprite Dead;

  private bool isScared;

  void Start ()
  {
    isScared = false;
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    if (!isScared && other.gameObject.tag == "Player") {
      TurnScared();
    }
  }

  void UpdateSprite (Sprite sprite)
  {
    SpriteRenderer sr = (SpriteRenderer) renderer;
    sr.sprite = sprite;
  }

  public void TurnScared ()
  {
    isScared = true;
    BubbleGenerator.GenerateBubbles(transform.position, 1, 4);
    UpdateSprite(NearBy);
  }

  public void TurnDead ()
  {
    UpdateSprite(Dead);
    GetComponent<AudioSource>().Play();
  }
}
