using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mision2 : MonoBehaviour
{
    public ControladorVoz npc;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && npc != null && npc.MisionActual == 1)
        {
            Debug.Log("Garrafon cambiado");
            npc.CompletarMision();
        }
    }
}