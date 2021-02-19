using UnityEngine;

namespace Schwer.States {
    public class Knockback : State {
        private ICanKnockback target;

        private Vector2 force;
        private float timer;

        public Knockback(ICanKnockback target, Vector2 force, float duration) {
            this.target = target;
            this.force = force;
            timer = duration;
        }

        public override void Enter() {
            target.rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        public override void FixedUpdate() {
            if (timer > 0) {
                timer -= Time.deltaTime;
            }
            else {
                target.currentState = null;
            }
        }
    }

    public interface ICanKnockback {
        Rigidbody2D rigidbody { get; }
        State currentState { set; }
    }
}
