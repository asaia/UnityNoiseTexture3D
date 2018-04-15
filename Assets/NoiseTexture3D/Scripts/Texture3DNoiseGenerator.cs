using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture3DNoiseGenerator : MonoBehaviour 
{
	[Header("Texture Settings")]
	[SerializeField] private int _textureSize = 32;
	[SerializeField] private TextureFormat _format = TextureFormat.RHalf;
	[Header("Noise Settings")]
	[SerializeField] [Range(1, 20)]	private int _octaves = 1;
	[SerializeField] [Range(1, 10)] private int _multiplier = 2;
	[SerializeField] [Range(0.0f, 15.0f)] private float _amplitude = 0.5f;
	[SerializeField] [Range(0.0f, 500.0f)] private float _lacunarity = 2.0f;
	[SerializeField] [Range(0.0f, 500.0f)] private float _persistence = 0.9f;

	public Texture3D GenerateNoise()
	{
		SimplexNoiseGenerator noise = new SimplexNoiseGenerator();
		Color[] colorArray = new Color[_textureSize * _textureSize * _textureSize];
        Texture3D texture = new Texture3D (_textureSize, _textureSize, _textureSize, _format, false);
        for (int x = 0; x < _textureSize; x++) 
		{
            for (int y = 0; y < _textureSize; y++) 
			{
                for (int z = 0; z < _textureSize; z++) 
				{
					float value = noise.coherentNoise(x, y, z, _octaves, _multiplier, _amplitude, _lacunarity, _persistence);
					Color c = new Color (value, 0.0f, 0.0f, 1.0f);
                    colorArray[x + (y * _textureSize) + (z * _textureSize * _textureSize)] = c;
                }
            }
        }

        texture.SetPixels(colorArray);
        texture.Apply();
		return texture;
	}
}
