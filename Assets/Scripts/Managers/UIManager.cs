using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingsPanel;


    private bool isPaused = false;
    private GameObject[] respawns;
    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("Respawn");
    }
    public void ToggleDeathPanel() 
    {
        if (deathPanel.activeSelf == true){
            return;
        }
        else deathPanel.SetActive(true);
    }
    public void TogglePausePanel() 
    {
        isPaused = !isPaused;
        pausePanel.SetActive(!pausePanel.activeSelf);
        foreach (GameObject obj in respawns){
            obj.SetActive(!obj.activeSelf);

        if (isPaused == true)Time.timeScale = 0f;
              
        else Time.timeScale = 1f;            

        
    }
}
        public void ToggleSettingsPanel() 
        {
            if (SceneManager.GetActiveScene().buildIndex == 1){
                pausePanel.SetActive(!pausePanel.activeSelf);
                settingsPanel.SetActive(!settingsPanel.activeSelf);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2) settingsPanel.SetActive(!settingsPanel.activeSelf);
            
    }
}
