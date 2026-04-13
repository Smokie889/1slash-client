using UnityEngine;

namespace OneSlash.Client.Core
{
    public enum PlayerVisualState
    {
        Idle,
        Move,
        Charge,
        Attack,
        Parry,
        Hit,
        Dead
    }

    [System.Serializable]
    public class PlayerSnapshot
    {
        public string PlayerId;
        public Vector2 Position;
        public int Facing = 1;
        public PlayerVisualState State = PlayerVisualState.Idle;
    }
}
