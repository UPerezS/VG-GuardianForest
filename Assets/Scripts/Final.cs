using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    public ControladorDatosJuego controlador; // Referencia al controlador de puntajes

    private void Start()
    {
        controlador = FindObjectOfType<ControladorDatosJuego>(); // Obtenemos la referencia al controlador
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controlador.GuardarPuntajeFinal(controlador.score);

            Debug.Log("¡Has Completado el Juego! Puntaje Final: " + controlador.score);

            // Cargar la siguiente escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}