using UnityEngine;
using TMPro; // Importante para controlar TextMeshPro

public class Canasta : MonoBehaviour
{
    public TextMeshProUGUI textoMarcador; // Referencia al objeto de la UI
    private int puntos = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Ajusta el Tag de tu esfera a "Finish" o el que prefieras
        if (other.CompareTag("Player")) 
        {
            puntos++;
            ActualizarInterfaz();
        }
    }

    void ActualizarInterfaz()
    {
        textoMarcador.text = "Puntos: " + puntos;
    }
}
