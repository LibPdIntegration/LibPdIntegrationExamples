// Midi2Unity.cs - Script to visualise MIDI input from our PD patch.
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

/// <summary>Script to visualise MIDI input from our PD patch.</summary>
/// 
///	<remarks>This script does 2 things:
///	
/// 1.) Toggles note playback when the player enters/exits the trigger volume.
/// 
/// 2.) Visualises incoming MIDI notes from our PD patch.</remarks>
public class Midi2Unity : MonoBehaviour
{
	/// Our PD patch.
	public LibPdInstance pdPatch;

	///	Our 12 note lights.
	public GameObject[] lights;

	///	This will get called for every note on/off message we receive from our patch.
	public void NoteReceived(int channel, int note, int velocity)
	{
		int wrappedNote = note % 12;

		for(int i=0;i<lights.Length;++i)
			lights[i].SetActive(false);

		if(lights.Length > wrappedNote)
		{
			lights[wrappedNote].SetActive(true);
		}
	}

	///	Toggle note playback when player enters trigger volume.
	private void OnTriggerEnter(Collider other)
	{
		pdPatch.SendBang("Toggle");
	}

	///	Toggle note playback when player exits trigger volume.
	private void OnTriggerExit(Collider other)
	{
		for(int i=0;i<lights.Length;++i)
			lights[i].SetActive(false);

		pdPatch.SendBang("Toggle");
	}
}
