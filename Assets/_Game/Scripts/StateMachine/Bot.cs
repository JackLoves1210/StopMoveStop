using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;
    public NavMeshAgent agent;
    public float range; 
    public Transform centrePoint ;      
    Vector3 nextPoint;
    public bool isCanMove;
    public AudioSource audioSource;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material fadedMaterial;
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
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new IdleState());
        ChangeWeapon(indexWeapon);
        SetMask(false);
        SetIndicator(this);
        ChangeColorOnRevive();
        ChangeHat(0);
        ChangeAccessory(0);
        ChangePant(0);
        ChangeSkin(0);
    }
    public override void ChangeWeapon(int index)
    {
        base.ChangeWeapon(index);
        index = Random.Range(0, ItemManager.Ins.weaponTypes.Length);
        _weaponType = ItemManager.Ins.weaponTypes[index];
        OnEnableWeapon(_weaponType);
    }
    public override void ChangeHat(int index)
    {
        base.ChangeHat(index);
        index = Random.Range(0, ItemManager.Ins.pantTypes.Length);
        if (hatType != null)
        {
            Destroy(hatType);
        }
        hatType = Instantiate(ItemManager.Ins.hatTypes[index]);
        hatType.transform.SetParent(hatTranform, false);
    }
    public override void ChangeSkin(int index)
    {
        base.ChangeSkin(index);
        index = Random.Range(0, ItemManager.Ins.materialsSkin.Length);
        modelSkin.GetComponent<Renderer>().material = ItemManager.Ins.materialsSkin[index];
    }
    public override void ChangeAccessory(int index)
    {
        base.ChangeAccessory(index);
        index = UnityEngine.Random.Range(0, ItemManager.Ins.accessoryTypes.Length - 1);
        if (hatType != null)
        {
            Destroy(accessoryType);
        }
        accessoryType = Instantiate(ItemManager.Ins.accessoryTypes[index]);
        accessoryType.transform.SetParent(accessoryTranform, false);
    }
    public override void ChangePant(int index)
    {
        base.ChangePant(index);
        index = UnityEngine.Random.Range(0, ItemManager.Ins.pantTypes.Length);
        modelPant.transform.GetComponent<Renderer>().material = ItemManager.Ins.pantTypes[index];

    }
    public override void SetIndicator(Character character)
    {
        base.SetIndicator(character);
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
        float timer = 1.11f;
        while (time < timer)
        {
            time += Time.deltaTime;
            
            yield return null;
        } 
        int numRand = Random.Range(0, 100);
        if (numRand > 90)
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
    public override void SpawnWeapon()
    {
        base.SpawnWeapon();
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
    public override void SetSize(float size)
    {
        base.SetSize(size);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        this.IsDead = false;
        SimplePool.Despawn(this);
        CancelInvoke();
    }
    public override void OnDeath()
    {
        OnMoveStop();
        base.OnDeath();
        SetMask(false);
        ClearTarget(this);
        // Invoke(nameof(OnDespawn), 2f);
        audioSource.Play();
        LevelManager.Ins.RemoveTarget(this);
    }
    public void ChangeColorOnDead()
    {
    
        TF.gameObject.GetComponentInChildren<Renderer>().material.color = Color.Lerp(originalMaterial.color, fadedMaterial.color, 1f);
    }
    public void ChangeColorOnRevive()
    {
        TF.gameObject.GetComponentInChildren<Renderer>().material.color = originalMaterial.color;
    }
}