using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class Balle : MonoBehaviour
{

    [Header("État de jeu")]
    Vector3 positionBalle;
    private int nbCoups;
    public bool peutJouer;


    [Header("Paramètres de tir")]
    Rigidbody rbBalle;
    [SerializeField] float angleTir;


    [Header("Gauge de force")]
    [SerializeField] float forceTir;
    [SerializeField] float accumulateurForce = 1f;

    [SerializeField] Slider jaugeForce;


    [Header("Input Actions")]
    [SerializeField] private InputAction tirAction;
    [SerializeField] private InputAction angleAction;


    [Header("Composant")]
    LineRenderer lineRendererBalle;
    [SerializeField] TMP_Text coupsTexte;
    AudioSource audioSourceBalle;
    [SerializeField] AudioClip sonErreur;
    [SerializeField] AudioClip sonFin;

    void Start()
    {
        rbBalle = GetComponent<Rigidbody>();
        lineRendererBalle = GetComponent<LineRenderer>();
        audioSourceBalle = GetComponent<AudioSource>();
        nbCoups = 0;
        MettreAJourUI();
        peutJouer = true;
    }

    void Update()
    {
        if (peutJouer && GestionnaireJeu.instance.etat == "jeu")
        {
            angleTir += angleAction.ReadValue<float>();
            Vector3 direction = Quaternion.Euler(0, angleTir, 0) * Vector3.forward;
            lineRendererBalle.SetPosition(0, transform.position);
            lineRendererBalle.SetPosition(1, transform.position + direction);

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
            positionBalle = transform.position;
            rbBalle.AddForce(direction*(forceTir*2)*Time.deltaTime, ForceMode.Impulse);
            forceTir = 0;
            jaugeForce.value = forceTir;
            nbCoups++;

            MettreAJourUI();
            StartCoroutine(AttendreFinCoup());
        }

            coupsTexte.text = $"{nbCoups} coup(s)";
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "horsParcours")
        {
            audioSourceBalle.PlayOneShot(sonErreur);
            rbBalle.linearVelocity = Vector3.zero;
            rbBalle.angularVelocity = Vector3.zero;
            transform.position = positionBalle;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "trou")
        {
            peutJouer = false;
            audioSourceBalle.PlayOneShot(sonFin);
            rbBalle.linearVelocity = Vector3.zero;
            rbBalle.angularVelocity = Vector3.zero;
            rbBalle.useGravity = false;
            transform.position = collision.transform.position;
        }
    }

    // ===================
    IEnumerator AttendreFinCoup()
    {
        Debug.Log("Debut");
        peutJouer = false;
        lineRendererBalle.enabled = false;
        yield return new WaitForFixedUpdate();// Attends de calculer la physique
        // yield return new WaitForSeconds(2); // Attend 2 secondes
        float vitesse = rbBalle.linearVelocity.magnitude;
        while(vitesse>0.1f)
        {
            vitesse = rbBalle.linearVelocity.magnitude;
            yield return null; //Attends au prochain frame
        }

        lineRendererBalle.enabled = true;
        peutJouer = true;
        Debug.Log("Fin");
    }
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
        angleAction.Enable();
    }

    void OnDisable()
    {
        tirAction.Disable();
        angleAction.Disable();
    }
}
