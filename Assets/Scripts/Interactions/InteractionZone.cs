using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : ZoneBehaviour
{
    private List<ExclusionZone> exclusionZones = new List<ExclusionZone>();

    private void Start()
    {
        exclusionZones.AddRange(FindObjectsOfType<ExclusionZone>());
    }

    override public bool Contains(Vector2 point)
    {
        bool contained = base.Contains(point);

        if (contained)
        {
            foreach (ExclusionZone zone in exclusionZones)
            {
                if (zone.Contains(point))
                {
                    contained = false;
                    break;
                }
            }
        }

        return contained;
    }
}