using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
	public float movePower = 10f;
	public float KickBoardMovePower = 15f;
	public float jumpPower = 20f; //Set Gravity Scale in Rigidbody2D Component to 5
    public float maxSpeed = 10f;

    private Rigidbody2D rb;
	private Animator anim;
	Vector3 movement;
	private int direction = 1;
	bool isJumping = false;
	private bool alive = true;
	private bool isKickboard = false;

	private bool isShooting = false;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		Restart();
		if (alive)
		{
			Hurt();
			Attack();
			Jump();
			KickBoard();
			Run();

		}
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{

        anim.SetBool("isJump", false);

        if (other.tag == "DeathZone")
        {
            Debug.Log("t'est dans la zone");
            Died();
        }
    }
	void KickBoard()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4) && isKickboard)
		{
			isKickboard = false;
			anim.SetBool("isKickBoard", false);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4) && !isKickboard)
		{
			isKickboard = true;
			anim.SetBool("isKickBoard", true);
		}

	}

	void Run()
	{
		if (!isKickboard)
		{
			Vector3 moveVelocity = Vector3.zero;
			anim.SetBool("isRun", false);


			if (Input.GetAxisRaw("Horizontal") < 0)
			{
				direction = -1;
				moveVelocity = Vector3.left;

				transform.localScale = new Vector3(direction, 1, 1);
				if (!anim.GetBool("isJump"))
					anim.SetBool("isRun", true);

			}
			if (Input.GetAxisRaw("Horizontal") > 0)
			{
				direction = 1;
				moveVelocity = Vector3.right;

				transform.localScale = new Vector3(direction, 1, 1);
				if (!anim.GetBool("isJump"))
					anim.SetBool("isRun", true);

			}
			transform.position += moveVelocity * movePower * Time.deltaTime;

		}
		if (isKickboard)
		{
			Vector3 moveVelocity = Vector3.zero;
			if (Input.GetAxisRaw("Horizontal") < 0)
			{
				direction = -1;
				moveVelocity = Vector3.left;

				transform.localScale = new Vector3(direction, 1, 1);
			}
			if (Input.GetAxisRaw("Horizontal") > 0)
			{
				direction = 1;
				moveVelocity = Vector3.right;

				transform.localScale = new Vector3(direction, 1, 1);
			}
			transform.position += moveVelocity * KickBoardMovePower * Time.deltaTime;
		}
	}
	void Jump()
	{
		if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
		&& !anim.GetBool("isJump"))
		{
			isJumping = true;
			anim.SetBool("isJump", true);
		}
		if (!isJumping)
		{
			return;
		}

		rb.velocity = Vector2.zero;

		Vector2 jumpVelocity = new Vector2(0, jumpPower);
		rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

		isJumping = false;
	}
	void Attack()
	{
		if (Input.GetMouseButtonDown(0))
		{
			anim.SetTrigger("PrepareShooting");
		}
		else if (Input.GetMouseButtonUp(0))
		{
			anim.SetTrigger("Shoot");
		}
	}
	void Hurt()
	{
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			anim.SetTrigger("hurt");
			if (direction == 1)
				rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
			else
				rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
		}
	}
	public void Die()
	{
		isKickboard = false;
		anim.SetBool("isKickBoard", false);
		anim.SetTrigger("die");
		alive = false;
	}
	void Restart()
	{
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			isKickboard = false;
			anim.SetBool("isKickBoard", false);
			anim.SetTrigger("idle");
			alive = true;
		}
	}

    public void Died()
    {
        {
            isKickboard = false;
            anim.SetBool("isKickBoard", false);
            anim.SetTrigger("die");
            alive = false;
        }
    }
}