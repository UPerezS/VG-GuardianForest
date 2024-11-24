using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovimientoCeldas : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    private Animator animator;
    private Rigidbody2D rb2D;  

    public float limiteIzquierda = -7.5f;
    public float limiteDerecha = 7.5f;
    public float limiteArriba = 3.5f;
    public float limiteAbajo = -3.5f;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    private ControladorDatosJuego controladorDatos;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        controladorDatos = FindObjectOfType<ControladorDatosJuego>();
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        float movimientoVertical = Input.GetAxisRaw("Vertical");

        Vector2 movimiento = new Vector2(movimientoHorizontal, movimientoVertical).normalized * velocidadMovimiento;

        Vector2 nuevaPosicion = rb2D.position + movimiento * Time.deltaTime;

        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierda, limiteDerecha);
        nuevaPosicion.y = Mathf.Clamp(nuevaPosicion.y, limiteAbajo, limiteArriba);

        rb2D.MovePosition(nuevaPosicion);

        animator.SetFloat("Horizontal", movimientoHorizontal);
        animator.SetFloat("Vertical", movimientoVertical);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Consumable consumable = collision.gameObject.GetComponent<Consumable>();
            if (consumable != null)
            {
                Item hitObject = consumable.item;
                if (hitObject != null)
                {
                    print("Nombre: " + hitObject.objectName);

                    controladorDatos.score += 10;
                    controladorDatos.scoreText.text = "Puntaje: " + controladorDatos.score;

                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

}
