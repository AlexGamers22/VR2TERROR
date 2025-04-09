using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirarobjetivo : MonoBehaviour
{
    public Transform objetivo;  // El XR Origin o el jugador

    void Update()
    {
        if (objetivo != null)
        {
            // Calculamos la dirección hacia el objetivo
            Vector3 direccion = objetivo.position - transform.position;

            // Rotamos la flecha para que siempre apunte al objetivo
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            transform.rotation = rotacionDeseada;
        }
    }
}
