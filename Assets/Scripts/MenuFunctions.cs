using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using Tablero;

public class MenuFunctions : MonoBehaviour
{
  //public GameObject selectedType;
  // public GameObject player1;

  //private Sprite player1sprite;

  //el "component"que hace el sprite del objeto visible
  public SpriteRenderer sr1;
  public SpriteRenderer sr2;
  public SpriteRenderer sr3;
  public SpriteRenderer sr4;

  //Lista de todos los posibles sprite
  public List<Sprite> skins1 = new List<Sprite>();
  public List<Sprite> skins2 = new List<Sprite>();

  public List<Sprite> skins3 = new List<Sprite>();

  public List<Sprite> skins4 = new List<Sprite>();

  //Lista de todos los posibles scripts segun el type
  // public List<MonoBehaviour> scripts = new List<MonoBehaviour>();
  //public GameObject scriptsStorage;
  // static MonoBehaviour[] scripts;


  //Los GameObject que van a aparecer en pantalla
  public GameObject player1Type;
  public GameObject player2Type;

  public GameObject player3Type;

  public GameObject player4Type;

  //El int que va a dar la posicion en la lista donde esta el sprite seleccionado para cada jugador
  public static int selectedType1;
  public static int selectedType2;
  public static int selectedType3;
  public static int selectedType4;



  public void NextButton1()
  {
    selectedType1++;
    if (selectedType1 == skins1.Count)
    {
      selectedType1 = 0;
    }

    sr1.sprite = skins1[selectedType1];

  }
  public void BackButton1()
  {
    selectedType1 = selectedType1 - 1;
    if (selectedType1 == -1)
    {
      selectedType1 = skins1.Count - 1;
    }

    sr1.sprite = skins1[selectedType1];

  }

  public void NextButton2()
  {
    selectedType2++;
    if (selectedType2 == skins2.Count)
    {
      selectedType2 = 0;
    }

    sr2.sprite = skins2[selectedType2];

  }

  public void BackButton2()
  {
    selectedType2 = selectedType2 - 1;
    if (selectedType2 == -1)
    {
      selectedType2 = skins2.Count - 1;
    }

    sr2.sprite = skins2[selectedType2];

  }

  public void NextButton3()
  {
    selectedType3++;
    if (selectedType3 == skins3.Count)
    {
      selectedType3 = 0;
    }

    sr3.sprite = skins3[selectedType3];

  }

  public void BackButton3()
  {
    selectedType3 = selectedType3 - 1;
    if (selectedType3 == -1)
    {
      selectedType3 = skins3.Count - 1;
    }

    sr3.sprite = skins3[selectedType3];


  }



  public void NextButton4()
  {
    selectedType4++;
    if (selectedType4 == skins4.Count)
    {
      selectedType4 = 0;
    }

    sr4.sprite = skins4[selectedType4];

  }

  public void BackButton4()
  {
    selectedType4 = selectedType4 - 1;
    if (selectedType4 == -1)
    {
      selectedType4 = skins4.Count - 1;
    }

    sr4.sprite = skins4[selectedType4];

  }

  public void StartButton()
  {

    PrefabUtility.SaveAsPrefabAsset(player1Type, "Assets/Prefabs/selectedSkin1.prefab");
    PrefabUtility.SaveAsPrefabAsset(player2Type, "Assets/Prefabs/selectedSkin2.prefab");
    PrefabUtility.SaveAsPrefabAsset(player3Type, "Assets/Prefabs/selectedSkin3.prefab");
    PrefabUtility.SaveAsPrefabAsset(player4Type, "Assets/Prefabs/selectedSkin4.prefab");

    SceneManager.LoadScene("SampleScene");

    /*if (selectedType1 == 0)
    {
      player1Type.AddComponent<Type1>();
    }

    else if (selectedType1 == 1)
    {
      player1Type.AddComponent<Type2>();
    }

    if (selectedType2 == 0)
    {
      player2Type.AddComponent<Type1>();
    }

    else if (selectedType2 == 1)
    {
      player2Type.AddComponent<Type2>();
    }
*/




    //Manager.player1Type = selectedType1 + 1;
  }
  void Start()
  {
    selectedType1 = 0;
    selectedType2 = 0;
    selectedType3 = 0;
    selectedType4 = 0;


    sr1.sprite = skins1[selectedType1];
    sr2.sprite = skins2[selectedType2];
    sr3.sprite = skins3[selectedType3];
    sr4.sprite = skins4[selectedType4];

    /*
        //esto es para que si ejecuto el juego mas de dos veces, no se guarde mas de un script en un prefab
        RemoveComponent<MonoBehaviour>(player1Type);
        RemoveComponent<MonoBehaviour>(player2Type);
        RemoveComponent<MonoBehaviour>(player3Type);
        RemoveComponent<MonoBehaviour>(player4Type);
    */

  }

  void RemoveComponent<Script>(GameObject player)
  where Script : Component
  {
    Script component = player.GetComponent<Script>();
    if (component != null)
    {
      Destroy(component);
    }

  }




}
