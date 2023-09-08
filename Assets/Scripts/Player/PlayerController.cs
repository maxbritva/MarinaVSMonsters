using UnityEngine;

namespace Player
{
   public class PlayerController : MonoBehaviour
   {
      [SerializeField] private float _moveSpeed;
      [SerializeField] private Animator _animator;
      private Vector3 _movement;
      private float _maxXPos = 64.8f;
      private float _minXPos = -66.79f;
      private float _maxYPos = 37.16f;
      private float _minYPos = -34.61f;
      public Vector3 Movement => _movement;

      private void Update() => Move();

      private void Move()
      {
         _movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
         transform.position += _movement.normalized * (_moveSpeed * Time.deltaTime);
         _animator.SetFloat("Horizontal", _movement.x);
         _animator.SetFloat("Vertical", _movement.y);
         _animator.SetFloat("Speed", _movement.sqrMagnitude);
         WorldBounds();
      }

      private void WorldBounds()
      {
         if (transform.position.x > _maxXPos) 
            transform.position = new Vector3(_maxXPos, transform.position.y, 0);
         if (transform.position.x < _minXPos) 
            transform.position = new Vector3(_minXPos, transform.position.y, 0);
         if (transform.position.y < _minYPos) 
            transform.position = new Vector3(transform.position.x, _minYPos, 0);
         if (transform.position.y > _maxYPos) 
            transform.position = new Vector3(transform.position.x, _maxYPos, 0);
      }
   }
}
