using UnityEngine;
using System.Collections;

public class ControleCamionGlace : MonoBehaviour
{
    [SerializeField] private Transform[] pointsDePassage;
    [SerializeField] private float vitesse = 3f;
    [SerializeField] private float amplitudeVerticale = 0.05f;
    [SerializeField] private float frequenceVerticale = 10f;

    private int indexPointActuel = 0;
    private bool enPause = false;
    private Vector3 positionInitiale;

    void Start()
    {
        positionInitiale = transform.position;
    }

    void Update()
    {
        if (pointsDePassage.Length == 0 || enPause) return;

        Transform cible = pointsDePassage[indexPointActuel];
        Vector3 direction = cible.position - transform.position;
        Vector3 directionHorizontale = new Vector3(direction.x, 0f, direction.z);

        // Mouvement horizontal
        transform.position += directionHorizontale.normalized * vitesse * Time.deltaTime;

        // Appliquer mouvement vertical "rebond"
        float yOffset = Mathf.Sin(Time.time * frequenceVerticale) * amplitudeVerticale;
        transform.position = new Vector3(transform.position.x, positionInitiale.y + yOffset, transform.position.z);

        // Tourner vers la direction
        if (directionHorizontale != Vector3.zero)
            transform.forward = directionHorizontale.normalized;

        // Arrivé au point
        if (direction.magnitude < 0.5f)
        {
            if (indexPointActuel == 1)
            {
                StartCoroutine(PauseAvantDeContinuer(5f));
            }
            else
            {
                AvancerOuDisparaitre();
            }
        }
    }

    private void AvancerOuDisparaitre()
    {
        indexPointActuel++;
        if (indexPointActuel >= pointsDePassage.Length)
        {
            // Disparaître à la fin
            gameObject.SetActive(false);
        }
    }

    private IEnumerator PauseAvantDeContinuer(float duree)
    {
        enPause = true;
        yield return new WaitForSeconds(duree);
        AvancerOuDisparaitre();
        enPause = false;
    }
}
