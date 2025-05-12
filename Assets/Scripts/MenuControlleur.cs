using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuControlleur : MonoBehaviour
{
    [SerializeField] private Button[] boutonsNiveaux;
    [SerializeField] private GameObject[] ecrans;
    [SerializeField] private Slider curseurCouleur;
    [SerializeField] private Slider curseurHauteur;
    [SerializeField] private Slider curseurMusique;
    [SerializeField] private Slider curseurEffets;
    [SerializeField] private GameState gameState;

    void Start()
    {
        // Applique la couleur initiale et �coute les changements du slider
        curseurCouleur.onValueChanged.AddListener(gameState.SetClubColor);
        curseurCouleur.SetValueWithoutNotify(gameState.GetClubColorValue());

        // �coute les changements du slider de hauteur
        curseurHauteur.onValueChanged.AddListener(gameState.SetOffsetHeight);
        curseurHauteur.SetValueWithoutNotify(gameState.GetOffsetHeight());
        
        // �coute les changements du slider de musique
        curseurMusique.onValueChanged.AddListener(gameState.SetMusiqueVolume);
        curseurMusique.SetValueWithoutNotify(gameState.GetMusiqueVolume());

        // �coute les changements du slider des effets
        curseurEffets.onValueChanged.AddListener(gameState.SetEffectsVolume);
        curseurEffets.SetValueWithoutNotify(gameState.GetEffectsVolume());

        // Pr�parer les boutons
        foreach (Button bouton in boutonsNiveaux)
        {
            bouton.onClick.AddListener(() => {
                string text = bouton.GetComponentInChildren<TextMeshProUGUI>().text;
                int.TryParse(text, out int nombre);
                gameState.ChargerScene(nombre);
            });
        }
    }

    public void SwitchActive(GameObject view)
    {
        for (int i = 0; i < ecrans.Length; i++)
        {
            ecrans[i].SetActive(false);
        }

        view.SetActive(true);
    }
}
