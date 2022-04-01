using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    #region Public Fields
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject inGamePanel;
    public GameObject gameOverPanel;
    public GameObject endGamePanel;
    [Header("Main Menu")]
    public Text mainMenuTotalCoinText;
    public Text mainMenuTotalRockText;
    public Text mainMenuLevelText;
    [Header("In Game")]
    public Text inGameCoinText;
    public Text inGameRockText;
    public Slider levelProgressBar;
    [Header("In Game")]
    public Text endGameCoinText;
    public Text endGameRockText;

    #endregion

    #region Private Fields
    private GameObject _currentPanel;
    #endregion
        

    private void Start()
    {
        _currentPanel = mainMenuPanel;
        levelProgressBar.maxValue = GameManager.Instance.targets.Count;
    }

    #region Panel
    public void PanelChange(GameObject openPanel)
    {
        _currentPanel.SetActive(false);
        openPanel.SetActive(true);
        _currentPanel = openPanel;
    }
    #endregion

    #region Button Func.
    public void StartButton()
    {
        EventManager.Instance.InGame();
        PanelChange(inGamePanel);
    }

    public void RestartButton()
    {
        LevelManager.Instance.ChangeLevel("LEVEL " + LevelManager.Instance.GetLevelName());
    }

    public void NextLevelButton()
    {
        LevelManager.Instance.ChangeLevel("LEVEL " + LevelManager.Instance.GetLevelName());
    }

    #endregion

    #region Game Happens
    public void EndGame()
    {
        PanelChange(endGamePanel);
        EndGameUIUpdate();
    }
    public void GameOver()
    {
        PanelChange(gameOverPanel);
    }
    #endregion

    #region UI UPDATE
    // main menu UI
    public void MainMenuUIUpdate()
    {
        mainMenuTotalCoinText.text = PlayerPrefs.GetInt("Coin").ToString();
        mainMenuTotalRockText.text = PlayerPrefs.GetInt("Rock").ToString();
        mainMenuLevelText.text = "LEVEL " + (LevelManager.Instance.CurrentLevel).ToString();
    }
    // In Game
    public void InGameCoinUpdate()
    {
        inGameCoinText.text = GameManager.Instance.CurrentCoin.ToString();
    }
    public void InGameRockUpdate()
    {
        inGameRockText.text = GameManager.Instance.CurrentRock.ToString();
    }

    public void StartLevelProgressBarUpdate()
    {
        levelProgressBar.gameObject.SetActive(true);
        StartCoroutine(LevelProgressUpdate());
    }

    IEnumerator LevelProgressUpdate()
    {
        levelProgressBar.DOValue(levelProgressBar.value + 1, 1);
        yield return new WaitForSeconds(2);
        levelProgressBar.gameObject.SetActive(false);

    }
    // end game
    public void EndGameUIUpdate()
    {
        endGameCoinText.text = inGameCoinText.text;
        endGameRockText.text = inGameRockText.text;
    }
    #endregion

}
