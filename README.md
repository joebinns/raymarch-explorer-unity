# Raymarch Explorer in Unity
3D fractal explorer prototype in Unity. Prototyping a method for deep zoom functionalities by passing minimum distance values from the compute shader to a C# script. The minimum distance value is used to determine appropriate velocities.<br/>

The depth of the fractal is restricted by data-type precision. In order to achieve true deep zoom functionality, Arbritrary Preicision Arithmetics is required. Unfortunately, this task does not seem well suited to Unity due to inbuilt limitations on the transform components data-type. The project has therefore been moved to the Lightweight Java Game Library (LWJGL).<br/>

Checkout the LWJGL repository [here](https://github.com/joebinns/raymarch-explorer-lwjgl).

Developed from [Sebastian Lague's Ray-Marching repository](https://github.com/SebLague/Ray-Marching). 
