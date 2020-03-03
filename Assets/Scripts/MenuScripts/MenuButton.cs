using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{

    public enum ButtonType { PLAY }

    public ButtonType buttonType;

    private void OnMouseDown()
    {
        switch (buttonType)
        {
            case ButtonType.PLAY:
                PlayerPrefs.SetInt("LevelToLoad", 1);
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
                break;
        }
    }
}
