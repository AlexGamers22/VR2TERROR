using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class CamaraPC : MonoBehaviour
{
    public PlayableDirector timelineDirector; // Timeline que se reproduce
    private XRGrabInteractable grabInteractable;

    public GameObject Funcion1;
    public GameObject Funcion2;
    public GameObject Funcion3;
    public GameObject Sofia;
    public PlayableDirector ElevadorOpen;

    

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabSilla);
        }
    }

    private void OnGrabSilla(SelectEnterEventArgs args)
    {
        if (timelineDirector != null)
        {
            // Desactiva objetos
            Funcion1.SetActive(false);
            Funcion2.SetActive(false);
            Funcion3.SetActive(false);
            Sofia.SetActive(true);

            // Escucha cuando termine la Timeline
            timelineDirector.stopped += OnTimelineTerminada;

            // Reproduce Timeline
            timelineDirector.Play();

            // Ejecuta elevador
            StartCoroutine(AbriElevador());
        }
    }

    private void OnTimelineTerminada(PlayableDirector director)
    {
        // Reactiva objetos
        Funcion1.SetActive(true);
        Funcion2.SetActive(true);
        Funcion3.SetActive(true);

        // Mueve al jugador suavemente
        

        // Deja de escuchar el evento
        timelineDirector.stopped -= OnTimelineTerminada;
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabSilla);
        }
    }

    public IEnumerator AbriElevador()
    {
        if (ElevadorOpen != null)
            ElevadorOpen.Play();
        yield return null;
    }

    
}
