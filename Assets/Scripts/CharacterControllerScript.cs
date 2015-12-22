using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool grounded = false;
	public Transform groundCheck;

	public Light pointLight2;

	public GameObject player;
	private float move = 0;
	private bool moving = false;
	float groundRadius = 0.4f;

	public LayerMask whatIsGround;
	private const int jumpForce = 1200;
	private float runningTimeLimit = 10f;
	private float stoppedTimeLimit = 4f;
	Animator anim;
	public Button btnRun;

	public Canvas gameOverCanvas;
	public Canvas UICanvas;

	// Use this for initialization
	void Start() {
		anim = GetComponent<Animator>();
		pointLight2.range = 8f;
	}

	public void jump() {
		if (grounded) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
			//anim.SetBool("Ground", false);
		}
	}

	void Update() {
		// jump
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		foreach (Touch t in Input.touches) {
			if (t.position.x <= (Screen.width / 2)) {
				if (t.phase == TouchPhase.Ended) {
					// If finger is lifted, stop running
					stop();
				} else {
					// If finger touches left side of screen, start running
					run();
				}
			} else {
				if (t.phase == TouchPhase.Began) {
					jump();
				}
			}
		}
		anim.SetFloat("speed", Mathf.Abs(move));
		if (moving) {
			if (move < 1) {
				move += (Time.deltaTime * 2.5f);
			} else {
				move = 1;
			}
		} else {
			if (move > 0) {
				move -= (Time.deltaTime * 3);
			} else {
				move = 0;
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate() {

		bool jumping = Input.GetKeyDown(KeyCode.Space);

		if (jumping) {
			jump();
		}

		if (anim.GetFloat("speed") > 0.01) {
            stoppedTimeLimit = 5f;

            if (runningTimeLimit > 1f) {
				// Decrease timeLimit
                if (pointLight2.range < 9) {
					pointLight2.range += .1f;
				}
				runningTimeLimit -= Time.deltaTime;
				// If Time limit reaches goal, increase light
				if (pointLight2.range >= 9 && pointLight2.range <= 20 && runningTimeLimit < 9f) {
					pointLight2.range +=.08f*2;
				}
			}
		}
		else {
			if(stoppedTimeLimit > 1f) {
				// Decrease timeLimit.
				runningTimeLimit = 10f;
				stoppedTimeLimit -= Time.deltaTime;
				// If Time limit reaches goal, decrease light
				if (stoppedTimeLimit <= 5.2f && pointLight2.range > 4f) { 
					pointLight2.range -= 0.1f;
				}
			}
		}
								 //anim.SetBool ("Ground", grounded);
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}

	public void run() {
		moving = true;
	}
	public void stop() {
		moving = false;
	}
}