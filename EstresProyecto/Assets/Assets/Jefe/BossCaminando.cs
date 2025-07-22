using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCaminando : MonoBehaviour
{
    public float velMovimiento, velRotacion;

    public float tiempoReaccion;
    int movimiento;
    bool espera, camina, gira, jugadorDetectado;
    private Transform player;



    private void Start()
    {
        accion();
    }
    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 7f))
        {
            if(hit.collider)
            {
                gira = true;
                StartCoroutine(timepoGiro());
            }
        }

        if (jugadorDetectado)
        {
            // Hacer animación de espera y girar hacia el jugador
            espera = true;
            camina = false;
            gira = false;

            GetComponent<Animator>().SetBool("Activo", false);

            // Girar hacia el jugador
            Vector3 direccion = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * velRotacion);
        }


        if (espera) 
        {
            GetComponent<Animator>().SetBool("Activo", false);
        }

        if(camina)
        {
            GetComponent<Animator>().SetBool("Activo", true);
            transform.position += (transform.forward * velMovimiento * Time.deltaTime);
        }

        if (gira)
        {
            transform.Rotate(Vector3.up * velRotacion * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            jugadorDetectado = true;
            player = other.transform; // Guardar la referencia al jugador
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el jugador sale de la zona del NPC
        if (other.CompareTag("Player"))
        {
            jugadorDetectado = false;
            player = null; // Limpiar la referencia al jugador
        }
    }



    void accion()
    {
        movimiento = Random.Range(1, 4);

        if (movimiento == 1)
        {
            camina = true;
            espera = false;

        }
        if (movimiento == 2)
        {
            espera = true;
            camina = false;
        }
        if (movimiento == 3)
        {
            gira = true;
            StartCoroutine(timepoGiro());
        }
        Invoke("accion", tiempoReaccion);
    }
    IEnumerator timepoGiro()
    {
        yield return new WaitForSeconds(2);
        gira = false;
    }
}
