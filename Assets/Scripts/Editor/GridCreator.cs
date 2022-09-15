using UnityEditor;
using UnityEngine;

    public class GridCreator : EditorWindow
    {
        #region Vars

        public int x_Dimention, y_Dimention;
        public Vector2 startPos;
        private Vector2 generatePos;
        public Sprite grid_Sprite;

        #endregion

        #region GUI

        [MenuItem("Tools For Panteon Case/Grid Creator")]
        public static void ShowWindow()
        {
            var temp = GetWindow(typeof(GridCreator));
            temp.maxSize = new Vector2(300f, 170f);
            temp.minSize = new Vector2(300f, 170f);
        }

        private void OnGUI()
        {
            DimentionsTextFields();
            GenerateButton();
        }

        #endregion

        #region GUI Input Funcs.

        private void DimentionsTextFields()
        {
            x_Dimention = EditorGUILayout.IntField("X Dimention", x_Dimention);
            y_Dimention = EditorGUILayout.IntField("Y Dimention", y_Dimention);
            startPos = EditorGUILayout.Vector2Field("Start Pos", startPos);
            grid_Sprite = (Sprite)EditorGUILayout.ObjectField("Grid Sprite", grid_Sprite, typeof(Sprite));
        }

        private void GenerateButton()
        {
            if (GUILayout.Button("Create Grids"))
            {
                GenerateGrids();
            }
        }

        #endregion

        #region Generate Funcs.

        private void GenerateGrids()
        {
            generatePos = startPos;
            GameObject holder = new GameObject("Grids");
            for (int i = 0; i < x_Dimention; i++)
            {
                for (int j = 0; j < y_Dimention; j++)
                {
                    GameObject temp = new GameObject("GridBlock");
                    temp.transform.position =generatePos;
                    temp.transform.parent = holder.transform;
                    temp.AddComponent<GridBlock>();
                    temp.AddComponent<BoxCollider2D>().isTrigger = true;
                    SpriteRenderer tmpSpriteRenderer = temp.AddComponent<SpriteRenderer>();
                    tmpSpriteRenderer.sprite = grid_Sprite;
                    tmpSpriteRenderer.color = new Color(1,1,1,0.2f);
                    
                    generatePos += new Vector2(0, -1);
                }

                generatePos = new Vector2(generatePos.x, startPos.y);
                generatePos += new Vector2(1, 0);
            }
        }

        #endregion
    }