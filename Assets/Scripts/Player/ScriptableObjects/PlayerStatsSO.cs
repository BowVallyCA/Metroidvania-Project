using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/Player/Stats", order = 0)]
public class PlayerStatsSO : ScriptableObject
{
    [field:SerializeField, Range(0.1f, 10f)]public float Speed { get; private set; }
    [field:SerializeField] public float _jumpVelocity {  get; private set; }

    [Header("Ground Check")]
    public float GroundCheckDistance = 0.1f;
    public Vector2 groundCheckOffset;
    public LayerMask GroundLayer;

}
