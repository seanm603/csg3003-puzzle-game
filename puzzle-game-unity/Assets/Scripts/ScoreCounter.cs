using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public int score = 0;
    private Text uiText;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<Text>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        score = _gameManager.GetScore();
        uiText.text = score.ToString("#,0");
    }
}
