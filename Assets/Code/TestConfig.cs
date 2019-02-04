using UnityEditor;
using UnityEngine;

public class TestConfig : ScriptableObject
{
    [AssetUri(typeof(Sprite))] public string SpriteUri;
    [AssetUri(typeof(TestMonoBehaviour))] public string PrefabUri;
    public string RawString1;
    public string RawString2;

    [MenuItem("Assets/Create/TestConfig")]
    static void CreateTestConfig()
    {
        // Create a simple material asset

        var config = CreateInstance<TestConfig>();
        AssetDatabase.CreateAsset(config, "Assets/Resources/TestConfig.asset");
    }
}