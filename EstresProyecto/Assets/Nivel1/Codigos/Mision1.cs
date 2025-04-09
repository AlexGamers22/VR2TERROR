using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mision1 : MonoBehaviour
{
    public ControladorVoz npc;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && npc != null && npc.MisionActual == 0)
        {
            Debug.Log("Copias hechas");
            npc.CompletarMision();
        }
    }
}