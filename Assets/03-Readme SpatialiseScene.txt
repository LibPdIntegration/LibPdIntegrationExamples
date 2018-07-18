SpatialiseScene Notes
---------------------
This scene demonstrates how to spatialise PD patches within Unity. Due to the
way Unity spatialises sound, this is a slightly involved process.

The first thing to note is that, by default, Unity only spatialises
AudioSources. It won't apply spatialisation to any other audio producing
Components (like LibPdInstance). In order to get around that limitation, we'll
need to cheat the system a bit.

After adding an AudioSource and LibPdInstance to a GameObject in your scene, the
steps you'll need to take to spatialise your PD patch are:

1.) Set the AudioSource's AudioClip to the included SpatialiserFix.wav file.
2.) Set the AudioSource to Play On Awake and Loop.
3.) Adjust the Spatial Blend slider to 1 (3D).
4.) In your PD patch, multiply the 2 outputs of an adc~ object by the output of
    your patch before feeding it to the dac~ object.

This process will ensure that the spatialisation applied to the AudioSource gets
applied to the output of your patch. See FilteredNoise.pd for more information.


Conversely, if you don't want to apply any spatialisation to your patch, you
don't need to do any of the aforementioned steps.


Alternative Approach
--------------------
An alternative, less-involved approach is to use Unity's OculusSpatializer
plugin. When I tested it, this seemed to apply some odd filtering to the sound,
so it's not used in this project, but you may prefer this approach.

To use the OculusSpatializer plugin, the steps are:

1.) Edit -> Project Settings... -> Audio.
2.) Set Spatializer Plugin to OculusSpatializer.

This will spatialise all audio for you, without any need for the aforementioned
SpatialiserFix file and adc~ object in each patch.
