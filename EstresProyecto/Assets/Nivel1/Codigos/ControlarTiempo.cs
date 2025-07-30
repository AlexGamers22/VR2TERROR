using System.Collections.Generic;
using UnityEngine;

public class TiempoManager : MonoBehaviour
{
    public static TiempoManager Instancia;

    public List<string> tiemposTareas = new List<string>();
    public string tiempoCapitulo2;
    public string tiempoNivel1;

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
