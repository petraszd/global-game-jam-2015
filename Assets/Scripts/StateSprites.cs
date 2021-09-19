using UnityEngine;
using System.Collections;

public class StateSprites : MonoBehaviour {
  public Sprite[] Sprites;

  void OnEnable() {
    PlayerController.OnOxygenChanged += OnPlayerOxygenChanged;
  }

  void OnDisable() {
    PlayerController.OnOxygenChanged -= OnPlayerOxygenChanged;
  }

  void OnPlayerOxygenChanged (int old, int current, int stage)
  {
    SpriteRenderer sr = (SpriteRenderer) renderer;
    sr.sprite = Sprites[stage];
  }
}
