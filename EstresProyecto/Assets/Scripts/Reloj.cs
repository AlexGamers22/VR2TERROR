using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloj : MonoBehaviour
{
    public GameObject canvasAActivar;
    private bool canvasActivo = false;
    private bool puedeCambiar = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Indice") && puedeCambiar)
        {
            canvasActivo = !canvasActivo; // Cambia el estado del canvas
            canvasAActivar.SetActive(canvasActivo);
            puedeCambiar = false; // Bloquea hasta que salga
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Indice"))
        {
            puedeCambiar = true; // Permite volver a activar/desactivar
        }
    }
}
