using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Volver : MonoBehaviour
{
    // M�todo para ir al men� principal
    public void IrAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
