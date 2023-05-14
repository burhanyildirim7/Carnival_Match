using UnityEngine;
using UnityEngine.UI;

namespace GameVanilla.Core
{
    /// <summary>
    /// Utility class for swapping the sprite of a UI Image between two predefined values.
    /// </summary>
    public class SpriteSwapper : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField]
        private Sprite enabledSprite;

        [SerializeField]
        private Sprite disabledSprite;
#pragma warning restore 649
        [SerializeField]
        private Image image;

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        public void Awake()
        {
        }

        /// <summary>
        /// Swaps the sprite.
        /// </summary>
        public void SwapSprite()
        {
            image.sprite = image.sprite == enabledSprite ? disabledSprite : enabledSprite;
        }

        /// <summary>
        /// Sets the sprite as enabled/disabled.
        /// </summary>
        /// <param name="spriteEnabled">True if the sprite should be enabled; false otherwise.</param>
        public void SetEnabled(bool spriteEnabled)
        {
            image.sprite = spriteEnabled ? enabledSprite : disabledSprite;
        }
    }
}
