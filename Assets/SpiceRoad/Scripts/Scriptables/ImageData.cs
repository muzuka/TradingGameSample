using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ImageData")]
public class ImageData : ScriptableObject
{
    public List<string> Keys;
    public List<Sprite> Values;

    Dictionary<string, Sprite> _images;

    void OnValidate()
    {
        _images = new Dictionary<string, Sprite>();
        for (int i = 0; i < Keys.Count; i++)
        {
            _images.Add(Keys[i], Values[i]);
        }
    }

    public Sprite GetSprite(string name)
    {
        if (!_images.ContainsKey(name))
        {
            return null;
        }

        return _images[name];
    }
}
