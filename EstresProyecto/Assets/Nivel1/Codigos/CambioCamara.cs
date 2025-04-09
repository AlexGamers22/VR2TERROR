using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCamara : MonoBehaviour
{
    // Referencias a los objetos que vas a activar y desactivar.
    public GameObject objetoADesactivar;
    public GameObject objetoAActivar;

    private void Start()
    {
        // Inicia la corrutina para manejar la activación y desactivación.
        StartCoroutine(CambiarObjetos());
    }

    private IEnumerator CambiarObjetos()
    {
        // Espera 20 segundos.
        yield return new WaitForSeconds(20);

        // Desactiva el primer objeto y activa el segundo.
        objetoADesactivar.SetActive(false);
        objetoAActivar.SetActive(true);
    }

}
