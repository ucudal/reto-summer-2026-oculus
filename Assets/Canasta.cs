using UnityEngine;
using TMPro;

public class Canasta : MonoBehaviour
{
    public TextMeshProUGUI textoMarcador;
    private int puntos = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Solo suma punto si la pelota está bajando
                if (rb.linearVelocity.y < 0f)
                {
                    puntos++;
                    ActualizarInterfaz();
                }
            }
        }
    }

    void ActualizarInterfaz()
    {
        textoMarcador.text = "Puntos: " + puntos;
    }
}
