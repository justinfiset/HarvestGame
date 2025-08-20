using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CropManager : MonoBehaviour
{
    public static CropManager Instance { get; private set;}

    [Header("Field Settings")]
    public static float s_tileSize = 1.0f;

    [Header("Prefabs")]
    [SerializeField] private GameObject m_fieldPlotInstance;

    [Header("Audio")]
    [SerializeField] private SoundCollection m_plowSounds;
    [SerializeField] private SoundCollection m_harvestingSounds;
    private AudioSource m_audioSource;

    private Dictionary<Vector2, FieldPlot> m_plots = new Dictionary<Vector2, FieldPlot>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        m_audioSource = GetComponent<AudioSource>();
    }

    public static List<FieldPlot> GetPlotsAround(FieldPlot plot)
    {
        List<FieldPlot> plots = new List<FieldPlot>();

        if (plot == null) return plots;

        Vector2 plotPos = plot.transform.position;
        Vector2 basePosition = GetTilePosition(plotPos) + (Vector2.up + Vector2.left) * s_tileSize;

        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                Vector2 pos = basePosition + (Vector2.down * i + Vector2.right * j) * s_tileSize;
                if(Instance.m_plots.TryGetValue(pos, out FieldPlot neighbor))
                {
                    plots.Add(neighbor);
                }
            }
        }

        return plots;
    }

    public static void UpdateAround(FieldPlot plot)
    {
        foreach(FieldPlot neighbor in GetPlotsAround(plot))
        {
            if(neighbor.IsComplete())
            {
                neighbor.NotifyUpdate();
            }
        }
    }

    public static Tile.Neighbor GetPlotNeighbors(FieldPlot plot)
    {
        if (plot == null) return Tile.Neighbor.None;

        Vector2 plotPos = plot.transform.position;
        Vector2 tilePos = GetTilePosition(plotPos);

        Tile.Neighbor neighbors = Tile.Neighbor.None;

        // Up
        if (Instance.m_plots.TryGetValue(tilePos + Vector2.up * s_tileSize, out FieldPlot neighbor) && neighbor.IsComplete())
            neighbors |= Tile.Neighbor.Up;

        // Down
        if (Instance.m_plots.TryGetValue(tilePos + Vector2.down * s_tileSize, out neighbor) && neighbor.IsComplete())
            neighbors |= Tile.Neighbor.Down;

        // Left
        if (Instance.m_plots.TryGetValue(tilePos + Vector2.left * s_tileSize, out neighbor) && neighbor.IsComplete())
            neighbors |= Tile.Neighbor.Left;

        // Right
        if (Instance.m_plots.TryGetValue(tilePos + Vector2.right * s_tileSize, out neighbor) && neighbor.IsComplete())
            neighbors |= Tile.Neighbor.Right;

        // UpLeft
        if (Instance.m_plots.TryGetValue(tilePos + (Vector2.up + Vector2.left) * s_tileSize, out neighbor) && neighbor.IsComplete())
            neighbors |= Tile.Neighbor.UpLeft;

        // UpRight
        if (Instance.m_plots.TryGetValue(tilePos + (Vector2.up + Vector2.right) * s_tileSize, out neighbor) && neighbor.IsComplete())
            neighbors |= Tile.Neighbor.UpRight;

        // DownLeft
        if (Instance.m_plots.TryGetValue(tilePos + (Vector2.down + Vector2.left) * s_tileSize, out neighbor) && neighbor.IsComplete())
            neighbors |= Tile.Neighbor.DownLeft;

        // DownRight
        if (Instance.m_plots.TryGetValue(tilePos + (Vector2.down + Vector2.right) * s_tileSize, out neighbor) && neighbor.IsComplete())
            neighbors |= Tile.Neighbor.DownRight;

        return neighbors;
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
            if(!plot.IsComplete()) m_plowSounds.PlayRandom(m_audioSource);

            bool completed = plot.Plow();
            if (completed)
                UpdateAround(plot);
        } else
        {
            GameObject obj = Instantiate(m_fieldPlotInstance, transform);
            obj.transform.position = tilePos;

            FieldPlot newPlot = obj.GetComponent<FieldPlot>();
            m_plots[tilePos] = newPlot;

            m_plowSounds.PlayRandom(m_audioSource);
        }
    }

    public void Unplow(Vector2 position)
    {
        Vector2 tilePos = GetTilePosition(position);
        if(m_plots.TryGetValue(tilePos, out FieldPlot plot))
        {
            m_plots.Remove(tilePos);
            UpdateAround(plot);
            Destroy(plot.gameObject);
        }
    }

    public void Plant(Vector2 position, Crop crop)
    {
        Vector2 tilePos = GetTilePosition(position);
        if (m_plots.TryGetValue(tilePos, out FieldPlot plot) && plot.GetCrop() == null)
        {
            plot.Plant(crop);
        }
    }

    public bool WaterPlot(Vector2 position)
    {
        bool success = false;

        Vector2 tilePos = GetTilePosition(position);
        if(m_plots.TryGetValue(tilePos, out FieldPlot plot))
        {
            success = plot.WaterPlot();
        }

        return success;
    }
    
    public static void Harvest(FieldPlot plot)
    {
        Instance.m_harvestingSounds.PlayRandom(Instance.m_audioSource);
    }
}
