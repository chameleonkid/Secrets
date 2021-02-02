using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Primitives/Constrained Float")]
public class ConstrainedFloat : ScriptableObject
{
    //These are the Events
    public event Action OnMinChanged;
    public event Action OnMaxChanged;
    public event Action OnCurrentChanged;

    [SerializeField] private float _min = 0;
    public float min {                                                                                      // These Parts just make sure min isnt greater than max
        get => _min;
        set {
            if (value > _max) {
                Debug.Log("Warning: `min` (" + value + ") is greater than `max` (" + _max + ")!");
            }

            if (_min != value) {
                _min = value;
                OnMinChanged?.Invoke();
            }
        }
    }

    [SerializeField] private float _max = 6;
    public float max
    {                                                                                                   // These Parts just make sure max isnt smaller than min
        get => _max;
        set {
            if (value < _min) {
                Debug.Log("Warning: `max` (" + value + ") is less than `min` (" + _min + ")!");
            }

            if (_max != value) {
                _max = value;
                OnMaxChanged?.Invoke();
            }
        }
    }

    [SerializeField] private float _current;                                                        // These Parts just make sure current is between min and max
    public float current {
        get => _current;
        set {
            if (value > _max) {
                value = _max;
            }
            else if (value < _min) {
                value = _min;
            }

            if (_current != value) {
                _current = value;
                OnCurrentChanged?.Invoke();      //When Current is changed, subscribers do something
            }
        }
    }
}
