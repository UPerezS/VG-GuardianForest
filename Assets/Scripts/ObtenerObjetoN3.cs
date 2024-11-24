using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObtenerObjetoN3 : MonoBehaviour
{
    public float moveDistance = 0.5f;  // Distancia total que el objeto se moverá a los costados (derecha e izquierda)
    public float moveDuration = 2.0f;  // Tiempo total que lleva completar el movimiento (2 segundos ida y vuelta)
    private Vector3 startPosition;     // Posición inicial del objeto
    public static int objetosRecogidos = 0; // Contador de objetos recogidos por el jugador

    private void Start()
    {
        startPosition = transform.position; // Guardar la posición inicial del objeto
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objetosRecogidos++; // Aumentar el contador cuando el jugador recoge un objeto
            Destroy(gameObject); // Destruir el objeto una vez recogido

            // Verificar si el jugador ha recogido dos objetos
            if (objetosRecogidos >= 3)
            {
                // Finalizar el juego o cambiar de escena
                Debug.Log("¡Has recogido los dos objetos! El juego ha terminado.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Cambia a la escena de Game Over
            }
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Movimiento suave de izquierda a derecha utilizando PingPong
        float t = Mathf.PingPong(Time.time, moveDuration) / moveDuration;
        float xOffset = Mathf.Lerp(-moveDistance, moveDistance, t); // Mueve entre -5 y 5 unidades
        transform.position = new Vector3(startPosition.x + xOffset, startPosition.y, startPosition.z);
    }
}
