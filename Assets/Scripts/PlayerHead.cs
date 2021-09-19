using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour {
  public PlayerController controller;

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Fish") {
      if (controller.EatFish()) {
        other.transform.position = transform.position;
      }
    }
  }
}
