using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
  public delegate void OxygenChangeEvent(int old, int current, int stage);
  public static event OxygenChangeEvent OnOxygenChanged;

  public delegate void KilledEvent();
  public static event KilledEvent OnKilled;

  public Animator LeftHandAnimator;
  public Animator RightHandAnimator;
  public Transform Body;
  public Transform Head;
  public Transform OnJelly;
  public Transform Normal;

  private const int N_OXYGEN_STAGES = 4;
  private const int OXYGEN_FISH_DELTA = 20;
  private const int OXYGEN_JELLY_DELTA = -50;
  private const float OXYGEN_LOOSE_TIMER = 0.25f;
  private const int OXYGEN_TIMER_DELTA = -1;

  //private const float FORCE_X = 20.0f;
  //private const float FORCE_Y = 30.0f;

  private const float FORCE_X = 30.0f;
  private const float FORCE_Y = 30.0f;

  //private const float FORCE_X = 20.0f;
  //private const float FORCE_Y = 120.0f;

  private const float FORCE_TIME_DELTA = 0.32f;

  private const float ALLOW_TIMEOUT_DELTA = 0.55f;
  private const float TURN_TO_NORMAL_TIMEOUT = 1.0f;

  private const float ROTATE_SIDE_SPEED = 4.0f;
  private const float ROTATE_RESET_SPEED = 2.0f;
  private const float ROTATE_INITIAL_DELTA = 20.0f;

  private int oxygen;
  private float bodyRotate;
  private bool allowLeft;
  private bool allowRight;

  void Start ()
  {
    oxygen = 100;
    bodyRotate = 0.0f;
    allowLeft = true;
    allowRight = true;

    InvokeRepeating("LooseOxygen", OXYGEN_LOOSE_TIMER, OXYGEN_LOOSE_TIMER);
  }

  void Update ()
  {
    float current = Body.transform.eulerAngles.z;
    float newZ = Mathf.LerpAngle(
        current, bodyRotate, Time.deltaTime * ROTATE_SIDE_SPEED);

    Body.transform.eulerAngles = new Vector3(0.0f, 0.0f, newZ);

    bodyRotate = Mathf.Lerp(bodyRotate, 0.0f,
        Time.deltaTime * ROTATE_RESET_SPEED);
  }

  void FixedUpdate ()
  {
    if (allowLeft && IsLeftDown()) {
      LeftHandAnimator.SetTrigger("Wave");
      allowLeft = false;
      Invoke("AddLeftForce", FORCE_TIME_DELTA);
      Invoke("TurnOnAllowLeft", ALLOW_TIMEOUT_DELTA);
    }
    if (allowRight && IsRightDown()) {
      RightHandAnimator.SetTrigger("Wave");
      allowRight = false;
      Invoke("AddRightForce", FORCE_TIME_DELTA);
      Invoke("TurnOnAllowRight", ALLOW_TIMEOUT_DELTA);
    }
  }

  void LooseOxygen ()
  {
    ChangeOxygen(OXYGEN_TIMER_DELTA);
  }

  bool IsLeftDown ()
  {
    if (Input.touchCount > 0) {
      foreach (Touch touch in Input.touches) {
        if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2) {
          return true;
        }
      }
      return false;
    }

    return Input.GetKeyDown(KeyCode.LeftArrow) ||
      Input.GetKeyDown(KeyCode.A) ||
      Input.GetMouseButtonDown(0);
  }

  bool IsRightDown ()
  {
    if (Input.touchCount > 0) {
      foreach (Touch touch in Input.touches) {
        if (touch.phase == TouchPhase.Began && touch.position.x >= Screen.width / 2) {
          return true;
        }
      }
      return false;
    }

    return Input.GetKeyDown(KeyCode.RightArrow) ||
      Input.GetKeyDown(KeyCode.D) ||
      Input.GetMouseButtonDown(1);
  }

  void AddRightForce ()
  {
    UpdateBodyRotate(-ROTATE_INITIAL_DELTA);
    AddDirectionalForce(new Vector2(FORCE_X, FORCE_Y));
  }

  void AddLeftForce ()
  {
    UpdateBodyRotate(ROTATE_INITIAL_DELTA);
    AddDirectionalForce(new Vector2(-FORCE_X, FORCE_Y));
  }

  void TurnOnAllowLeft () {
    allowLeft = true;
  }

  void TurnOnAllowRight () {
    allowRight = true;
  }

  void AddDirectionalForce (Vector2 force)
  {
    GetComponent<AudioSource>().Play();
    rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    BubbleGenerator.GenerateBubbles(Head.position, 1, 3);
  }

  void UpdateBodyRotate (float delta)
  {
    bodyRotate += delta;
    bodyRotate = Mathf.Clamp(bodyRotate, -ROTATE_INITIAL_DELTA,
        ROTATE_INITIAL_DELTA);
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Jelly") {
      ChangeOxygen(OXYGEN_JELLY_DELTA);
      ActivateOnJelly();
      Invoke("ActivateNormal", TURN_TO_NORMAL_TIMEOUT);
    } else if (other.gameObject.tag == "Finish") {
      Application.LoadLevel("Finish");
    }
  }

  public bool EatFish ()
  {
    if (enabled) {
      ChangeOxygen(OXYGEN_FISH_DELTA);
      return true;
    }
    return false;
  }

  void ChangeOxygen (int delta)
  {
    var old = oxygen;
    oxygen += delta;

    if (delta < 0) {
      BubbleGenerator.GenerateBubbles(Head.position, 0, 1);
    }

    oxygen = Mathf.Clamp(oxygen, 0, 100);

    SignalOxygenChange(old, oxygen);

    if (oxygen <= 0) {
      SignalPlayerKilled();
      StopActing();
    }
  }

  void SignalOxygenChange (int old, int current)
  {
    if (OnOxygenChanged != null) {
      OnOxygenChanged(old, current, GetOxygenStage());
    }
  }

  void SignalPlayerKilled ()
  {
    if (OnKilled != null) {
      OnKilled();
    }
  }

  void StopActing ()
  {
    enabled = false;
    Head.gameObject.tag = "Player";
  }

  int GetOxygenStage ()
  {
    int temp = (int) (Mathf.Ceil(oxygen * N_OXYGEN_STAGES / 100.0f) - 1.0f);
    return (N_OXYGEN_STAGES - temp - 1);
  }

  void ActivateOnJelly ()
  {
    OnJelly.gameObject.SetActive(true);
    NormalRenderers(false);
  }

  void ActivateNormal ()
  {
    OnJelly.gameObject.SetActive(false);
    NormalRenderers(true);
  }

  void NormalRenderers (bool enabled)
  {
    SpriteRenderer[] renderers = Normal.GetComponentsInChildren<SpriteRenderer>();
    foreach (var r in renderers) {
      r.enabled = enabled;
    }
  }
}
