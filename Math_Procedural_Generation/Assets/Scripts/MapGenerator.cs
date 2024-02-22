using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    public float[,] NoiseMap;

    // Start is called before the first frame update
    public void CreateNoiseMap(in float _finalPow, in List<float> _scale, in uint _width, in uint _height, Vector2 _pos)
    {
        NoiseMap = Noise.GenerateNoiseMap(_scale[0], _width, _height, _pos);
        for (int i = 1; i < _scale.Count; i++)
        {
            float[,] tmpNoise = Noise.GenerateNoiseMap(_scale[i], _width, _height, _pos);
            for (uint y = 0U; y < _height; ++y)
            {
                for (uint x = 0U; x < _width; ++x)
                {
                    NoiseMap[x, y] += tmpNoise[x, y] / Mathf.Pow(2f, i);
                }
            }
        }

        float amplitudeSum = 1f;
        for (int i = 1; i < _scale.Count; i++)
        {
            amplitudeSum += 1f / Mathf.Pow(2f, i);
        }

        for (uint y = 0U; y < _height; ++y)
        {
            for (uint x = 0U; x < _width; ++x)
            {
                NoiseMap[x, y] /= amplitudeSum;
                NoiseMap[x, y] = Mathf.Pow(NoiseMap[x, y], _finalPow);
            }
        }
    }
}
