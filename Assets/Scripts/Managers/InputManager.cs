using UnityEngine;

// Managers
public class InputManager : MonoBehaviour
{
    #region Singleton

    public static InputManager Instance { get; private set; }

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
    
    public RaycastHit2D MouseToScreenRay() =>
        Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    
    public bool LeftClick() => Input.GetMouseButtonDown(0);
    
    public bool RightClick() => Input.GetMouseButtonDown(1);
}
