using TMPro;
using UnityEngine;

public class MostrarTiemposCompletos : MonoBehaviour
{
    public TextMeshProUGUI textoTareas;
    public TextMeshProUGUI textoCapitulo;
    public TextMeshProUGUI textoNivel1;

    void Start()
    {
        if (TiempoManager.Instancia == null) return;

        if (textoTareas != null)
        {
            textoTareas.text = "Tiempos de Tareas:\n";
            for (int i = 0; i < TiempoManager.Instancia.tiemposTareas.Count; i++)
            {
                textoTareas.text += $"Tarea {i + 1}: {TiempoManager.Instancia.tiemposTareas[i]}\n";
            }
        }
        if (textoNivel1 != null)
        {
            textoNivel1.text = "Tiempo total del Capitulo 1: " + TiempoManager.Instancia.tiempoNivel1;
        }
        if (textoCapitulo != null)
        {
            textoCapitulo.text = "Tiempo total del Capítulo 2: " + TiempoManager.Instancia.tiempoCapitulo2;
        }

       
    }
}
