    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;


public class Hojas : MonoBehaviour
    {
    [Header("Objeto a desactivar")]
    public GameObject objetoADesactivar;  // El objeto que quieres desactivar cuando se detecte el trigger
    public GameObject ActivarTexto; // El objeto que contiene el texto (TextMeshPro o cualquier otro)
    public GameObject consejo;
    // Tiempo que el texto se mantendrá activo antes de desactivarse

    void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entró tiene el tag "Hojas"
        if (other.CompareTag("Hojas"))
        {
            // Desactivamos el objeto que tiene este script
            gameObject.SetActive(false);

            // Desactivamos el objeto asignado en la referencia
            if (objetoADesactivar != null)
            {
                objetoADesactivar.SetActive(false); // Desactivamos el objeto asignado
                consejo.SetActive(false);
            }

            // Activamos el texto
            if (ActivarTexto != null)
            {
                ActivarTexto.SetActive(true); // Activamos el objeto de texto
            }

            
        }
    }

    // Coroutine para desactivar el texto después de un tiempo
    

}
