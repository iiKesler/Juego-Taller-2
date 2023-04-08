using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonJugar : MonoBehaviour
{
    public int gameStartScene;
    
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}
