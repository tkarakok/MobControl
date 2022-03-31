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


    private void Start() {
        MainMenu += SubscribeAllEvent;
        MainMenu();
    }

    void SubscribeAllEvent(){
        #region InGame
            InGame += () => StateManager.Instance.state = State.InGame;
        #endregion

        #region CannonMove
            CannonMove += () => StateManager.Instance.state = State.CannonMove;
            CannonMove += CannonController.Instance.RotateCannonBody;
            CannonMove += GameManager.Instance.ActiveAllCharactersDestroy;
        #endregion

        #region GameOver
            GameOver += () => StateManager.Instance.state = State.GameOver;
            GameOver += GameManager.Instance.ActiveAllCharactersDestroy;
        #endregion

        #region EndGame
            EndGame += () => StateManager.Instance.state = State.EndGame;
            EndGame += GameManager.Instance.ActiveAllCharactersDestroy;
        #endregion
        
    }

}


