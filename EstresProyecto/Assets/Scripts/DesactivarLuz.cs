using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarLuz : MonoBehaviour
{
    public string tagObjetivo = "Player";     // Cambia si necesitas otro tag
    public GameObject[] objetosADesactivar;   // Arrastra aquí los 5 objetos desde el inspector
    public GameObject FondoPC;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            foreach (GameObject obj in objetosADesactivar)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
            FondoPC.SetActive(true);
            // Si no quieres que vuelva a activarse, puedes desactivar este objeto también:
            // gameObject.SetActive(false);
        }
    }
}
