SpatialiseScene Notes
---------------------
This scene demonstrates a couple of methods for spatialising Pure Data patches
in Unity. 


Working with Unity's default spatialisation
-------------------------------------------

This method makes use of Unity's default audio spatialisation framework. It is a
bit more involved than the alternative approach, but doesn't an external
spatial audio framework.

The first thing to note is that, by default, Unity only spatialises
AudioSources. It won't apply spatialisation to any other audio producing
Components (like LibPdInstance). In order to get around that limitation, we'll
need to cheat the system a bit.

After adding an AudioSource and LibPdInstance to a GameObject in your scene,
the steps you'll need to take to spatialise your PD patch are:

1.) Set the AudioSource's AudioClip to the included SpatialiserFix.wav file.
2.) Set the AudioSource to Play On Awake and Loop.
3.) Adjust the Spatial Blend slider to 1 (3D).
4.) In your PD patch, multiply the 2 outputs of an adc~ object by the output of
    your patch before feeding it to the dac~ object.

This process will ensure that the spatialisation applied to the AudioSource gets
applied to the output of your patch. See FilteredNoise-ADC.pd for more
information.


Working with spatializer plugins
--------------------------------

Alternatively you can work with a custom spatialisation framework, using a
Spatializer Plugin. Spatializer Plugins make implementation a bit simpler, but
they do require you to import a spatialisation framework into your project. The
following example uses Steam Audio, but the steps are identical regardless of
which framework you use.

It should be noted that Spatializer Plugins are almost always intended for VR
(i.e. headphone) use. So this approach may not make sense if you're building a
project intended for speaker output.

For the project:
1.) Edit -> Project Settings... -> Audio.
2.) Set Spatializer Plugin to Steam Audio Spatializer.

For each Audio Source in the scene:
1.) Toggle 'Spatialize' and 'Spatialize Post Effect' on.
