using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextureDB", menuName = "Game/TextureDB")]
public class TextureDatabase : ScriptableObject
{
    [System.Serializable]
    public struct TextureEntry
    {
        public string id;
        public Sprite sprite;
    }

    private static TextureDatabase _instance;

    // Used only to edit in the editor -> content is delivered in the dictionnary on playtime
    public List<TextureEntry> textures;

    private Dictionary<string, Sprite> _dict;

    public void Init()
    {
        _dict = new Dictionary<string, Sprite>();
        foreach (var entry in textures)
        {
            string cleanId = entry.id.Trim().ToLower();
            _dict[cleanId] = entry.sprite;
        }
    }

    public static TextureDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<TextureDatabase>("TextureDB");
                if (_instance == null)
                {
                    Debug.LogError("TextureDB.asset not found in Resources folder !");
                }
            }
            return _instance;
        }
    }

    public static Sprite Get(string id)
    {
        if (Instance._dict == null)
            Instance.Init();

        string cleanId = id.Trim().ToLower();
        return Instance._dict.TryGetValue(cleanId, out var sprite) ? sprite : null;
    }
}
