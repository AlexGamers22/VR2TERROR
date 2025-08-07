using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerminarEscena : MonoBehaviour
{
    public string escena;

   public void CambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
