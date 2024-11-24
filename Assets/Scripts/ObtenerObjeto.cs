using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObtenerObjeto : MonoBehaviour
{
    public ControladorDatosJuego controladorDatosJuego;

    private void Start()
    {
        // Obtén la referencia del controlador de datos del juego
        controladorDatosJuego = FindObjectOfType<ControladorDatosJuego>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guarda el puntaje antes de cambiar de nivel
            controladorDatosJuego.GuardarDatos();

            // Cambia a la siguiente escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
