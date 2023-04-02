using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
    {
        private IState<Bot> currentState;
        public NavMeshAgent agent;
        public float range; //radius of sphere
        public Transform centrePoint ; //centre of the area the agent wants to move around in
                                       //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
        Vector3 nextPoint;

        public bool isCanMove;
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            ChangeState(new IdleState());
            OnEnableWeapon();
            OnInit();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }


    public override void OnInit()
    {
        base.OnInit();
    }
    public void ChangeState(IState<Bot> state)
        {
            if (currentState != null)
            {
                currentState.OnExit(this);
            }

            currentState = state;

            if (currentState != null)
            {
                currentState.OnEnter(this);
            }
        }

    public IEnumerator DoAttack()
    {
        OnAttack();
        float time = 0;
        float timer = 1.1f;
        while (time < timer)
        {
            time += Time.deltaTime;
            
            yield return null;
        } 
        int numRand = Random.Range(0, 100);
        if (numRand > 50)
        {
            ChangeState(new IdleState());
        }
        else
        {
            ChangeState(new PatrolState());
        }
        yield return null;
    }

    public override void OnAttack()
    {
        base.OnAttack();
        SpawnWeapon();
    }
    public void Moving()
    {
        agent.enabled = true;
        if (agent.enabled)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (RandomPoint(centrePoint.position, range, out nextPoint))
                {
                    ChangeAnim(Constant.ANIM_RUN);
                    Debug.DrawRay(nextPoint, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    agent.SetDestination(nextPoint);
                }
            }
            if (IsDestination())
            {
                ChangeState(new IdleState());
            }
        }
        
    }
    public  void OnMoveStop()
    {
        agent.enabled = false;
        //ChangeAnim(Constant.ANIM_IDLE);
    }
    bool IsDestination() => Vector3.Distance(transform.position, nextPoint) - Mathf.Abs(transform.position.y - nextPoint.y) < 0.1f;
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        CancelInvoke();
    }

    public override void OnDeath()
    {
       // ChangeState(null);
        OnMoveStop();
        base.OnDeath();
        SetMask(false);
        Invoke(nameof(OnDespawn), 2f);
        LevelManager._instance.RemoveTarget(this);
    }

}