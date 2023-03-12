using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Losing : MonoBehaviour
{
    [SerializeField] private GameObject restartButton;
    private void OnEnable() => BoatHealth.loseGame += endGame;

    private void OnDisable() => BoatHealth.loseGame -= endGame;

    private void endGame()
    {
        Time.timeScale = 0f;
        restartButton.SetActive(true);
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
