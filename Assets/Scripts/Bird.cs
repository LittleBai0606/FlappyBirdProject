using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    //跳跃的速度
    public float JumpSpeed = 5f;

    private BoxCollider2D colider;

    //使用重力
    public bool UseGravity
    {
        get { return GetComponent<Rigidbody2D>().gravityScale == 1; }
        set { GetComponent<Rigidbody2D>().gravityScale = value ? 1 : 0; }
    }

    //穿过事件
    public event Action OnHit;

    //死亡事件
    public event Action OnDead;

    //默认位置
    internal Vector3 DefaultPosition;

    void Awake()
    {
        //记录小鸟初始位置(世界位置)
        DefaultPosition = this.transform.position;

        //监听死亡事件
        OnDead += Bird_OnDead;
    }

    void Start()
    {
        colider = GetComponent<BoxCollider2D>();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("碰到东西了");
        if (Game.Instance.GameState == GameState.Play && OnDead != null) { OnDead(); }
    }
    void Update()
    {

    }

    public bool IsVisible
    {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value);}
    }

    public void Jump()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.up * JumpSpeed;
    }

    public void Die()
    {
        //停止动画
        GetComponent<Animator>().enabled = false;
    }

    public void Reset()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        //还原
        this.transform.position = DefaultPosition;
        rigid.velocity = Vector3.zero;
        IsVisible = true;
        //禁用重力
        UseGravity = false;
        //重播动画
        GetComponent<Animator>().enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void Bird_OnDead()
    {
        Die();
    }
}
