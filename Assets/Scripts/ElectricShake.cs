using UnityEngine;
using System.Collections;

public class ElectricShake : MonoBehaviour {

	void Start ()
  {
    InvokeRepeating("Shake", 0.05f, 0.05f);
	}

  void Shake ()
  {
    transform.eulerAngles = new Vector3(
        0.0f, 0.0f,
        Random.Range(-5.0f, 5.0f));
  }
}
