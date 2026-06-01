using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;
        private bool facingRight = false;

        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;
        public Transform groundCheck;
        public LayerMask groundLayer; // ← TAMBAH INI di Inspector, set ke layer "Ground"

        private Rigidbody2D rb;
        private Animator animator;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            CheckGround();

            // ✅ Pindahkan movement ke FixedUpdate agar lebih smooth dan konsisten
            if (!deathState)
            {
                rb.linearVelocity = new Vector2(moveInput * movingSpeed, rb.linearVelocity.y);
            }
        }

        void Update()
        {
            moveInput = Input.GetAxis("Horizontal"); // ✅ Selalu baca input, bukan hanya saat GetButton

            // Animasi gerak
            if (Mathf.Abs(moveInput) > 0.1f)
            {
                animator.SetInteger("playerState", 1);
            }
            else if (isGrounded)
            {
                animator.SetInteger("playerState", 0);
            }

            // ✅ Jump: cek di Update tapi pakai flag isGrounded yang diupdate FixedUpdate
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                // ✅ Reset velocity Y dulu agar force konsisten
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (!isGrounded)
            {
                animator.SetInteger("playerState", 2);
            }

            // Flip karakter
            if (!facingRight && moveInput > 0)
                Flip();
            else if (facingRight && moveInput < 0)
                Flip();
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }

        private void CheckGround()
        {
            // ✅ Gunakan LayerMask agar hanya deteksi layer Ground, bukan diri sendiri
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }

        
    }
}