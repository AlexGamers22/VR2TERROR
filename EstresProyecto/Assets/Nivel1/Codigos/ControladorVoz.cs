using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorVoz : MonoBehaviour
{
    public TextMeshPro textoPersonaje;
    public AudioSource audioSource;

    public AudioClip[] saludosClips;
    public AudioClip[] ayudaClip;
    public AudioClip[] trabajoClips;
    public AudioClip QuequieresClip;

    public GameObject LuzCopias;
    public GameObject LuzGarrafon;
    public GameObject LuzTeclado;

    public GameObject TextoCopias;



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
            { "que hago", Trabajo },
            { "que trabajo hago", Trabajo },
            { "que mas hago", Trabajo },
            { "que necesita", Trabajo },
            { "ayuda", Ayuda },
            { "necesito ayuda", Ayuda },
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
            "Hey estas listo para trabajar?",
            "Hola ponte a trabajar, YAAA",
            "Hey no estes perdiendo el tiempo y ponte a trabajar"
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
            "No te voy ayudar siempre, FLOJO",
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
                Copias();
                break;
            case 1:
                textoPersonaje.text = "Cambia el garrafón RAPIDO, no ves que ya no hay agua";
                Garrafon();
                break;
            case 2:
                textoPersonaje.text = "Le falta el teclado a mi computadora , VE POR UNO RAPIDO";
                Teclado();
                break;
            default:
                textoPersonaje.text = "Hoy realizaras horas extras PORQUE YO LO DIGO";
                index = 3;
                StartCoroutine(CambiarEscenaConDelay());
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
            Debug.Log("Mision completada. Nueva mision: " + misionActual);
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
                textoPersonaje.text = "¿Que quieres?";
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReproducirAudio(QuequieresClip);
        }
    }

    private void Copias()
    {
        LuzCopias.SetActive(true);
    }

    private void Garrafon()
    {
        LuzGarrafon.SetActive(true);
        TextoCopias.SetActive(false);
    }

    private void Teclado() 
    {
        LuzTeclado.SetActive(true);
    }

    IEnumerator CambiarEscenaConDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }
}
