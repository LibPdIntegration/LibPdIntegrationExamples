// PlayerMovement.cs - Simple first person movement script.
// -----------------------------------------------------------------------------
// Copyright (c) 2018 Niall Moody
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// -----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Simple first person movement script, included so we don't need to include
/// any Standard Assets (they're far heavier than we need).
public class PlayerMovement : MonoBehaviour
{
	/// Used to implement mouselook on the vertical axis.
	public Camera playerCamera;

	/// Used to set the player's rotation around the y-axis.
	private float playerRotation;
	/// Used to implement mouselook on the vertical axis.
	private float viewY;

	/// Used to let the player jump.
	private float jumpAmount;

	/// We use this to hide the mouse cursor.
	void Start()
	{
		Cursor.visible = false;
	}
	
	/// This is where we move the Player object and Camera.
	void Update()
	{
		//Get our current WASD speed.
		Vector3 strafe = new Vector3(Input.GetAxis("Horizontal") * 10.0f, 0.0f, 0.0f);
		float forwardSpeed = Input.GetAxis("Vertical") * 10.0f;

		//Get our current mouse/camera rotation.
		playerRotation = Input.GetAxis("Mouse X") * 6.0f;
		viewY = Input.GetAxis("Mouse Y") * 4.0f;

		//Don't let the player rotate the camera more than 90 degrees on the
		//y-axis.
		viewY = Mathf.Clamp(viewY, -90.0f, 90.0f);

		//Rotate the camera up/down.
		playerCamera.transform.Rotate(new Vector3(-viewY, 0.0f, 0.0f));

		//Rotate player (clamped so they can't move so fast they make themselves
		//sick).
		Mathf.Clamp(playerRotation, -5.0f, 5.0f);
		transform.Rotate(0.0f, playerRotation, 0.0f);

		//Jump player.
		CharacterController controller = GetComponent<CharacterController>();
		Vector3 jumpVector = Vector3.zero;

		//If the player is holding the jump button down, AND they're not yet
		//jumping AND on the ground, OR they are jumping but they've not reached
		//the top of the jump, increase their jumpAmount and move them
		//accordingly on the y-axis.
		if(Input.GetButton("Jump"))
		{
			if(((jumpAmount <= 0.0f) && controller.isGrounded) ||
			   ((jumpAmount > 0.0f) && (jumpAmount < 1.0f)))
			{
				jumpAmount += Time.deltaTime * 5.0f;

				jumpVector.y = 4.0f + ((1.0f - jumpAmount) * 20.0f);
			}
		}
		//Otherwise, if they're on the ground but their jumpAmount is not 0,
		//reset it.
		else if((jumpAmount > 0.0f) && controller.isGrounded)
		{
			jumpAmount = 0.0f;
		}

		//Move player.
		Vector3 moveDirection = Vector3.zero;

		//Set the player's direction based on the direction of the mouse and
		//which WASD keys they're pressing.
		moveDirection = transform.rotation * ((Vector3.forward * forwardSpeed) + strafe);
		moveDirection.y = jumpVector.y;

		//Apply gravity to the player's y-axis.
		moveDirection.y -= 6.0f;
		
		//Finally, apply the updated direction to the player's Controller (this
		//will figure out any collisions with the ground, other objects, etc.).
		controller.Move(moveDirection * Time.deltaTime);
	}
}
