using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    #region variables

    // Start is called before the first frame update
    //player gameobject
    public Transform player;
    [Header("AI Values")]
    [Tooltip("use this to apply behaviour when moving to goals/chasing")]
    public float speed = 1.5f;
    public float noticeWaypoint = 0;
    public float minGoalDistance = 0.05f;
    public float chaseDistanace = 3f;
    [Header("waypoints")]
    [Tooltip("waypoints when patrolling")]
    public int wayPointIndex = 0;
    public List<GameObject> wayPoints;
    //respawn coins when all gone
    public GameObject coinPrefab;
    public GameManager gameManager;
    #endregion

    #region methods
   
    //AI behaviour when noticing player
    public bool NoticePlayer()
    {
        return Vector2.Distance(transform.position, player.position) < chaseDistanace;

    }

    //change waypoint index to move between coins
    public void WaypointUpdate()
    {


        if (Vector2.Distance(transform.position, wayPoints[wayPointIndex].transform.position) < minGoalDistance)
        {
            wayPointIndex++;
            if (wayPointIndex >= wayPoints.Count)
            {
                wayPointIndex = 0;
            }
        }
    }
    public void WayPointsMovement()
    {
        AIMoveTowards(wayPoints[wayPointIndex].transform);
        WaypointUpdate();
    }

    //method to ai move towards goal
    public void AIMoveTowards(Transform goal)
    {

        if (Vector2.Distance(transform.position, goal.position) > minGoalDistance)
        {
            Vector2 directionToGoal = (goal.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }


    }

    //spawn new coins and add to waypoints
    public void newCoins()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);

        GameObject newPoint = Instantiate(coinPrefab, new Vector2(x, y), Quaternion.identity);
        wayPoints.Add(newPoint);
    }

    //move to closest waypoint
    public void findClosestWayPoint()
    {
        int nearestIndex = 0;
        float currentNearest = float.PositiveInfinity;

        if (wayPoints.Count <= 0)
        {
            //change game state to defensive
        }

        else
        {
            for (int i = 0; i < wayPoints.Count; i++)
            {
                float test = Vector2.Distance(transform.position, wayPoints[i].transform.position);
                if (currentNearest > test)
                {
                    nearestIndex = i;
                    currentNearest = test;
                }
            }
            wayPointIndex = nearestIndex;
        }

    }
    //remove coins and waypoint when player changes the tag
    void RemoveCollectedWayPoints()
    {
        foreach (var wayPoint in wayPoints)
        {
            if (wayPoint.gameObject.tag == "Finish")
            {
                //reduce index from one to avoid index to array error
                if (wayPointIndex != 0)
                {
                    wayPointIndex--;

                }
                //remove from index

                wayPoints.Remove(wayPoint);
                Destroy(wayPoint);

                //if only one remains
                if (wayPointIndex >= wayPoints.Count && wayPoints.Count != 0)
                {
                    wayPointIndex = 0;
                }
            }
        }
    }
    #endregion

    private void Awake()
    {
        //gets gameplay manager
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        //when player collides changes tag to finished and removes it from array and destory gameObject instance
        RemoveCollectedWayPoints();
    }

  




}
