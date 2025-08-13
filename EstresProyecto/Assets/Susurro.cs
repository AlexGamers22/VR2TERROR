using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Susurro : MonoBehaviour
{
    [Header("Componentes")]
    public PlayableDirector director; // Arrastra aquí tu Timeline
    public AudioSource audioSource;   // Arrastra aquí el AudioSource con el clip a reproducir

    private void Start()
    {
        if (director != null)
        {
            director.stopped += OnTimelineTerminado;
        }
        else
        {
            Debug.LogError("No se asignó el PlayableDirector.");
        }
    }

    private void OnTimelineTerminado(PlayableDirector obj)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No se asignó el AudioSource.");
        }
    }

    private void OnDestroy()
    {
        if (director != null)
        {
            director.stopped -= OnTimelineTerminado; // Evitar errores al destruir el objeto
        }
    }
}
