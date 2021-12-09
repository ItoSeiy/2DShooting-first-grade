using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    static GameManager _instance;

    public int Score => _score;
    public int Power => _power;
    public int BombCount => _bombCount;
    public int InvincibleObjectCount => _invicibleObjectCount;


    private int _score = default;
    private int _power = default;
    private int _bombCount = default;
    private int _invicibleObjectCount = default;
    [SerializeField] UnityEvent _gameOverEvent;
    
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GameStart()
    {
        _score = 0;
    }

    public void GetScore(int score)
    {
        _score += score;
    }

    public void GetPower(int power)
    {
        _power += power;
    }

    public void GetBomb(int bombCount)
    {
        _bombCount += bombCount;
    }

    public void UseBomb(int useBombCount)
    {
        _bombCount -= useBombCount;
    }

    public void GetInvicibleObjectCount(int invicibleObjectCount)
    {
        _invicibleObjectCount += invicibleObjectCount;
    }

    public void GameOver()
    {
        _gameOverEvent.Invoke();
    }
}