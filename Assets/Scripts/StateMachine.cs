using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{
    #region variables
    //
    public State currentState;
    public AiMovement aiMovement;
    public Text stateText;

    #endregion
    #region states
    public enum State
    {
        //the 3 states the AI has
        Attack,
        Defence,
        Patrol
    }
    //changing between states
    private void NextState()
    {
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(DefenceState());
                break;
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
        }
    }

    //attack state
    private IEnumerator AttackState()
    {
        //display ai state in ui
        Debug.Log("Attack: Enter");
        stateText.text = "AI is in Attack State";
        while (currentState == State.Attack)
        {
            //moves towards player
            aiMovement.AIMoveTowards(aiMovement.player);

            //change state to patrol when not chasing player
            if (!aiMovement.NoticePlayer())
            {
                currentState = State.Patrol;
            }
            //change to defence when no waypoints/coins
            if (aiMovement.wayPoints.Count < 1)
            {
                currentState = State.Defence;
            }
            yield return null;
        }

        Debug.Log("Attack: Exit");
        NextState();
    }

    private IEnumerator DefenceState()
    {
        //display AI state in UI
        stateText.text = "AI is in Defence State";

        Debug.Log("Defence: Enter");
        aiMovement.wayPoints = new List<GameObject>();
        aiMovement.findClosestWayPoint();
        //state is defense  wait and spawn new coins/waypoints
        while (currentState == State.Defence)
        {
            yield return new WaitForSecondsRealtime(3);
            aiMovement.newCoins();
            aiMovement.newCoins();
            aiMovement.newCoins();
            currentState = State.Patrol;
            yield return null;

        }
        //once spawned change state
        Debug.Log("Defence: Exit");
        NextState();
    }

    private IEnumerator PatrolState()
    {
        //display AI state in UI

        stateText.text = "AI is in Patrol State";

        Debug.Log("Patrolling: Enter");
        aiMovement.findClosestWayPoint();
        while (currentState == State.Patrol)
        {
            if (aiMovement.wayPoints.Count < 1)
            {
                //when no coins/waypoints change state to defense
                currentState = State.Defence;
            }
            else
            {
                //change to attack state when player is noticed by AI
                aiMovement.WayPointsMovement();
                if (aiMovement.NoticePlayer())
                {
                    currentState = State.Attack;
                }

                yield return null;
            }


        }
        Debug.Log("Patrolling: Exit");
        NextState();
    }
    #endregion


    private void Start()
    {
        aiMovement = GetComponent<AiMovement>();
        NextState();
    }


}
