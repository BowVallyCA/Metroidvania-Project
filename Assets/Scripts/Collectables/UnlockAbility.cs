using UnityEngine;

public class UnlockAbility : MonoBehaviour
{
    [SerializeField] private bool _changeAbility;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            if (_changeAbility == false)
            {
                playerController.dashActive = true;
                Debug.Log("Unlocked Dash Ability");
            }
            else if (_changeAbility == true)
            {
                playerController.doubleJumpActive = true;
                Debug.Log("Unlocked Double Jump Ability");
            }
            
        }

        Destroy(gameObject);
    }
}
