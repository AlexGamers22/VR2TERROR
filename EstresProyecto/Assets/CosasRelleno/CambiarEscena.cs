using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void Escena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void adios()
    {
        Application.Quit();
    }

}
