using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
public float timeLeft = 60f; 
public TextMeshProUGUI timerText; 


void Update()
{
if (timeLeft > 0)
{
timeLeft -= Time.deltaTime;
timerText.text = Mathf.Floor(timeLeft).ToString();
}
else
{
timeLeft = 0; 
timerText.text = "0"; 
Debug.Log("¡Tiempo terminado!");
// Llamar a función para terminar el juego
}
}
}