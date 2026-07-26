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
                ObjectPoolManager.instance.GameEnd();
                if (name == "RestartButton")
                {
                    SceneManager.LoadScene("GameScene");
                }
                else if (name == "MainmenuButton")
                {
                    SoundManager.instance.ChangeBgm();
                    SceneManager.LoadScene("MainScene");
                    for (int i = 0; i < GameManager.instance.deckUnitNumber.Length; i++)
                    {
                        GameManager.instance.deckUnitNumber[i] = -1;
                    }
                }
                else if (name == "NextStageButton")
                {
                    if (GameManager.instance.NowStage > GameManager.instance.ClearStage)//1스테이지 클리어했을때 => 현재스테이지 1 => 2스테이지 가능
                        return;                                                              // 1 > 2, 2 > 2, 3 > 2

                    GameManager.instance.SetStage(GameManager.instance.NowStage + 1);
                    ObjectPoolManager.instance.GameEnd();
                    SceneManager.LoadScene("GameScene");
                }
            }
        }
    }

}
