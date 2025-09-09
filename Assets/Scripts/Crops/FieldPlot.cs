using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FieldPlot : Tile, IPlayerInteractable
{
    [SerializeField] private TileAsset m_regularTileAsset;
    [SerializeField] private TileAsset m_wateredTileAsset;

    [Header("Field")]
    [SerializeField] SpriteCollectionLibrary m_plowStages;

    private SpriteRenderer m_spriteRenderer;
    [SerializeField] private SpriteRenderer m_cropRenderer;
    [SerializeField] private GameObject m_readyIndicator;
    [SerializeField] private SpriteRenderer m_iconRenderer;

    private bool m_isWatered;

    private int m_plowState = 0;
    private int m_maxPlowState;

    private Crop m_crop;
    private int m_currentStage = 0;
    private float m_growthTimer = 0.0f;
    private bool m_readyToHarvest = false;

    public ItemTooltipData GetTooltipData()
    {
        if (m_crop != null)
            return new ItemTooltipData(m_crop.GetName(), m_crop.GetData().CropDescription);
        else return null;
    }

    public void Start()
    {
        m_maxPlowState = m_plowStages.collections.Count;
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        NotifyUpdate();

        m_tileAsset = m_regularTileAsset;
    }

    public void Update()
    {
        if (m_crop != null && m_plowState == m_maxPlowState && m_isWatered && !m_readyToHarvest)
        {
            m_growthTimer += Time.deltaTime;

            if(m_growthTimer >= m_crop.cropData.TimePerStage)
            {
                m_growthTimer = 0.0f;
                m_currentStage++;

                if (m_currentStage == m_crop.cropData.GrowthStages.Length - 1)
                {
                    m_readyToHarvest = true;
                }

                NotifyUpdate();
            }
        }
    }

    public bool Harvest(Player player)
    {
        int qt = Random.Range(m_crop.cropData.MinYield, m_crop.cropData.MaxYield);
        bool added = player.inventory.AddItem(m_crop.cropData.GetYieldItem(), qt);

        if(added)
        {
            m_currentStage = 0;
            m_growthTimer = 0.0f;
            m_readyToHarvest = false;
            NotifyUpdate();
        }

        return added;
    }

    /// <summary>
    /// Fonction to plow the ground, returns true if the parcel is completed
    /// </summary>
    /// <returns></returns>
    public bool Plow()
    {
        if (m_plowState == m_maxPlowState) return true;

        m_plowState += 1;
        NotifyUpdate();

        return m_plowState == m_maxPlowState;
    }

    public bool IsComplete()
    {
        return m_plowState == m_maxPlowState;
    }

    public bool WaterPlot()
    {
        if (m_isWatered) return false;

        if(m_plowState == m_maxPlowState)
        {
            m_isWatered = true;
            NotifyUpdate();
            return true;
        }

        return false;
    }

    public Crop GetCrop()
    {
        return m_crop;
    }

    public void Plant(Crop newCrop)
    {
        if (m_plowState == m_maxPlowState)
        {
            m_crop = newCrop;
            m_currentStage = 0;
            m_growthTimer = 0.0f;
            m_readyToHarvest = false;
            m_iconRenderer.sprite = newCrop.cropData.Icon;
            NotifyUpdate();
        }
    }

    public void NotifyUpdate()
    {
        if(m_crop != null)
            m_cropRenderer.sprite = m_crop.cropData.GrowthStages[m_currentStage];
        m_cropRenderer.color = (m_crop == null) ? Color.clear : Color.white;

        m_readyIndicator.SetActive(m_readyToHarvest);

        m_tileAsset = m_isWatered ? m_wateredTileAsset : m_regularTileAsset;
        UpdateTile();

        if (m_plowState == m_maxPlowState)
        {
            m_spriteRenderer.color = Color.clear;
            m_tileHolder.SetActive(true);
        }
        else
        {
            m_spriteRenderer.sprite = m_plowStages.GetRandomFromCollection(m_plowState);
            m_spriteRenderer.color = Color.white;
            m_tileHolder.SetActive(false);
        }

        /* TODO REMOVE FOR DEBUG ONLY */
        //m_spriteRenderer.gameObject.transform.localScale = Vector3.one * m_plowState / m_maxPlowState;

        ///*TODO : REMOVE FOR DEBUG PURPOSES */
        //m_spriteRenderer.color = m_isWatered ? new Color32(36, 25, 28, 255) : new Color32(164, 118, 74, 255);
    }

    public bool Interact(Player player)
    {
        if (m_readyToHarvest)
        {
            bool ready = Harvest(player);
            if (ready) CropManager.Harvest(this);
            return ready;
        }
        else return false;
    }

    public override Neighbor GetNeighbors()
    {
        return CropManager.GetPlotNeighbors(this);
    }
}