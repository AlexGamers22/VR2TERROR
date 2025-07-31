using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloj : MonoBehaviour
{
    public GameObject canvasAActivar; //Canvas del mensaje sin tanto rollo
    public GameObject mensajeNotificacion; //Canvas del mensaje
    public GameObject objetoExtra; // Panel Mensaje del mensaje Miguelon
    public AudioSource audioNotificacion;
    public Material relojRenderer;
    public float Parpadeo = 0.5f;
    public float ActivarNotificacionEscena = 5f;
    public float tiempoActivarreloj = 15f;

    private bool canvasActivo = false;
    private bool puedeCambiar = true;
    private bool notificacionActiva = false;
    private Color colorOriginal;
    private Coroutine parpadeoCoroutine;

    private void Start()
    {
        if (relojRenderer != null)
            colorOriginal = relojRenderer.color;

        if (objetoExtra != null)
            objetoExtra.SetActive(false); 

        StartCoroutine(NotificarYMostrarReloj());
    }

    private IEnumerator NotificarYMostrarReloj()
    {
        yield return new WaitForSeconds(ActivarNotificacionEscena);

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

        yield return new WaitForSeconds(tiempoActivarreloj);
        canvasAActivar.SetActive(true);
        if (mensajeNotificacion != null)
            mensajeNotificacion.SetActive(true);
        if (objetoExtra != null)
            objetoExtra.SetActive(true);
        canvasActivo = true;

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

    private IEnumerator ParpadearMaterial()
    {
        bool blanco = false;
        while (true)
        {
            relojRenderer.color = blanco ? Color.white : colorOriginal;
            blanco = !blanco;
            yield return new WaitForSeconds(Parpadeo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Indice") && puedeCambiar)
        {
            canvasActivo = !canvasActivo;
            canvasAActivar.SetActive(canvasActivo);
            if (mensajeNotificacion != null)
                mensajeNotificacion.SetActive(canvasActivo);

            if (objetoExtra != null)
                objetoExtra.SetActive(canvasActivo && notificacionActiva);

            puedeCambiar = false;

            if (canvasActivo && parpadeoCoroutine != null && relojRenderer != null)
            {
                StopCoroutine(parpadeoCoroutine);
                relojRenderer.color = colorOriginal;
            }

            if (notificacionActiva && audioNotificacion != null)
            {
                audioNotificacion.Stop();
                notificacionActiva = false;
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
