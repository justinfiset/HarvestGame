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