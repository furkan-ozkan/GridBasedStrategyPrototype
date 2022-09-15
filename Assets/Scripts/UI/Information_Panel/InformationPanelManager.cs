using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Canvas/InformationPanel
public class InformationPanelManager : MonoBehaviour
{
    #region Singleton

    public static InformationPanelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        gameObject.SetActive(false);
    }

    #endregion

    #region Vars

    public Building building;
    public GameObject informationPanel;
    public Button buttonUp, buttonDown;

    public RawImage buildingImage;
    public TextMeshProUGUI buildingName;
    public List<GameObject> products;

    public int productStartIndex = 0;

    #endregion

    #region Unity Funcs.

    private void Start()
    {
        ButtonUpOnClick();
        ButtonDownOnClick();
    }

    #endregion

    #region Fill Information On Enable

    private void OnEnable()
    {
        InformationPanelGettingReady();
    }

    private void InformationPanelGettingReady()
    {
        FillImage();
        FillName();
        FillProducts();
    }

    private void FillImage() => buildingImage.texture = building.BuildingImage.texture;
    private void FillName() => buildingName.text = building.BuildingName;

    private void FillProducts()
    {
        if (products.Count > 0)
        {
            for (int i = productStartIndex; i < productStartIndex + products.Count; i++)
            {
                products[i-productStartIndex].transform.GetChild(1).GetComponent<RawImage>().texture =
                    building.BuildingProducts[i].ProductImage.texture;
                products[i-productStartIndex].transform.GetChild(1).GetComponent<RawImage>().enabled = true;
                products[i-productStartIndex].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                    building.BuildingProducts[i].ProductName;
            }
        }
    }

    #endregion

    #region Clear Information On Disable

    private void OnDisable()
    {
        ClearImage();
        ClearName();
        ClearProducts();
    }
    private void ClearImage() => buildingImage.texture = null;
    private void ClearName() => buildingName.text = "";

    private void ClearProducts()
    {
        for (int i = productStartIndex; i < productStartIndex + products.Count; i++)
        {
            products[i-productStartIndex].transform.GetChild(1).GetComponent<RawImage>().texture = null;
            products[i-productStartIndex].transform.GetChild(1).GetComponent<RawImage>().enabled = false;
            products[i-productStartIndex].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    #endregion

    #region ObjectPooling Buttons

    private void ButtonUpOnClick() => buttonUp.onClick.AddListener(ButtonUp);
    private void ButtonDownOnClick() => buttonDown.onClick.AddListener(ButtonDown);

    public void ButtonUp()
    {
        if (productStartIndex < building.BuildingProducts.Count-4)
        {
            productStartIndex++;
        }
        FillProducts();
    }

    public void ButtonDown()
    {
        if (productStartIndex > 0)
        {
            productStartIndex--;
        }
        FillProducts();
    }

    #endregion
}
