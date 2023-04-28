using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIExample

{
    public class MainMenu : UICanvas
    {
        public static MainMenu Ins;
        [SerializeField] private Text textCoin;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject buttonMuteOn;
        [SerializeField] private GameObject buttonMuteOff;
        public bool muteSound;
        public void Awake()
        {
            Ins = this;
        }
        private void Start()
        {
            //LevelManager.Ins.stateGame = LevelManager.StateGame.MainMenu;
            PlayerPrefs.GetString(UserData.Key_NamePlayer);
        }
        public override void Open()
        {
            base.Open();
            UserData.Ins.coin = PlayerPrefs.GetInt("Coin", 0);
            SetCoin(UserData.Ins.coin);
            CameraFollow.Ins.ChangeState(CameraFollow.State.MainMenu);
            if (LevelManager.Ins != null)
            {
                LevelManager.Ins.stateGame = LevelManager.StateGame.MainMenu;
            }
            muteSound = UserData.Ins.GetBool(UserData.Key_SoundIsOn);
            if (muteSound)
            {
                MuteOn();
            }
            else
            {
                MuteOff();
            }
        }
        public void PlayButton()
        {
            LevelManager.Ins.canvasIndicator.gameObject.SetActive(true);
            LevelManager.Ins.player.isCanMove = true;
            UIManager.Ins.OpenUI<GamePlay>();
            foreach (var item in BotManager._instance.bots)
            {
                item.isCanMove = true;
            }
            CameraFollow.Ins.ChangeState(CameraFollow.State.Gameplay);
            CloseDirectly();
             LevelManager.Ins.stateGame = LevelManager.StateGame.GamePlay;
        }

        public void OpenWeaponShop()
        {
            UIManager.Ins.OpenUI<WeaponShop>();
            Close(0);
        }

        public void OpenSkinShop()
        {
            UIManager.Ins.OpenUI<UIShop>();
            Close(0);
        }

        public void SetCoin(int coin)
        {
            textCoin.text = coin.ToString();
        }

        public void SetName()
        {
            PlayerPrefs.SetString(UserData.Key_NamePlayer, inputField.text);
            PlayerPrefs.Save();
            LevelManager.Ins.player.SetName();
        }
        public void MuteOn()
        {
            buttonMuteOff.SetActive(true);
            buttonMuteOn.SetActive(false);
            AudioManager.Ins.MuteHandler(true);
            UserData.Ins.SetBool(UserData.Key_SoundIsOn, true);
            UserData.Ins.soundIsOn = UserData.Ins.GetBool(UserData.Key_SoundIsOn);
           // Debug.Log(UserData.Ins.soundIsOn);
        }

        public void MuteOff()
        {
            buttonMuteOff.SetActive(false);
            buttonMuteOn.SetActive(true);
            AudioManager.Ins.MuteHandler(false);
            UserData.Ins.SetBool(UserData.Key_SoundIsOn, false);
            UserData.Ins.soundIsOn = UserData.Ins.GetBool(UserData.Key_SoundIsOn);
           // Debug.Log(UserData.Ins.soundIsOn);
        }
    }
}