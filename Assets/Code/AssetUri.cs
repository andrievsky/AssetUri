using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class AssetUri : PropertyAttribute
{
    public readonly Type Type;

    public AssetUri(Type type)
    {
        Type = type ?? typeof(Object);
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(AssetUri))]
public class AssetUriDrawer : PropertyDrawer
{
    private const string DefaultResourcesPath = "Assets/Resources/";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.LabelField(position, label.text, "Use AssetUri with string.");
            return;
        }

        var attr = attribute as AssetUri;
        var uri = property.stringValue;
        Object value = null;
        if (!string.IsNullOrEmpty(uri))
        {
            value = Resources.Load(uri);
        }
        property.stringValue = Resolve(EditorGUI.ObjectField(position, value, attr.Type, false));
    }

    private static string Resolve(Object value)
    {
        if (value == null)
        {
            return null;
        }
        var assetPath = AssetDatabase.GetAssetPath(value);
        if (string.IsNullOrEmpty(assetPath))
        {
            return null;
        }
        if (!assetPath.Contains(DefaultResourcesPath))
        {
            throw new ArgumentException("Asset should be in " + DefaultResourcesPath);
        }
        var startIndex = DefaultResourcesPath.Length;
        var endIndex = assetPath.LastIndexOf('.');
        if (endIndex == -1)
        {
            throw new ArgumentException("Asset file name should ends with .*");
        }

        return assetPath.Substring(startIndex, endIndex - startIndex);
    }
}
#endif