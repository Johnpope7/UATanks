using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankSpace
{

    public class Tank_Inputs : MonoBehaviour
    {
        [Header("Input Properties")]
        #region Variables
        public Camera camera;
        #endregion

        #region Properties
        private Vector3 reticlePosition; //the location of the reticle
        public Vector3 p_ReticlePosition //gets the value of reticlePosition
        {
            get { return reticlePosition; }
        }

        private Vector3 reticleNormal; //normal for the reticle
        public Vector3 p_ReticleNormal //gets the reticle normal
        {
            get { return reticleNormal; }
        }

        private float forwardInput; //stores the forward input of the tank
        public float ForwardInput //gets the forward input of the tank
        {
            get { return forwardInput;  } 
        }

        private float rotationInput; //stores the rotational input of the tank
        public float RotationInput //gets the rotational input of the tank
        {
            get { return rotationInput; }
        }
        #endregion


        #region Builtin Methods
        // Update is called once per frame
        void Update()
        {
            if (camera) 
            {
                HandleInputs();
            }
        }
        #endregion

       private void OnDrawGizmos() 
        {

        }

        #region Custom Methods
        protected virtual void HandleInputs()
        {
            Ray screenRay = camera.ScreenPointToRay(Input.mousePosition); //sets the Ray screenRay equal to the position of the mouse on the screen
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit)) //this returns a variable and stores it into the hit Raycast if our Ray, screenRay is hitting something
            {
                reticlePosition = hit.point; //stores the worldspace position of the hit
                reticleNormal = hit.normal;
            }

            forwardInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");
        }
        #endregion
    }

}
