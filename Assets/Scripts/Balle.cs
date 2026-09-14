using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Balle : MonoBehaviour
{

    // [Header("État de jeu")]



    [Header("Paramètres de tir")]
    Rigidbody rbBalle;


    [Header("Gauge de force")]
    [SerializeField] float forceTir;
    [SerializeField] float accumulateurForce = 0.5f;

    [SerializeField] Slider jaugeForce;


    [Header("Input Actions")]
    [SerializeField] private InputAction tirAction;


    // [Header("Composant")]


    void Start()
    {
        rbBalle = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (tirAction.WasPressedThisFrame())
        {
            forceTir = 0;
            jaugeForce.value = forceTir;
        }
        if (tirAction.IsPressed())
        {
            forceTir+= accumulateurForce;
            forceTir = Mathf.Clamp(forceTir,jaugeForce.minValue,jaugeForce.maxValue);
            jaugeForce.value = forceTir;
        }
        if (tirAction.WasReleasedThisFrame())
        {
            //Envoyer balle
            rbBalle.AddForce(Vector3.forward*forceTir, ForceMode.Impulse);
            forceTir = 0;
            jaugeForce.value = forceTir;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

    }

    void OnTriggerEnter(Collider collision)
    {

    }

    // ===================
    void FrapperBalle()
    {

    }

    void MettreAJourUI()
    {
    }

    // IEnumerator FinJeu()
    // {

    // }

    void SauvegarderScore()
    {

    }

    //=================================
    // Gestion des inputs actions
    void OnEnable()
    {
        tirAction.Enable();
    }

    void OnDisable()
    {
        tirAction.Disable();
    }
}
