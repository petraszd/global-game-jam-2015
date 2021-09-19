using UnityEngine;
using System.Collections;

public class FishController : MonoBehaviour {

  public FishBodyStates Body;

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "PlayerHead") {
      gameObject.tag = "DyingFish";
      Body.TurnDead();
      GetComponent<RandomMoving>().enabled = false;
      GetComponent<MoveDown>().enabled = true;
    }
  }
}
