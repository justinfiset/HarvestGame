using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpriteCollection
{
    public List<Sprite> sprites;

    public Sprite GetRandomSprite()
    {
        return sprites[UnityEngine.Random.Range(0, sprites.Count)];
    }
}

[Serializable]
public class SpriteCollectionLibrary
{
    public List<SpriteCollection> collections;

    public Sprite GetRandomFromCollection(int collectionIndex)
    {
        return collections[collectionIndex].GetRandomSprite();
    }
}
