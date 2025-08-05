using UnityEngine;
using TMPro;

public class DesactivarLuz : MonoBehaviour
{
    public string tagObjetivo = "Player";
    public GameObject[] objetosADesactivar;
    public GameObject[] objetosActivar;

    [Header("Texto personalizable")]
    [SerializeField] private TextMeshProUGUI textoMostrar;
    [TextArea]
    [SerializeField] private string mensaje;

    [Header("Script del reloj desactivado")]
    [SerializeField] private Reloj1 scriptReloj; // Cambiado a tipo Reloj

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            foreach (GameObject obj in objetosADesactivar)
                if (obj != null) obj.SetActive(false);

            foreach (GameObject obj in objetosActivar)
                if (obj != null) obj.SetActive(true);

            if (textoMostrar != null)
                textoMostrar.text = mensaje;

            if (scriptReloj != null)
                scriptReloj.activarReloj();
        }
    }
}
