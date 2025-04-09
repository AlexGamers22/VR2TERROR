using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;   

public class CambioGarra : MonoBehaviour
{
    public GameObject objetoObjetivo;  // Arrastra el objeto específico aquí en el Inspector

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el que has especificado
        if (other.gameObject == objetoObjetivo)
        {
            // Verifica si el objeto tiene un Rigidbody
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Activa la opción isKinematic del Rigidbody
                rb.isKinematic = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale es el que has especificado
        if (other.gameObject == objetoObjetivo)
        {
            // Verifica si el objeto tiene un Rigidbody
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Desactiva la opción isKinematic del Rigidbody
                rb.isKinematic = false;
            }
        }
    }
}
