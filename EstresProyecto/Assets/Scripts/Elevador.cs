using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Elevador : MonoBehaviour
{
    public PlayableDirector director;

    public Animator animadorObjetivo;           // Primera animación
    public string nombreAnimacion;

    public GameObject objetoAMover;             // Objeto que se mueve y gira
    public Vector3 nuevaPosicion;
    public Vector3 nuevaRotacionEuler;
    public float duracionMovimiento = 2f;

    public Animator animadorDespuesDeMover;     // Segunda animación
    public string animacionDespuesDeMover;

    public GameObject ActivadorLuz;
    public GameObject CambiodeEscena;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Indice"))
        {
            if (director != null)
            {
                director.Play();
            }

            StartCoroutine(EsperarYAnimar());
            StartCoroutine(EsperarYMoverYRotarYAnimarConDelay());
        }
    }

    IEnumerator EsperarYAnimar()
    {
        yield return new WaitForSeconds(9f);

        if (animadorObjetivo != null && !string.IsNullOrEmpty(nombreAnimacion))
        {
            animadorObjetivo.Play(nombreAnimacion);
        }
    }

    IEnumerator EsperarYMoverYRotarYAnimarConDelay()
    {
        yield return new WaitForSeconds(11f);

        if (objetoAMover != null)
        {
            Vector3 posicionInicial = objetoAMover.transform.position;
            Quaternion rotacionInicial = objetoAMover.transform.rotation;
            Quaternion rotacionFinal = Quaternion.Euler(nuevaRotacionEuler);

            float tiempo = 0f;

            while (tiempo < duracionMovimiento)
            {
                float t = tiempo / duracionMovimiento;
                objetoAMover.transform.position = Vector3.Lerp(posicionInicial, nuevaPosicion, t);
                objetoAMover.transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, t);

                tiempo += Time.deltaTime;
                yield return null;
            }

            objetoAMover.transform.position = nuevaPosicion;
            objetoAMover.transform.rotation = rotacionFinal;

            // Esperar 5 segundos después de mover antes de animar
            yield return new WaitForSeconds(5f);

            if (animadorDespuesDeMover != null && !string.IsNullOrEmpty(animacionDespuesDeMover))
            {
                CambiodeEscena.gameObject.SetActive(true);
                ActivadorLuz.gameObject.SetActive(true);
                animadorDespuesDeMover.Play(animacionDespuesDeMover);

            }

            
        }
    }
}
