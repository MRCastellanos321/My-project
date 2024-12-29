
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
namespace Tablero
{
    public class MessageManager : MonoBehaviour
    {
        //Start is called once before the first execution of Update after the MonoBehaviour is created

        //static TextMeshProUGUI[] textObject;
        public TextMeshProUGUI text1;
        public TextMeshProUGUI text2;
        public TextMeshProUGUI text3;

        public TextMeshProUGUI text4;

        void Start()
        {


            // textObject[0] = GameObject.Find("Text1").GetComponent<TextMeshProUGUI>();
            // textObject[1] = GameObject.Find("Text2").GetComponent<TextMeshProUGUI>();
            // textObject[2] = GameObject.Find("Text3").GetComponent<TextMeshProUGUI>();
            // textObject[3] = GameObject.Find("Text4").GetComponent<TextMeshProUGUI>();


            MessageShowing(false, 1);
            MessageShowing(false, 2);
            MessageShowing(false, 3);
            MessageShowing(false, 4);
        }

        public void MessageShowing(bool show, int currentPlayerIndex)
        {
            text1.gameObject.SetActive(show);


            text2.gameObject.SetActive(show);


            text3.gameObject.SetActive(show);


            text4.gameObject.SetActive(show);
            /* if (currentPlayerIndex == 1)
             {
                 text1.gameObject.SetActive(show);
             }
             else if (currentPlayerIndex == 2)
             {
                 text2.gameObject.SetActive(show);
             }
             else if (currentPlayerIndex == 3)
             {
                 text3.gameObject.SetActive(show);
             }
             else if (currentPlayerIndex == 4)
             {
                 text4.gameObject.SetActive(show);
             }*/
            // textObject[currentPlayerMessage].gameObject.SetActive(show);
        }


        public void ChangeMessage(string message, int currentPlayerIndex)
        {
            MessageShowing(true, currentPlayerIndex);

            if (currentPlayerIndex == 1)
            {
                text1.text = message;
            }
            else if (currentPlayerIndex == 2)
            {
                text2.text = message;
            }
            else if (currentPlayerIndex == 3)
            {
                text3.text = message;
            }
            else if (currentPlayerIndex == 4)
            {
                text4.text = message;
            }

        }


        // Update is called once per frame
        void Update()
        {
            if (Manager.diceNumber == 0)
            {
                ChangeMessage("Presiona espacio para pasar el turno", Manager.currentPlayerIndex);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MessageShowing(false, Manager.currentPlayerIndex);
            }

        }
    }
}