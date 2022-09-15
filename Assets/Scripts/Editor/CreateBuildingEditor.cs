using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ToolEditors
{
    public class CreateBuildingEditor : EditorWindow
    {
        #region Var

        private string _buildingName;
        private int _buildingLevel;
        private Sprite _buildingImage;
        private int[] _buildingDimentions = new int[2];

        #endregion

        #region GUI

        [MenuItem("Tools For Panteon Case/Building Creator")]
        public static void ShowWindow()
        {
            var temp = GetWindow(typeof(CreateBuildingEditor));
            temp.maxSize = new Vector2(500f, 175f);
            temp.minSize = new Vector2(500f, 175f);
        }

        private void OnGUI()
        {
            NameField();
            LevelField();
            ImageField();
            DimentionsField();
            CreateButton();
        }

        #endregion

        #region GUI Funcs.

        private void NameField() => _buildingName = EditorGUILayout.TextField("Building Name", _buildingName);
        private void LevelField() => _buildingLevel = EditorGUILayout.IntField("Building Level", _buildingLevel);

        private void ImageField() => _buildingImage =
            (Sprite)EditorGUILayout.ObjectField("Building Image", _buildingImage, typeof(Sprite));
        
        private void DimentionsField()
        {
            GUILayout.Label("Dimentions", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            _buildingDimentions[0] = EditorGUILayout.IntField("X Dimention", _buildingDimentions[0]);
            _buildingDimentions[1] = EditorGUILayout.IntField("Y Dimention", _buildingDimentions[1]);
            EditorGUILayout.EndHorizontal();
        }

        private void CreateButton()
        {
            if (GUILayout.Button("Create Building"))
            {
                CreateBuildingScriptableObject();
            }
        }

        #endregion

        #region Building Create Funcs.

        private void CreateBuildingScriptableObject()
        {
            Building building = CreateInstance<Building>();

            building.BuildingName = _buildingName;
            building.BuildingLevel = _buildingLevel;
            building.BuildingImage = _buildingImage;
            building.BuildingDimentions = _buildingDimentions;

            string path = "Assets/Resources/ScriptableObjects/Buildings/" + _buildingName + " - " + _buildingLevel +
                          ".asset";
            AssetDatabase.CreateAsset(building, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = building;

            AddInBuildingPanelManager(building);
        }

        private void AddInBuildingPanelManager(Building building)
        {
            FindObjectOfType<BuildingPanelManager>().BuildingsList.Add(building);
        }

        #endregion
        
    }
}