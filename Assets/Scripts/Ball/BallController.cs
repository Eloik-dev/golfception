using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class BallController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float maxSpeed = 10f;
    [SerializeField] bool alwaysRespawnAtStart = false;
    [SerializeField] private LevelState levelState;
    [SerializeField] private Transform holeTransform;
    
    private AudioSource sonCoup;

    private Vector3 initialPositionSinceStart = Vector3.zero;

    private Vector3 lastHitPosition = Vector3.zero;

    Renderer ballRenderer;

    bool resettingCooldown = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sonCoup = GetComponent<AudioSource>();
        ballRenderer = GetComponentInChildren<Renderer>();
    }

    private void Start()
    {
        initialPositionSinceStart = transform.position;

        // Trouver la distance entre la balle et le trou
        levelState.InitializeDistance(Vector3.Distance(initialPositionSinceStart, holeTransform.position));
    }

    private void Update()
    {
        if (levelState.BallCooldown && !resettingCooldown)
        {
            resettingCooldown = true;
            StartCoroutine(TriggerReactivation());
        }

        ballRenderer.material.SetColor("_BaseColor", levelState.BallCooldown ? Color.gray : Color.white);
    }

    private void FixedUpdate()
    {
        // Limiter la vélocité à la vitesse maximale possible
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private IEnumerator TriggerReactivation()
    {
        yield return new WaitForSeconds(1.5f);
        levelState.SetBallCooldown(false);
        resettingCooldown = false;
    }

    /// <summary>
    /// S'active lorsque la balle entre en collision
    /// </summary>
    /// <param name="collision">L'objet que la balle a fait collision</param>
    private void OnCollisionEnter(Collision collision)
    {
        sonCoup.Play();

        // Vérifier si la collision est avec le trou
        if (collision.collider.CompareTag("Hole"))
        {
            levelState.GameState.ProchainNiveau();
            return;
        }

        // Vérifier si la collision est avec une zone de redémarrage
        if (collision.collider.CompareTag("RestartZone"))
        {
            transform.position = alwaysRespawnAtStart ? initialPositionSinceStart : lastHitPosition;
            rb.linearVelocity = Vector3.zero;
            levelState.SetBallCooldown(true);
            return;
        }

        // Si avec le club, garder la position du coup
        if (!levelState.IsBallFlying && collision.collider.CompareTag("ClubHeel"))
        {
            levelState.AddBallHitCount();
            lastHitPosition = transform.position;
        }
    }
}
