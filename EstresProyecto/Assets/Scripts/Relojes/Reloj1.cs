using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reloj1 : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject canvasAActivar;
    [SerializeField] private GameObject mensajeNotificacion;
    [SerializeField] private GameObject objetoExtra;
    [SerializeField] private AudioSource audioNotificacion;
    [SerializeField] private Material relojRenderer;
    [SerializeField] private TextMeshProUGUI[] textos;

    [Header("Parámetros de Tiempo")]
    [SerializeField] private float parpadeoVelocidad = 0.5f;
    [SerializeField] private float activarNotificacionEscena = 5f;
    [SerializeField] private float tiempoActivarReloj = 15f;
    [SerializeField] private float tiempoEntreTextos = 0.2f;
    [SerializeField] private float duracionFadeIn = 0.2f;

    [Header("Colores")]
    [SerializeField] private Color colorActivado = Color.red;
    [SerializeField] private Color colorOriginalTexto = Color.white;

    private bool canvasActivo = false;
    private bool puedeCambiar = true;
    private bool notificacionActiva = false;
    private bool relojAbiertoManual = false;
    private Color colorOriginalReloj;
    private Coroutine parpadeoCoroutine;
    private List<TextMeshProUGUI> textosActivados = new List<TextMeshProUGUI>();

    public void activarReloj()
    {
        if (relojRenderer != null)
            colorOriginalReloj = relojRenderer.color;

        if (objetoExtra != null)
            objetoExtra.SetActive(false);

        if (textos != null)
        {
            foreach (TextMeshProUGUI t in textos)
            {
                if (t != null)
                {
                    Color c = t.color;
                    c.a = 0;
                    t.color = c;
                }
            }
            StartCoroutine(ActivarTextosConFadeIn());
        }

        StartCoroutine(NotificarYMostrarReloj());
    }

    private IEnumerator ActivarTextosConFadeIn()
    {
        yield return new WaitForSeconds(2f);

        foreach (TextMeshProUGUI t in textos)
        {
            if (t != null)
            {
                float tiempo = 0;
                while (tiempo < duracionFadeIn)
                {
                    tiempo += Time.deltaTime;
                    float alpha = Mathf.Lerp(0, 1, tiempo / duracionFadeIn);
                    Color c = new Color(colorOriginalTexto.r, colorOriginalTexto.g, colorOriginalTexto.b, alpha);
                    t.color = c;
                    yield return null;
                }

                t.color = colorActivado;
                textosActivados.Add(t);
                yield return new WaitForSeconds(tiempoEntreTextos);

            }

        }

        yield return new WaitForSeconds(1.5f);
        if (canvasAActivar != null)
        {
            canvasAActivar.SetActive(false);
        }

        Destroy(objetoExtra.gameObject);
        Destroy(this);

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
            MostrarCanvas(true);

        if (parpadeoCoroutine != null && relojRenderer != null)
        {
            StopCoroutine(parpadeoCoroutine);
            relojRenderer.color = colorOriginalReloj;
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

        if (!estado)
            ResetearTextos();
    }

    private IEnumerator ParpadearMaterial()
    {
        bool blanco = false;
        while (true)
        {
            relojRenderer.color = blanco ? Color.white : colorOriginalReloj;
            blanco = !blanco;
            yield return new WaitForSeconds(parpadeoVelocidad);
        }
    }

    private void ResetearTextos()
    {
        foreach (TextMeshProUGUI t in textosActivados)
        {
            if (t != null)
            {
                Color c = t.color;
                c.a = 0;
                t.color = c;
            }
        }
        textosActivados.Clear();
    }


}