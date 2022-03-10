using System.Collections;
using System.Collections.Generic;
using Player_Related;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    private PlayerController player;
    //private GameController game;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        //game = GameController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText("Score: " + GameController.instance.getScore());
        healthText.SetText("Health: " + player.CurrentHealth);
    }
}
