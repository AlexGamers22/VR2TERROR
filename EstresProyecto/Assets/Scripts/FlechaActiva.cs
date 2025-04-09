using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlechaActiva : MonoBehaviour
{
    [Header("Objeto que debe aparecer")]
    public GameObject objetoAparece;

    public GameObject Objetivo;
    


    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
        }

        if (objetoAparece != null)
        {
            objetoAparece.SetActive(false); // Lo ocultamos al inicio
        }

        if (Objetivo != null)
        {
            Objetivo.SetActive(false); // Lo ocultamos al inicio
        }
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (objetoAparece != null)
        {
            objetoAparece.SetActive(true); // Lo activamos al agarrar
            
        }

        if (Objetivo != null)
        {
            Objetivo.SetActive(true); // Lo ocultamos al inicio
        }
    }

    private void OnDestroy()
    {
        // Siempre limpiamos los listeners para evitar errores
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    
}
