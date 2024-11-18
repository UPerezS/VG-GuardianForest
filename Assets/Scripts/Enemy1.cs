using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Enemy1 : MonoBehaviour
{
    public float velocidadMovimiento = 2f;
    private Transform player;
    private Animator animator;
    private bool puedeMoverse = false;
    public event EventHandler MuerteJugador;

    // Límites del mapa
    public float limiteIzquierda = -7.5f;
    public float limiteDerecha = 7.5f;
    public float limiteArriba = 3.5f;
    public float limiteAbajo = -3.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine(IniciarMovimiento());
    }

    void Update()
    {
        if (puedeMoverse && player != null)
        {
            Vector2 direccion = (player.position - transform.position).normalized;
            Vector2 movimiento = direccion * velocidadMovimiento * Time.deltaTime;
            transform.Translate(movimiento, Space.World);
            LimitarMovimiento();
            UpdateAnimation(direccion);
        }
    }

    IEnumerator IniciarMovimiento()
    {
        yield return new WaitForSeconds(5f);
        puedeMoverse = true;
        animator.SetBool("EnMovimiento", true);
    }

    void LimitarMovimiento()
    {
        Vector3 posicionActual = transform.position;
        posicionActual.x = Mathf.Clamp(posicionActual.x, limiteIzquierda, limiteDerecha);
        posicionActual.y = Mathf.Clamp(posicionActual.y, limiteAbajo, limiteArriba);
        transform.position = posicionActual;
    }

    void UpdateAnimation(Vector2 direccion)
    {
        animator.SetFloat("Horizontal", direccion.x);
        animator.SetFloat("Vertical", direccion.y);

        if (direccion.magnitude > 0)
        {
            animator.SetBool("EnMovimiento", true);
        }
        else
        {
            animator.SetBool("EnMovimiento", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MuerteJugador?.Invoke(this, EventArgs.Empty);
            Debug.Log("Perdiste!");
        }
    }
}
