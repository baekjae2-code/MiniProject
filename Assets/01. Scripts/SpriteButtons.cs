using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpriteButtons : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Collider2D col = Physics2D.OverlapPoint(mousePos);

            if (col != null && col.gameObject == gameObject)
            {
                print(col.gameObject.name);

                if (name == "RestartButton")
                {
                    SceneManager.LoadScene("GameScene");
                }
                else if (name == "MainmenuButton")
                {
                    SceneManager.LoadScene("MainScene");
                    for (int i = 0; i < GameManager.instance.deckUnitNumber.Length; i++)
                    {
                        GameManager.instance.deckUnitNumber[i] = -1;
                    }
                }
                else if (name == "NextStageButton")
                {

                }
            }
        }
    }

}
