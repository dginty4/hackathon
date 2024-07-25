using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnOnCollision : MonoBehaviour
{
    public float healthAmount = 100f;
    public Image HealthBar;
    public Transform[] spawnPoints; // Array of predefined spawn points
	public Transform finalPoint;
	bool isFinalButton; 
	public GameObject boss;
	public GameObject promptPanel;

    public void DamageBoss(float damage)
    {

        healthAmount -= damage;
        HealthBar.fillAmount = healthAmount / 100f;
    }   
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RepositionObject(this.gameObject);
            DamageBoss(5f);
            
            if (healthAmount <= 0)
            {
				if (!isFinalButton) {
                	FinalButton(this.gameObject);
				} 
				else {
					Destroy(boss);
					promptPanel.SetActive(true);
				}
            }
        }
    }
    
    public void RepositionObject(GameObject obj)
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector2 newPosition = spawnPoints[randomIndex].position;
        obj.transform.position = newPosition;
    }

    public void FinalButton(GameObject obj)
    {
		isFinalButton = true;
        Vector2 finalPosition = finalPoint.position;
        obj.transform.position = finalPosition;
    }
}
