using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Copiadora : MonoBehaviour
{
    public TMP_InputField inputFieldCopias;
    public Button startButton;
    public GameObject hojaPrefab;            // Prefab de la hoja a imprimir
    public Transform puntoSalida;            // Punto donde salen las hojas
    public float tiempoEntreHojas = 0.5f;    // Tiempo entre cada hoja

    private int numeroCopias = 0;

    void Start()
    {
        startButton.onClick.AddListener(IniciarImpresion);
    }

    void IniciarImpresion()
    {
        if (int.TryParse(inputFieldCopias.text, out numeroCopias))
        {
            if (numeroCopias > 0)
            {
                StartCoroutine(AnimarImpresion());
            }
            else
            {
                Debug.LogWarning("El número debe ser mayor a 0.");
            }
        }
        else
        {
            Debug.LogWarning("Introduce un número válido.");
        }
    }

    IEnumerator AnimarImpresion()
    {
        for (int i = 0; i < numeroCopias; i++)
        {
            // Instanciar una hoja
            Instantiate(hojaPrefab, puntoSalida.position, puntoSalida.rotation);
            yield return new WaitForSeconds(tiempoEntreHojas);
        }
    }
}
