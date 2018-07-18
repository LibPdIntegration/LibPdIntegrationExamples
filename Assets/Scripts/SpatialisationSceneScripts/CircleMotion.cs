// CircleMotion.cs - Script used to move an object in a circle.
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

/// Script used to move an object in a circle.
public class CircleMotion : MonoBehaviour {

	/// The Transform of the GameObject we're going to move in a circle.
	public Transform objectToMove;
	/// The radius of the circle we're going to move the object in.
	[Range(0.1f, 50.0f)]
	public float radius = 10.0f;

	/// Where we are in the circle right now.
	private float circleIndex;
	/// The circle's centre position.
	private Vector3 centrePos;

	/// Used to setup centrePos.
	void Start () {
		//These lines calculate the centre of the circle we're going to move the
		//object in. We assume the developer has placed the object at the
		//12 o'clock position of the circle, so the centre position is its
		//position - the circle's radius on the z-axis.
		centrePos = objectToMove.transform.position;
		centrePos.z -= radius;
	}
	
	/// Move the object along its path.
	void Update () {
		Vector3 pos = centrePos;

		//Update circleIndex to move the object further around the circle.
		circleIndex += 0.01f;
		if(circleIndex > (2.0f * Mathf.PI))
			circleIndex -= 2.0f * Mathf.PI;

		//We calculate the object's position by feeding circleIndex into the
		//Sin function for it's x-axis, and Cos for it's z-axis.
		pos.x += Mathf.Sin(circleIndex) * radius;
		pos.z += Mathf.Cos(circleIndex) * radius;

		//Finally, apply our updated position to the object's Transform.
		objectToMove.transform.position = pos;
	}
}
