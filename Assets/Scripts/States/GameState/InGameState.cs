using UnityEngine;
public class InGameState : BaseGameState
{
    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.LeftClick())
        {
            RaycastHit2D hit = InputManager.Instance.MouseToScreenRay();
            if (hit && hit.transform.GetComponent<BuildingAttributeHolder>())
            {
                InformationPanelManager.Instance.building = hit.transform.GetComponent<BuildingAttributeHolder>().building;
                InformationPanelManager.Instance.informationPanel.SetActive(true);
            }
            else if (hit && hit.transform.GetComponent<GridBlock>())
            {
                InformationPanelManager.Instance.informationPanel.SetActive(false);
            }
        }
    }

    public override void ExitState()
    {
        
    }
}