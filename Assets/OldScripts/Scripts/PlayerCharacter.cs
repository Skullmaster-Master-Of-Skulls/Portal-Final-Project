using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxHealth = 1.0f;
    private float health;
    private float healthFloat;

    // Use this for initialization 
    void Start()
    {
        health = maxHealth;
    }
    public float getHealth()
    {
        return healthFloat;
    }
    public void Hit()
    {
        health -= 0.2f;
        healthFloat = health;
        Messenger.Broadcast(GameEvent.HEALTH_CHANGED);
        Debug.Log("Health: " + health);
        if (health <= 0.01f)
        {
            //Debug.Break();
            Messenger.Broadcast(GameEvent.PLAYER_DEAD);
        }
    }

    public void FirstAid(float healthAdded)
    {
        health += healthAdded;
        healthFloat = health;
        if (health > maxHealth)
        {
            health = maxHealth;
            healthFloat = maxHealth;
        }
        Debug.Log("Health: " + health);
        Messenger.Broadcast(GameEvent.HEALTH_CHANGED);
    }



// Update is called once per frame
void Update()
    {
          
    }
}
