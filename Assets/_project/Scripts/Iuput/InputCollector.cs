using UnityEngine;
using UnityEngine.InputSystem;

namespace OneSlash.Client.Input
{
    public class InputCollector : MonoBehaviour
    {
        public InputFrame Collect()
        {
            InputFrame frame = new InputFrame
            {
                MoveX = 0f,
                ChargeHeld = false,
                AttackPressed = false,
                ParryPressed = false
            };

            Keyboard keyboard = Keyboard.current;
            Mouse mouse = Mouse.current;

            if (keyboard == null)
            {
                return frame;
            }

            if (keyboard.aKey.isPressed)
            {
                frame.MoveX -= 1f;
            }

            if (keyboard.dKey.isPressed)
            {
                frame.MoveX += 1f;
            }

            if (mouse != null && mouse.rightButton.isPressed)
            {
                frame.ChargeHeld = true;
            }

            if (mouse != null && mouse.leftButton.wasPressedThisFrame)
            {
                frame.AttackPressed = true;
            }

            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                frame.ParryPressed = true;
            }

            return frame;
        }
    }
}
