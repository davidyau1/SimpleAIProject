using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    #region variables

    // Start is called before the first frame update
    public Transform player;
    public float speed = 1.5f;
    public float noticeWaypoint = 0;
    public float minGoalDistance = 0.05f;
    public float chaseDistanace = 3f;
    public int wayPointIndex = 0;
    public List<GameObject> wayPoints;
    public GameObject coinPrefab;
    #endregion
    #region methods
    public bool NoticePlayer()
    {
        return Vector2.Distance(transform.position, player.position) < chaseDistanace;

    }
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
    public void AIMoveTowards(Transform goal)
    {

        if (Vector2.Distance(transform.position, goal.position) > minGoalDistance)
        {
            Vector2 directionToGoal = (goal.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }


    }
    public void WayPointsMovement()
    {
        AIMoveTowards(wayPoints[wayPointIndex].transform);
        WaypointUpdate();
    }
    public void newCoins()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);

        GameObject newPoint = Instantiate(coinPrefab, new Vector2(x, y), Quaternion.identity);
        wayPoints.Add(newPoint);
    }

    public void findClosestWayPoint()
    {
        int nearestIndex = 0;
        float currentNearest = float.PositiveInfinity;
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
    #endregion
    private void Start()
    {
        newCoins();
        newCoins();
        newCoins();


    }


  
}
