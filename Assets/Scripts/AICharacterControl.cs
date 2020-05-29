using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        public Transform player;                                    // player reference
        public float angleToPlayer;
        [SerializeField] GameObject alertIndicator;
        [SerializeField] float enemyFOV = 60;
        [SerializeField] float maxDistance = 10f;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            angleToPlayer = AngleToPlayer();
            if (angleToPlayer <= enemyFOV && ClearVisionCheck())
            {
                alertIndicator.SetActive(true);
            }
            else
            {
                alertIndicator.SetActive(false);
            }


            if (target != null)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else
                character.Move(Vector3.zero, false, false);
        }

        //private bool ClearVisionCheck()
        //{
        //    Ray rayToPlayer = new Ray(transform.position, player.transform.position - transform.position);
        //    LayerMask playerLayerMask = LayerMask.NameToLayer("Player");
        //    RaycastHit hit;
        //    bool result = false;

        //    Debug.DrawRay(transform.position, player.transform.position - transform.position);

        //    result = Physics.Raycast(rayToPlayer, out hit, maxDistance, playerLayerMask);

        //    if (result)
        //        Debug.Log(hit.transform.name);

        //    return result;
        //}

        private bool ClearVisionCheck()
        {
            Ray rayToPlayer = new Ray(transform.position, player.transform.position - transform.position);
            LayerMask playerLayerMask = LayerMask.NameToLayer("Player");
            RaycastHit hit;
            bool result = false;

            Debug.DrawRay(transform.position, player.transform.position - transform.position);

            result = Physics.Raycast(rayToPlayer, out hit, maxDistance);

            if (result)
            {
                Debug.Log(hit.transform.name + " " + hit.collider.tag);

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

        /// <summary>
        /// Calculate direction between player and NPC
        /// Get forward vector of enemy
        /// Calculate angle (V3.angle)
        /// Check angle w/ enemy FOV.
        /// Ray cast to check walls
        /// </summary>
        private float AngleToPlayer()
        {
            Vector3 directionToPlayer = (player.transform.position - this.transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            return angle;
        }
    }
}
