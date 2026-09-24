using UnityEngine;

public class GestionnaireJeu : MonoBehaviour
{
    public static GestionnaireJeu instance;
    public string etat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Pattern singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        etat = "jeu";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
