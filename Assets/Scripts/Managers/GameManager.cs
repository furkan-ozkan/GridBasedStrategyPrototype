using DG.Tweening;
using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        #endregion
        
        #region Var

        private GameObject buildingStateCurrentObject;
        private GameObject inGameStateCurrentObject;
        private GameObject productStateCurrentObject;

        #endregion

        #region Unity Funcs.

        private void Start()
        {
            
        }

        #endregion

        #region General Funcs.

        #region Getters && Setters

        public GameObject BuildingStateCurrentObject
        {
            get => buildingStateCurrentObject;
            set => buildingStateCurrentObject = value;
        }
        
        public GameObject InGameStateCurrentObject
        {
            get => inGameStateCurrentObject;
            set => inGameStateCurrentObject = value;
        }

        public GameObject ProductStateCurrentObject
        {
            get => productStateCurrentObject;
            set => productStateCurrentObject = value;
        }

        #endregion

        #endregion
    }
