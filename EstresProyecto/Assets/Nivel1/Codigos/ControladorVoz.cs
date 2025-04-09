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
    public AudioClip[] ayudaClip;
    public AudioClip[] trabajoClips;
    public AudioClip QuequieresClip;


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
            { "hey", Hola },
            { "¿qué hago?", Trabajo },
            { "¿qué trabajo hago?", Trabajo },
            { "¿qué más hago?", Trabajo },
            { "¿qué necesita?", Trabajo },
            { "¿que sigue?", Trabajo },
            { "ayuda", Ayuda },
            { "Necesito ayuda", Ayuda },
            { "ayudame", Ayuda }
        };

        keyWordRecognizer = new KeywordRecognizer(wordToAction.Keys.ToArray());
        keyWordRecognizer.OnPhraseRecognized += WordRecognized;
    }

    private void WordRecognized(PhraseRecognizedEventArgs word)
    {
        string palabraReconocida = word.text.ToLower();
        Debug.Log("Que: " + palabraReconocida);
        if (wordToAction.ContainsKey(palabraReconocida))
        {
            wordToAction[palabraReconocida].Invoke();
        }
        else
        {
            Debug.LogWarning("No entendi");
        }
    }

    private void Hola()
    {
        string[] saludos =
        {
            "Hey ¿listo para trabajar?",
            "Hola ponte a trabajar, YAAA",
            "Hey no estés perdiendo el tiempo y ponte a trabajar"
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
        string[] ayudas =
        {
            "No te voy ayudar siempre, INUTIL",
            "No me estes molestando y PONTE A TRABAJAR"
        };
         int index = random.Next(ayudas.Length);
        textoPersonaje.text = ayudas[index];
        if (ayudaClip.Length > index)
        {
            ReproducirAudio(ayudaClip[index]);
        }
    }

    private void Trabajo()
    {
        int index = misionActual;
        switch (misionActual)
        {
            case 0:
                textoPersonaje.text = "Necesito que saques copias y no lo eches a perder";
                break;
            case 1:
                textoPersonaje.text = "Cambia el garrafón RAPIDO, no ves que ya no hay agua";
                break;
            case 2:
                textoPersonaje.text = "Le falta el teclado a mi computadora , VE POR UNO RAPIDO";
                break;
            default:
                textoPersonaje.text = "Hoy realizaras horas extras PORQUE YO LO DIGO";
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
                Debug.Log("Escucha");
                keyWordRecognizer.Start();
                textoPersonaje.text = "¿Qué quieres?";
                ReproducirAudio(QuequieresClip);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyWordRecognizer.IsRunning)
            {
                Debug.Log("No escucha");
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
