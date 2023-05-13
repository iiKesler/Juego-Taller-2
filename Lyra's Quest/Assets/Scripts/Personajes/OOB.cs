using UnityEngine;
using UnityEngine.SceneManagement;

namespace Personajes
{
    public class OOB : MonoBehaviour
    {
        // when the player falls it resets the level
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
