using UnityEngine;

namespace Platformer
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 5f;        // Kecepatan musuh
        public float patrolDistance = 10f;   // Jarak bolak-balik

        private Vector3 startPosition;
        private bool movingRight = true;

        void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            Patrol();
        }

        void Patrol()
        {
            if (movingRight)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                if (transform.position.x >= startPosition.x + patrolDistance)
                    Flip();
            }
            else
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                if (transform.position.x <= startPosition.x - patrolDistance)
                    Flip();
            }
        }

        void Flip()
        {
            movingRight = !movingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}