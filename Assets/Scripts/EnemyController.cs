using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    public ParticleSystem fixEffect;
    public AudioClip fixedClip;

    Rigidbody2D rigi2D;
    float timer;
    int direction = 1;
    Animator animator;
    bool broken;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigi2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
        broken = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigi2D.position;
        if (vertical)
        {
            position.y += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            
        }

        rigi2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController ruby = 
            collision.gameObject.GetComponent<RubyController>();
        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        audioSource.PlayOneShot(fixedClip);
        broken = false;
        rigi2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        Instantiate(fixEffect, transform.position + Vector3.up * 0.7f, 
            Quaternion.identity);
        GameObject go = GameObject.FindWithTag("RUBY");
        if (go != null)
        {
            RubyController ruby = go.GetComponent<RubyController>();
            ruby.PlaySound(fixedClip);
            ruby.TellMeFixed();
        }
    }
}
