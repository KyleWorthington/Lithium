using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    [Header("Obejcts")]
    public Animator m_animator;
    private Rigidbody2D m_rigidbody2D;
    public GameObject m_currentProjectile;

    [Header("Attributes")]
    public float m_playerHealth;
    private float m_playerCurrentHealth;

    public float m_playerSpeed;
    private float m_playerCurrentSpeed;

    public float m_playerProjectileSpeed;

    // Player Movement and Direction
    private float m_horizontal;
    private float m_vertical;

    private Vector2 m_lookDirection = new Vector2(1, 0);

    #region Unity API
    void Start()
    {
        // Gets Attached Componenets
        m_animator = GetComponent<Animator>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();

        // Assign Variables
        m_playerCurrentHealth = m_playerHealth;
        m_playerCurrentSpeed = m_playerSpeed;
    }


    void Update()
    {
        Movement();

        // Attack Keybinds!
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            LaunchProjectile();
        }
    }

    void FixedUpdate()
    {
        // Gets current position of player
        Vector2 position = transform.position;

        // Updates player position 
        position.x += m_playerCurrentSpeed * m_horizontal * Time.deltaTime;
        position.y += m_playerCurrentSpeed * m_vertical * Time.deltaTime;

        // Moves player
        m_rigidbody2D.MovePosition(position);
    }
    #endregion

    #region Methods
    void Movement()
    {
        // Gets Input data and stores it in Movement
        Vector2 movement = new Vector2(m_horizontal, m_vertical);
        m_horizontal = Input.GetAxisRaw("Horizontal");
        m_vertical = Input.GetAxisRaw("Vertical");

        // Player will face down when not moving
        if (!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f))
        {
            m_lookDirection.Set(movement.x, movement.y);
            m_lookDirection.Normalize();
        }
    }

    /// <summary>
    /// +Positive Number for gaining health, - Negative Number for losing health
    /// </summary>
    /// <param name="amount"></param>
    public void HealthChange(float amount)
    {
        m_playerCurrentHealth = Mathf.Clamp(m_playerCurrentHealth + amount, 0, m_playerHealth);
    }
    
    private void LaunchProjectile()
    {
        GameObject projectileObject = Instantiate(m_currentProjectile, m_rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectiles projectile = projectileObject.GetComponent<Projectiles>();
        projectile.LaunchProjectile(m_lookDirection, m_playerProjectileSpeed);
    }

    #endregion 
}
