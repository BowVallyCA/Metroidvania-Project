using UnityEngine;

public class DmgObjects : MonoBehaviour
{
    [SerializeField] private int _dmgAmount;
    [SerializeField] private GameManager _gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController._health -= _dmgAmount;
            _gameManager.ChangeHealthIcons(false, _dmgAmount);

            Destroy(gameObject);
        }
    }
}
