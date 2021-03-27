using UnityEngine;

namespace Schwer.States {
    //! Likely should refactor, since a lot of the heavy work is still in PlayerMovement.cs
    public class PlayerRoundAttack : State {
        private PlayerMovement player;

        public PlayerRoundAttack(PlayerMovement player) {
            this.player = player;
        }

        public override void Enter() {
            player.animator.SetBool("RoundAttacking", true);
        }

        public override void FixedUpdate() {
            var weapon = player.inventory.currentWeapon;
            player.roundAttack.damage = Random.Range(weapon.minDamage, weapon.maxDamage + 1);
            player.roundAttack.isCritical = player.IsCriticalHit();
            //! Is this missing a sound request?
            player.mana.current--;
        }

        public override void Exit() {
            player.animator.SetBool("RoundAttacking", false);
        }
    }
}
