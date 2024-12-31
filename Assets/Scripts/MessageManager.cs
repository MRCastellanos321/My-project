
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
        private static TextMeshProUGUI[] changeTurnTexts;


        public TextMeshProUGUI validAtackText1;
        public TextMeshProUGUI validAtackText2;
        public TextMeshProUGUI validAtackText3;
        public TextMeshProUGUI validAtackText4;

        private static TextMeshProUGUI[] validAttacks;

        void Start()
        {


            // textObject[0] = GameObject.Find("Text1").GetComponent<TextMeshProUGUI>();
            // textObject[1] = GameObject.Find("Text2").GetComponent<TextMeshProUGUI>();
            // textObject[2] = GameObject.Find("Text3").GetComponent<TextMeshProUGUI>();
            // textObject[3] = GameObject.Find("Text4").GetComponent<TextMeshProUGUI>();

            changeTurnTexts = new TextMeshProUGUI[4];
            changeTurnTexts[0] = text1;
            changeTurnTexts[1] = text2;
            changeTurnTexts[2] = text3;
            changeTurnTexts[3] = text4;




            MessageShowing(false, 1);
            MessageShowing(false, 2);
            MessageShowing(false, 3);
            MessageShowing(false, 4);

            validAttacks = new TextMeshProUGUI[4];

            validAttacks[0] = validAtackText1;
            validAttacks[1] = validAtackText2;
            validAttacks[2] = validAtackText3;
            validAttacks[3] = validAtackText4;

        }

        public static void MessageShowing(bool show, int currentPlayerIndex)

        {
            for (int i = 0; i < changeTurnTexts.Length; i++)
            {
                changeTurnTexts[i].gameObject.SetActive(show);
            }
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


        public static void ChangeMessage(string message, int currentPlayerIndex)
        {
            MessageShowing(true, currentPlayerIndex);

            if (currentPlayerIndex == 1)
            {
                changeTurnTexts[0].text = message;
            }
            else if (currentPlayerIndex == 2)
            {
                changeTurnTexts[1].text = message;
            }
            else if (currentPlayerIndex == 3)
            {
                changeTurnTexts[2].text = message;
            }
            else if (currentPlayerIndex == 4)
            {
                changeTurnTexts[3].text = message;
            }

        }

        public static void AttackMessage(bool show, string message)
        {
            validAttacks[Manager.currentPlayerIndex - 1].text = message;
            validAttacks[Manager.currentPlayerIndex - 1].gameObject.SetActive(show);
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