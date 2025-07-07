using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucesParpadeo : MonoBehaviour
{
    public Light lightSource;          // Referencia a la luz
    public float minIntensity = 0.2f;  // Intensidad mínima
    public float maxIntensity = 1.0f;  // Intensidad máxima
    public float flickerSpeed = 0.1f;  // Velocidad base de cambio
    public bool randomizeSpeed = true; // Si el tiempo de parpadeo varía aleatoriamente

    private float timer = 0f;
    private float nextChangeTime = 0f;

    void Start()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        SetNextChangeTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextChangeTime)
        {
            // Cambia a una nueva intensidad aleatoria
            lightSource.intensity = Random.Range(minIntensity, maxIntensity);

            // Reinicia el temporizador
            timer = 0f;
            SetNextChangeTime();
        }
    }

    void SetNextChangeTime()
    {
        if (randomizeSpeed)
            nextChangeTime = Random.Range(flickerSpeed * 0.5f, flickerSpeed * 1.5f);
        else
            nextChangeTime = flickerSpeed;
    }
}
