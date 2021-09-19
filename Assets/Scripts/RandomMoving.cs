using UnityEngine;
using System.Collections;

public class RandomMoving : MonoBehaviour {
  public float SpeedMin;
  public float SpeedMax;

  public float ChangeDirTimeoutMin;
  public float ChangeDirTimeoutMax;

  private Vector3 direction;
  private float speed;

  void Start ()
  {
    SetRandomDirectionAndSpeed();
  }

	void Update ()
  {
    transform.position = transform.position +
      direction * Time.deltaTime * speed;
	}

  void SetRandomDirectionAndSpeed ()
  {
    Vector2 random = Random.insideUnitCircle;
    random.Normalize();

    direction = new Vector3(random.x, random.y, 0.0f);

    speed = Random.Range(SpeedMin, SpeedMax);

    Invoke("SetRandomDirectionAndSpeed",
        Random.Range(ChangeDirTimeoutMin, ChangeDirTimeoutMax));
  }
}
