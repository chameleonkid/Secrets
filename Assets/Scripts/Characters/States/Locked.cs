using UnityEngine;

namespace Schwer.States {
    public class Locked : State {
        private Character target;

        private float? timer;
        private string animation;

        public Locked(Character target) {
            this.target = target;
        }

        public Locked(Character target, float duration) {
            this.target = target;
            timer = duration;
        }

        public Locked(Character target, string animation) {
            this.target = target;
            this.animation = animation;
        }

        public Locked(Character target, float duration, string animation) {
            this.target = target;
            timer = duration;
            this.animation = animation;
        }

        public override void Enter() {
            if (!string.IsNullOrEmpty(animation)) {
                target.animator.SetBool(animation, true);
            }
        }

        public override void FixedUpdate() {
            if (timer.HasValue) {
                if (timer > 0) {
                    timer -= Time.deltaTime;
                }
                else {
                    target.currentState = null;
                }
            }
        }

        public override void Exit() {
            if (!string.IsNullOrEmpty(animation)) {
                target.animator.SetBool(animation, false);
            }
        }
    }
}
