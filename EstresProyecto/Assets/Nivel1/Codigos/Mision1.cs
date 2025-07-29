using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misiones : MonoBehaviour
{
    public ControladorVoz npc;
    public GameObject TextoGarrafon;
    public ContadorTiempo contador;

    private void OnTriggerEnter(Collider other)
    {
        if (npc == null) return;

        if (other.CompareTag("Hojas") && npc.MisionActual == 0)
        {
            Debug.Log("Hojas listas");
            npc.CompletarMision();
            contador.RegistrarTiempo();
        }
        else if (other.CompareTag("Garrafon") && npc.MisionActual == 1)
        {
            Debug.Log("Garrafon cambiado");
            npc.CompletarMision();
            contador.RegistrarTiempo();
        }
        else if (other.CompareTag("Teclado") && npc.MisionActual == 2)
        {
            Debug.Log("Teclado ya");
            npc.CompletarMision();
            contador.RegistrarTiempo(); 
        }
    }
}
