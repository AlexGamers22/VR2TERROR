using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IniciarCopias : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputFieldCopias;

    [Header("Impresora")]
    public GameObject hojaPrefab;
    public Transform puntoSalida;
    public float tiempoEntreHojas = 0.5f;

    public void IniciarImpresion()
    {
        if (int.TryParse(inputFieldCopias.text, out int copias) && copias > 0)
        {
            StartCoroutine(AnimarImpresion(copias));
        }
        else
        {
            Debug.LogWarning("Introduce un número válido mayor a 0.");
        }
    }

    IEnumerator AnimarImpresion(int copias)
    {
        for (int i = 0; i < copias; i++)
        {
            Instantiate(hojaPrefab, puntoSalida.position, puntoSalida.rotation);
            yield return new WaitForSeconds(tiempoEntreHojas);
        }
    }
}
