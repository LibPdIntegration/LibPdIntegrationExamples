// EnvelopeFollower.cs - Script to set the y-axis position of a GameObject based
//						 an amplitude value sent to use from PD.
// -----------------------------------------------------------------------------
// Copyright (c) 2020 Niall Moody
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

using UnityEngine;

/// Script to set the y-axis position of a GameObject based an amplitude value sent to use from PD.
public class EnvelopeFollower : MonoBehaviour
{
	/// The PD patch we are going to listen to.
	public LibPdInstance pdPatch;

	///	The Transform of the GameObject we are going to move.
	public Transform moveObject;

    ///	We need to tell pdPatch which PD send object we want to listen to.
    void Start()
    {
        //Bind to the named send object.
		pdPatch.Bind("AmplitudeEnvelope");
    }

	///	Clean up after ourselves and unbind from the AmplitudeEnvelope object.
	void OnDestroy()
	{
		//Unbind from the named send object.
		pdPatch.UnBind("AmplitudeEnvelope");
	}

	///	Our receive function. This will be called whenever a bound send object
	///	in our PD patch fires.
	public void FloatReceive(string sender, float value)
	{
		//This function will get called for *every* Float event sent by our
		//patch, so we need to make sure we're only acting on the
		//*AmplitudeEnvelope* event that we're actually interested in.
		if(sender == "AmplitudeEnvelope")
		{
			moveObject.position = new Vector3(moveObject.position.x,
											  0.5f + value,
											  moveObject.position.z);
		}
	}
}
