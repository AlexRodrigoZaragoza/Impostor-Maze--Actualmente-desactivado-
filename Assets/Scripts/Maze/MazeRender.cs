using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeRender : MonoBehaviour
{
    public Text seed_UI_Text;

    [Header("CONFIGURACI�N")]

    [SerializeField]
    [Range(1, 50)]
    private int width = 10;

    [SerializeField]
    [Range(1, 50)]
    private int height = 10;

    [SerializeField]
    private int seed = 9999;

    [SerializeField]
    private float size = 1f;

    //Partes del labertinto
    [Header("Components")]
    [SerializeField]
    private Transform wallPrefab = null;
    [SerializeField]
    private Transform floorPrefab = null;
    [SerializeField]
    private GameObject trapGO;
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject finish;

    [Header("Trampas")]
    [SerializeField]
    [Range(1, 100)]
    private int porcenTrampas;
    private Vector2 lastPosition;
    [SerializeField] Transform trapParent;

    public bool test;

    [Header("Player")]
    [SerializeField]
    private GameObject player;
    GameObject currentPlayer;
    private Vector3 startPlayerPos;

    [Header("Canvas")]
    [SerializeField] GameObject gameMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (test)
        {
            var maze = MazeGenerator.Generate(width, height, seed);
            seed_UI_Text.text = "Seed: " + seed;
            Draw(maze);
            currentPlayer = Instantiate(player, start.transform.position, Quaternion.identity);
        }
    }
    public void StartNewMaze()
    {
        ReInitMaze();
    }

    public int GenerateNewSeed()
    {
        return UnityEngine.Random.Range(1010,9090);
    }

    public void startDraw()
    {
        seed = ServiceLocator.Instance.GetService<IMazeInfo>().getSeed();
        var maze = MazeGenerator.Generate(width, height, seed);
        seed_UI_Text.text = "Seed: " + seed;
        Draw(maze);
        gameMenu.SetActive(true);
        for(int i = 0; i < gameMenu.transform.childCount -1; ++i){
            gameMenu.transform.GetChild(i).gameObject.SetActive(true);
        }
        currentPlayer = Instantiate(player, startPlayerPos, Quaternion.identity);
        currentPlayer.transform.position = getStartPosition();
    }

    public void Draw(WallState[,] maze)
    {
        var rng = new System.Random(seed);
        Debug.Log("SEED: " + seed);
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width / 2, 1, height / 2);

        GameObject newStart = Instantiate(start, transform);
        newStart.transform.position = new Vector3(-width / 2, 0, -height / 2);
        newStart.name = "Start";
        startPlayerPos = newStart.transform.position;

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {

                int probTrampas = rng.Next(0, 100);
                lastPosition = new Vector2(i, j);

                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }

                if (porcenTrampas >= probTrampas)
                {
                    
                    GameObject newTrap = Instantiate(trapGO, trapParent);
                    newTrap.transform.position = new Vector3(-width / 2 + i, 0, -height / 2 + j);
                    int trapType = rng.Next(0, 4);
                    newTrap.GetComponent<Trap>().setTipo(trapType);
                }

            }
        }
        GameObject newFinish = Instantiate(finish, transform);
        newFinish.transform.position = new Vector3(-width / 2 + lastPosition.x, 0, -height / 2 + lastPosition.y);
        newFinish.name = "Finish";
    }

    // Update is called once per frame
    void Update()
    {
        if (ServiceLocator.Instance.GetService<Common.Installer>()._getMazeIniciated && !test)
        {
            ServiceLocator.Instance.GetService<Common.Installer>()._getMazeIniciated = false;

            Camera.main.gameObject.SetActive(false);

            startDraw();
        }
    }
    public void ReInitMaze()
    {
        //Destruimos muros
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name != "Traps" && transform.GetChild(i).gameObject.name != "InvisibleRoof" && transform.GetChild(i).gameObject.name != "Messages")
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        //Destruimos trampas
        for (int i = 0; i < trapParent.transform.childCount; ++i)
        {
            Destroy(trapParent.transform.GetChild(i).gameObject);
        }
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        startDraw();
    }

    public Vector3 getStartPosition()
    {
        return startPlayerPos + new Vector3(0,0.25f, 0);
    }

    public Transform getPlayerTransform()
    {
        return player.transform;
    }

    public int getSeed()
    {
        return seed;
    }

    public void setSeed(int seed)
    {
        this.seed = seed;
    }

}
