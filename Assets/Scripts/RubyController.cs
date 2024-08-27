using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    #region Public
    public const int GOAL_FIXED_ROBOT = 3;
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    public GameObject projectilePrefab;
    public GameObject hitEffect;
    public float timeInvincible = 2.0f;
    public AudioClip hitClip;
    public AudioClip throwClip;
    public AudioClip questClip;
    public int FixedRobotCount
    {
        get
        {
            return fixedRobot;
        }
        set
        {
            fixedRobot = value;
        }
    }

    #endregion

    #region private
#if (UNITY_EDITOR && UNITY_ANDROID)
    float speed = 23.0f;
#else
    float speed = 3.0f;
#endif
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigi2D;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    AudioSource audioSource;
    int fixedRobot;
    PlayerMove moves;
#endregion

    // Start is called before the first frame update
    void Start()
    {
        rigi2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        fixedRobot = 0;
        moves = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
#if (!UNITY_ANDROID)
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(hori, vert);
#else
        Vector2 move = moves.MoveInput.normalized;
#endif
        if (!Mathf.Approximately(move.x, 0.0f) ||
            !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigi2D.position;
        position += speed * move * Time.deltaTime;
        
        rigi2D.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Talk();
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            animator.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;
            Instantiate(hitEffect, rigi2D.position + Vector2.up * 0.5f, Quaternion.identity);
            PlaySound(hitClip);
        }        

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab,
            rigi2D.position + Vector2.up * 0.5f,
            Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(throwClip);
    }

    public void Talk()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigi2D.position + Vector2.up * 0.2f,
                lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            NPC jambi = hit.collider.GetComponent<NPC>();
            if (jambi != null)
            {
                if (IsQuestComplete())
                {
                    jambi.ChangeDialogValue();
                }
                jambi.DisplayDialog();
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void TellMeFixed()
    {
        fixedRobot++;
    }

    public bool IsQuestComplete()
    {
        bool value = false;
        if (fixedRobot == GOAL_FIXED_ROBOT)
        {
            PlaySound(questClip);
            value = true;
        }
        return value;
    }
}
