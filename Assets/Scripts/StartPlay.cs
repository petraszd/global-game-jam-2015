using UnityEngine;
using System.Collections;

public class StartPlay : MonoBehaviour {
  public float Delay = 0.0f;

  bool isControllable;

	void Start ()
  {
    isControllable = false;
    Invoke("AllowToControl", Delay);
	}

	void Update ()
  {
    if (isControllable && Input.anyKeyDown) {
      Application.LoadLevel("Intro");
    }
	}

  void AllowToControl ()
  {
    isControllable = true;
  }
}
