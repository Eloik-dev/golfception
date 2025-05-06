using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int levelNumber = 0;
    [SerializeField] private LevelState levelState;

    private void Awake()
    {
        levelState.GameState.SynchronizeLevelNumber(levelNumber);
    }
}
