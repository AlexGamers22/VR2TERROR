using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class Seguirplayer : MonoBehaviour
{
    public Transform jugador;            // Asigna el jugador en el Inspector
    public PlayableDirector timeline;    // Timeline que debe terminar para que el NPC empiece a seguir
    public Animator animador;            // Animator del NPC (asigna en el Inspector)

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
        if (seguirJugador && jugador != null)
        {
            agente.SetDestination(jugador.position);

            // Verifica si el NPC se está moviendo
            if (agente.velocity.magnitude > 0.1f)
            {
                animador.SetBool("isWalking", true); // Cambia a caminar
            }
            else
            {
                animador.SetBool("isWalking", false); // Cambia a Idle
            }
        }
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
