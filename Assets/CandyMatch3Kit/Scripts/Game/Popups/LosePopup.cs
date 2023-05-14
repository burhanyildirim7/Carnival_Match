using GameVanilla.Core;

namespace GameVanilla.Game.Popups
{
    /// <summary>
    /// This class contains the logic associated to the popup that is shown when the player loses a game.
    /// </summary>
    public class LosePopup : EndGamePopup
    {
        /// <summary>
        /// Unity's OnDestroy method.
        /// </summary>
        private void OnDestroy()
        {
            var playSound = GetComponent<PlaySound>();
            if (playSound != null)
            {
               playSound.Stop("Rain");
            }
        }
    }
}
