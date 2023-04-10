using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public class MenuManager : MonoBehaviour
{
   public void QuitGame()
   {
      Application.Quit();
   }
}
