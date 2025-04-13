using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int _healAmount;
    [SerializeField] private GameManager _gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            if (playerController._health < playerController._healthMax)
            {
                playerController._health += _healAmount;
                _gameManager.ChangeHealthIcons(true, _healAmount);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
