using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    private int score = 0;

    private int currentLevel;

    public TextMeshProUGUI currentLevelText;

    public TextMeshProUGUI nextLevelText;

    public Slider gameProgressSlider;

    public static CanvasManager Instance { get; set; }

    public int Score { get => score; set => score = value; }

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private GameManager gameManager;

    [SerializeField] private Text scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        //  currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        currentLevelText.text = CurrentLevel.ToString();

        nextLevelText.text = (CurrentLevel + 1).ToString();
    }

    void Start()
    {
        gameManager = GameManager.Instance;
    }


    void Update()
    {
        int progress = gameManager.NumOfPassedRings * 100 / FindObjectOfType<GameManager>().NumberOfRings;
        scoreText.text = Score.ToString();
        gameProgressSlider.value = progress;

    }
}
