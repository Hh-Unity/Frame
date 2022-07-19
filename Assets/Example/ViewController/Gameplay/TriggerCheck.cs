using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public LayerMask targetLayers;

    private int enterCount = 0;
    public bool IsTrigger
    {
        get
        {
            return enterCount > 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject, targetLayers))
            enterCount--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject, targetLayers))
            enterCount++;
    }

    bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        int objLayerMask = 1 << obj.layer;
        return (layerMask.value & objLayerMask) > 0;
    }
}
