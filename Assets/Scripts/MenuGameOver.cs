using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private Enemy1 enemy1;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Enemy");
        if (playerObject != null)
        {
            enemy1 = playerObject.GetComponent<Enemy1>();
            if (enemy1 != null)
            {
                enemy1.MuerteJugador += ActivarMenu;
            }
            else
            {
                Debug.LogError("El objeto con tag 'Player' no tiene un componente 'Enemy1'.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� ning�n objeto con el tag 'Player'.");
        }

        // Aseg�rate de que el men� de Game Over est� inicialmente desactivado
        if (menuGameOver != null)
        {
            menuGameOver.SetActive(false);
        }
        else
        {
            Debug.LogError("El men� de Game Over no est� asignado en el Inspector.");
        }
    }

    private void ActivarMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
