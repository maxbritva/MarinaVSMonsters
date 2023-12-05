using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore.LevelSystem
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gameTimer;
        private LevelSystem _levelSystem;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private Coroutine _timerRoutine;
        private int _seconds, _minutes;
        public int Minutes => _minutes;

        private void Awake() => Activate();
        
        public void Activate() => _timerRoutine = StartCoroutine(Timer());

        public void Deactivate()
        {
            if(_timerRoutine != null)
                StopCoroutine(_timerRoutine);
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                _seconds++;
                if (_seconds >= 60)
                {
                    _minutes++;
                    _seconds = 0;
                    _levelSystem.OnLevelChanged?.Invoke();
                }
                TimeFormat();
              yield return _tick;
            }
        }

        private void TimeFormat()
        {
            _gameTimer.text = $"{_minutes}:{_seconds}";
            if(_seconds < 10 && _minutes < 10)
                _gameTimer.text = $"0{_minutes}:0{_seconds}";
            else if(_seconds < 10)
                _gameTimer.text = $"{_minutes}:0{_seconds}";
            else if(_minutes < 10)
                _gameTimer.text = $"0{_minutes}:{_seconds}";
        }

       [Inject] private void Construct(LevelSystem level) => _levelSystem = level;
    }
}