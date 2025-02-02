using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementInputManger : MonoBehaviour
{
    [SerializeField] private Camera sceneCamaera;
    private Vector3 lastPosition;

    [SerializeField] private LayerMask placementLayerMask;

    public (Vector3, RaycastHit) GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamaera.nearClipPlane;
        Ray ray = sceneCamaera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastPosition = hit.transform.position;
        }
        return (lastPosition, hit);
    }
}
