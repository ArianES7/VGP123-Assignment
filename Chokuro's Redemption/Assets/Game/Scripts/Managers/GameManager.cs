
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singelton<GameManager>
{
    // === ADJUST THESE TO MATCH YOUR BUILD SETTINGS ===
    private const int TitleSceneBuildIndex = 0;
    private const int MainSceneBuildIndex  = 1;
    private const int CreditsSceneBuildIndex = 2;
    // ==================================================

    public int Score { get; private set; }
    public int Gems { get; private set; }
    public int Lives { get;  set; }



    // Fired whenever score/coins/lives change
    public event Action<int,int,int> OnHUDChanged;
    // Fired when the player runs out of lives
    public event Action OnGameOver;

    // Reference to the Game Over UI Canvas in the Main (game) scene
    private GameObject _gameOverCanvas;
    // Tracks whether we are currently in “Game Over” state
    private bool _isGameOver = false;

    public Transform gameObjectTransform;
    protected override void Awake()
    {
        base.Awake();

        // Initialize stats at the very start
        Lives = 3;
        Score = 0;
        Gems = 0;

        // Listen for scene loads so we can grab the GameOverCanvas reference
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Subscribe internal handler so that when TriggerGameOver() fires, we show UI
        OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        OnGameOver -= HandleGameOver;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When MainScene (by build index) is loaded, find the GameOverCanvas and deactivate it
        if (scene.buildIndex == MainSceneBuildIndex)
        {
            _gameOverCanvas = GameObject.Find("GameOver_Panel");
            if (_gameOverCanvas != null)
            {
                _gameOverCanvas.SetActive(false);
            }

            // Reset the Game Over flag so ESC won't fire immediately
            _isGameOver = false;
        }
    }

    private void Update()
    {
        // If we are in Game Over, pressing ESC returns to Title scene
        if (_isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(TitleSceneBuildIndex);
            _isGameOver = false;
        }
    }

    #region PUBLIC METHODS FOR GAMEPLAY

    public void AddScore(int points)
    {
        Score += points;
        OnHUDChanged?.Invoke(Score, Gems, Lives);
    }

    public void addGem(int gem) 
    { 
    
        Gems += gem;
        OnHUDChanged?.Invoke(Score, Gems, Lives);

    }
    public void addLives(int life)
    {

        if(Lives >= 3)
        {
            return;
        }

        Lives += life;
        OnHUDChanged?.Invoke(Score, Gems, Lives);

    }

    public void PlayerDamaged()
    {
      Lives--;
      if (Lives <= 0)
      {
            TriggerGameOver();
      }
      else
      {
            
            OnHUDChanged?.Invoke(Score, Gems, Lives);
      }

    }

    // Called by PlayerController.OnDeathAnimationComplete()
    public void TriggerGameOver()
    {
        OnGameOver?.Invoke();
        Destroy(gameObjectTransform.gameObject); // Destroy the GameManager object
        
    }

    /// <summary>
    /// Call this from your Title‐scene “Play” button.
    /// Resets all stats, then loads the Main scene by build index.
    /// </summary>
    public void StartNewGame()
    {
        ResetStats();
        SceneManager.LoadScene(MainSceneBuildIndex);
    }
    
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(CreditsSceneBuildIndex);
    }

    #endregion

    #region INTERNALS

    private void HandleGameOver()
    {
        // Show the Game Over canvas (if found in OnSceneLoaded)
        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(true);
        }

        // Enable ESC-to-return
        _isGameOver = true;
    }

    private void ResetStats()
    {
        Lives = 3;
        Score = 0;
        Gems = 0;
        
        OnHUDChanged?.Invoke(Score, Gems, Lives);
    }

    #endregion
}