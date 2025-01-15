using UnityEngine;

namespace UnityProjectCsan
{
    public class test : MonoBehaviour
    {
        [Header("Grid Boundaries (Top-Down)")]
        public float minX = -10f;
        public float maxX = 10f;
        public float minY = -10f;
        public float maxY = 10f;

        [Header("Movement Settings")]
        public float moveSpeed = 3f;             // How fast the AI moves
        public float turnSpeed = 180f;           // How fast the AI turns
        public float directionChangeInterval = 3f;
        public float raycastDistance = 1.5f;     // Distance to check for obstacles in front

        private Vector2 currentDirection;
        private float directionChangeTimer = 0f;

        void Start()
        {
            // Pick an initial random direction
            PickNewRandomDirection();
        }

        void Update()
        {
            // Keep track of time since last direction change
            directionChangeTimer += Time.deltaTime;

            // If we've exceeded our interval, pick a new direction
            if (directionChangeTimer >= directionChangeInterval)
            {
                PickNewRandomDirection();
            }

            // If there is an obstacle in the way, pick a new direction immediately
            if (IsObstacleInFront())
            {
                PickNewRandomDirection();
            }

            // Move in the chosen direction
            MoveInCurrentDirection();

            // Stay within the defined grid boundaries
            StayWithinBounds();
        }

        private void PickNewRandomDirection()
        {
            directionChangeTimer = 0f;

            // Pick a random 2D direction
            float randX = Random.Range(-1f, 1f);
            float randY = Random.Range(-1f, 1f);

            Vector2 newDir = new Vector2(randX, randY).normalized;

            // Ensure we don't pick a zero vector
            if (newDir.sqrMagnitude <= Mathf.Epsilon)
            {
                newDir = Vector2.right; // Default to facing right if that happens
            }

            currentDirection = newDir;
        }

        private bool IsObstacleInFront()
        {
            // Cast a 2D ray from the transform’s position in the current direction
            // If it hits any collider, we have an obstacle in front
            RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, raycastDistance);
            return hit.collider != null;
        }

        private void MoveInCurrentDirection()
        {
            // Calculate the angle to face based on our direction
            float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            // Smoothly rotate to face the direction
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );

            // Move "forward" in local space — in 2D top-down, often "forward" is the local X-axis.
            // Since our sprite faces right by default, we translate along Vector2.right.
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }

        private void StayWithinBounds()
        {
            Vector3 pos = transform.position;

            // Clamp X
            if (pos.x < minX)
            {
                pos.x = minX;
                PickNewRandomDirection();
            }
            else if (pos.x > maxX)
            {
                pos.x = maxX;
                PickNewRandomDirection();
            }

            // Clamp Y
            if (pos.y < minY)
            {
                pos.y = minY;
                PickNewRandomDirection();
            }
            else if (pos.y > maxY)
            {
                pos.y = maxY;
                PickNewRandomDirection();
            }

            transform.position = pos;
        }
    }
}