using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int _value = 1;
    [SerializeField] private GameManager _gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _gameManager.UpdateCoinCount(_value);
        Destroy(gameObject);
    }
}
