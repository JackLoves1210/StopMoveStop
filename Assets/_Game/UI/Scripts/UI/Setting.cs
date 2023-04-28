using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIExample
{
    public class Setting : UICanvas
    {
        [SerializeField] private GameObject btnTurnOffSound;
        [SerializeField] private GameObject btnTurnOnSound;
        [SerializeField] private GameObject btnTurnOffVibration;
        [SerializeField] private GameObject btnTurnOnVibration;

        public override void Open()
        { 
            base.Open();
            LevelManager.Ins.player.isCanMove = false;
            if (MainMenu.Ins.muteSound)
            {
                MuteOnSound();
            }
            else
            {
                MuteOffSound();
            }
        }
        public void HomeButton()
        {
            UIManager.Ins.OpenUI<GamePlay>().Resume();
            UIManager.Ins.OpenUI<GamePlay>().CloseDirectly();
            LevelManager.Ins.LoseGame();
            LevelManager.Ins.canvasIndicator.gameObject.SetActive(false);
            CloseDirectly();

        }
        public void ContinueButton()
        {
            UIManager.Ins.OpenUI<GamePlay>().Resume();
            CloseDirectly();
            LevelManager.Ins.player.isCanMove = true;
        }

        public void MuteOnSound()
        {
            MainMenu.Ins.MuteOn();
            btnTurnOffSound.SetActive(false);
            btnTurnOnSound.SetActive(true);
        }

        public void MuteOffSound()
        {
            MainMenu.Ins.MuteOff();
            btnTurnOffSound.SetActive(true);
            btnTurnOnSound.SetActive(false);
        }
        public void MuteOnVibration()
        {
            btnTurnOffVibration.SetActive(false);
            btnTurnOnVibration.SetActive(true);
        }

        public void MuteOffVibration()
        {
            btnTurnOnVibration.SetActive(false);
            btnTurnOffVibration.SetActive(true);
        }
    }
}