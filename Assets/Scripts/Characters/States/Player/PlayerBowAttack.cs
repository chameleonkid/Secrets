using UnityEngine;

namespace Schwer.States {
    //! Likely should refactor, since a lot of the heavy work is still in PlayerMovement.cs
    public class PlayerBowAttack : State {
        private PlayerMovement player;

        private float timer;

        public PlayerBowAttack(PlayerMovement player, float duration) {
            this.player = player;
            this.timer = duration;
        }

        public override void Enter() {
            player.animator.SetBool("isShooting", true);
        }

        public override void FixedUpdate() {
            if (timer > 0) {
                timer -= Time.deltaTime;
            }
            else {
                player.currentState = null;
            }
        }

        public override void Exit() {
            player.animator.SetBool("isShooting", false);
        }
    }
}
