using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class CamaraPC : MonoBehaviour
{
    public PlayableDirector timelineDirector; // Asigna el PlayableDirector (Timeline) desde el Inspector

    private XRGrabInteractable grabInteractable;
    public GameObject Funcion1;
    public GameObject Funcion2;
    public GameObject Funcion3;


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
            timelineDirector.Play();
        }

        // Si quieres que el jugador ya no pueda soltar la silla:
        // grabInteractable.enabled = false;
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabSilla);
        }
    }



}
