using UnityEngine;

namespace Schwer.States
{
    public class PlayerBlock : State
    {
        private PlayerMovement player;

        public PlayerBlock(PlayerMovement player)
        {
            this.player = player;
        }

        public override void Enter()
        {
            player.isBlocking = true; // Set the blocking flag to true
            StopPlayerMovement(); // Ensure the player stops moving immediately
            player.shieldBlock.block(player.GetFacingDirection()); // Start the block with the facing direction
        }

        public override void FixedUpdate()
        {
            StopPlayerMovement(); // Ensure that movement remains halted while blocking
        }

        public override void Exit()
        {
            player.isBlocking = false; // Reset the blocking flag to false
            player.shieldBlock.stopBlock(); // Stop the block
            player.rigidbody.isKinematic = false; // Re-enable physics interactions
        }

        private void StopPlayerMovement()
        {
            player.rigidbody.velocity = Vector2.zero; // Ensure velocity is zero
            player.rigidbody.angularVelocity = 0f; // Stop any angular movement (in case of rotation)
            player.rigidbody.isKinematic = true; // Set rigidbody to kinematic to stop all movement
        }
    }
}