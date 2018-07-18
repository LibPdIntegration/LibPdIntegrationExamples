// Proximity2Float.cs - Script to send a float to a PD patch determined by the
//						player's proximity to a specific GameObject.
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

/// Script to send a float to a PD patch determined by the player's proximity to
/// a specific GameObject.
public class Proximity2Float : MonoBehaviour {

	// The Pd patch we'll be communicating with.
	public LibPdInstance pdPatch;
	// We'll use the transform of the red sphere to judge the player's proximity.
	public Transform sphereTransform;
	
	/// All our calculations for this class take place in MonoBehaviour's
	/// Update() function.
	void Update () {
		//Get the distance between the sphere and the main camera.
		float proximity = Vector3.Distance(sphereTransform.position, Camera.main.transform.position);

		//We want proximity to be in the range 0 -> 1.
		//Since our blue circle has a diameter of 15, its radius will be 7.5,
		//hence the following scaling.
		proximity /= 7.5f;

		//We also want the pitch to increase as we get closer to the sphere,
		//so we invert proximity.
		proximity = 1.0f - proximity;
		
		if(proximity < 0.0f)
			proximity = 0.0f;

		//Send our frequency value to the PD patch.
		//Like in Button2Bang.cs/ButtonExample.pd, all we need to be able to
		//send floats to our PD patch is a named receive object in the patch (in
		//this case, named proximity). We can then use the SendFloat() function
		//to send our float value to that named receive object.
		//
		//See the FloatExample.pd patch for details.
		pdPatch.SendFloat("proximity", proximity);
	}
}
