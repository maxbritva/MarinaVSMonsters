using System;
using System.Collections;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
	public class EnemyMove : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private float _freezeTimer;
		[SerializeField] private float _moveSpeed;
		private PlayerController _playerController;
		private float _initialMoveSpeed;
		private WaitForSeconds _timer;
		private Vector3 _direction;

		private void Update()
		{
			Move();
			CheckDistanceToHide();
		}

		private void Start()
		{
			_initialMoveSpeed = _moveSpeed;
			_timer = new WaitForSeconds(_freezeTimer);
		}

		public void StopEnemy(bool value) => _moveSpeed = value ? 0f : _initialMoveSpeed;

		public void Freeze()
		{
			if (gameObject.activeSelf)
				StartCoroutine(StartFreeze());
		}
		private void Move()
		{
			_direction = (_playerController.transform.position - transform.position).normalized;
			transform.position += _direction * (_moveSpeed * Time.deltaTime);
			_animator.SetFloat("Horizontal", _direction.x);
			_animator.SetFloat("Vertical", _direction.y);
		}

		private void CheckDistanceToHide()
		{
			float distance = Vector3.Distance(transform.position, _playerController.transform.position);
			if(distance > 20f)
				gameObject.SetActive(false);
		}

		private IEnumerator StartFreeze()
		{
			_moveSpeed /= 2;
			yield return _timer;
			_moveSpeed = _initialMoveSpeed;
		}

		[Inject] private void Construct(PlayerController playerController) => 
			_playerController = playerController;
	}
}