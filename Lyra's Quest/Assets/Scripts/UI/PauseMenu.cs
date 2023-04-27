using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;
        [FormerlySerializedAs("PauseMenuCanvas")] 
        public GameObject pauseMenuCanvas;

        private void Start()
        {
            Time.timeScale = 1f;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(gameIsPaused)
                {
                    Play();
                }
                else
                {
                    Stop();
                }
            }
        }

        public void Play()
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        private void Stop()
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Resume()
        {
        }
    }
}
