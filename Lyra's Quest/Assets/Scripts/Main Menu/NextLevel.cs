using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Menu
{
    public class NextLevel : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Collider2D>();
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("No es el jugador o no tiene el tag Player");
            }
        }
        
    
    }
}
