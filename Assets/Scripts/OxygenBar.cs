using UnityEngine;
using System.Collections;

public class OxygenBar : MonoBehaviour
{
  public Transform Bar;
  public SpriteRenderer Face;
  public Sprite[] FaceStages;

  void OnEnable() {
    PlayerController.OnOxygenChanged += OnPlayerOxygenChanged;
  }

  void OnDisable() {
    PlayerController.OnOxygenChanged -= OnPlayerOxygenChanged;
  }

  void OnPlayerOxygenChanged (int old, int current, int stage)
  {
    float scale = current / 100.0f;
    Bar.localScale = new Vector3(1.0f, scale, 1.0f);

    Face.sprite = FaceStages[stage];
  }
}
