using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace UIExample
{
    public class Loses : UICanvas
    {
        [SerializeField] Player player;
        public void MainMenuButton()
        {
            UIManager.Ins.OpenUI<MainMenu>();
            ResetGame();
            this.CloseDirectly();
        }
        public void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Reset");
        }
    }
}