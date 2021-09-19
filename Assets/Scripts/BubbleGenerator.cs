using UnityEngine;
using System.Collections;

public class BubbleGenerator : MonoBehaviour {
  public GameObject Bubble;

  public static BubbleGenerator GetInstance ()
  {
    BubbleGenerator instance = Camera.main.GetComponent<BubbleGenerator>();
    return instance;
  }

  public static void GenerateBubbles (Vector3 position, int min, int max)
  {
    GetInstance().DoGenerateBubbles(position, min, max);
  }

	void Awake ()
  {
	}

	void Update ()
  {
	}

  public void DoGenerateBubbles (Vector3 position, int min, int max)
  {
    int count = Random.Range(min, max);
    for (int i = 0; i < count; ++i) {
      Instantiate(Bubble, position, Quaternion.identity);
    }
  }
}
