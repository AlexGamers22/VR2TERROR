using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossCaminando : MonoBehaviour
{
    [Header("Movimiento aleatorio")]
    public float radioMovimiento = 10f;
    public float velocidad = 3.5f;

    [Header("Detección del jugador")]
    public Transform jugador;
    public float distanciaDeteccion = 5f;

    private NavMeshAgent agente;
    private bool jugadorCerca = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        if (agente == null)
        {
            Debug.LogError("Este NPC necesita un NavMeshAgent para funcionar.");
            enabled = false;
            return;
        }

        agente.speed = velocidad;
        IrAPosicionAleatoria();
    }

    void Update()
    {
        if (jugador == null) return; // Seguridad si no asignaste el jugador

        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= distanciaDeteccion)
        {
            // Si está cerca del jugador
            jugadorCerca = true;
            agente.isStopped = true;
            MirarAlJugador();
        }
        else
        {
            // Si estaba cerca pero ya no
            if (jugadorCerca)
            {
                jugadorCerca = false;
                agente.isStopped = false;
                IrAPosicionAleatoria();
            }

            // Si llegó a su destino, buscar otro
            if (!agente.pathPending && agente.remainingDistance < 0.5f)
            {
                IrAPosicionAleatoria();
            }
        }
    }

    void IrAPosicionAleatoria()
    {
        // Busca un punto aleatorio en el NavMesh
        Vector3 puntoAleatorio = Random.insideUnitSphere * radioMovimiento + transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(puntoAleatorio, out hit, radioMovimiento, NavMesh.AllAreas))
        {
            agente.SetDestination(hit.position);
        }
    }

    void MirarAlJugador()
    {
        // Rotar hacia el jugador sin inclinarse
        Vector3 direccion = (jugador.position - transform.position).normalized;
        direccion.y = 0;

        if (direccion != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 5f);
        }
    }
}
