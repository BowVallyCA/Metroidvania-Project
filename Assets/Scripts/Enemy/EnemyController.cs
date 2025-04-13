using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerVisual;

    [SerializeField] private Vector2 _raycastCheckWall;
    [SerializeField] private float _raycastWallDistance;
    [SerializeField] private LayerMask _wallMask;

    [SerializeField] private Vector2 _raycastCheckEdge;
    [SerializeField] private float _raycastEdgeDistance;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _dmgAmount = 1;

    private int direction = 1;
    private bool changedRaycast = false;
    //private int days = 4;



    void Update()
    {
        HandleMove();
        CheckForWall();
        CheckForEdge();
    }

    private void HandleMove()
    {
        transform.Translate(transform.right * direction * Time.deltaTime);
    }

    private void FlipEnemy()
    {
        if (direction == 1)
        {
            direction = -1;
            playerVisual.localScale = new Vector2(-1 * playerVisual.localScale.x, playerVisual.localScale.y);
        }
        else if (direction == -1)
        {
            direction = 1;
            playerVisual.localScale = new Vector2(-1 * playerVisual.localScale.x, playerVisual.localScale.y);
        }
    }

    private void CheckForWall()
    {
        if (IsThereWall())
        {
            FlipEnemy();
            //Debug.Log("There is a wall in the way!");
        }
    }

    private bool IsThereWall()
    {
        return Physics2D.Raycast((Vector2)transform.position + new Vector2(direction * _raycastCheckWall.x, _raycastCheckWall.y), direction * Vector2.right, _raycastWallDistance, _wallMask);
    }

    private void CheckForEdge()
    {
        if (!IsThereEdge())
        {
            //direction *= -1;
            FlipEnemy();
            if (changedRaycast == false)
            {
                _raycastCheckEdge.y -= 1;
                changedRaycast = true;
            }
            else if (changedRaycast == true)
            {
                _raycastCheckEdge.y += 1;
                changedRaycast = false;
            }

        }
    }

    private bool IsThereEdge()
    {
        return Physics2D.Raycast((Vector2)transform.position + new Vector2(direction * _raycastCheckEdge.x, _raycastCheckEdge.y), direction * Vector2.down, _raycastEdgeDistance, _wallMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine((Vector2)transform.position + new Vector2(direction * _raycastCheckWall.x, _raycastCheckWall.y),
                        (Vector2)transform.position + new Vector2(direction * _raycastCheckWall.x, _raycastCheckWall.y) + direction * _raycastWallDistance * Vector2.right);

        Gizmos.DrawLine((Vector2)transform.position + new Vector2(direction * _raycastCheckEdge.x, _raycastCheckEdge.y),
                        (Vector2)transform.position + new Vector2(direction * _raycastCheckEdge.x, _raycastCheckEdge.y) + direction * _raycastEdgeDistance * Vector2.down);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController._health -= _dmgAmount;
            _gameManager.ChangeHealthIcons(false, _dmgAmount);
        }
    }
}
