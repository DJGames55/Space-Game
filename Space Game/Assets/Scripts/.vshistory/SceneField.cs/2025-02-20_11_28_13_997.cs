using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SceneField
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset _sceneAsset;
#endif
    [SerializeField] private string _sceneName;

    public string SceneName => _sceneName;

#if UNITY_EDITOR
    public void SetScene(SceneAsset scene)
    {
        _sceneAsset = scene;
        _sceneName = scene != null ? scene.name : "";
    }
#endif
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneField))]
public class ScenePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty sceneName = property.FindPropertyRelative("_sceneName");

#if UNITY_EDITOR
        SerializedProperty sceneAsset = property.FindPropertyRelative("_sceneAsset");

        EditorGUI.BeginChangeCheck();
        SceneAsset newScene = EditorGUI.ObjectField(position, label, sceneAsset.objectReferenceValue, typeof(SceneAsset), false) as SceneAsset;
        if (EditorGUI.EndChangeCheck())
        {
            sceneAsset.objectReferenceValue = newScene;
            sceneName.stringValue = newScene != null ? newScene.name : "";
        }
#else
        EditorGUI.LabelField(position, label.text, sceneName.stringValue);
#endif

        EditorGUI.EndProperty();
    }
}
#endif
