using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AsteroidSpawner))]
public class AsteroidSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Show default Inspector fields

        AsteroidSpawner spawner = (AsteroidSpawner)target;

        if (GUILayout.Button("Generate Asteroids"))
        {
            spawner.GenerateAsteroidsInEditor();
        }

        if (GUILayout.Button("Clear Asteroids"))
        {
            spawner.ClearAsteroids();
        }
    }
}
