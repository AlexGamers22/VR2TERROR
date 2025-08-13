using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class Seguirplayer : MonoBehaviour
{
    [Header("Configuración del Jugador y Timeline")]
    public Transform jugador;            // Asigna el jugador en el Inspector
    public PlayableDirector timeline;    // Timeline que debe terminar para que el NPC empiece a seguir
    public Animator animador;            // Animator del NPC (asigna en el Inspector)

    [Header("Movimiento")]
    public float distanciaDeteccion = 5f; // Distancia para teletransportarse
    public Transform plano;              // El plano donde se puede teletransportar
    public Vector2 limitesPlano = new Vector2(10f, 10f); // Tamaño del área de teletransporte

    private NavMeshAgent agente;
    private bool seguirJugador = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        if (timeline != null)
            timeline.stopped += OnTimelineTerminado;
    }

    void Update()
    {
        if (!seguirJugador || jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= distanciaDeteccion)
        {
            TeletransportarNPC();
        }
        else
        {
            agente.SetDestination(jugador.position);

            // Animaciones
            animador.SetBool("isWalking", agente.velocity.magnitude > 0.1f);
        }
    }

    private void TeletransportarNPC()
    {
        // Calcula una posición aleatoria en el plano
        Vector3 nuevaPosicion = plano.position + new Vector3(
            Random.Range(-limitesPlano.x, limitesPlano.x),
            0f,
            Random.Range(-limitesPlano.y, limitesPlano.y)
        );

        // Teletransporta al NPC
        agente.Warp(nuevaPosicion);

        // Cambia animación a idle después del TP
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
