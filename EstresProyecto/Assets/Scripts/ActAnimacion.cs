using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActAnimacion : MonoBehaviour
{
    public GameObject objetoAnimado;
    private Animator animator;

    private void Start()
    {
        animator = objetoAnimado.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("Nombre de la animacion");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
