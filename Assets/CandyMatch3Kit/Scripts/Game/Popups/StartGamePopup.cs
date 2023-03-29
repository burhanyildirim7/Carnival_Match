using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using FullSerializer;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.UI;

namespace GameVanilla.Game.Popups
{
    public class StartGamePopup : Popup
    {
#pragma warning disable 649
        [SerializeField]
        private Text levelText;

        [SerializeField]
        private GameObject goalPrefab;

        [SerializeField]
        private GameObject goalGroup;

        [SerializeField]
        private GameObject playButton;


#pragma warning restore 649

        private int numLevel;

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(levelText);        
            Assert.IsNotNull(goalPrefab);
            Assert.IsNotNull(goalGroup);
            Assert.IsNotNull(playButton);

        }

        /// <summary>
        /// Loads the level data corresponding to the specified level number.
        /// </summary>
        /// <param name="levelNum">The number of the level to load.</param>
        public void LoadLevelData(int levelNum)
        {
            numLevel = levelNum;

            var serializer = new fsSerializer();
            var level = FileUtils.LoadJsonFile<Level>(serializer, "Levels/" + numLevel);
            levelText.text = "Level " + numLevel;
           
            foreach (var goal in level.goals)
            {

                    var goalObject = Instantiate(goalPrefab);
                    goalObject.transform.SetParent(goalGroup.transform, false);
                    goalObject.GetComponent<GoalUiElement>().Fill(goal);

            }
            var reachScoreGoal = level.goals.Find(x => x is ReachScoreGoal);
        }

        /// <summary>
        /// Called when the play button is pressed.
        /// </summary>
        public void OnPlayButtonPressed()
        {
            PuzzleMatchManager.instance.lastSelectedLevel = numLevel;
            GetComponent<SceneTransition>().PerformTransition();
        }

        /// <summary>
        /// Called when the close button is pressed.
        /// </summary>
        public void OnCloseButtonPressed()
        {
            Close();
        }
    }
}
