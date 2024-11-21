using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{

    public class PlayerMovement : MonoBehaviour
    {

        /* void Start()
         {
             transform.position = new Vector3(1, 1, 0);
         }

         float posCellX = 0;
         float posCellY = 0;

         void Update()
         {
             float directionX = Input.GetAxis("Horizontal");
             if (directionX == 0)
             {



             }
             float directionY = Input.GetAxis("Vertical");
             posCellX += directionX;
             posCellY += directionY;
             transform.position = new Vector3(posCellX * 32, posCellY * 32, 0);*/


        public int cellSize = 1; 
        private Vector3 targetPosition;

        public Vector3 lastPosition;

        void Start()
        {

            Vector3 temp = transform.position;
            transform.position = new Vector3(temp.x , temp.y, temp.z);

            
            targetPosition = transform.position;

        }

        void Update()
        {

            lastPosition = transform.position;



            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector3.up);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector3.down);

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector3.right);
            }
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
           



           



            void Move(Vector3 direction)
            {
                
                targetPosition += direction * cellSize;
            }
           
        }
    }
}


