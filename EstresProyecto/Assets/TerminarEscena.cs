using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerminarEscena : MonoBehaviour
{
    public int indiceEscena; // Índice de la escena en Build Settings
    public string tagObjetivo = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            StartCoroutine(CambiarEscenaConDelay(indiceEscena));
        }
    }

    private IEnumerator CambiarEscenaConDelay(int indice)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(indice);
    }
}
