using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //[SerializeField] private Transform _tf;
    //[SerializeField] private Transform _target;
    //[SerializeField] private Vector3 _offset;
    //[SerializeField] private float _moveSpeed;

    public static CameraFollow _instance;
    public enum State { MainMenu, Gameplay, Shop }

    [SerializeField] Transform tf;

    [Header("Rotation")]
    [SerializeField] Vector3 playerRotate;
    [SerializeField] Vector3 gamePlayRotate;

    [Header("Offset")]
    [SerializeField] Vector3 playerOffset;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 offsetMax;
    [SerializeField] Vector3 offsetMin;

    [SerializeField] Transform[] offsets;

    [SerializeField] float moveSpeed = 5f;
    Transform target;

    private Vector3 targetOffset;
    private Quaternion targetRotate;

    public Camera Camera;

    private void Awake()
    {
        _instance = this;
        target = FindObjectOfType<Player>().transform;
        Camera = Camera.main;
    }

    private void LateUpdate()
    {
        offset = Vector3.Lerp(offset, targetOffset, Time.deltaTime * moveSpeed);
        tf.rotation = Quaternion.Lerp(tf.rotation, targetRotate, Time.deltaTime * moveSpeed);
        //_tf.position = Vector3.Lerp(_tf.position, _offset + _target.position, _moveSpeed * Time.deltaTime);
        tf.position = Vector3.Lerp(tf.position, target.position + targetOffset, Time.deltaTime * moveSpeed);
    }

    //rate
    public void SetRateOffset(float rate)
    {
        targetOffset = Vector3.Lerp(offsetMin, offsetMax, rate);
    }

    public void ChangeState(State state)
    {
        targetOffset = offsets[(int)state].localPosition;
        targetRotate = offsets[(int)state].localRotation;
        return;

        switch (state)
        {
            case State.MainMenu:
                targetOffset = playerOffset;
                targetRotate = Quaternion.Euler(playerRotate);
                break;

            case State.Gameplay:
                targetOffset = offsetMin;
                targetRotate = Quaternion.Euler(gamePlayRotate);
                break;

            default:
                break;
        }
    }
}
