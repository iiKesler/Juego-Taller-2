using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Menu
{
    public class NextLevel : MonoBehaviour
    {
        private Collider2D _collider2D;
    
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
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
