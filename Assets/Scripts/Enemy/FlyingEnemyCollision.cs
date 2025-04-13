using UnityEngine;

public class FlyingEnemyCollision : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _dmgAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController._health -= _dmgAmount;
            _gameManager.ChangeHealthIcons(false, _dmgAmount);
        }
        else
        {
            Debug.Log("Dident work");
        }
    }
}
