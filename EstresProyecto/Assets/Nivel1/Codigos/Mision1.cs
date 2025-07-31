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
            npc.CompletarMision();
            contador.RegistrarTiempo();
        }
        else if (other.CompareTag("Garrafon") && npc.MisionActual == 1)
        {
            npc.CompletarMision();
            contador.RegistrarTiempo();
        }
        else if (other.CompareTag("Teclado") && npc.MisionActual == 2)
        {
            npc.CompletarMision();
            contador.RegistrarTiempo();
        }
    }
}
