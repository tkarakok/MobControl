using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void StateActions();
    public StateActions MainMenu;
    public StateActions InGame;
    public StateActions CannonMove;
    public StateActions EndGame;
    public StateActions GameOver;


    private void Awake()
    {
        MainMenu += SubscribeAllEvent;
        MainMenu += UIManager.Instance.MainMenuUIUpdate;
        MainMenu();
    }

    void SubscribeAllEvent()
    {
        #region InGame
        InGame += () => StateManager.Instance.state = State.InGame;
        InGame += UIManager.Instance.InGameCoinUpdate;
        InGame += UIManager.Instance.InGameRockUpdate;
        InGame += () => EventManager.Instance.CannonMove();
        #endregion

        #region CannonMove
        CannonMove += () => StateManager.Instance.state = State.CannonMove;
        CannonMove += CannonController.Instance.RotateCannonBody;
        CannonMove += GameManager.Instance.ActiveAllCharactersDestroy;
        #endregion

        #region GameOver
        GameOver += () => StateManager.Instance.state = State.GameOver;
        GameOver += GameManager.Instance.ActiveAllCharactersDestroy;
        GameOver += UIManager.Instance.GameOver;
        #endregion

        #region EndGame
        EndGame += () => StateManager.Instance.state = State.EndGame;
        EndGame += GameManager.Instance.ActiveAllCharactersDestroy;
        EndGame += GameManager.Instance.SaveRockAndCoin;
        EndGame += LevelManager.Instance.NextLevel;
        EndGame += UIManager.Instance.EndGame;
        #endregion

        
    }

}


