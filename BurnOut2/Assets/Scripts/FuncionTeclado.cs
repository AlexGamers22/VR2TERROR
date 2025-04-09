using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;    
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FuncionTeclado : MonoBehaviour
{
    public Animator animador;  // Arrastra tu Animator aquí en el Inspector
    public Button boton;      // Arrastra tu botón de Canvas aquí en el Inspector
    public string nombreAnimacion;  // Nombre de la animación que quieres reproducir

    void Start()
    {
        // Asegúrate de que el botón tiene un listener que ejecuta la función cuando se hace clic
        boton.onClick.AddListener(ReproducirAnimacion);
    }

    void ReproducirAnimacion()
    {
        // Aquí se reproduce la animación usando el nombre que pongas en el Inspector
        animador.Play(nombreAnimacion);  // Reproduce la animación por su nombre
    }
}
