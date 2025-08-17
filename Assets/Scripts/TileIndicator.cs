using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIndicator : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tilePos = CropManager.GetTilePosition(mousePos);
        transform.position = tilePos;
    }
}
