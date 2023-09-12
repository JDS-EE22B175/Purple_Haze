using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public Animator animator;
    public GameObject logicHandler;
    bool dead = false;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f && !dead)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        logicHandler.GetComponent<LogicHandler>().mageCount -= 1;
        animator.Play("Death");
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
