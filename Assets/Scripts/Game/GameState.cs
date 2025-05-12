using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameState", menuName = "Scriptable Objects/GameState")]
public class GameState : ScriptableObject
{
    private float _offsetHeight = 0;
    private Color _clubColor = Color.white;
    private float _clubColorValue = 0;
    public UnityEvent<float> OnMusicVolumeChanged = new UnityEvent<float>();
    private float _musicVolume = 0;
    public UnityEvent<float> OnEffectsVolumeChanged = new UnityEvent<float>();
    private float _effectsVolume = 0;
    private int _currentLevel = 0;
    private bool _rightHanded = true;

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
            SceneManager.LoadScene("Menu");
            //Debug.LogError($"La sc�ne '{scene}' n'existe pas dans les sc�nes du build.");
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
        _clubColorValue = value;
    }
    
    public float GetClubColorValue()
    {
        return _clubColorValue;
    }
    public float GetMusiqueVolume()
    {
        return _musicVolume;
    }
    
    public void SetMusiqueVolume(float value)
    {
        _musicVolume = value;
        OnMusicVolumeChanged.Invoke(_musicVolume);
    }
    
    public float GetEffectsVolume()
    {
        return _effectsVolume;
    }
    
    public void SetEffectsVolume(float value)
    {
        _effectsVolume = value;
        OnEffectsVolumeChanged.Invoke(_effectsVolume);
    }

    public bool GetRightHanded()
    {
        return _rightHanded;
    }

    public void ToggleRightHanded()
    {
        _rightHanded = !_rightHanded;
    }

    private void Reset()
    {
    }
}
