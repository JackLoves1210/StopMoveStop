using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIExample
{
    public class Win : UICanvas
    {
        public Text score;

        public void MainMenuButton()
        {
            //UIManager.Ins.OpenUI<MainMenu>();
            LevelManager._instance.NextLevelGame();
            CloseDirectly();
        }

        public void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Reset");
        }
    }
}