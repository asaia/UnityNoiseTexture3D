using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Texture3DNoiseGenerator))]
public class Texture3DNoiseGeneratorEditor : Editor 
{
	private string _path;
	override public void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		EditorGUILayout.Separator();
		if (GUILayout.Button("Generate Noise Texture", GUILayout.Height(EditorGUIUtility.singleLineHeight * 2.0f)))
		{
			Texture3DNoiseGenerator noiseGenerator = target as Texture3DNoiseGenerator;
			Texture3D texture = noiseGenerator.GenerateNoise();

			if (string.IsNullOrEmpty(_path))
			{
				_path = EditorUtility.SaveFilePanel("Noise Texture", Application.dataPath, "NoiseTexture3D", "asset");
			}

			if (!string.IsNullOrEmpty(_path))
			{
				if (_path.StartsWith(Application.dataPath)) 
				{
					_path = "Assets" + _path.Substring(Application.dataPath.Length);
				}
				
				Texture3D oldTexture3D = AssetDatabase.LoadMainAssetAtPath(_path) as Texture3D;
				if (oldTexture3D != null) 
				{
					EditorUtility.CopySerialized(texture, oldTexture3D);
					AssetDatabase.SaveAssets();
				}
				else 
				{
					AssetDatabase.CreateAsset(texture, _path);
				}
			}
		}
		EditorGUILayout.Separator();
	}
}
