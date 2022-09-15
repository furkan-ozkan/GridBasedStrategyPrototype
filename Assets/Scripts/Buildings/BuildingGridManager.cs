using UnityEngine;

public class BuildingGridManager : MonoBehaviour
{
    #region Controller Funcs.

    /// <summary>
    /// if suitable return true
    /// else return false
    /// </summary>
    public bool CheckSuitable()
    {
        GameObject grids = gameObject.transform.GetChild(2).gameObject;
        bool isSuitable = true;
        
        for (int i = 0; i < grids.transform.childCount; i++)
        {
            if (!grids.transform.GetChild(i).GetComponent<BuildingGridChecker>().grid || !grids.transform.GetChild(i).GetComponent<BuildingGridChecker>().isEmpty)
            {
                isSuitable = false;
            }
        }
        return isSuitable;
    }

    #endregion

    #region Update Funcs.

    public void ChangeSuitable()
    {
        GameObject grids = gameObject.transform.GetChild(2).gameObject;
        
        for (int i = 0; i < grids.transform.childCount; i++)
        {
            grids.transform.GetChild(i).GetComponent<BuildingGridChecker>().grid.GetComponent<GridBlock>().isEmpty = false;
        }
    }

    #endregion
}
