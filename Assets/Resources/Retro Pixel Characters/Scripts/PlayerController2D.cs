using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour {
	private bool moveEnabled = true; //Bool used to check if movement is enabled or not. Use ToggleMovement() to set.
	[Range (0.0f, 10.0f)]
	[Tooltip("The movement speed of the controller.")]
	public float moveSpeed = 5.0f;
	private Vector2 moveVector; //The vector used to apply movement to the controller.
	private float origSpeed; //Temp variable that stores the original speed upon start. Used to set speed back when running stops.
	private float moveSense = 0.2f; //An axis value above this is considered movement.

	private enum MoveState { Stand, Walk, Run } //States for standing, walking and running.
	private MoveState moveState	= MoveState.Stand; //Create and set a MoveState variable for the controller.
	private Animator anim; //The parent animator.

	void Start()
	{
		origSpeed = moveSpeed;
		anim = transform.GetComponent<Animator>();
	}

	/// <summary>
	/// Toggles the controller's movement.
	/// </summary>
	/// <param name="enable">If set to <c>true</c>, enable movement. If set to <c>false</c>, disable movement.</param>
	public void ToggleMovement(bool enable)
	{
		moveEnabled = enable;
	}

	void FixedUpdate()
	{
		if (moveEnabled == true)
		{
			if (moveVector.x > moveSense || moveVector.x < -moveSense || moveVector.y > moveSense || moveVector.y < -moveSense)
			{
				transform.Translate(moveVector * (moveSpeed / 100)); //If movement is enabled and any movement above the threshold (sense) is detected, move controller.
			}
		}
	}

	void Update()
	{
		//Only check for movement if the movement bool is set to true.
		if (moveEnabled == true)
		{
			//Set the move vector to horizontal and vertical input axis values.
			moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

			//If horizontal or vertical axis is above the threshold value (moveSense), set the move state to Walk.
			if (Input.GetAxisRaw("Horizontal") > moveSense || Input.GetAxisRaw("Horizontal") < (-moveSense) || Input.GetAxisRaw("Vertical") > moveSense || Input.GetAxisRaw("Vertical") < (-moveSense))
			{
				moveState = MoveState.Walk;

				//Pass the moveVector axes to the animators move variables and set animator's isMoving to true.
				anim.SetFloat("moveX", moveVector.x);
				anim.SetFloat("moveY", moveVector.y);
				anim.SetBool("isMoving", true);
			}
			else
			{
				//If there's no input, set the state to stand again and change Animator's isMoving to false.
				moveState = MoveState.Stand;

				anim.SetBool("isMoving", false);
			}

			if (Input.GetButton("Fire3") && moveState == MoveState.Walk)
			{
				//If the controller is moving and we're holding the run button, double the moveSpeed and change to Run state. Also tell animator to display running animation.
				moveSpeed = origSpeed * 2;
				moveState = MoveState.Run;

				anim.SetBool("isRunning", true);
			}
			else if (Input.GetButtonUp("Fire3") || moveState == MoveState.Stand)
			{
				//Set the speed and Animator running bool back when we're not running.
				moveSpeed = origSpeed;

				anim.SetBool("isRunning", false);
			}
		}
	}
}
