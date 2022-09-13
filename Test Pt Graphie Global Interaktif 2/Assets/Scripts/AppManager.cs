using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AppManager
{
    public enum GameplayState
    {
        iddle,//when user first run and didnt do anything yet
        pause,//when game paused
        play,//when user playing the game        
    }

    public static GameplayState gameplayState;
    public static PlayerController playerController;

    static AppManager() {
        gameplayState = GameplayState.play;
    }

}
