using UnityEngine;
using UnityEngine.UI;

public class TestLoader : TestMonoBehaviour
{
    [SerializeField] private TestConfig config;
    [SerializeField] private Image image;
    [SerializeField] private Transform container;
    private string spriteUri;
    private string prefabUri;

    private void Update()
    {
        UpdateImage();
        UpdatePrefab();
    }


    private void UpdateImage()
    {
        if (spriteUri == config.SpriteUri)
        {
            return;
        }
        spriteUri = config.SpriteUri;
        image.sprite = Resources.Load<Sprite>(spriteUri);
    }

    private void UpdatePrefab()
    {
        if (prefabUri == config.PrefabUri)
        {
            return;
        }
        prefabUri = config.PrefabUri;
        Instantiate(Resources.Load(prefabUri), container);
    }
}