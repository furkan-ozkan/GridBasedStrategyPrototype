using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingPanelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buildings_Parent;
    [SerializeField] private List<Building> _buildings_List;
    
    GameObject newBuilding;
    GameObject newBuildingSprite;
    GameObject newBuildingGridCheckers;
    GameObject newBuildingProductSpawnPoint;
    
    public int buildingStartIndex = 0;
    public Button buttonUp, buttonDown;
    public List<Building> BuildingsList
    {
        get => _buildings_List;
        set => _buildings_List = value;
    }

    private void Start()
    {
        CreateBuildingsInBuildingList();
        ButtonUpOnClick();
        ButtonDownOnClick();
    }

    #region Building Panel Func.

        /// <summary>
        /// In unity we are filling building list and this func.
        /// Create panel items for each building in the list.
        /// </summary>
        private void CreateBuildingsInBuildingList()
        {
            /*
             * foreach (var i in _buildings_List)
            {
                GameObject item = new GameObject(i.BuildingName);
                GridLayoutGroup itemGridLayout = item.AddComponent<GridLayoutGroup>();
                itemGridLayout.startAxis = GridLayoutGroup.Axis.Vertical;
                itemGridLayout.spacing = new Vector2(30, 0);
                item.transform.parent = _buildings_Parent.transform;
                
                GameObject tempHolder = new GameObject(i.BuildingName);
                tempHolder.AddComponent<BuildingPanelAttributeHolder>().building = i;
                tempHolder.AddComponent<RectTransform>().localScale = Vector3.one;
                tempHolder.AddComponent<RawImage>().texture = i.BuildingImage.texture;
                tempHolder.AddComponent<Button>().onClick.AddListener(CreateBuilding);
                tempHolder.transform.parent = item.transform;
                
                GameObject buildingNameTxt = new GameObject("Name");
                TextMeshProUGUI buildingNameTMP = buildingNameTxt.AddComponent<TextMeshProUGUI>();
                buildingNameTMP.text = i.BuildingName;
                buildingNameTMP.color = Color.black;
                buildingNameTMP.alignment = TextAlignmentOptions.Center;
                buildingNameTMP.enableAutoSizing = true;
                buildingNameTxt.transform.parent = item.transform;
                
                item.transform.localScale = Vector3.one;
            }
             */
            if (_buildings_List.Count > 0)
            {
                for (int i = buildingStartIndex; i < buildingStartIndex + _buildings_Parent.Count && i < _buildings_List.Count; i++)
                {
                    _buildings_Parent[i - buildingStartIndex].GetComponent<BuildingPanelAttributeHolder>().building =
                        _buildings_List[i];
                    _buildings_Parent[i - buildingStartIndex].GetComponent<Button>().onClick.AddListener(CreateBuilding);
                    _buildings_Parent[i - buildingStartIndex].transform.GetChild(1).GetComponent<RawImage>().texture =
                        _buildings_List[i].BuildingImage.texture;
                    _buildings_Parent[i - buildingStartIndex].transform.GetChild(1).GetComponent<RawImage>().enabled =
                        true;
                    _buildings_Parent[i - buildingStartIndex].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        _buildings_List[i].BuildingName;
                }
            }
        }
        
        private void CreateBuilding()
        {
            if (GameManager.Instance.BuildingStateCurrentObject)
            {
                Destroy(GameManager.Instance.BuildingStateCurrentObject);
            }
            #region Get Clicked Building Attributes and Create GameObjects

            Building tempBuilding = EventSystem.current.currentSelectedGameObject.GetComponent<BuildingPanelAttributeHolder>().building;

            newBuilding = new GameObject(tempBuilding.BuildingName + " - " + tempBuilding.BuildingLevel);
            newBuildingSprite = new GameObject("Sprite");
            newBuildingGridCheckers = new GameObject("Grids");
            newBuildingProductSpawnPoint = new GameObject("Product Spawn Point");
            
            #endregion

            #region Adding Components && settings

            // Parent building gameobject
            newBuilding.AddComponent<BuildingAttributeHolder>().building = tempBuilding;
            newBuilding.AddComponent<Rigidbody2D>().isKinematic = true;
            BoxCollider2D tmpCollder = newBuilding.AddComponent<BoxCollider2D>();
            tmpCollder.enabled = false;
            tmpCollder.size = new Vector2(tempBuilding.BuildingDimentions[0], tempBuilding.BuildingDimentions[1]);
            newBuilding.AddComponent<BuildingMouseFollow>();
            newBuilding.AddComponent<BuildingGridManager>();

            
            // Product Spawn Poing gameobject
            newBuildingProductSpawnPoint.transform.parent = newBuilding.transform;
            
            // Sprite gameobject
            newBuildingSprite.transform.parent = newBuilding.transform;
            newBuildingSprite.AddComponent<SpriteRenderer>().sprite = tempBuilding.BuildingImage;
            switch (tempBuilding.BuildingDimentions[0] % 2)
            {
                case 0:
                    switch (tempBuilding.BuildingDimentions[1] % 2)
                    {
                        case 0:
                            newBuildingSprite.transform.position = new Vector3(tempBuilding.BuildingDimentions[0] / 2 - 0.5f, tempBuilding.BuildingDimentions[1] / 2 - 0.5f, 0);
                            break;
                        case 1:
                            newBuildingSprite.transform.position = new Vector3(tempBuilding.BuildingDimentions[0] / 2 - 0.5f, tempBuilding.BuildingDimentions[1] / 2, 0);
                            break;
                    }
                    break;
                case 1:
                    switch (tempBuilding.BuildingDimentions[1] % 2)
                    {
                        case 0:
                            newBuildingSprite.transform.position = new Vector3(tempBuilding.BuildingDimentions[0] / 2, tempBuilding.BuildingDimentions[1] / 2 - 0.5f, 0);
                            break;
                        case 1:
                            newBuildingSprite.transform.position = new Vector3(tempBuilding.BuildingDimentions[0] / 2, tempBuilding.BuildingDimentions[1] / 2, 0);
                            break;
                    }
                    break;
            }
            newBuildingSprite.transform.localScale = new Vector3(tempBuilding.BuildingDimentions[0], tempBuilding.BuildingDimentions[1], 0);
            newBuildingSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .25f);

            // Grid Checkers Holder Parent
            newBuildingGridCheckers.transform.parent = newBuilding.transform;
            newBuildingGridCheckers.transform.position = Vector3.zero;
            
            // Grid Checker childs
            for (int i = 0; i < tempBuilding.BuildingDimentions[0]; i++)
            {
                for (int j = 0; j < tempBuilding.BuildingDimentions[1]; j++)
                {
                    GameObject tempGridChecker = new GameObject("GridChecker");
                    tempGridChecker.transform.parent = newBuildingGridCheckers.transform;
                    tempGridChecker.transform.position = new Vector3(i,j,0);
                    tempGridChecker.transform.localScale = new Vector3(.1f, .1f, .1f);
                    tempGridChecker.AddComponent<BuildingGridChecker>();
                    tempGridChecker.AddComponent<BoxCollider2D>().isTrigger = true;
                }
            }

            GameManager.Instance.BuildingStateCurrentObject = Instantiate(newBuilding);
            GameStateManager.Instance.SetState(GameStateManager.Instance.BuildingGameState());
            
            Destroy(newBuilding);
            #endregion
        }
        
        private void ButtonUpOnClick() => buttonUp.onClick.AddListener(ButtonUp);
        private void ButtonDownOnClick() => buttonDown.onClick.AddListener(ButtonDown);

        public void ButtonUp()
        {
            if (buildingStartIndex < _buildings_List.Count-5)
            {
                buildingStartIndex++;
            }
            CreateBuildingsInBuildingList();
        }

        public void ButtonDown()
        {
            if (buildingStartIndex > 0)
            {
                buildingStartIndex--;
            }
            CreateBuildingsInBuildingList();
        }


        #endregion
}