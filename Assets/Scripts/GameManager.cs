using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject finishPlatform;

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private GameObject levelCompletedScreen;

    [SerializeField] private float spawnY = 0;

    [SerializeField] private float ringDistance;

    [SerializeField] private GameObject[] helixRings;

    private LevelScriptable[] scriptable;

    private CanvasManager canvasManager;

    private bool gameOver;

    private bool levelCompleted;

    private int numOfPassedRings;

    private int numberOfRings;

    public GameObject gameManager;

    public static GameManager Instance { get; private set; }

    public bool GameOver { get => gameOver; set => gameOver = value; }

    public bool LevelCompleted { get => levelCompleted; set => levelCompleted = value; }

    public int NumOfPassedRings { get => numOfPassedRings; set => numOfPassedRings = value; }

    public int NumberOfRings { get => numberOfRings; set => numberOfRings = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        canvasManager = CanvasManager.Instance;

        gameManager = this.gameObject;

        NumOfPassedRings = 0;

        // NumberOfRings = canvasManager.CurrentLevel *5;

        Time.timeScale = 1;

        canvasManager.Score = 0;

        scriptable = Resources.LoadAll<LevelScriptable>("Scriptables");

        ScriptableLevels();

        for (int i = 0; i < NumberOfRings; i++)
        {

            RingSpawner(0);
        }
        //SpawnRing(helixRings.Length-1);
        GameObject finish = Instantiate(finishPlatform, new Vector3(0, spawnY - 2f, 0), Quaternion.identity);

        GameOver = false;

        LevelCompleted = false;
    }

    void Update()
    {
        if (GameOver == true)
        {
            /*
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("Level");
            }
            */
            SceneManager.LoadScene(StringVariables.SCENE);
        }
        if (LevelCompleted == true)
        {
            PlayerPrefs.SetInt("CurrentLevel", canvasManager.CurrentLevel + 1);
            SceneManager.LoadScene(StringVariables.SCENE);
            /* Time.timeScale = 0;
             levelCompletedScreen.SetActive(true);
            if (Input.anyKeyDown)
            {
                PlayerPrefs.SetInt("CurrentLevel", CanvasManager.currentLevel + 1);
                SceneManager.LoadScene("Level");

            }*/

        }
    }

    public void RingSpawner(int ringIndex)
    {

        GameObject go = Instantiate(helixRings[ringIndex], transform.up * spawnY, Quaternion.identity);

        go.transform.parent = transform;

        spawnY -= ringDistance;

    }

    public void ScriptableLevels()
    {
        int index = canvasManager.CurrentLevel - 1;

        if (index >= 9)
        {
            index = 8;
        }

        NumberOfRings = scriptable[index].ringNumbers;
        
        Debug.Log(scriptable[index].name);
    }



}
