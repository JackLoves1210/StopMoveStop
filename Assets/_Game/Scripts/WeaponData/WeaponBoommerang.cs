using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoommerang : WeaponCtl
{

    private Vector3 target;

    public enum State { Forward, Backward, Stop }

    private State state;

    private void Update()
    {
        //MoveForward();
        //RotateAndMove();
        Boomearang();
    }
    public override void Oninit(Character character, Vector3 target)
    {
        base.Oninit(character, target);
        this.target = (target - character.TF.position).normalized * (character.ATT_RANGE + 1) + character.TF.position + Vector3.up;
        state = State.Forward;
    }


    public void Boomearang()
    {
        switch (state)
        {
            case State.Forward:
                TF.position = Vector3.MoveTowards(TF.position, this.target, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(TF.position, target) < 0.1f)
                {
                    state = State.Backward;
                }
                break;

            case State.Backward:
                TF.position = Vector3.MoveTowards(TF.position, this._character.TF.position, moveSpeed * Time.deltaTime);
                if (_character.IsDead || Vector3.Distance(TF.position, this._character.TF.position) < 0.1f) // 
                {
                    OnDespawn();
                }
                break;
        }

    }
}
