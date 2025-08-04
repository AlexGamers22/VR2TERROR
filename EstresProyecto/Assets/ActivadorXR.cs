using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorXR : MonoBehaviour
{
    public string tagObjetivo = "Sofi"; // El tag del objeto que debe entrar (puedes cambiarlo)
    public GameObject objetoAActivar;     // El objeto que se activará

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            if (objetoAActivar != null)
            {
                objetoAActivar.SetActive(true);
            }
        }
    }
}
