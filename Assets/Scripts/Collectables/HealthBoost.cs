using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    [SerializeField] private int _addToMaxHalth = 1;
    [SerializeField] private GameManager _gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController._healthMax += _addToMaxHalth;
            playerController._health = playerController._healthMax;

            _gameManager.ChangeHealthIcons(true, _addToMaxHalth);

            Destroy(gameObject);
        }
    }
}
