using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    [SerializeField] private Transform _playerLocation;
    [SerializeField] private float _speed = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //gameObject._playerLocation.position = Vector2.Lerp(gameObject.transform.position, _playerLocation, _speed * Time.deltaTime);
    }
}
