using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject gameOverText;

    // Update is called once per frame
    void Update()
    {
        if (player.isGameOver && !gameOverText.activeSelf)
        {
            gameOverText.SetActive(true);
        } else if (!player.isGameOver && gameOverText.activeSelf)
        {
            gameOverText.SetActive(false);
        }
    }
}
