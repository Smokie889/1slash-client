using OneSlash.Client.Core;
using OneSlash.Client.Input;
using OneSlash.Client.Network;
using OneSlash.Client.View;
using UnityEngine;

namespace OneSlash.Client.Gameplay
{
    public class ClientGameController : MonoBehaviour
    {
        [SerializeField] private ClientGameState gameState;
        [SerializeField] private InputCollector inputCollector;
        [SerializeField] private WsClient wsClient;
        [SerializeField] private PlayerView localPlayerView;
        [SerializeField] private PlayerView remotePlayerView;

        private int inputSeq = 0;

        private void Start()
        {
            if (wsClient != null)
            {
                wsClient.OnSnapshotReceived += HandleSnapshot;
                wsClient.Connect();
            }
        }

        private void OnDestroy()
        {
            if (wsClient != null)
            {
                wsClient.OnSnapshotReceived -= HandleSnapshot;
            }
        }

        private void Update()
        {
            if (gameState == null || inputCollector == null)
            {
                return;
            }

            InputFrame input = inputCollector.Collect();

            if (wsClient != null && wsClient.IsConnected)
            {
                InputMessage message = new InputMessage
                {
                    seq = ++inputSeq,
                    moveX = input.MoveX,
                    chargeHeld = input.ChargeHeld,
                    attackPressed = input.AttackPressed,
                    parryPressed = input.ParryPressed
                };

                wsClient.SendInput(message);
            }

            if (localPlayerView != null)
            {
                localPlayerView.Apply(gameState.LocalPlayer);
            }

            if (remotePlayerView != null)
            {
                remotePlayerView.Apply(gameState.RemotePlayer);
            }
        }

        private void HandleSnapshot(SnapshotMessage snapshot)
        {
            if (snapshot.players == null)
            {
                return;
            }

            foreach (PlayerNetState player in snapshot.players)
            {
                PlayerVisualState visualState = ParseState(player.state);

                gameState.ApplyPlayerState(
                    player.id,
                    new Vector2(player.x, player.y),
                    player.facing,
                    visualState
                );
            }
        }

        private PlayerVisualState ParseState(string state)
        {
            if (string.IsNullOrEmpty(state))
            {
                return PlayerVisualState.Idle;
            }

            return state switch
            {
                "Move" => PlayerVisualState.Move,
                "Charge" => PlayerVisualState.Charge,
                "Attack" => PlayerVisualState.Attack,
                "Parry" => PlayerVisualState.Parry,
                "Hit" => PlayerVisualState.Hit,
                "Dead" => PlayerVisualState.Dead,
                _ => PlayerVisualState.Idle
            };
        }
    }
}
