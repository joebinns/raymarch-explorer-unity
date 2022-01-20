# Raymarch Explorer in Unity

Before you read on, **watch** [Raymarch Explorer in Unity: Mandelbulb Rendered in Real Time](https://youtu.be/3GsXkzbfNBo)!
[![Raymarch Explorer in Unity: Mandelbulb Rendered in Real Time](https://joebinns.com/documents/fake_thumbnails/raymarch_explorer_unity_thumbnail_time.png)](https://youtu.be/uKb3W-Hu32U "Raymarch Explorer in Unity: Mandelbulb Rendered in Real Time. Click to Watch")

Real-time 3D fractal explorer in Unity. Prototyping a method for deep zoom functionalities by passing minimum distance values from the compute shader to a C\# script. The minimum distance value is used to determine appropriate velocities and level of detail, such as to give the effect of zooming into the fractal surface.

The depth of the fractal is restricted by data-type precision. In order to achieve true deep zoom functionality, arbitrary precision arithmetics on the Graphics Processing Unit (GPU) is required. Unfortunately, this task does not seem well suited to Unity due to inbuilt limitations on the data-type of the transform component. The project has therefore been moved to the Lightweight Java Game Library (LWJGL).

Checkout the LWJGL repository [here](https://github.com/joebinns/raymarch-explorer-lwjgl).

Developed from [Sebastian Lague's Ray-Marching repository](https://github.com/SebLague/Ray-Marching). 
