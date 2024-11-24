using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControladorDatosJuego : MonoBehaviour
{
    private int _score;
    public string archivoDeGuardado;
    public string archivoScoresFinales;
    public DatosJuego datosJuego = new DatosJuego();
    public TMPro.TextMeshProUGUI scoreText;
    private int contadorJugadores = 1;

    public int score
    {
        get => _score;
        set
        {
            if (_score != value)
            {
                _score = value;
                scoreText.text = "Puntaje: " + _score; 
                GuardarDatos(); 
            }
        }
    }

    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";
        archivoScoresFinales = Application.dataPath + "/ScoresFinales.json";

        CargarDatos();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            CargarDatos();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            GuardarDatos();
        }
    }

    private void CargarDatos()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            score = datosJuego.puntaje;
            Debug.Log("Puntaje Cargado: " + score);
        }
        else
        {
            Debug.Log("No Existe el archivo de guardado");
            score = 0;
        }
    }

    public void GuardarDatos()
    {
        DatosJuego nuevosDatos = new DatosJuego()
        {
            puntaje = score
        };

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Puntaje Guardado: " + score);
    }

    public void GuardarPuntajeFinal(int finalScore)
    {
        List<Jugador> puntajesFinales = new List<Jugador>();

        // Verificamos si ya existe un archivo de puntajes finales
        if (File.Exists(archivoScoresFinales))
        {
            string contenido = File.ReadAllText(archivoScoresFinales);
            puntajesFinales = JsonUtility.FromJson<PuntajesFinales>(contenido).jugadores;
        }

        // Generamos el nombre del jugador como "Jugador 1", "Jugador 2", etc.
        string nombreJugador = "Jugador " + contadorJugadores.ToString();
        contadorJugadores++;  // Incrementamos el contador para el siguiente jugador

        // Agregamos el puntaje final con el nombre del jugador
        Jugador nuevoJugador = new Jugador(nombreJugador, finalScore);
        puntajesFinales.Add(nuevoJugador);

        // Ordenamos la lista de puntajes de mayor a menor
        puntajesFinales.Sort((x, y) => y.puntaje.CompareTo(x.puntaje));

        // Guardamos la lista actualizada de puntajes finales
        PuntajesFinales datosFinales = new PuntajesFinales() { jugadores = puntajesFinales };
        string cadenaJSON = JsonUtility.ToJson(datosFinales);
        File.WriteAllText(archivoScoresFinales, cadenaJSON);

        Debug.Log("Puntaje Final Guardado: " + finalScore + " por " + nombreJugador);
    }

    private void OnApplicationQuit()
    {
        ReiniciarPuntaje();
    }

    public void ReiniciarPuntaje()
    {
        score = 0;
        scoreText.text = "Puntaje: " + score;

        Debug.Log("Puntaje reiniciado.");
    }
}