using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivos : MonoBehaviour
{
    [Header("Objetos a manejar")]
    public GameObject objetoAparece; // El objeto desactivado que se activar�
    public Transform objetivo; // El objetivo hacia el cual debe apuntar el objeto activado

    void Start()
    {
        if (objetoAparece != null)
        {
            objetoAparece.SetActive(false); // Lo aseguramos que est� desactivado al inicio
        }
    }

    void Update()
    {
        // Si el objeto est� activado y tiene un objetivo asignado
        if (objetoAparece != null && objetoAparece.activeSelf && objetivo != null)
        {
            // Calculamos la direcci�n hacia el objetivo
            Vector3 direccion = objetivo.position - objetoAparece.transform.position;

            // Rotamos el objeto para que apunte hacia el objetivo
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            objetoAparece.transform.rotation = rotacionDeseada;
        }
    }

    // M�todo para activar el objeto y que apunte hacia el objetivo
    public void ActivarObjeto()
    {
        if (objetoAparece != null)
        {
            objetoAparece.SetActive(true); // Activamos el objeto
        }
    }
}
