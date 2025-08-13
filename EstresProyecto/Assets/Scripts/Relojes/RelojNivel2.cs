using System.Collections;
using UnityEngine;
using TMPro;

public class Relojnivel2 : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject canvasAActivar;
    [SerializeField] private GameObject mensajeNotificacion;
    [SerializeField] private GameObject objetoExtra;
    [SerializeField] private AudioSource audioNotificacion;
    [SerializeField] private Material relojRenderer;
    [SerializeField] private TextMeshProUGUI texto;

    // 🔹 Objeto que quieres ocultar después de 5 segundos
    [SerializeField] private GameObject objetoADesaparecer;

    [Header("Parámetros de Tiempo")]
    [SerializeField] private float parpadeoVelocidad = 0.5f;
    [SerializeField] private float activarNotificacionEscena = 5f;
    [SerializeField] private float tiempoActivarReloj = 15f;
    [SerializeField] public float tiempoCerrarAutomatico = 10f;

    private bool canvasActivo = false;
    private bool puedeCambiar = true;
    private bool notificacionActiva = false;
    private bool relojAbiertoManual = false;
    private Color colorOriginal;
    private Coroutine parpadeoCoroutine;
    private Coroutine cerrarRelojCoroutine;

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

        while (canvasAActivar != null && canvasAActivar.activeSelf)
            yield return null;

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
        {
            MostrarCanvas(true);
        }

        if (parpadeoCoroutine != null && relojRenderer != null)
        {
            StopCoroutine(parpadeoCoroutine);
            relojRenderer.color = colorOriginal;
        }

        if (audioNotificacion != null && notificacionActiva)
        {
            audioNotificacion.Stop();
            notificacionActiva = false;
        }
    }

    private void MostrarCanvas(bool estado)
    {
        canvasActivo = estado;
        if (canvasAActivar != null) canvasAActivar.SetActive(estado);
        if (mensajeNotificacion != null) mensajeNotificacion.SetActive(estado);
        if (objetoExtra != null) objetoExtra.SetActive(estado && notificacionActiva);

        if (estado)
        {
            if (cerrarRelojCoroutine != null)
                StopCoroutine(cerrarRelojCoroutine);
            cerrarRelojCoroutine = StartCoroutine(CerrarRelojAutomatico());
        }
        else
        {
            if (cerrarRelojCoroutine != null)
            {
                StopCoroutine(cerrarRelojCoroutine);
                cerrarRelojCoroutine = null;
            }
        }
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

    private IEnumerator CerrarRelojAutomatico()
    {
        yield return new WaitForSeconds(tiempoCerrarAutomatico);
        if (canvasActivo)
        {
            MostrarCanvas(false);
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

        // 🔹 Si detecta el tag Player, inicia el temporizador para desaparecer el objeto
        if (other.CompareTag("Player") && objetoADesaparecer != null)
        {
            StartCoroutine(DesaparecerObjetoDespuesDe5Segundos());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Indice"))
        {
            puedeCambiar = true;
        }
    }

    // 🔹 Corrutina para desaparecer el objeto después de 5 segundos
    private IEnumerator DesaparecerObjetoDespuesDe5Segundos()
    {
        yield return new WaitForSeconds(5f);
        objetoADesaparecer.SetActive(false);
    }
}
