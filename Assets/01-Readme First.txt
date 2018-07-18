LibPd Unity Integration Examples
--------------------------------
This project is intended to demonstrate how to use the LibPdIntegration wrapper
for LibPd in Unity. It is split into different scenes which each demonstrate a
different aspect of the wrapper, and what you might want to do with it.

At the time of writing, there are 2 scenes:
- A Spatialisation scene, demonstrating how to spatialise PD patches within
  Unity.
- A Unity -> LibPd scene, demonstrating how to communicate with a PD patch from
  Unity.

More scenes will be added as the project develops.


How LibPdIntegration Works:
---------------------------
This wrapper revolves a single C# script (LibPdInstance.cs), which communicates
with the native libpd plugin. At the time of writing, there are plugins included
for OSX and Windows (32-bit & 64-bit). Time and resources permitting, more
platforms will be supported in future.

Incorporating a PD patch into your Unity project requires you to add 2
Components to a GameObject:

1.) An AudioSource Component. This is necessary for Unity to perform audio
    processing on the GameObject. Without it your PD patch will not be run.
2.) The LibPdInstance script. You can then give the script a reference to your
    PD patch in the Inspector.

Note that the order of these Components matters. The AudioSource must come
before LibPdInstance.

See the example scenes for more information.


Project Structure
-----------------
All assets are organised into folders categorised by type. As there are multiple
scenes in this project, these can be found in the Scenes folder. Assets that are
unique to a specific scene will reside in subfolders specifying the scene they
belong to.


PD Patch Location
-----------------
Due to the way Unity handles assets, all PD patches must reside within the
Assets/StreamingAssets/PdAssets folder (you can implement further subfolders
within that folder however).


- Niall Moody (11/07/18).
