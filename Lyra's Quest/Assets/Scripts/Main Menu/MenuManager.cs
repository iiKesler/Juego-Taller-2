using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Main_Menu
{
   public class MenuManager : MonoBehaviour
   {
      public void QuitGame()
      {
         Application.Quit();
      }
   }
}
