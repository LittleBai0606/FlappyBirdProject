using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoop : MonoBehaviour {

    //边界常量
    public const float LEFT = -4.74f;
    public const float CENTER = 1.983472f;
    public const float RIGHT = 8.7f;

    public const float MAX_Y = 2.6f;
    public const float MIN_Y = -2.6f;

    //移动速度
    public float MoveSpeed = 1.5f;

    //后续是否显示管道
    public bool IsShowPipes = false;

    public bool IsMove = false;

    //管理两个障碍物
    public GameObject[] Obstacles;

    //管理两个障碍物初始化时的位置
    private Vector3[] DefaultPositions = null;

    public void SetPipesVisible(bool visible)
    {
        foreach (GameObject gameObject in Obstacles)
        {
            SetPipesVisibleForObstacle(gameObject, visible);
        }
    }

    public void SetPipesVisibleForObstacle(GameObject obstacle, bool visible)
    {
        Transform obstacleTransform = obstacle.transform;
        foreach (Transform child in obstacleTransform)
        {
            if (child.name == "LeftPipe" || child.name == "RightPipe")
            {
                child.gameObject.SetActive(visible);
            }
        }
    }

    void SetPipesCollision(bool enable)
    {
        foreach (GameObject obstacle in Obstacles)
        {
            SetPipesCollisionForObstacle(obstacle, enable);
        }
    }

    private void SetPipesCollisionForObstacle(GameObject obstacle, bool enable)
    {
        foreach (Transform child in obstacle.transform)
        {
            if (child.name == "LeftPipe" || child.name == "RightPipe")
            {
                BoxCollider2D[] coliders = child.GetComponentsInChildren<BoxCollider2D>();
                foreach (BoxCollider2D colider in coliders)
                {
                    colider.enabled = enable;
                }
            }
        }
    }
    //设置某个障碍物里面所有管道的随机高度
    void SetPipesRandowYObstacle(GameObject obstacle)
    {
        foreach (Transform child in obstacle.transform)
        {
            if (child.name == "LeftPipe" || child.name == "RightPipe")
            {
                //随机得到一个Y值
                float randomY = UnityEngine.Random.Range(MIN_Y, MAX_Y);
                //设置LocalPosition
                Vector3 localPos = child.localPosition;
                localPos.y = randomY;
                child.localPosition = localPos;
            }
        }
    }
    void Awake()
    {
        //记录障碍物的默认位置
        DefaultPositions = new Vector3[Obstacles.Length];

        for (int i = 0; i < Obstacles.Length; i++)
        {
            DefaultPositions[i] = Obstacles[i].transform.localPosition;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (IsMove)
	    {
            for (int i = 0; i < Obstacles.Length; i++)
            {
                //当前障碍物
                GameObject currentObstacle = Obstacles[i];

                //另外障碍物
                GameObject anotherObstacle = Obstacles[i == 0 ? 1 : 0];

                //当前坐标
                Vector3 localPos = currentObstacle.transform.localPosition;
                localPos.x -= MoveSpeed * Time.deltaTime;
                
                //越界判断
                if (localPos.x <= LEFT)
                {
                    //--------------------------------currentObstacle
                   
                    //修正X
                    localPos.x = RIGHT;
                    //设置新位置
                    currentObstacle.transform.localPosition = localPos;

                    //控制管道显示
                    SetPipesVisibleForObstacle(currentObstacle, IsShowPipes);

                    //设置管道随即高度
                    if (IsShowPipes)
                    {
                        SetPipesRandowYObstacle(currentObstacle);
                    }
                    //--------------------------------anotherObstacle
                    //修正另外障碍物的位置
                    Vector3 anotherlocalPos = anotherObstacle.transform.localPosition;
                    anotherlocalPos.x = CENTER;
                    anotherObstacle.transform.localPosition = anotherlocalPos;
                   
                    //退出
                    break;
                }
                else
                {
                    //设置新位置
                    currentObstacle.transform.localPosition = localPos;
                }
            }
        }else { return;}
	}

    public void Restore()
    {
        //位置还原
        for (int i = 0; i < Obstacles.Length; i++)
        {
            Obstacles[i].transform.localPosition = DefaultPositions[i];
        }

        //可以移动
        IsMove = true;

        //管道不可见
        IsShowPipes = false;
        SetPipesVisible(false);

        //启用碰撞
        SetPipesCollision(true);
    }

    //下面是我自己写的
    /*public  void Reset()
    {
        foreach (GameObject obstacle in Obstacles)
        {
            Transform obstacleTransform = obstacle.transform;
            
                if (obstacleTransform.name == "Obstacle")
                {
                    Vector3 LeftPipeLocalPos = obstacleTransform.localPosition;
                    LeftPipeLocalPos.x = CENTER;
                obstacleTransform.localPosition = LeftPipeLocalPos;
                }
                if (obstacleTransform.name == "Obstacle2")
                {
                    Vector3 RightPipeLocalPos = obstacleTransform.localPosition;
                    RightPipeLocalPos.x = RIGHT;
                obstacleTransform.localPosition = RightPipeLocalPos;
                }
        } 
    }*/
}
