using System.Collections;
using UnityEngine;
using TMPro;

public class RelojElevador : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject canvasAActivar;
    [SerializeField] private GameObject mensajeNotificacion;
    [SerializeField] private GameObject objetoExtra;
    [SerializeField] private AudioSource audioNotificacion;
    [SerializeField] private Material relojRenderer;
    [SerializeField] private TextMeshProUGUI texto;

    [Header("Parámetros de Tiempo")]
    [SerializeField] private float parpadeoVelocidad = 0.5f;
    [SerializeField] private float activarNotificacionEscena = 5f;
    [SerializeField] private float tiempoActivarReloj = 15f;

    private bool canvasActivo = false;
    private bool puedeCambiar = true;
    private bool notificacionActiva = false;
    private bool relojAbiertoManual = false; 
    private Color colorOriginal;
    private Coroutine parpadeoCoroutine;

    private void Start()
    {
        activarReloj();
    }

 
    public void activarReloj()
    {
        if (relojRenderer != null)
            colorOriginal = relojRenderer.color;

        if (objetoExtra != null)
            objetoExtra.SetActive(false);

        StartCoroutine(NotificarYMostrarReloj());
    }



    private IEnumerator NotificarYMostrarReloj()
    {
        yield return new WaitForSeconds(activarNotificacionEscena);

        notificacionActiva = true;

        if (audioNotificacion != null)
        {
            audioNotificacion.loop = true;
            audioNotificacion.Play();
        }

        if (relojRenderer != null)
            parpadeoCoroutine = StartCoroutine(ParpadearMaterial());

        yield return new WaitForSeconds(tiempoActivarReloj);

        if (!relojAbiertoManual)
            MostrarCanvas(true);

        StopCoroutine(parpadeoCoroutine); 
        relojRenderer.color = colorOriginal; 

        if (audioNotificacion != null)
            audioNotificacion.Stop(); 

        yield return new WaitForSeconds(5f);

        if (objetoExtra != null)
            objetoExtra.SetActive(false);

        if (canvasAActivar != null)
            canvasAActivar.SetActive(false);

        
    }


    private void MostrarCanvas(bool estado)
    {
        canvasActivo = estado;
        if (canvasAActivar != null) canvasAActivar.SetActive(estado);
        if (mensajeNotificacion != null) mensajeNotificacion.SetActive(estado);
        if (objetoExtra != null) objetoExtra.SetActive(estado && notificacionActiva);
    }

    private IEnumerator ParpadearMaterial()
    {
        bool blanco = false;
        while (true)
        {
            relojRenderer.color = blanco ? Color.white : colorOriginal;
            blanco = !blanco;
            yield return new WaitForSeconds(parpadeoVelocidad);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Indice") && puedeCambiar)
        {
            MostrarCanvas(!canvasActivo);
            puedeCambiar = false;

            if (canvasActivo)
            {
                if (notificacionActiva)
                    relojAbiertoManual = true;

                if (parpadeoCoroutine != null && relojRenderer != null)
                {
                    StopCoroutine(parpadeoCoroutine);
                    relojRenderer.color = Color.black;
                }

                if (notificacionActiva && audioNotificacion != null)
                {
                    audioNotificacion.Stop();
                    notificacionActiva = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Indice"))
        {
            puedeCambiar = true;
        }
    }
}
