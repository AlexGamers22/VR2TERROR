using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Textos : MonoBehaviour
{
    [Header("Texto de UI")]
    public TextMeshProUGUI textoTMP;  // El texto TextMeshPro que se mostrar�

    void Start()
    {
        // Aseg�rate de que el texto TMP est� desactivado al inicio
        if (textoTMP != null)
        {
            textoTMP.gameObject.SetActive(false); // Desactivamos el texto al inicio
        }
    }

    // M�todo para mostrar el texto
    public void MostrarTexto(string mensaje)
    {
        if (textoTMP != null)
        {
            textoTMP.gameObject.SetActive(true);  // Activamos el texto
            textoTMP.text = mensaje;  // Establecemos el mensaje del texto
        }

        // Llamamos a la funci�n para desactivar el texto despu�s de unos segundos
        StartCoroutine(DesactivarTexto());
    }

    // Coroutine para desactivar el texto despu�s de un tiempo
    private IEnumerator DesactivarTexto()
    {
        // Esperamos el tiempo especificado (por ejemplo, 3 segundos)
        yield return new WaitForSeconds(3f);

        // Desactivamos el texto despu�s de los 3 segundos
        if (textoTMP != null)
        {
            textoTMP.gameObject.SetActive(false); // Desactivamos el texto
        }
    }
}
