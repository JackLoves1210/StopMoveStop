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

        public override void Open()
        {
            base.Open();
            
        }
        public void MainMenuButton()
        {
            LevelManager._instance.LoseGame();
            LevelManager._instance.canvasIndicator.gameObject.SetActive(false);
            this.CloseDirectly();
        }
    }
}