using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Persecucion : MonoBehaviour
{
    [Header("Referencia al Timeline")]
    public PlayableDirector timeline;

    [Header("Audio a reproducir")]
    public AudioSource audioSource;

    private void Start()
    {
        if (timeline != null)
        {
            timeline.stopped += OnTimelineTerminado; // Se ejecuta cuando acaba
        }
        else
        {
            Debug.LogError("No se asignó el Timeline.");
        }

        if (audioSource == null)
        {
            Debug.LogError("No se asignó un AudioSource.");
        }
    }

    private void OnTimelineTerminado(PlayableDirector obj)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        if (timeline != null)
        {
            timeline.stopped -= OnTimelineTerminado; // Evita errores si el objeto se destruye
        }
    }
}
