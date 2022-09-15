using DG.Tweening;
using UnityEngine;

public class BuildingMouseFollow : MonoBehaviour
{
    void Update()
    {
        if (InputManager.Instance.MouseToScreenRay())
        {
            RaycastHit2D hit = InputManager.Instance.MouseToScreenRay();
            transform.DOMove(hit.transform.position + new Vector3(0,0,-1), .05f);
        }
    }
}