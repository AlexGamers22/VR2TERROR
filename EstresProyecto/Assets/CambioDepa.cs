using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CambioDepa : MonoBehaviour
{
    [Header("Configuración de cambio de escena")]
    public int indiceEscenaDestino = 1; // Índice de la escena en Build Settings
    public float tiempoEspera = 5f; // Segundos a esperar antes de cambiar

    [Header("Detección")]
    public string tagCarro = "Carro"; // Tag del carro

    private bool cambioEnProgreso = false; // Evita múltiples llamadas

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagCarro) && !cambioEnProgreso)
        {
            cambioEnProgreso = true;
            StartCoroutine(CambiarEscenaDespuesDeTiempo());
        }
    }

    private IEnumerator CambiarEscenaDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(indiceEscenaDestino);
    }
}
