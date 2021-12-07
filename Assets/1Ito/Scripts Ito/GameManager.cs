using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    public int InvincibleObjectCount => _invicibleObjectCount;

    public int Score => _score;
    public int Power => _power;
    public int BombCount => _bombCount;


    static GameManager _instance;
    private int _score = default;
    private int _power = default;
    private int _bombCount = default;
    private int _invicibleObjectCount = default;
    
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

    public void GetInvicibleObjectCount(int invicibleObjectCount)
    {
        _invicibleObjectCount += invicibleObjectCount;
    }
}