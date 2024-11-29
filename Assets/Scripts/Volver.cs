using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Volver : MonoBehaviour
{
    // Método para ir al menú principal
    public void IrAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
