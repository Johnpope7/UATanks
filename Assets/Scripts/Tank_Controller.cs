using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankSpace
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Tank_Inputs))]
    public class Tank_Controller : MonoBehaviour
    {
        #region Variables
        [Header("Movement Properties")]
        public float tankSpeed = 15f;
        public float tankRotationSpeed = 100f;

        [Header("Turret Properties")]
        public Transform turretTransform;
        public float turretLagSpeed = 7f;

        [Header("Reticle Properties")]
        public Transform reticleTransform;




        private Rigidbody trb; //stores the rigid bod required, presumably the tank
        private Tank_Inputs input; //stores the input for the tanks
        private Vector3 finalTurretFacing;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            trb = GetComponent<Rigidbody>(); //gets the rigid body from the object its attached to.
            input = GetComponent<Tank_Inputs>();//gets the Tank_Inputs script on the object its attached to.
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (trb && input) 
            {
                HandleMovement();
                HandleTurret();
                HandleReticle();
                
            }
        }
        #endregion

        #region Custom Methods
        protected virtual void HandleMovement() 
        {
            //moves the tank forward and backwards
            Vector3 desiredPosition = transform.position + (transform.forward * input.ForwardInput * tankSpeed * Time.deltaTime);
            trb.MovePosition(desiredPosition);

            //Rotates the tank
            Quaternion desiredRotation = transform.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * input.RotationInput * Time.deltaTime));
            trb.MoveRotation(desiredRotation);
        }

        protected virtual void HandleTurret() 
        {
            if (turretTransform) 
            {
                Vector3 turretFace = input.p_ReticlePosition - turretTransform.position;
                turretFace.y = 0f;
                finalTurretFacing = Vector3.Lerp(finalTurretFacing, turretFace, Time.deltaTime * turretLagSpeed);
                turretTransform.rotation = Quaternion.LookRotation(finalTurretFacing);
                
            }
        }

        protected virtual void HandleReticle() 
        {
            reticleTransform.position = input.p_ReticlePosition;
        }
        #endregion

    }
}