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


  //Los GameObject que van a aparecer en pantalla
  public GameObject player1Skin;
  public GameObject player2Skin;

  public GameObject player3Skin;

  public GameObject player4Skin;

  //El int que va a dar la posicion en la lista donde esta el sprite seleccionado para cada jugador
  private int selectedSkin1 = 0;
  private int selectedSkin2 = 0;

  private int selectedSkin3 = 0;
  private int selectedSkin4 = 0;




  public void NextButton1()
  {
    selectedSkin1++;
    if (selectedSkin1 == skins1.Count)
    {
      selectedSkin1 = 0;
    }

    sr1.sprite = skins1[selectedSkin1];

  }
  public void BackButton1()
  {
    selectedSkin1 = selectedSkin1 - 1;
    if (selectedSkin1 == -1)
    {
      selectedSkin1 = skins1.Count - 1;
    }

    sr1.sprite = skins1[selectedSkin1];

  }

  public void NextButton2()
  {
    selectedSkin2++;
    if (selectedSkin2 == skins2.Count)
    {
      selectedSkin2 = 0;
    }

    sr2.sprite = skins2[selectedSkin2];

  }

  public void BackButton2()
  {
    selectedSkin2 = selectedSkin2 - 1;
    if (selectedSkin2 == -1)
    {
      selectedSkin2 = skins2.Count - 1;
    }

    sr2.sprite = skins2[selectedSkin2];

  }

  public void NextButton3()
  {
    selectedSkin3++;
    if (selectedSkin3 == skins3.Count)
    {
      selectedSkin3 = 0;
    }

    sr3.sprite = skins3[selectedSkin3];

  }

  public void BackButton3()
  {
    selectedSkin3 = selectedSkin3 - 1;
    if (selectedSkin3 == -1)
    {
      selectedSkin3 = skins3.Count - 1;
    }

    sr3.sprite = skins3[selectedSkin3];

  }



  public void NextButton4()
  {
    selectedSkin4++;
    if (selectedSkin4 == skins4.Count)
    {
      selectedSkin4 = 0;
    }

    sr4.sprite = skins4[selectedSkin4];

  }

  public void BackButton4()
  {
    selectedSkin4 = selectedSkin4 - 1;
    if (selectedSkin4 == -1)
    {
      selectedSkin4 = skins4.Count - 1;
    }

    sr4.sprite = skins4[selectedSkin4];

  }
  public void StartButton()
  {
    PrefabUtility.SaveAsPrefabAsset(player1Skin, "Assets/Prefabs/selectedSkin1.prefab");
    PrefabUtility.SaveAsPrefabAsset(player2Skin, "Assets/Prefabs/selectedSkin2.prefab");
    PrefabUtility.SaveAsPrefabAsset(player3Skin, "Assets/Prefabs/selectedSkin3.prefab");
    PrefabUtility.SaveAsPrefabAsset(player4Skin, "Assets/Prefabs/selectedSkin4.prefab");

    SceneManager.LoadScene("SampleScene");

    //Manager.player1Type = selectedSkin1 + 1;
  }
  void Start()
  {

    sr1.sprite = skins1[selectedSkin1];
    sr2.sprite = skins2[selectedSkin2];
    sr3.sprite = skins3[selectedSkin3];
    sr4.sprite = skins4[selectedSkin4];

    //playerSkin = skins[selectedSkin].SpriteGetComponent<SpriteRenderer>().sprite;

  }
}
