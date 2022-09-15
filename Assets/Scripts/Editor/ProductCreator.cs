using UnityEngine;
using UnityEditor;

public class ProductCreator : EditorWindow
{

    #region Vars

    private Sprite _productImage;
    private string _productName;
    private Building _productBuilding;

    #endregion
    
    #region GUI

    [MenuItem("Tools For Panteon Case/Product Creator")]
    public static void ShowWindow()
    {
        var temp = GetWindow(typeof(ProductCreator));
        temp.maxSize = new Vector2(300, 130);
        temp.minSize = new Vector2(300, 130);
    }

    private void OnGUI()
    {
        NameField();
        ImageField();
        BuildingField();
        CreateButton();
    }

    #endregion
    
    #region GUI Funcs.

    private void NameField() => _productName = EditorGUILayout.TextField("Product Name", _productName);

    private void ImageField() => _productImage =
        (Sprite)EditorGUILayout.ObjectField("Product Image", _productImage, typeof(Sprite));
    
    private void BuildingField() => _productBuilding = (Building)EditorGUILayout.ObjectField(_productBuilding, typeof(Building));

    private void AddInBuilding(Product product) => _productBuilding.BuildingProducts.Add(product);

    private void CreateButton()
    {
        if (GUILayout.Button("Create Product"))
        {
            Product product = CreateInstance<Product>();

            product.ProductName = _productName;
            product.ProductImage = _productImage;

            string path = "Assets/Resources/ScriptableObjects/Products/" + _productName + ".asset";
            AssetDatabase.CreateAsset(product, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            AddInBuilding(product);
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = product;
        }
    }
    

    #endregion
}
