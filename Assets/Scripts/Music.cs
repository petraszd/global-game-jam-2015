using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
  public static Music instance;

  public AudioClip Start;
  public AudioClip Level;

  void Awake()
  {
    if (instance != null && instance != this) {
      Destroy( this.gameObject );
      return;
    } else {
      instance = this;
    }
    DontDestroyOnLoad(gameObject);
  }

  void OnLevelWasLoaded(int level)
  {
    if (level == 1) {
      audio.Stop();
      audio.clip = Level;
      audio.Play();
    } else if (level == 3) {
      audio.Stop();
      audio.clip = Start;
      audio.Play();
    }
  }
}
