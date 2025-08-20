using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [System.Flags]
    public enum Neighbor
    {
        None = 0,
        Up = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
        UpLeft = 1 << 4,
        UpRight = 1 << 5,
        DownLeft = 1 << 6,
        DownRight = 1 << 7
    }

    private const int DirectMask = (int)(Neighbor.Up | Neighbor.Down | Neighbor.Left | Neighbor.Right);

    [Header("Autotiling")]
    protected TileAsset m_tileAsset;
    protected GameObject m_tileHolder;

    abstract public Neighbor GetNeighbors();
    public void UpdateTile()
    {
        List<Sprite> sprites = GetShape(GetNeighbors(), m_tileAsset);

        if (m_tileHolder != null)
        {
            foreach (Transform child in m_tileHolder.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(m_tileHolder);
        }
        m_tileHolder = new GameObject("Tile Sprites");
        m_tileHolder.transform.SetParent(transform, false);

        for (int i = 0; i < sprites.Count; i++)
        {
            GameObject obj = new GameObject("Sprite " + i);
            obj.transform.SetParent(m_tileHolder.transform, false);
            SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[i];
        }
    }

    public static List<Sprite> GetShape(Neighbor n, TileAsset assets)
    {
        List<Sprite> pieces = new List<Sprite>();

        if(assets == null)
        {
            Debug.LogError("Tried to tile a shape with a null TileAsset.");
            return pieces;
        }

        pieces.Add(assets.Center);

        pieces.Add(((n & Neighbor.Up) == 0) ? assets.TopBorder : assets.TopJunction);
        pieces.Add(((n & Neighbor.Down) == 0) ? assets.BottomBorder : assets.BottomJunction);
        pieces.Add(((n & Neighbor.Left) == 0) ? assets.LeftBorder : assets.LeftJunction);
        pieces.Add(((n & Neighbor.Right) == 0) ? assets.RightBorder : assets.RightJunction);

        // Corner junctions (coins complets)
        if ((n & Neighbor.UpLeft) != 0 && (n & Neighbor.Up) != 0 && (n & Neighbor.Left) != 0)
            pieces.Add(assets.TopLeftJunction);
        if ((n & Neighbor.UpRight) != 0 && (n & Neighbor.Up) != 0 && (n & Neighbor.Right) != 0)
            pieces.Add(assets.TopRightJunction);
        if ((n & Neighbor.DownLeft) != 0 && (n & Neighbor.Down) != 0 && (n & Neighbor.Left) != 0)
            pieces.Add(assets.BottomLeftJunction);
        if ((n & Neighbor.DownRight) != 0 && (n & Neighbor.Down) != 0 && (n & Neighbor.Right) != 0)
            pieces.Add(assets.BottomRightJunction);

        // Outter corners
        if ((n & Neighbor.Up) == 0 && (n & Neighbor.Left) == 0) pieces.Add(assets.TopLeftCorner);
        if ((n & Neighbor.Up) == 0 && (n & Neighbor.Right) == 0) pieces.Add(assets.TopRightCorner);
        if ((n & Neighbor.Down) == 0 && (n & Neighbor.Left) == 0) pieces.Add(assets.BottomLeftCorner);
        if ((n & Neighbor.Down) == 0 && (n & Neighbor.Right) == 0) pieces.Add(assets.BottomRightCorner);

        // Inner corners

        if ((n & Neighbor.Left) != 0 && (n & Neighbor.Down) != 0 && (n & Neighbor.DownLeft) == 0)
            pieces.Add(assets.BottomLeftInnerCorner);
        if ((n & Neighbor.Right) != 0 && (n & Neighbor.Down) != 0 && (n & Neighbor.DownRight) == 0)
            pieces.Add(assets.BottomRightInnerCorner);
        if ((n & Neighbor.Left) != 0 && (n & Neighbor.Up) != 0 && (n & Neighbor.UpLeft) == 0)
            pieces.Add(assets.TopLeftInnerCorner);
        if ((n & Neighbor.Right) != 0 && (n & Neighbor.Up) != 0 && (n & Neighbor.UpRight) == 0)
            pieces.Add(assets.TopRightInnerCorner);

        if ((n & Neighbor.Left) != 0 && (n & Neighbor.Up) == 0) pieces.Add(assets.HorizontalJunctionTopLeft);
        if ((n & Neighbor.Left) != 0 && (n & Neighbor.Down) == 0) pieces.Add(assets.HorizontalJunctionBottomLeft);
        if ((n & Neighbor.Right) != 0 && (n & Neighbor.Up) == 0) pieces.Add(assets.HorizontalJunctionTopRight);
        if ((n & Neighbor.Right) != 0 && (n & Neighbor.Down) == 0) pieces.Add(assets.HorizontalJunctionBottomRight);

        if ((n & Neighbor.Up) != 0 && (n & Neighbor.Left) == 0) pieces.Add(assets.VerticalJunctionTopLeft);
        if ((n & Neighbor.Up) != 0 && (n & Neighbor.Right) == 0) pieces.Add(assets.VerticalJunctionTopRight);
        if ((n & Neighbor.Down) != 0 && (n & Neighbor.Left) == 0) pieces.Add(assets.VerticalJunctionBottomLeft);
        if ((n & Neighbor.Down) != 0 && (n & Neighbor.Right) == 0) pieces.Add(assets.VerticalJunctionBottomRight);

        // (Optionnel) tu peux réactiver inner/outer corners si besoin,
        // suivant si tu veux gérer les diagonales manquantes.
        return pieces;
    }

    private static int CountDirectNeighbors(int x)
    {
        int count = 0;
        x &= DirectMask;
        while (x > 0)
        {
            count += x & 1;
            x >>= 1;
        }
        return count;
    }

    private static int CountNeighbors(int x)
    {
        int count = 0;
        while (x > 0)
        {
            count += x & 1;
            x >>= 1;
        }
        return count;
    }
}