using UnityEngine;

using GameVanilla.Game.Common;

namespace GameVanilla.Game.UI
{
	/// <summary>
	/// This class loads the booster data into the in-game booster buttons when the game starts.
	/// </summary>
	public class BoosterBar : MonoBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private BuyBoosterButton button1;

		[SerializeField]
		private BuyBoosterButton button2;

		[SerializeField]
		private BuyBoosterButton button3;

		[SerializeField]
		private BuyBoosterButton button4;

        [SerializeField]
        private GameObject _lock1;

        [SerializeField]
        private GameObject _lock2;

        [SerializeField]
        private GameObject _lock3;

        [SerializeField]
        private GameObject _lock4;
#pragma warning restore 649

        /// <summary>
        /// Sets the data of the in-game booster buttons.
        /// </summary>
        /// <param name="level">The current level.</param>
        public void SetData(Level level)
		{
			if (level.availableBoosters[BoosterType.Lollipop])
			{
				button1.UpdateAmount(PlayerPrefs.GetInt("num_boosters_0"));
                _lock1.SetActive(false);
            }
			else
			{
				button1.gameObject.SetActive(false);
				
			}

			if (level.availableBoosters[BoosterType.Bomb])
			{
				button2.UpdateAmount(PlayerPrefs.GetInt("num_boosters_1"));
                _lock2.SetActive(false);
            }
			else
			{
				button2.gameObject.SetActive(false);
			}

			if (level.availableBoosters[BoosterType.Switch])
			{
				button3.UpdateAmount(PlayerPrefs.GetInt("num_boosters_2"));
                _lock3.SetActive(false);
            }
			else
			{
				button3.gameObject.SetActive(false);
			}

			if (level.availableBoosters[BoosterType.ColorBomb])
			{
				button4.UpdateAmount(PlayerPrefs.GetInt("num_boosters_3"));
                _lock4.SetActive(false);
            }
			else
			{
				button4.gameObject.SetActive(false);
			}
		}
	}
}
