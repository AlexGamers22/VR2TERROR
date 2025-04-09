using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teclado : MonoBehaviour
{
    [Header("Objeto a desactivar")]
    public GameObject objetoADesactivar;  // El objeto que quieres desactivar cuando se detecte el trigger
    public GameObject TecladoTexto;
    void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entró tiene el tag "Hojas"
        if (other.CompareTag("Teclado"))
        {
            // Desactivamos el objeto que tiene este script
            gameObject.SetActive(false);

            // Desactivamos el objeto que se ha asignado a la referencia
            if (objetoADesactivar != null)
            {
                objetoADesactivar.SetActive(false); // Desactivamos el objeto asignado
                TecladoTexto.SetActive(false);
            }
        }
    }
}
