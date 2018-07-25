SpatialiseScene Notes
---------------------
This scene demonstrates a couple of methods for spatialising Pure Data patches
in Unity. 


OculusSpatializer Plugin Method
-------------------------------

This is the simplest method of spatialising patches in Unity. It requires the
following steps:

For the project:
1.) Edit -> Project Settings... -> Audio.
2.) Set Spatializer Plugin to OculusSpatializer.

For each Audio Source in the scene:
1.) Toggle 'Spatialize' and 'Spatialize Post Effect' on.

In testing it, the OculusSpatializer plugin seems to attenuate the sound quite
aggressively, so an alternative approach is also included.


Alternative Method
------------------

This method makes use of Unity's default audio spatialisation framework. It is a
bit more involved than the OculusSpatializer approach, but doesn't attenuate the
sound as aggressively.

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
