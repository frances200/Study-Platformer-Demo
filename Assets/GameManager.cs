using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject gameOverText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
