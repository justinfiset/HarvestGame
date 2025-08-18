using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public static CropManager Instance { get; private set;}

    [Header("Field Settings")]
    public static float s_tileSize = 1.0f;

    [Header("Prefabs")]
    [SerializeField] private GameObject m_fieldPlotInstance;

    private Dictionary<Vector2, FieldPlot> m_plots = new Dictionary<Vector2, FieldPlot>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public static Vector2 GetTilePosition(Vector2 position)
    {
        int x = Mathf.FloorToInt(position.x / s_tileSize);
        int y = Mathf.FloorToInt(position.y / s_tileSize);
        float worldX = x * s_tileSize + s_tileSize / 2f;
        float worldY = y * s_tileSize + s_tileSize / 2f;
        return new Vector2(worldX, worldY);
    }

    public void Plow(Vector2 position)
    {
        Vector2 tilePos = GetTilePosition(position);

        if(m_plots.TryGetValue(tilePos, out FieldPlot plot))
        {
            plot.Plow();
        } else
        {
            GameObject obj = Instantiate(m_fieldPlotInstance, transform);
            obj.transform.position = tilePos;

            FieldPlot newPlot = obj.GetComponent<FieldPlot>();
            m_plots[tilePos] = newPlot;
        }
    }

    public void Unplow(Vector2 position)
    {
        Vector2 tilePos = GetTilePosition(position);
        if(m_plots.TryGetValue(tilePos, out FieldPlot plot))
        {
            Destroy(plot.gameObject);
        }
        m_plots.Remove(tilePos);
    }

    public void WaterPlot(Vector2 position)
    {
        Vector2 pos = GetTilePosition(position);
        if(m_plots.TryGetValue(pos, out FieldPlot plot))
        {
            plot.WaterPlot();
        }
    }
}
