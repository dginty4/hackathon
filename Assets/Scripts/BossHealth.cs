using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public float healthAmount = 100f;
    public Image HealthBar;
	public GameObject boss;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DamageBoss(2f);
        }
    }
    
    public void DamageBoss(float damage)
    {

            healthAmount -= damage;
            HealthBar.fillAmount = healthAmount / 100f;

            // Player died
            if (healthAmount <= 0)
            {
                Debug.Log("You Win");
				
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
    }
}
