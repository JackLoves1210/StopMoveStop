using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionBot : MonoBehaviour
{
    [SerializeField] private Transform tfPlayer;
    [SerializeField] private Transform tfSelf;

    [SerializeField] private float limit;
    void Start()
    {
        RandomPos();
    }

    void RandomPos()
    {
        float randX = Random.Range(-limit, limit);
        float randZ = Random.Range(-limit, limit);
        tfSelf.position = new Vector3(randX, 0, randZ);
        while (Vector3.Distance(tfPlayer.position, tfSelf.position) < 5f)
        {
            randX = Random.Range(-limit, limit);
            randZ = Random.Range(-limit, limit);
            tfSelf.position = new Vector3(randX, 0, randZ);
        }  
    }
}
