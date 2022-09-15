using UnityEngine;

public class BuildingGameState : BaseGameState
{
    private BuildingGridManager _buildingGridManager;
    private BuildingMouseFollow _buildingMouseFollow;
    private SpriteRenderer _buildingSpriteRenderer;
    private BoxCollider2D _buildingBoxCollider;
    private Building _building;

    public override void EnterState()
    {
        _buildingGridManager = GameManager.Instance.BuildingStateCurrentObject.GetComponent<BuildingGridManager>();
        _buildingMouseFollow = GameManager.Instance.BuildingStateCurrentObject.GetComponent<BuildingMouseFollow>();
        _buildingSpriteRenderer = GameManager.Instance.BuildingStateCurrentObject.transform.GetChild(1)
            .GetComponent<SpriteRenderer>();
        _buildingBoxCollider = GameManager.Instance.BuildingStateCurrentObject.GetComponent<BoxCollider2D>();
        _building = GameManager.Instance.BuildingStateCurrentObject.GetComponent<BuildingAttributeHolder>().building;
        GridBlockManager.Instance.GreyToAllGridBlocks();
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.LeftClick() &&
            InputManager.Instance.MouseToScreenRay().transform.GetComponent<GridBlock>())
        {
            if (_buildingGridManager.CheckSuitable())
            {
                StopMouseFollowing();
                ChangeGridSuitables();
                ChangeBuildingColor();
                UpdateBoxCollider();
                DisableGrids();
                ChangeState();
                ClearCurrentBuilding();
            }
        }
        else if (InputManager.Instance.RightClick())
        {
            GameObject.Destroy(GameManager.Instance.BuildingStateCurrentObject);
            GridBlockManager.Instance.GreyToAllGridBlocks();
            ChangeState();
        }
    }

    public override void ExitState()
    {

    }

    private void StopMouseFollowing() => _buildingMouseFollow.enabled = false;
    private void ChangeGridSuitables() => _buildingGridManager.ChangeSuitable();
    private void ChangeBuildingColor() => _buildingSpriteRenderer.color = Color.white;

    private void UpdateBoxCollider()
    {
        _buildingBoxCollider.enabled = true;

        switch (_building.BuildingDimentions[0] % 2)
        {
            case 0:
                switch (_building.BuildingDimentions[1] % 2)
                {
                    case 0:
                        _buildingBoxCollider.offset = new Vector2(_building.BuildingDimentions[0] / 2 - 0.5f,
                            _building.BuildingDimentions[1] / 2 - 0.5f);
                        break;
                    case 1:
                        _buildingBoxCollider.offset = new Vector2(_building.BuildingDimentions[0] / 2 - 0.5f,
                            _building.BuildingDimentions[1] / 2);
                        break;
                }

                break;
            case 1:
                switch (_building.BuildingDimentions[1] % 2)
                {
                    case 0:
                        _buildingBoxCollider.offset = new Vector2(_building.BuildingDimentions[0] / 2,
                            _building.BuildingDimentions[1] / 2 - 0.5f);
                        break;
                    case 1:
                        _buildingBoxCollider.offset = new Vector2(_building.BuildingDimentions[0] / 2,
                            _building.BuildingDimentions[1] / 2);
                        break;
                }

                break;
        }
    }

    private void DisableGrids()
    {
        GameManager.Instance.BuildingStateCurrentObject.transform.GetChild(2).gameObject.SetActive(false);
        _buildingGridManager.enabled = false;
    }

    private void ChangeState() => GameStateManager.Instance.SetState(GameStateManager.Instance.PlayGameState());

private void ClearCurrentBuilding() => GameManager.Instance.BuildingStateCurrentObject = null;
}