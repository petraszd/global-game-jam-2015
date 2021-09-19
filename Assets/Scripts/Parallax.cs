using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
  Vector3 originalPosition;

	void Start ()
  {
    originalPosition = transform.position;
	}

	void Update ()
  {
    Vector3 camPos = Camera.main.transform.position;

    Vector3 newPosition = originalPosition;
    newPosition.y = newPosition.y + camPos.y / 4.0f;

    transform.position = newPosition;
	}
}
