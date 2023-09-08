using System;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
	public class EnemyMove : MonoBehaviour
	{
		[SerializeField] private float _moveSpeed;
		private PlayerController _playerController;
		private Vector3 _direction;

		private void Update()
		{
			Move();
			CheckDistanceToHide();
		}

		private void Move()
		{
			_direction = (_playerController.transform.position - transform.position).normalized;
			transform.position += _direction * (_moveSpeed * Time.deltaTime);
		}

		private void CheckDistanceToHide()
		{
			float distance = Vector3.Distance(transform.position, _playerController.transform.position);
			if(distance > 20f)
				gameObject.SetActive(false);
		}


		[Inject] private void Construct(PlayerController playerController) => 
			_playerController = playerController;
	}
}