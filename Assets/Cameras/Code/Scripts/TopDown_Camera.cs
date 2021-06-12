using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSpace
{
    public class TopDown_Camera : MonoBehaviour
    {
        #region Variable
        public Transform m_Target; //stores the target of the camera
        public float m_Height = 30f;//this is the cameras height off the ground
        public float m_Distance = 35f; //this is the cameras distance from its target
        public float m_Angle = 35f; //this is the cameras angle
        #endregion

        #region BuiltIn Methods
        // Start is called before the first frame update
        void Start()
        {
            HandleCamera();
        }

        // Update is called once per frame
        void Update()
        {
            HandleCamera();
        }
        #endregion

        #region Custom Methods
        protected virtual void HandleCamera() //handles the positioning of the camera
        {
            if (!m_Target) //if there is no target, then nothing happens
            {
                return;
            }

            Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height); //creates the proper vector3 for the world Position of the camera
            Debug.DrawLine(m_Target.position, worldPosition, Color.red);

            //Builds our rotate vector
            Vector3 rotateVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
            Debug.DrawLine(m_Target.position, rotateVector, Color.green);

            //moves vector positions
            Vector3 flatTargetPosition = m_Target.position;
            flatTargetPosition.y = 0f; //flattens the y so the camera doesnt move with bumps and stuff in the map
            Vector3 finalPosition = flatTargetPosition + rotateVector; //creates the final position of the camera
            Debug.DrawLine(m_Target.position, finalPosition, Color.blue);

            transform.position = finalPosition; //sets final position equal to cameras position
            transform.LookAt(flatTargetPosition);
        }
        #endregion
    }
}
