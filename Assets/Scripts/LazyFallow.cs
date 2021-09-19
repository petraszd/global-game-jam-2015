using UnityEngine;
using System.Collections;

public class LazyFallow : MonoBehaviour {
  public Transform Fallowed;
  public Vector3 Delta;

	void LateUpdate ()
  {
    float otherWeight = Time.deltaTime * 2.0f;
    float selfWeight = 1.0f - otherWeight;

    Vector3 newPosition = transform.position * selfWeight +
      (Fallowed.position + Delta) * otherWeight;

    //Vector3 newPosition = Fallowed.position;

    newPosition.z = transform.position.z;
    newPosition.x = transform.position.x;

    transform.position = newPosition;
	}
}
