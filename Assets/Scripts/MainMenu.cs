using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Play()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void Quite()
   {
    Application.Quit();
    Debug.Log("Player Has Quit The Game");
   }
}
