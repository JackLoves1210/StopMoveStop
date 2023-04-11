using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class WeaponShop : UICanvas
{
    public override void Open()
    {
        base.Open();
        CameraFollow._instance.ChangeState(CameraFollow.State.Shop);
    }
    public void ExitBtn()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        CloseDirectly();
    }

    public override void Close(float delayTime)
    {
        base.Close(delayTime);
        CameraFollow._instance.ChangeState(CameraFollow.State.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();
    }
}
