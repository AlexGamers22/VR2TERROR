using UnityEngine;
using TMPro;
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
        int horas = Mathf.FloorToInt(tiempoTranscurrido / 3600f);
        int minutos = Mathf.FloorToInt((tiempoTranscurrido % 3600f) / 60f);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60f);

        string tiempo = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
        tiemposRegistrados.Add(tiempo);
        Debug.Log("Tiempo registrado: " + tiempo);

        ActualizarTextoTiempos();
    }

    void ActualizarTextoTiempos()
    {
        textoTiemposRegistrados.text = "";

        for (int i = 0; i < tiemposRegistrados.Count; i++)
        {
            textoTiemposRegistrados.text += $"Tarea {i + 1}: {tiemposRegistrados[i]}\n";
        }
    }
}
