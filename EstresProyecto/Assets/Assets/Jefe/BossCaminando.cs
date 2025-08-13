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
    private Animator anim; // Referencia al Animator

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); // Obtener el Animator

        agente.speed = velocidad;
        IrAPosicionAleatoria();
    }

    void Update()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaAlJugador <= distanciaDeteccion)
        {
            jugadorCerca = true;
            agente.isStopped = true;
            MirarAlJugador();

            if (anim != null)
                anim.enabled = false; // Detener animaciones
        }
        else
        {
            if (jugadorCerca)
            {
                jugadorCerca = false;
                agente.isStopped = false;
                IrAPosicionAleatoria();

                if (anim != null)
                    anim.enabled = true; // Reanudar animaciones
            }

            if (!agente.pathPending && agente.remainingDistance < 0.5f)
            {
                IrAPosicionAleatoria();
            }
        }
    }

    void IrAPosicionAleatoria()
    {
        Vector3 puntoAleatorio = Random.insideUnitSphere * radioMovimiento + transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(puntoAleatorio, out hit, radioMovimiento, NavMesh.AllAreas))
        {
            agente.SetDestination(hit.position);
        }
    }

    void MirarAlJugador()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        direccion.y = 0;
        if (direccion != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 5f);
        }
    }
}
