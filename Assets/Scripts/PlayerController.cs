using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LevelState levelState;
    [SerializeField] private GameObject mainMenu;

    private float _initialHeight = 0f;

    private void Awake()
    {
        _initialHeight = transform.position.y;
    }

    public void ToggleShowMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
    }
}