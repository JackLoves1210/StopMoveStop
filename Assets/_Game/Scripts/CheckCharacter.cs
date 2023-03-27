using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCharacter : MonoBehaviour
{
    public Character Character;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character _target = other.GetComponent<Character>();
            if (!_target.IsDead)
            {
                Character.AddTarget(_target);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character _target = other.GetComponent<Character>();
            Character.RemoveTarget(_target);
        }
    }
}
