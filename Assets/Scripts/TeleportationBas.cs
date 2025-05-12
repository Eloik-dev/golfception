using UnityEngine;
using UnityEngine.UI;

public class TeleportationBas : MonoBehaviour
{
    [SerializeField] private Button bonutonsTeleportation;
    [SerializeField] private Transform joueur;
    [SerializeField] private Vector3 teleportationPosition;
    [SerializeField] private AudioClip son;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        bonutonsTeleportation.onClick.AddListener(TeleportPlayer);
    }

    private void TeleportPlayer()
    {
        joueur.position = teleportationPosition;

        if (son != null && audioSource != null)
        {
            audioSource.resource = son;
            audioSource.Play();
        }
    }
}
