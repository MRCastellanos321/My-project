using Tablero;
using UnityEngine;
using UnityEngine.UI;

public class MazeCanvas : MonoBehaviour
{
    public GameObject imagePrefab;
    public GameObject playerImage;
    private GameObject playerIcon;
    public Transform canvas;
    public Canvas mazeCanvas;

    void Start()
    {
        var laberinto = Laberinto.ElLaberinto;

        CreateMazeGuideCanvas(laberinto);
        playerIcon = Instantiate(playerImage, canvas);
        UpdatePlayerIcon();
    }
    void CreateMazeGuideCanvas(Laberinto laberinto)
    {
        float size = 3f;

        for (int i = 0; i < laberinto.GetSize(); i++)
        {
            for (int j = 0; j < laberinto.GetSize(); j++)
            {
                GameObject newImage = Instantiate(imagePrefab, canvas);
                RectTransform rt = newImage.GetComponent<RectTransform>();
                //+ 211 y + 232 funcionan para 2 pixeles
                // +161 y +231 funcionan para 3 pixeles
                rt.anchoredPosition = new Vector2(j * size + 161, -i * size + 231); // a medida que aumentan las filas de la matriz, la coordenada Y diminuye, por eso el -i
                rt.sizeDelta = new Vector2(size, size);
                Image img = newImage.GetComponent<Image>();
                img.color = laberinto.Read(i, j) == 2 ? Color.black : Color.white;
            }
        }
    }


    void UpdatePlayerIcon()
    {
        if (Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].GetPositionVisibility() != 0)
        {
            playerIcon.SetActive(true);
            float size = 3f;
            RectTransform prt = playerIcon.GetComponent<RectTransform>();
            prt.anchoredPosition = new Vector2(Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] * size + 161, -Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] * size + 231);
        }
        else
        {
            playerIcon.SetActive(false);
        }
    }

    public void Update()
    {
        if (Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].GetMazeVisibility() == 0)
        {
            mazeCanvas.gameObject.SetActive(true);
            UpdatePlayerIcon();
        }
        else
        {
            mazeCanvas.gameObject.SetActive(false);
        }
    }

}

