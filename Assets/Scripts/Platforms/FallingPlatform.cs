using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject _startPosition;

    private void Start()
    {
        _startPosition.transform.position = gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        StartCoroutine(PlatformFall(rigidBody));
    }

    IEnumerator PlatformFall(Rigidbody2D rigidBody)
    {
        rigidBody.constraints = RigidbodyConstraints2D.None;

        yield return new WaitForSeconds(3);

        gameObject.transform.position = _startPosition.transform.position;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        Debug.Log("Timer done! Returning to start position.");
    }
}
