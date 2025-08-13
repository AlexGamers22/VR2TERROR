using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Seguirplayer : MonoBehaviour
{
    [Header("Configuración del Jugador y Timeline")]
    public Transform jugador;
    public PlayableDirector timeline;
    public Animator animador;

    [Header("Movimiento")]
    public float distanciaDeteccion = 5f;
    public Transform plano;
    public Vector2 limitesPlano = new Vector2(10f, 10f);

    [Header("Screamer Settings")]
    public Image screamerImage;          // Imagen del screamer (UI Image)
    public AudioClip screamerSound;      // Sonido del screamer
    public float screamerDuration = 2f;  // Tiempo que se muestra el screamer
    public float minScreamerDistance = 1.5f; // Distancia mínima para activar screamer
    public Collider screamerTrigger;     // Collider para activar el screamer (opcional)

    private NavMeshAgent agente;
    private bool seguirJugador = false;
    private AudioSource audioSource;
    private bool screamerActive = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (timeline != null)
            timeline.stopped += OnTimelineTerminado;

        // Configuración inicial del screamer
        if (screamerImage != null)
            screamerImage.gameObject.SetActive(false);

        if (screamerTrigger != null)
            screamerTrigger.isTrigger = true;
    }

    void Update()
    {
        if (!seguirJugador || jugador == null || screamerActive) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= distanciaDeteccion)
        {
            if (distancia <= minScreamerDistance)
            {
                StartCoroutine(ActivateScreamer());
            }
            else
            {
                TeletransportarNPC();
            }
        }
        else
        {
            agente.SetDestination(jugador.position);
            animador.SetBool("isWalking", agente.velocity.magnitude > 0.1f);
        }
    }

    // Para activar el screamer por collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !screamerActive)
        {
            Debug.Log("Screamer activado por trigger");
            StartCoroutine(ActivateScreamer());
        }
    }

    private IEnumerator ActivateScreamer()
    {
        screamerActive = true;
        agente.isStopped = true; // Detiene el movimiento

        // Activa imagen y sonido
        if (screamerImage != null)
        {
            screamerImage.gameObject.SetActive(true);
        }

        if (screamerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(screamerSound);
        }

        // Espera el tiempo definido
        yield return new WaitForSeconds(screamerDuration);

        // Desactiva el screamer
        if (screamerImage != null)
        {
            screamerImage.gameObject.SetActive(false);
        }

        // Teletransporta después del screamer
        TeletransportarNPC();
        screamerActive = false;
        agente.isStopped = false;
    }

    private void TeletransportarNPC()
    {
        Vector3 nuevaPosicion = plano.position + new Vector3(
            Random.Range(-limitesPlano.x, limitesPlano.x),
            0f,
            Random.Range(-limitesPlano.y, limitesPlano.y)
        );
        agente.Warp(nuevaPosicion);
        animador.SetBool("isWalking", false);
    }

    private void OnTimelineTerminado(PlayableDirector director)
    {
        seguirJugador = true;
    }

    private void OnDestroy()
    {
        if (timeline != null)
            timeline.stopped -= OnTimelineTerminado;
    }
}