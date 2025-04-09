using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;

public class ControladorVoz : MonoBehaviour
{
    public TextMeshPro textoPersonaje;
    public AudioSource audioSource;

    public AudioClip[] saludosClips;
    public AudioClip ayudaClip;
    public AudioClip[] trabajoClips;

    private KeywordRecognizer keyWordRecognizer;
    private Dictionary<string, Action> wordToAction;
    private int misionActual = 0;
    private System.Random random = new System.Random();

    public int MisionActual => misionActual;

    private void Start()
    {
        wordToAction = new Dictionary<string, Action>
        {
            { "hola", Hola },
            { "que hay", Hola },
            { "¿qué hago?", Trabajo },
            { "¿qué trabajo hago?", Trabajo },
            { "¿qué más hago?", Trabajo },
            { "ayuda", Ayuda },
            { "ayudame", Ayuda }
        };

        keyWordRecognizer = new KeywordRecognizer(wordToAction.Keys.ToArray());
        keyWordRecognizer.OnPhraseRecognized += WordRecognized;
    }

    private void WordRecognized(PhraseRecognizedEventArgs word)
    {
        string palabraReconocida = word.text.ToLower();
        Debug.Log("Reconocido: " + palabraReconocida);
        if (wordToAction.ContainsKey(palabraReconocida))
        {
            wordToAction[palabraReconocida].Invoke();
        }
        else
        {
            Debug.LogWarning("Palabra no encontrada en el diccionario.");
        }
    }

    private void Hola()
    {
        string[] saludos =
        {
            "Hola soy Miguelon y soy tu jefe.",
            "Hey ¿listo para trabajar?",
            "Hola ponte a trabajar"
        };

        int index = random.Next(saludos.Length);
        textoPersonaje.text = saludos[index];

        if (saludosClips.Length > index)
        {
            ReproducirAudio(saludosClips[index]);
        }
    }

    private void Ayuda()
    {
        textoPersonaje.text = "¿Qué necesitas?";
        ReproducirAudio(ayudaClip);
    }

    private void Trabajo()
    {
        int index = misionActual;
        switch (misionActual)
        {
            case 0:
                textoPersonaje.text = "Tu primera tarea es sacar copias.";
                break;
            case 1:
                textoPersonaje.text = "Cambia el garrafón de agua RAPIDO.";
                break;
            case 2:
                textoPersonaje.text = "Ve a ver qué tiene la maldita COMPUTADORA.";
                break;
            default:
                textoPersonaje.text = "Ya terminaste por hoy, pero realizarás horas extras.";
                index = 3;
                break;
        }

        if (trabajoClips.Length > index)
        {
            ReproducirAudio(trabajoClips[index]);
        }
    }

    private void ReproducirAudio(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void CompletarMision()
    {
        if (misionActual < 3)
        {
            misionActual++;
            Debug.Log("Misión completada. Nueva misión: " + misionActual);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!keyWordRecognizer.IsRunning)
            {
                Debug.Log("Activando escucha");
                keyWordRecognizer.Start();
                textoPersonaje.text = "Te escucho.";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyWordRecognizer.IsRunning)
            {
                Debug.Log("Deteniendo escucha");
                keyWordRecognizer.Stop();
                textoPersonaje.text = "";
            }
        }
    }

    private void OnDestroy()
    {
        if (keyWordRecognizer != null && keyWordRecognizer.IsRunning)
        {
            keyWordRecognizer.Stop();
            keyWordRecognizer.Dispose();
        }
    }
}
