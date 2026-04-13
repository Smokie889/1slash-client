using System;
using UnityEngine;

namespace OneSlash.Client.Network
{
    [Serializable]
    public class InputMessage
    {
        public string type = "input";
        public int seq;
        public float moveX;
        public bool chargeHeld;
        public bool attackPressed;
        public bool parryPressed;
    }

    [Serializable]
    public class SnapshotMessage
    {
        public string type;
        public PlayerNetState[] players;
    }

    [Serializable]
    public class PlayerNetState
    {
        public string id;
        public float x;
        public float y;
        public int facing;
        public string state;
    }
}
