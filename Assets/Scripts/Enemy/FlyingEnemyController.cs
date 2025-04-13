using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private SpriteRenderer enemyvisual;

    int direction = 1;
    private float horizontalInput;

    private void Update()
    {
        Vector2 target = currentMovementTarget();

        enemy.position = Vector2.Lerp(enemy.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)enemy.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }

        if (horizontalInput > 0) direction = 1;
        else if (horizontalInput < 0) direction = -1;

        enemyvisual.flipX = direction < 0;
    }

    Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        if (enemy != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(enemy.transform.position, startPoint.transform.position);
            Gizmos.DrawLine(enemy.transform.position, endPoint.transform.position);
        }
    }
}
