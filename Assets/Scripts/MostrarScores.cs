using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class MostrarScores : MonoBehaviour
{
    public TextMeshProUGUI scoresText;  // Referencia al objeto de texto donde se mostrarán los puntajes
    public TextMeshProUGUI ultimoScoreText;

    private string archivoScoresFinales; // Ruta del archivo con los puntajes finales

    private void Start()
    {
        archivoScoresFinales = Application.dataPath + "/ScoresFinales.json";  // Ruta al archivo de puntajes finales
        CargarScores();
        MostrarUltimoScore();
    }

    private void CargarScores()
    {
        if (File.Exists(archivoScoresFinales))
        {
            // Leemos el contenido del archivo
            string contenido = File.ReadAllText(archivoScoresFinales);

            // Convertimos el contenido en un objeto de tipo PuntajesFinales
            PuntajesFinales puntajesFinales = JsonUtility.FromJson<PuntajesFinales>(contenido);

            // Ordenamos la lista de jugadores por puntaje de mayor a menor
            puntajesFinales.jugadores.Sort((jugador1, jugador2) => jugador2.puntaje.CompareTo(jugador1.puntaje));

            // Creamos un texto para mostrar todos los puntajes
            string textoScores = "Puntajes Finales:\n";
            for (int i = 0; i < puntajesFinales.jugadores.Count; i++)
            {
                Jugador jugador = puntajesFinales.jugadores[i];
                textoScores += jugador.nombre + ": " + jugador.puntaje.ToString() + "\n";  // Agregamos cada puntaje y nombre al texto
            }

            // Asignamos el texto al TextMeshProUGUI
            scoresText.text = textoScores;
        }
        else
        {
            // Si no existe el archivo, mostramos un mensaje indicando que no hay puntajes
            scoresText.text = "No hay puntajes disponibles.";
        }
    }

    private void MostrarUltimoScore()
    {
        Jugador ultimoJugador = ObtenerUltimoResultado();

        if (ultimoJugador != null)
        {
            // Mostramos el puntaje del último jugador registrado
            ultimoScoreText.text = "Tú Puntuación Final es:\n" + ultimoJugador.nombre + " - " + "Score: " + ultimoJugador.puntaje;
        }
        else
        {
            ultimoScoreText.text = "No hay puntajes disponibles.";
        }
    }

    private Jugador ObtenerUltimoResultado()
    {
        if (File.Exists(archivoScoresFinales))
        {
            // Leemos el contenido del archivo
            string contenido = File.ReadAllText(archivoScoresFinales);

            // Convertimos el contenido en un objeto de tipo PuntajesFinales
            PuntajesFinales puntajesFinales = JsonUtility.FromJson<PuntajesFinales>(contenido);

            // Verificamos si hay jugadores en la lista
            if (puntajesFinales.jugadores.Count > 0)
            {
                // Devolvemos el último jugador de la lista
                return puntajesFinales.jugadores[puntajesFinales.jugadores.Count - 1];
            }
        }
        return null; // Retornamos null si no hay jugadores o no existe el archivo
    }


}