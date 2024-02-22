# Procedural_generation
3rd year exercice that aims at learning how to procedurally generate a terrain using Perlin noise.<br/>

## Table of content ##
 - [Quick Start](#quick-start)
 - [How it works](#how-it-works)
 - [Technology](#technology)
 - [Credit](#credit)

## Quick Start ##
Unity project
1. Clone the project from git ``` git clone ```
2. Open the project
3. Open the scene ``` SampleScene ```
4. In the inspector of the ``` Plane ```, you can change value of the map generation (values that can be tweaked: height of the peaks, scale of the noise, position of the plane, size of the map)

## How it works ##
A 2D array of float is generated with Mathf.PerlinNoise included in Unity.
This array is converted to a Texture2D.
A grid mesh is generated of the size of the texture.
The grayscale of the texture is then used to generate the height of the mesh peaks.

## Technology ##
- Engine :  Unity 2022.3.4f1
- IDE : Visual Studio 2022
- Versionning : Git

## Credit ##
Exercice done at **ISART DIGITAL** <br>
Authors : Kristian GOUPIL, Vincent DEVINE <br>
Special thanks : Maël ADDOUM <br>
Project start : 29-11-2023 <br>
Project end : 01-12-2023 <br>
