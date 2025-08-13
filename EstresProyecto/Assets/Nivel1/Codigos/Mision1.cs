using TMPro;
using UnityEngine;

public class Misiones : MonoBehaviour
{
    public ControladorVoz npc;
    public GameObject TextoGarrafon;
    public ContadorTiempo contador;
    public Reloj reloj;
    [SerializeField] public TextMeshProUGUI textoreloj;

    private void OnTriggerEnter(Collider other)
    {
        if (npc == null) return;

        if (other.CompareTag("Hojas") && npc.MisionActual == 0)
        {
            npc.CompletarMision();
            contador.RegistrarTiempo();
            reloj.activarReloj();
            if (textoreloj != null)
                textoreloj.text = "Cambia el garrafón";
        }
        else if (other.CompareTag("Garrafon") && npc.MisionActual == 1)
        {
            npc.CompletarMision();
            contador.RegistrarTiempo();
            reloj.activarReloj();
            if (textoreloj != null)
                textoreloj.text = "Busca un teclado para el jefe";
        }
        else if (other.CompareTag("Teclado") && npc.MisionActual == 2)
        {
            npc.CompletarMision();
            contador.RegistrarTiempo();
            reloj.activarReloj();
            if (textoreloj != null)
                textoreloj.text = "Ve a casa";
        }
    }
}
