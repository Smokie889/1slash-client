using OneSlash.Client.Core;
using UnityEngine;

namespace OneSlash.Client.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform bodyTransform;

        public void Apply(PlayerSnapshot snapshot)
        {
            transform.position = snapshot.Position;

            Vector3 scale = bodyTransform.localScale;
            scale.x = Mathf.Abs(scale.x) * snapshot.Facing;
            bodyTransform.localScale = scale;
        }

        private void Reset()
        {
            if (transform.childCount > 0)
            {
                bodyTransform = transform.GetChild(0);
            }
        }
    }
}
