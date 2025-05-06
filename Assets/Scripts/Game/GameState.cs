using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameState", menuName = "Scriptable Objects/GameState")]
public class GameState : ScriptableObject
{
    private float _offsetHeight = 0;
    private Color _clubColor = Color.white;
    private float _musicVolume = 0;
    private int _currentLevel = 0;

    public void SynchronizeLevelNumber(int number)
    {
        _currentLevel = number;
    }

    public void ChargerScene(int nombre)
    {
        string scene = "Niveau" + nombre;

        if (Application.CanStreamedLevelBeLoaded(scene))
        {
            SceneManager.LoadScene(scene);
            _currentLevel = nombre;
        }
        else
        {
            Debug.LogError($"La scène '{scene}' n'existe pas dans les scènes du build.");
        }
    }

    public void ProchainNiveau()
    {
        ChargerScene(_currentLevel + 1);
    }

    public float GetOffsetHeight()
    {
        return _offsetHeight;
    }
    
    public void SetOffsetHeight(float value)
    {
        _offsetHeight = value;
    }
    
    public Color GetClubColor()
    {
        return _clubColor;
    }
    
    public void SetClubColor(float value)
    {
        _clubColor = Color.HSVToRGB(value, 1f, 1f);
    }
    
    public float GetMusiqueVolume()
    {
        return _musicVolume;
    }
    
    public void SetMusiqueVolume(float value)
    {
        _musicVolume = value;
    }

    private void Reset()
    {
    }
}
