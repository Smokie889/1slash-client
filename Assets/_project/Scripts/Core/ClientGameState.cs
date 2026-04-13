using UnityEngine;

namespace OneSlash.Client.Core
{
    public class ClientGameState : MonoBehaviour
    {
        public PlayerSnapshot LocalPlayer = new PlayerSnapshot
        {
            PlayerId = "p1",
            Position = new Vector2(-3f, -1.5f),
            Facing = 1,
            State = PlayerVisualState.Idle
        };

        public PlayerSnapshot RemotePlayer = new PlayerSnapshot
        {
            PlayerId = "p2",
            Position = new Vector2(3f, -1.5f),
            Facing = -1,
            State = PlayerVisualState.Idle
        };

        public void ApplyPlayerState(
            string playerId,
            Vector2 position,
            int facing,
            PlayerVisualState state)
        {
            PlayerSnapshot target = playerId == "p1" ? LocalPlayer : RemotePlayer;

            target.Position = position;
            target.Facing = facing;
            target.State = state;
        }
    }
}
