using System.Collections;
using System.Collections.Generic;
using Player_Related;
using UnityEngine;

public class OnScreenButtons : MonoBehaviour
{
    private PlayerController player;
    private PauseMenu _pauseMenu;
    private Weapon gun;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerController>();
        _pauseMenu = playerObj.GetComponent<PauseMenu>();
        gun = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
    }

    public void Jump()
    {
        player.OnJump();
    }

    public void Move(int direction)
    {
        player.Moving(new Vector2(direction, 0));
    }

    public void Fire()
    {
        gun.OnFire();
    }

    public void Pause()
    {
        PauseMenu.GameIsPaused = !PauseMenu.GameIsPaused;
    }

}
