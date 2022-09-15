using DG.Tweening;
using UnityEngine;

public class GridBlockManager : MonoBehaviour
{
    #region Singleton

    public static GridBlockManager Instance { get; private set; }

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

    public void GreyToAllGridBlocks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            DOTweenModuleSprite.DOColor(transform.GetChild(i).GetComponent<SpriteRenderer>(), new Color(1,1,1,.9f), .5f);
        }
    }
}
