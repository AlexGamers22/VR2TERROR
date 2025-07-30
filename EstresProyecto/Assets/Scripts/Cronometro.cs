using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ContadorTiempo : MonoBehaviour
{
    public TextMeshProUGUI textoContador;
    public TextMeshProUGUI textoTiemposRegistrados;

    private float tiempoTranscurrido = 0f;
    private bool contadorActivo = false;

    private List<string> tiemposRegistrados = new List<string>();

    void Start()
    {
        Invoke("ActivarContador", 5f);

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneUnloaded += RegistrarCapitulo2;
    }

    void OnDestroy()
    {
        SceneManager.sceneUnloaded -= RegistrarCapitulo2;
    }

    void ActivarContador()
    {
        contadorActivo = true;
    }

    void Update()
    {
        if (contadorActivo)
        {
            tiempoTranscurrido += Time.deltaTime;

            int horas = Mathf.FloorToInt(tiempoTranscurrido / 3600f);
            int minutos = Mathf.FloorToInt((tiempoTranscurrido % 3600f) / 60f);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60f);

            textoContador.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
        }
    }

    public void RegistrarTiempo()
    {
        string tiempo = FormatearTiempo(tiempoTranscurrido);
        TiempoManager.Instancia.tiemposTareas.Add(tiempo);
        ActualizarTextoTiempos();
    }

    void RegistrarCapitulo2(Scene escena)
    {
        if (!contadorActivo) return;

        contadorActivo = false;
        string tiempo = FormatearTiempo(tiempoTranscurrido);
        TiempoManager.Instancia.tiempoCapitulo2 = tiempo;
        Debug.Log("Tiempo Capítulo 2: " + tiempo);
    }


    void ActualizarTextoTiempos()
    {
        textoTiemposRegistrados.text = "";

        for (int i = 0; i < tiemposRegistrados.Count; i++)
        {
            textoTiemposRegistrados.text += $"{tiemposRegistrados[i]}\n";
        }
    }

    string FormatearTiempo(float tiempo)
    {
        int horas = Mathf.FloorToInt(tiempo / 3600f);
        int minutos = Mathf.FloorToInt((tiempo % 3600f) / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        return string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
    }
}
