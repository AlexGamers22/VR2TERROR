using UnityEngine;
using TMPro;

public class CronometroNivel1 : MonoBehaviour
{
    public TextMeshProUGUI textoCronometro;

    private float tiempo = 0f;
    private bool activo = true;

    void Update()
    {
        if (activo)
        {
            tiempo += Time.deltaTime;

            int minutos = Mathf.FloorToInt(tiempo / 60f);
            int segundos = Mathf.FloorToInt(tiempo % 60f);

            textoCronometro.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    void OnDisable()
    {
        if (TiempoManager.Instancia != null)
        {
            int minutos = Mathf.FloorToInt(tiempo / 60f);
            int segundos = Mathf.FloorToInt(tiempo % 60f);
            TiempoManager.Instancia.tiempoNivel1 = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }
}
