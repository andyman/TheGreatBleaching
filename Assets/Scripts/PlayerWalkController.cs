using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkController : MonoBehaviour
{
	public Animator anim;
	public bool controllable = false;

	private bool jumpPressed = false;
	private bool jumpHeld = false;

	private bool grounded = true;
	private Vector2 inputAxis;

	public Transform player;
	public Rigidbody playerRb;

	private Camera cam;
	public float speed = 1.0f;
	public float jumpSpeed = 2.0f;
	public float fallGravityMultiplier = 3.0f;

	public BoxCollider safeBounds;

	public bool canJump = false;
	public bool landedJump = false;

	public LayerMask groundLayerMask;

	// Start is called before the first frame update
	void Start()
	{
		//anim.SetBool("sitting", false);
		anim.SetBool("grounded", true);

		cam = Camera.main;

	}

	public void SetControllable()
	{
		controllable = true;
	}
	public void SetUncontrollable()
	{
		controllable = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (controllable)
		{
			jumpPressed = canJump && Input.GetButtonDown("Jump");
			jumpHeld = canJump && Input.GetButton("Jump");

			inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			if (inputAxis.magnitude > 1.0f)
			{
				inputAxis.Normalize();
			}
		}
		else
		{
			jumpPressed = false;
			inputAxis = Vector2.zero;
			jumpHeld = false;
			landedJump = false;
		}

		anim.SetBool("grounded", grounded);
	}

	private void FixedUpdate()
	{
		if (safeBounds != null)
		{
			// keep in bounds
			Bounds bounds = safeBounds.bounds;
			if (!bounds.Contains(playerRb.position))
			{
				playerRb.MovePosition(bounds.ClosestPoint(playerRb.position));
			}
		}

		grounded = Physics.CheckSphere(transform.position + Vector3.up * 0.1f, 0.25f, groundLayerMask);

		Vector3 camForward = cam.transform.forward;
		camForward.y = 0.0f;
		camForward.Normalize();

		Quaternion camRot = Quaternion.LookRotation(camForward);

		Vector3 v = camRot * (Vector3.right * inputAxis.x + Vector3.forward * inputAxis.y) * speed;

		Vector3 vJump = v;
		vJump.y = playerRb.velocity.y;

		playerRb.velocity = vJump;

		Vector3 vAnim = v;
		vAnim.y = 0.0f;

		if (v.magnitude > 0.01f)
		{
			Vector3 newFacing = v.normalized;
			newFacing.y = 0.0f;
			newFacing.Normalize();

			Quaternion newRot = Quaternion.LookRotation(newFacing);
			Quaternion rot = playerRb.rotation;
			rot = Quaternion.Slerp(rot, newRot, Time.deltaTime * 10.0f);

			playerRb.MoveRotation(rot);

		}

		if (jumpPressed && grounded)
		{
			v = playerRb.velocity;
			v.y = jumpSpeed;

			playerRb.velocity = v;
			grounded = false;
			jumpPressed = false;
		}

		if (!grounded && !jumpHeld)
		{
			// fall faster when jump not held down
			v = playerRb.velocity;
			v.y += Physics.gravity.y * Time.deltaTime * fallGravityMultiplier;

			playerRb.velocity = v;
			landedJump = true;
		}


	}
}
