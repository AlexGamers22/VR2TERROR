using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aparicion : MonoBehaviour
{
    [Header("Configuración del objeto")]
    public GameObject objetoAparecer; // El objeto que aparecerá
    public Vector3 posicionAparicion; // Posición donde aparecerá
    public Vector3 rotacionAparicion; // Rotación donde aparecerá

    [Header("Detección")]
    public string tagObjetivo = "Carro"; // Tag del objeto que activa el trigger

    private void Start()
    {
        if (objetoAparecer != null)
            objetoAparecer.SetActive(false); // Asegura que empiece oculto
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            if (objetoAparecer != null)
            {
                objetoAparecer.transform.position = posicionAparicion;
                objetoAparecer.transform.rotation = Quaternion.Euler(rotacionAparicion);
                objetoAparecer.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            if (objetoAparecer != null)
            {
                objetoAparecer.SetActive(false);
            }
        }
    }
}
