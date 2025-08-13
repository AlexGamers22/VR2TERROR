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
    private Animator animator;
    private Rigidbody rb;

    private bool jugadorCerca = false;
    private float velocidadOriginal;

    void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (agente == null)
        {
            Debug.LogError("Este NPC necesita un NavMeshAgent.");
            enabled = false;
            return;
        }

        if (jugador == null)
            Debug.LogWarning("Asigna el Transform del jugador en el inspector.");

        velocidadOriginal = velocidad;
        agente.speed = velocidad;
        IrAPosicionAleatoria();
    }

    void Update()
    {
        if (jugador == null) return;

        // Distancia con sqrMagnitude (más barato)
        float distSqr = (jugador.position - transform.position).sqrMagnitude;
        bool estaCerca = distSqr <= distanciaDeteccion * distanciaDeteccion;

        if (estaCerca)
        {
            if (!jugadorCerca)
            {
                jugadorCerca = true;
                DetenerAgente();
            }

            MirarAlJugador();
            ActualizarAnimaciones(0f); // Idle
        }
        else
        {
            if (jugadorCerca)
            {
                jugadorCerca = false;
                ReanudarAgente();
                IrAPosicionAleatoria();
            }

            if (!agente.pathPending && agente.remainingDistance <= 0.3f)
            {
                IrAPosicionAleatoria();
            }

            ActualizarAnimaciones(agente.velocity.magnitude);
        }
    }

    void DetenerAgente()
    {
        agente.isStopped = true;
        agente.ResetPath();
        agente.velocity = Vector3.zero;
        agente.updateRotation = false; // nosotros controlamos la rotación

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (animator)
        {
            animator.SetBool("activo", false);
            animator.SetBool("idle", true);
            // Si tu animación empuja con Root Motion, desactívalo:
            animator.applyRootMotion = false;
        }
    }

    void ReanudarAgente()
    {
        agente.isStopped = false;
        agente.updateRotation = true;
        agente.speed = velocidadOriginal;

        if (animator)
        {
            animator.SetBool("idle", false);
        }
    }

    void IrAPosicionAleatoria()
    {
        if (jugadorCerca) return; // no pedir destinos si está detenido mirando

        Vector3 puntoAleatorio = transform.position + Random.insideUnitSphere * radioMovimiento;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(puntoAleatorio, out hit, radioMovimiento, NavMesh.AllAreas))
        {
            agente.SetDestination(hit.position);
        }
    }

    void MirarAlJugador()
    {
        Vector3 dir = jugador.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion rotDeseada = Quaternion.LookRotation(dir.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotDeseada, Time.deltaTime * 6f);
    }

    void ActualizarAnimaciones(float vel)
    {
        if (!animator) return;

        animator.SetFloat("speed", vel);
        bool caminando = vel > 0.05f && !jugadorCerca;
        animator.SetBool("activo", caminando);
        animator.SetBool("idle", !caminando);
    }

    // Dibuja el radio de detección en escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccion);
    }
}