using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private DragonHealth playerHealth;
    [SerializeField] private DragonHealth enemyHealth;

    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private TMP_Text winnerText;

    private bool battleOver;

    private void Update()
    {
        if (battleOver)
            return;

        if (playerHealth == null || enemyHealth == null)
            return;

        if (playerHealth.CurrentHealth <= 0f)
        {
            EndBattle("ENEMY WINS!");
        }
        else if (enemyHealth.CurrentHealth <= 0f)
        {
            EndBattle("PLAYER WINS!");
        }
    }

    private void EndBattle(string message)
    {
        battleOver = true;

        winnerPanel.SetActive(true);
        winnerText.text = message;

        Time.timeScale = 0f;
    }

    public void RestartBattle()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}