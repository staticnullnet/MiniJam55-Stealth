using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{

    public enum GuardType
    {
        Idle,
        Turn,
        Patrol
    }

    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {

        [SerializeField] GuardType guardType;

        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        
        
        Transform player; // player reference
        Transform target; // target to aim for
        [SerializeField] Transform eyePosition; // where to look from

        float patrolTimer = 0f;
        [SerializeField] GameObject alertIndicator;
        [SerializeField] float enemyFOV = 60;
        [SerializeField] float maxDistance = 10f;
        [SerializeField] float rotationSpeed = 1000f;
        [SerializeField] float minDistance = 0.5f;
        
        [SerializeField] float moveDelay = 5f;
        [SerializeField] private int wayPointIndex = 0;
        private bool isMoving = false;
        public Transform[] wayPoints;
        float angleToPlayer;


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            player = GameObject.FindGameObjectWithTag("Player").transform;

            agent.updateRotation = false;
	        agent.updatePosition = true;
                        
        }


        private void Update()
        {
            IsPlayerVisible();

            switch (guardType)
            {
                case GuardType.Idle:
                    break;                
                case GuardType.Turn:
                    if (!isMoving)
                        TurnToNextPoint();
                    break;
                case GuardType.Patrol:
                        Patrol();
                    break;
            }
            
            
            //pre baked chase-related commands in AI controller.
            //if (target != null)
            //    agent.SetDestination(target.position);

            //if (agent.remainingDistance > agent.stoppingDistance)
            //    character.Move(agent.desiredVelocity, false, false);
            //else
            //    character.Move(Vector3.zero, false, false);
        }
   
        private void Patrol()
        { 
            // Returns if no points have been set up
            if (wayPoints.Length == 0)
                return;

            //Debug.Log(agent.remainingDistance + " remainingDistance | stoppingDistance " + agent.stoppingDistance);
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                patrolTimer += Time.deltaTime;

                // If the timer exceeds the wait time...
                if (patrolTimer >= moveDelay)
                {
                    // ... increment the wayPointIndex.
                    if (wayPointIndex == wayPoints.Length - 1)
                        wayPointIndex = 0;
                    else
                        wayPointIndex++;


                    Debug.Log("Current waypoint index:" + wayPointIndex);
                    // Reset the timer.
                    patrolTimer = 0;
                }
            }
            else
            {
                if (agent.remainingDistance > agent.stoppingDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    character.Move(Vector3.zero, false, false);

                patrolTimer = 0;
            }
            
            agent.destination = wayPoints[wayPointIndex].position;             
        }


        IEnumerator WaitThenMoveToNextWaypoint()
        {
            isMoving = false;
            yield return new WaitForSeconds(moveDelay);
            wayPointIndex = (wayPointIndex + 1) % wayPoints.Length;
            agent.SetDestination(wayPoints[wayPointIndex].transform.position);
            yield break;
        }

        private bool IsPlayerVisible()
            {
                bool isPlayerVisible = false;
                angleToPlayer = AngleToPlayer();
                bool isInsideEnemyFOV = angleToPlayer <= enemyFOV;            

                if (isInsideEnemyFOV && IsVisionToPlayerUnobstructed())
                {
                    alertIndicator.SetActive(true);
                    isPlayerVisible = true;
                }
                else
                {
                    alertIndicator.SetActive(false);
                }

                return isPlayerVisible;
            }

        private bool IsVisionToPlayerUnobstructed()
        {
            Ray rayToPlayer = new Ray(eyePosition.transform.position, player.GetComponent<CapsuleCollider>().bounds.center - eyePosition.transform.position);
            LayerMask playerLayerMask = LayerMask.NameToLayer("Player");
            RaycastHit hit;
            bool result = false;

            Debug.DrawRay(eyePosition.transform.position, player.GetComponent<CapsuleCollider>().bounds.center - eyePosition.transform.position);

            result = Physics.Raycast(rayToPlayer, out hit, maxDistance);

            if (result)
            {
                // Debug.Log(hit.transform.name + " " + hit.collider.tag);

                if (hit.collider.tag != "Player")
                {
                    result = false;
                }
            }           

            return result;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

    
        private float AngleToPlayer()
        {
            Vector3 directionToPlayer = (player.transform.position - this.transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            return angle;
        }
        

        IEnumerator TurnToPoint(Quaternion finalRotation, float duration)
        {           
            isMoving = true;            
            float t = 0;          
                        
            while (t < 1)
            {
                t += Time.smoothDeltaTime / duration;

                transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, t);

                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(to), Time.deltaTime * t);

                if (Quaternion.Dot(transform.rotation, finalRotation) > 0.9999f)
                {
                    yield return new WaitForSeconds(moveDelay);

                    //Debug.Log("loop finished");

                    // Make sure we got there
                    //transform.rotation = finalRotation;
                    isMoving = false;
                    wayPointIndex = (wayPointIndex + 1) % wayPoints.Length;
                    
                    yield break;
                }
                yield return null;
            }
        }


        void TurnToNextPoint()
        {
            // Returns if no points have been set up
            if (wayPoints.Length == 0)
                return;

            IEnumerator coroutine = TurnToPoint(Quaternion.LookRotation(wayPoints[wayPointIndex].position - transform.position), rotationSpeed);
            // Set the agent to turn to the currently selected destination.            

            StartCoroutine(coroutine);
        }
    }
}