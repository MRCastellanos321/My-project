using Tablero;
using UnityEngine;
using UnityEngine.UI;

public class MazeCanvas : MonoBehaviour
{
    public GameObject imagePrefab;

    public GameObject playerImage;

    public Transform canvas;
    private GameObject playerIcon;

    void Start()
    {
        var laberinto = Laberinto.ElLaberinto;

        CreateMazeGuideCanvas(laberinto);
        playerIcon = Instantiate(playerImage, canvas);
        RectTransform prt = playerIcon.GetComponent<RectTransform>();
        prt.sizeDelta = new Vector2(2f, 2f);
        UpdatePlayerIcon();

    }
    void CreateMazeGuideCanvas(Laberinto laberinto)
    {
        float size = 2f;

        for (int i = 0; i < laberinto.GetSize(); i++)
        {
            for (int j = 0; j < laberinto.GetSize(); j++)
            {
                GameObject newImage = Instantiate(imagePrefab, canvas);
                RectTransform rt = newImage.GetComponent<RectTransform>();

                // +161 y =231 funcionan para 3 pixeles
                rt.anchoredPosition = new Vector2(j * size + 211, -i * size + 232); // a medida que aumentan las filas de la matriz, la coordenada Y diminuye, por eso el -i
                rt.sizeDelta = new Vector2(size, size);
                Image img = newImage.GetComponent<Image>();
                img.color = laberinto.Leer(i, j) == 2 ? Color.black : Color.white;
            }
        }
    }


    void UpdatePlayerIcon()
    {
        float size = 2f;
        RectTransform prt = playerIcon.GetComponent<RectTransform>();
        prt.anchoredPosition = new Vector2(Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] * size + 211, -Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] * size + 232);
    }

    public void Update()
    {
        if (playerIcon != null)
        {
            UpdatePlayerIcon();
        }
    
    }

}

