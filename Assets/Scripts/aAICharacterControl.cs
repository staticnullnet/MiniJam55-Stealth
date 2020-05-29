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

            if (angleToPlayer <= enemyFOV)
            {
                alertIndicator.SetActive(true);
            }
            else
            {
                alertIndicator.SetActive(false);
            }

            if (target != null)
                agent.SetDestination(target.position);

            
            //if (agent.remainingDistance > agent.stoppingDistance)
            //    character.Move(agent.desiredVelocity, false, false);
            //else
            //    character.Move(Vector3.zero, false, false);
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

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
