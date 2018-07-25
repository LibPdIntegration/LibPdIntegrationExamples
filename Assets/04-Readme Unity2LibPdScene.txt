Unity2LibPdScene Notes
----------------------
This scene demonstrates how to communicate from Unity to LibPd.

At the time of writing, the scene provides examples for sending bangs and floats
to a PD patch. These are by far the simplest ways to communicate with a PD
patch, and use the SendBang() and SendFloat() functions provided by
LibPdInstance.

When called, SendBang() and SendFloat() will both send a bang or a float
(respectively) to a named receive object in the PD patch.


Steps necessary for sending a bang to a patch:

1.) In your patch, add a receive object, and give it a name (e.g. receive test).
2.) In the C# script you will use to trigger the bang, call SendBang() with
    the name of the receive object as its parameter
    (e.g. pdPatch.SendBang("test");).


Steps necessary for sending a float to a patch:

1.) In your patch, add a receive object, and give it a name (e.g. receive test).
2.) In the C# script you will use to trigger the float, call SendFloat() with
    the name of the receive object as its first parameter, and the float value
    you want to send as its second parameter
    (e.g. pdPatch.SendFloat("test", 1.0f);).


More examples of sending data to LibPd will be added in future. Until then you
can look at the documentation for LibPdInstance.cs, or check out the sister
LibPdIntegrationTests project, which is used to ensure the wrapper is
functioning correctly and thus implements all possible LibPdInstance functions.
