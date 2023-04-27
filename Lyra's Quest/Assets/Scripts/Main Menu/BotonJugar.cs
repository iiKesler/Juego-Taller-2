using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Menu
{
    public class BotonJugar : MonoBehaviour
    {
        public int gameStartScene;
    
        public void StartGame()
        {
            SceneManager.LoadScene(gameStartScene);
        }
    }
}
