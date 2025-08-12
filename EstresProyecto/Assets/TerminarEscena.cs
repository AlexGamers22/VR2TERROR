using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerminarEscena : MonoBehaviour
{
    public string escena;
    public string tagObjetivo = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            StartCoroutine(CambiarEscenaConDelay(escena));
        }
    }

    private IEnumerator CambiarEscenaConDelay(string escena)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(escena);
    }
}
