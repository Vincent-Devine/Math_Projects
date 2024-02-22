using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
    static public float[,] GenerateNoiseMap(in float _scale, in uint _width, in uint _height, Vector2 _pos)
    {
        float[,] noiseMap = new float[_width,_height];
        for(uint y = 0U; y < _height; ++y)
        {
            for(uint x = 0U; x < _width; ++x)
            {
                noiseMap[x,y] = Mathf.PerlinNoise(x / (float)_width * _scale + _pos.x, y / (float)_height * _scale + _pos.y);
            }
        }
        return noiseMap;
    }
}
