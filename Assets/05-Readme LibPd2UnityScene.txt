Unity2LibPdScene Notes
----------------------
This scene demonstrates how to communicate from libpd to Unity.

In our PD patch this is relatively simple: we just create a send object to send
data to Unity.


On the Unity end, however, things are a bit more complicated. The required steps
are:

1.) Bind() to the desired send object. libpd will not make send signals
    available to Unity by default. We need to tell it we are interested in
	specific signals. We do this using the Bind() function in LibPdInstance,
	e.g. pdPatch.Bind("BangTest"); (if BangTest is the name of our send object).

2.) Write a function that will be called whenever that send object sends a
    signal to us. The signature of that function will have to match the
	signature LibPdInstance is expecting. For instance, a function that reacts
	to bang signals would look like this:

	public void BangReceive(string name)

	And a function that reacts to float (or number) signals would look like
	this:

	public void FloatReceive(string name, float value)

	Where name is the name of the send object, and value is the float value that
	was sent from your PD patch.

	You can see a full list of function signatures on the wiki:
	https://github.com/LibPdIntegration/LibPdIntegration/wiki/libpd2unity#unityevents

3.) Connect your LibPdInstance Component to the above function. LibPdInstance
	uses Unity events for this. If you expand the Pure Data Events header you
	will see a list of 5 UnityEvent widgets, one for each of the signal types
	which can be sent from your PD patch.

	To connect to your function, you need to click the + button for your
	desired signal type, then drag the GameObject containing your receive
	script into the 'None (object)' box.

	Then, if you click the drop-down that says 'No Function', you will get a
	list of possible destinations. Select your receiving script/Component, and
	then, under the Dynamic header, select the receive function you created in
	the previous step.

	Note: You will see 2 copies of your function in that final menu: one under
	'Dynamic', one under 'Static'. It is important that you select the entry
	under 'Dynamic'; the one under 'Static' will not work for our purposes.
