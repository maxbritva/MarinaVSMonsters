using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float _moveSpeed;
   private Vector3 _movement;
   public Vector3 Movement => _movement;

   private void Update() => Move();

   private void Move()
   {
      _movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
      transform.position += _movement.normalized * (_moveSpeed * Time.deltaTime);
      
   }
}
