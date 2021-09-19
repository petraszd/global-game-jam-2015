using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
  SpriteRenderer spriteRenderer;
  bool isPlayerDead;

	void Start ()
  {
    spriteRenderer = (SpriteRenderer) renderer;
    isPlayerDead = false;
	}

  void OnEnable() {
    PlayerController.OnKilled += OnPlayerKilled;
  }

  void OnDisable() {
    PlayerController.OnKilled -= OnPlayerKilled;
  }

  void Update ()
  {
    if (isPlayerDead && Input.anyKeyDown) {
      Application.LoadLevel("Gameplay");
    }
  }

  void OnPlayerKilled ()
  {
    Invoke("AllowToRestart", 2.0f);
  }

  void AllowToRestart ()
  {
    spriteRenderer.enabled = true;
    isPlayerDead = true;
  }
}
