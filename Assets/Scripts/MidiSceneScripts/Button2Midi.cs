// Button2Midi.cs - Script to send a bang to a PD patch when the player enters
//					and leaves a collision volume.
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

/// Script to send a random MIDI note to a PD patch when the player enters and
/// leaves a collision volume.
public class Button2Midi : MonoBehaviour
{

	/// The PD patch we're going to communicate with.
	public LibPdInstance pdPatch;

	///	We need to store our random note so that we can send the corresponding
	///	note off message when the player exits the collision volume.
	private int note;

	/// We send a MIDI note when the player steps on the button (enters the
	/// collision volume).
	void OnTriggerEnter(Collider other)
	{
		//First, pick a random note. This should pick from an octave starting at
		//middle C.
		note = 60 + Mathf.FloorToInt(Random.value * 12.0f);

		//Now send our MIDI note on message to our PD patch.
		//SendMidiNoteOn's 3 arguments are: channel, note number, velocity
		pdPatch.SendMidiNoteOn(0, note, 127);
	}

	/// We send a note off message when the player steps off the button (leaves
	/// the collision volume).
	void OnTriggerExit(Collider other)
	{
		//Send a note off message to our patch when the player leaves the
		//button's collision volume.

		//Note that there is no dedicated 'SendMidiNoteOff' function. To send a
		//note off message we have to send a 'note on' message with a velocity
		//of 0.
		pdPatch.SendMidiNoteOn(0, note, 0);
	}
}
