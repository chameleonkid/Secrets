using UnityEngine;
using UnityEngine.SceneManagement;

namespace Schwer.States {
    //! This probably isn't the cleanest example of a State
    public class PlayerDead : State {
        private PlayerMovement player;

        private float timer;

        public PlayerDead(PlayerMovement player) {
            this.player = player;
            timer = 1;  // Hard-coded, as it was previously in PlayerMovement
        }

        public override void Enter() => player.animator.SetBool("isDead", true);

        public override void Update() {
            if (timer > 0) {
                timer -= Time.deltaTime;
            }
            else {
                SceneManager.LoadScene("DeathMenu");
            }
        }
    }
}
