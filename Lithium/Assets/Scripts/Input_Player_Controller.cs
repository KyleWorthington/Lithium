using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Player_Controller : MonoBehaviour
{
    //Input System
    private PlayerInput m_playerInput;
    private InputAction m_move;
    private InputAction m_shoot;

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

    // Start is called before the first frame update
    void Start()
    {
        // Gets Attached Componenets
        m_animator = GetComponent<Animator>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();

        // Assign Input System
        m_playerInput = GetComponent<PlayerInput>();
        m_move = m_playerInput.actions["Movement"];
        m_shoot = m_playerInput.actions["Attack"];

        // Assign Variables
        m_playerCurrentHealth = m_playerHealth;
        m_playerCurrentSpeed = m_playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    void FixedUpdate() {
        // Gets current position of player

        // Moves player
        m_rigidbody2D.MovePosition(m_rigidbody2D + m_move * m_playerSpeed * Time.fixedDeltaTime);
    }

    #region Methods

    void Movement() {
        // Gets Input data and stores it in Movement
        Vector2 input = m_move.ReadValue<Vector2>();
        Vector2 movement = new Vector2(input.x, input.y);

        // Player will face down when not moving
        if (!Mathf.Approximately(movement.x, 0.0f) || !Mathf.Approximately(movement.y, 0.0f)) {
            m_lookDirection.Set(movement.x, movement.y);
            m_lookDirection.Normalize();
        }
    }
    #endregion
}
