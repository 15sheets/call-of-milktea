using UnityEngine;
using UnityEngine.SceneManagement;

public class scenetransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    public void gotoScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
