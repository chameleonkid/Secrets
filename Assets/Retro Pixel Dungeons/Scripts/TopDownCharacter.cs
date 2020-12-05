using UnityEngine;
using System.Collections;

public class TopDownCharacter : MonoBehaviour {
	//This is a simple top-down perspective character script, which includes directional/diagonal movement.
	//Note that the character should be on the same Sorting Layer as the scenery objects.
	//This script requires a rigidbody and collider component, as it uses physics force to move. Refer to the "Wisp" prefab for ideal rigidbody values. Gravity scale must be set to 0!

	public float minMoveValue = 0.1f; //Minimum value that axis movement needs to be for the character to move.
	public float moveSpeed = 6.0f; //Move speed value in all directions. Value is later multiplied by below variable; pixels per unit.
	public int pixelsPerUnit = 32; //Amount of pixels per unit in Unity.
	private float speed; //Internal/private final speed value; moveSpeed multiplied by pixelsPerUnit.
    
	private Vector3 newPos = new Vector3(0.0f, 0.0f, 0.0f); //The new position vector which we pass to the object's actual position.

	void FixedUpdate() {
		speed = moveSpeed * pixelsPerUnit; //Multiply speed by unit size. Note that this does not mean, for instance, 1 unit per second, due to rigidbody mass and drag affecting speed.

		//Horizontal movement.
		if (Input.GetAxisRaw("Horizontal") > minMoveValue) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(speed,0) * Time.deltaTime);
		}
		else if (Input.GetAxisRaw("Horizontal") < -minMoveValue) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed,0) * Time.deltaTime);
		}

		//Vertical movement.
		if (Input.GetAxisRaw("Vertical") > minMoveValue) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,speed) * Time.deltaTime);
		}
		else if (Input.GetAxisRaw("Vertical") < -minMoveValue) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-speed) * Time.deltaTime);
		}
	}
	
	void Update () {
		newPos = new Vector3(transform.position.x, transform.position.y);
		transform.position = newPos;
	}
}
