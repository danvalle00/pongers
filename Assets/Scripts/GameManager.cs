using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI aiScoreText;
    [Header("Audio")]
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioSource audioSource;
    private int playerScore;
    private int aiScore;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void AddPlayerScore(int amount = 1)
    {
        playerScore += amount;
        UpdateScoreUI();
    }

    public void AddAIScore(int amount = 1)
    {
        aiScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (playerScoreText != null) playerScoreText.text = playerScore.ToString();
        if (aiScoreText != null) aiScoreText.text = aiScore.ToString();
        audioSource.PlayOneShot(scoreSound);
    }
}
