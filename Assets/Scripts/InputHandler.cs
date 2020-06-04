using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    
    public class InputHandler : MonoBehaviour
    {
        public bool freezeMovement = false;
        float horizontal;
        float vertical;

        bool isInit;

        float delta;

        public StatesManager states;
        public Transform camHolder;

        void Start()
        {
            InitInGame();
        }

        public void InitInGame()
        {
            states.Init();
            isInit = true;
        }

        void FixedUpdate()
        {
            if (!isInit)
                return;

            delta = Time.fixedDeltaTime;
            GetInput_FixedUpdate();
            InGame_UpdateStates_FixedUpdate();
            states.FixedTick(delta);
        }

        void GetInput_FixedUpdate()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }

        void GetInput_Update()
        {
            if (Input.GetButtonDown("Crouch"))
            {
                states.Crouch();
            }

            if (Input.GetButtonDown("Action")) {
                //currently supporting ONE collectable not multiples
                GameObject collectable = GameObject.FindWithTag("Collectable");
                if (Vector3.Distance(transform.position, collectable.transform.position) < 2f)
                {
                    collectable.GetComponent<Collectable>().Collect();
                }
            }
        }

        void InGame_UpdateStates_FixedUpdate()
        {
            if (!freezeMovement)
            {
                states.inp.horizontal = horizontal;
                states.inp.vertical = vertical;
                states.inp.moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

                Vector3 moveDir = camHolder.forward * vertical;
                moveDir += camHolder.right * horizontal;
                moveDir.Normalize();
                states.inp.moveDirection = moveDir;
            }
            else
            {

            }
        }


        void Update()
        {
            if (!isInit)
                return;

            delta = Time.deltaTime;
            if (!freezeMovement)
            {
                GetInput_Update();

                states.Tick(delta);
            }
        }


    }

    public enum GamePhase
    {
        inGame,inMenu
    }
}
