using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ClubHeelController : MonoBehaviour
{
    [SerializeField] private LevelState levelState;

    public float forceMultiplier = 0.1f;
    Rigidbody rb;
    BoxCollider heelCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        heelCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        heelCollider.enabled = !levelState.BallCooldown;
    }

    /// <summary>
    /// Sur collision de la raquette avec la balle, ajouter une force pour la lancer plus loin
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Ball")) return;

        // Ajouter au compteur de coups
        if (levelState.IsBallFlying) return;

        Rigidbody ballRigidbody = collision.rigidbody;
        Vector3 relativeVelocity = ballRigidbody.linearVelocity - rb.linearVelocity;
        Vector3 forceDirection = relativeVelocity.normalized;

        ballRigidbody.AddForce(forceDirection * forceMultiplier, ForceMode.Impulse);
    }
}
