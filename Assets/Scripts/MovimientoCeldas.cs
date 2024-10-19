using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCeldas : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    private Animator animator;
    private Rigidbody2D rb2D;  

    public float limiteIzquierda = -7.5f;
    public float limiteDerecha = 7.5f;
    public float limiteArriba = 3.5f;
    public float limiteAbajo = -3.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>(); 
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
}
