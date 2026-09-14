using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Balle : MonoBehaviour
{

    // [Header("État de jeu")]



    // [Header("Paramètres de tir")]
    private float force;



    // [Header("Gauge de force")]


    // [Header("Input Actions")]
    [SerializeField] private InputAction espace;


    // [Header("Composant")]


    void Start()
    {

    }

    void Update()
    {


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
        espace.Enable();
    }

    void OnDisable()
    {
        espace.Disable();
    }
}
