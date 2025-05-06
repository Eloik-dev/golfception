using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LevelState levelState;

    private float _initialHeight = 0f;

    private void Awake()
    {
        _initialHeight = transform.position.y;
    }

    private void Update()
    {
        // transform.position = new Vector3(transform.position.x, _initialHeight + levelState.GameState.GetOffsetHeight(), transform.position.z);
    }
}