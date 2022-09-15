using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Base scriptableobject for creating a new building.
    /// </summary>
    [CreateAssetMenu(menuName = "Create/Buildings/Building")]
    public class Building : ScriptableObject
    {
        #region Attributes

        [SerializeField] private Sprite _buildingImage;
        [SerializeField] private string _buildingName;
        [SerializeField] private int _buildingLevel;
        [SerializeField] private int[] _buildingDimentions = new int[2];
        [SerializeField] private List<Product> _buildingProducts = new List<Product>();

        #endregion

        #region Getters && Setters

        public Sprite BuildingImage
        {
            get => _buildingImage;
            set => _buildingImage = value;
        }

        public string BuildingName
        {
            get => _buildingName;
            set => _buildingName = value;
        }

        public int BuildingLevel
        {
            get => _buildingLevel;
            set => _buildingLevel = value;
        }

        public int[] BuildingDimentions
        {
            get => _buildingDimentions;
            set => _buildingDimentions = value;
        }

        public List<Product> BuildingProducts
        {
            get => _buildingProducts;
            set => _buildingProducts = value;
        }

        #endregion
    }
