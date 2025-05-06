using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelState", menuName = "Scriptable Objects/LevelState")]
public class LevelState : ScriptableObject
{
    [SerializeField] private GameState _gameState;
    public GameState GameState {  
        get 
        { 
            return _gameState; 
        }
    }

    private bool _isBallFlying = false;
    public bool IsBallFlying
    {
        get
        {
            return _isBallFlying;
        }
    }

    /// <summary>
    /// Distance initiale de la balle au trou
    /// </summary>
    private float _initialDistance = 0;
    public float InitialDistance
    {
        get
        {
            return _initialDistance;
        }
    }

    /// <summary>
    /// Nombre de coups sur la balle
    /// </summary>
    private int _numberOfHits = 0;
    public int NumberOfHits { 
        get 
        { 
            return _numberOfHits; 
        } 
    }

    /// <summary>
    /// Si la balle a été ramenée
    /// </summary>
    private bool _ballCooldown = false;
    public bool BallCooldown
    {
        get
        {
            return _ballCooldown;
        }
    }

    /// <summary>
    /// Initialiser la distance entre la balle et le trou
    /// </summary>
    public void InitializeDistance(float value)
    {
        _initialDistance = value;
    }
    
    /// <summary>
    /// Lorsque la balle est en mouvement
    /// </summary>
    public void NotifyBallIsFlying(bool value)
    {
        _isBallFlying = value;
    }
    
    /// <summary>
    /// Ajouter 1 au compteur de coups
    /// </summary>
    public void AddBallHitCount()
    {
        _numberOfHits++;
    }

    /// <summary>
    /// Met la balle en cooldown ou non
    /// </summary>
    public void SetBallCooldown(bool value)
    {
        _ballCooldown = value;
    }

    /// <summary>
    /// Retourne le score du joueur
    /// </summary>
    public int GetScore()
    {
        return ((int)_initialDistance * 100) / ((int)_numberOfHits + 1);
    }

    /// <summary>
    /// Réinitialise toutes les valeurs à leur défaut
    /// </summary>
    public void Reset()
    {
        _isBallFlying = false;
        _numberOfHits = 0;
        _ballCooldown = false;
    }
}
