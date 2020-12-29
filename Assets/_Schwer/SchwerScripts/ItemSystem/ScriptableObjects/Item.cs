using UnityEngine;
using UnityEngine.Events;

namespace Schwer.ItemSystem {
    [CreateAssetMenu(menuName = "Scriptable Object/Item System/Item")]
    public class Item : ScriptableObject {
        [SerializeField] private int _id = default;
        public int id => _id;

        [SerializeField] private string _name = default;
        public new string name => _name;

        [SerializeField][TextArea] private string _description = default;
        public string description => _description;

        public virtual string fullDescription => _description;

        [SerializeField] private Sprite _sprite = default;
        public Sprite sprite => _sprite;

        [SerializeField] private AudioClip _sound = default;
        public AudioClip sound => _sound;

        [SerializeField] private bool _unique = default;
        public bool unique => _unique;

        [SerializeField] private bool _usable = default;
        public bool usable => _usable;

        [SerializeField] private int _level = default;
        public int level => _level;

        [SerializeField] private UnityEvent OnUse = default;

        [SerializeField] private int _buyPrice = default;
        public int buyPrice => _buyPrice;

        [SerializeField] private int _sellPrice = default;
        public int sellPrice => _sellPrice;

        public void Use() => OnUse?.Invoke();
    }
}
