using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileAsset", menuName = "Game/Tiles/TileAsset", order = 1)]
public class TileAsset : ScriptableObject
{
    public Sprite Center;

    [Header("Borders")]
    public Sprite TopBorder;
    public Sprite BottomBorder;
    public Sprite LeftBorder;
    public Sprite RightBorder;

    [Header("Junctions")]
    public Sprite TopJunction;
    public Sprite BottomJunction;
    public Sprite LeftJunction;
    public Sprite RightJunction;

    [Header("Corner Junctions")]
    public Sprite TopLeftJunction;
    public Sprite TopRightJunction;
    public Sprite BottomLeftJunction;
    public Sprite BottomRightJunction;

    [Header("Segment Junction")]
    public Sprite HorizontalJunctionTopLeft;
    public Sprite HorizontalJunctionTopRight;
    public Sprite HorizontalJunctionBottomLeft;
    public Sprite HorizontalJunctionBottomRight;

    public Sprite VerticalJunctionTopLeft;
    public Sprite VerticalJunctionTopRight;
    public Sprite VerticalJunctionBottomLeft;
    public Sprite VerticalJunctionBottomRight;

    [Header("Inner Corners")]
    public Sprite TopLeftInnerCorner;
    public Sprite TopRightInnerCorner;
    public Sprite BottomLeftInnerCorner;
    public Sprite BottomRightInnerCorner;

    [Header("Outter Corners")]
    public Sprite TopLeftCorner;
    public Sprite TopRightCorner;
    public Sprite BottomLeftCorner;
    public Sprite BottomRightCorner;
}

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
    [SerializeField] protected TileAsset m_tileAsset;
    protected GameObject m_tileHolder;

    abstract public Neighbor GetNeighbors();
    public void UpdateTile()
    {
        List<Sprite> sprites = GetShape(GetNeighbors(), m_tileAsset);

        if (m_tileHolder != null) Destroy(m_tileHolder);
        m_tileHolder = Instantiate(new GameObject("Tile Sprites"), transform);

        for (int i = 0; i < sprites.Count; i++)
        {
            SpriteRenderer spriteRenderer = Instantiate(new GameObject("Sprite " + i), m_tileHolder.transform).AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[i];
        }
    }

    public static List<Sprite> GetShape(Neighbor n, TileAsset assets)
    {
        List<Sprite> pieces = new List<Sprite>();

        int directCount = CountDirectNeighbors((int)n);
        int count = CountNeighbors((int)n);

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