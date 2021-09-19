using UnityEngine;
using System.Collections;

public class MoveDown : MonoBehaviour {

	void Update ()
  {
    transform.Translate(Vector3.down * Time.deltaTime * 6.0f, Space.World);

    Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
    if (screenPos.y < 0) {
      Destroy(gameObject);
    }
	}
}
