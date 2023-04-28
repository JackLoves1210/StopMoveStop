using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetNavMeshData : Singleton<SetNavMeshData>
{
    public void SetNavMesh(NavMeshData navMeshData)
    {
        gameObject.GetComponent<NavMeshSurface>().navMeshData = navMeshData;
    }
}
