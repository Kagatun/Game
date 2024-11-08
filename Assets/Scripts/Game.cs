using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private StarterGame _starter;
    [SerializeField] private DispatcherWavesGeese _dispatcherWavesGeese;

    private void OnEnable()
    {
        _starter.PlayButtonClicked += StartGame;
    }

    private void OnDisable()
    {
        _starter.PlayButtonClicked -= StartGame;
    }

    private void StartGame()
    {
        _dispatcherWavesGeese.StartGame();
    }
}
