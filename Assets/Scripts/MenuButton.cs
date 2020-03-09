using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

    public enum ButtonType { PLAY }

    public ButtonType buttonType;
    public AudioClip clickSound;
    public AudioClip hoverSound;
    public bool hoverPlayed;
    private GameObject text;

    private void Start()
    {
        text = transform.GetChild(0).gameObject;
    }

    private void OnMouseDown()
    {
        switch (buttonType)
        {
            case ButtonType.PLAY:
                PlayerPrefs.SetInt("LevelToLoad", 1);
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
                SoundGuy.Instance.PlaySound(Vector3.zero, 1, clickSound);
                break;
        }
    }

    private void OnMouseOver()
    {
        if (!hoverPlayed)
        {
            SoundGuy.Instance.PlaySound(Vector3.zero, 1, hoverSound);
            hoverPlayed = true;
        }
        text.GetComponent<TextMesh>().color = Color.gray;
    }

    private void OnMouseExit()
    {
        hoverPlayed = false;
        text.GetComponent<TextMesh>().color = Color.white;
    }
}
