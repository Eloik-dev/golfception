using TMPro;
using UnityEngine;

public class PointsInfoController : MonoBehaviour
{
    [SerializeField] private LevelState levelState;
    [SerializeField] private TextMeshProUGUI scoreText;


    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + levelState.GetScore().ToString();
    }
}
