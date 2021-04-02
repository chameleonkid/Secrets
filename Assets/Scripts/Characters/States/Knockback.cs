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
            target.animator.SetBool("isHurt", true);
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

        public override void Exit() {
            target.animator.SetBool("isHurt", false);
        }
    }

    public interface ICanKnockback {
        Rigidbody2D rigidbody { get; }
        Animator animator { get; }
        State currentState { get; set; }
    }
}
