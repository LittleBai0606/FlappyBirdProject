using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoop : MonoBehaviour {

    //边界常量
    public const float LEFT = -4.74f;
    public const float CENTER = 1.983472f;
    public const float RIGHT = 8.7f;

    //移动速度
    public float MoveSpeed = 1.5f;

    //后续是否显示管道
    public bool IsShowPipes = false;

    public bool IsMove = false;

    //管理两个障碍物
    public GameObject[] Obstacles;

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

   public  void Reset()
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
        
    }
}
