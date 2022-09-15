using UnityEngine;

    [CreateAssetMenu(menuName = "Create/Building Products/Product")]
    public class Product : ScriptableObject
    {
        [SerializeField] private Sprite _productImage;
        [SerializeField] private string _productName;

        public Sprite ProductImage
        {
            get => _productImage;
            set => _productImage = value;
        }

        public string ProductName
        {
            get => _productName;
            set => _productName = value;
        }
    }
