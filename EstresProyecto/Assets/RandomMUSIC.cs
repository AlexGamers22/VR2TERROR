using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMUSIC : MonoBehaviour
{
    [Header("Lista de canciones")]
    public AudioClip[] canciones; // Aquí arrastras tus audios
    public AudioSource audioSource; // Arrastra el AudioSource

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("No se asignó un AudioSource.");
            return;
        }

        if (canciones.Length == 0)
        {
            Debug.LogError("No se asignaron canciones.");
            return;
        }

        ReproducirCancionAleatoria();
    }

    private void Update()
    {
        // Si no está reproduciendo nada, poner otra canción
        if (!audioSource.isPlaying)
        {
            ReproducirCancionAleatoria();
        }
    }

    void ReproducirCancionAleatoria()
    {
        int indice = Random.Range(0, canciones.Length);
        audioSource.clip = canciones[indice];
        audioSource.Play();
    }
}
