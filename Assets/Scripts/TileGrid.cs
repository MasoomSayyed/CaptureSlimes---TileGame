using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileGrid : MonoBehaviour
{
    public event EventHandler OnCaptured;
    public event EventHandler OnIncorrect;

    public static TileGrid Instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI logText;

    [SerializeField] private int width = 6;
    [SerializeField] private int height = 6;
    public float cellSize = 1f;
    private int Score = 0;

    private Tiles tile1 = null, tile2 = null;

    public GameObject[] tile;

    private GameObject[,] gridArray;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // Initialize the grid array
        gridArray = new GameObject[width, height];

        // Populate the grid array with the tile prefab
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Instantiate the tile prefab at the correct position
                GameObject tileInstance = Instantiate(GetRandomTile(), new Vector3(x * cellSize, y * cellSize, 0), Quaternion.identity);
                gridArray[x, y] = tileInstance;
            }
        }
    }

    private GameObject GetRandomTile()
    {
        return tile[UnityEngine.Random.Range(0, tile.Length)];
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void RemoveTiles()
    {

        // Calculate the tile positions relative to the grid's origin
        int x1 = (int)((tile1.transform.position.x - transform.position.x) / cellSize);
        int y1 = (int)((tile1.transform.position.y - transform.position.y) / cellSize);
        int x2 = (int)((tile2.transform.position.x - transform.position.x) / cellSize);
        int y2 = (int)((tile2.transform.position.y - transform.position.y) / cellSize);

        // Remove the tiles at the specified positions
        Destroy(gridArray[x1, y1]);
        Destroy(gridArray[x2, y2]);

        // Update the grid array to reflect the removed tiles
        gridArray[x1, y1] = null;
        gridArray[x2, y2] = null;

        // Nul the tile store
        tile1 = null;
        tile2 = null;

    }

    public void StoreRefForTiles(Tiles selectedTile)
    {
        if (tile1 == null)
        {
            tile1 = selectedTile;
        }
        else
        {
            tile2 = selectedTile;
        }

    }

    public void CheckSelectedTiles()
    {
        Debug.Log(tile1 + " " + tile2);
        isMatching();
    }

    public void isMatching()
    {
        if (tile2 != null)
        {
            if (tile1.tag == tile2.tag)
            {
                OnCaptured?.Invoke(this, EventArgs.Empty);
                Score++;
                scoreText.text = "Score :" + " " + Score.ToString();
                RemoveTiles();
                logText.text = "Log:" + "Captured !!!";
            }
            else
            {
                Debug.Log("Not matching");
                logText.text = "Log:" + "Not matching";
                OnIncorrect?.Invoke(this, EventArgs.Empty);
                //nul the tile store
                tile1 = null;
                tile2 = null;
            }
        }
        else
        {
            Debug.Log("select two slimes one after another then capture !!");
            logText.text = "Log:" + "select two slimes one after another then capture !!";
            OnIncorrect?.Invoke(this, EventArgs.Empty);
        }
    }
}
