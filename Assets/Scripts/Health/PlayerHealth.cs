using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : AbstractHealth
{
    bool Invincible = false;
    int InvincibilityTime = 3; //how long player is invincible in seconds


    private void Reset()
    {
        MaxHealth = 3;
    }

    protected override void OnDeath()
    {
        DeathScript.Instance.Show();
    }

    void ResetInvincibility()
    {
        Invincible = false;
        StopCoroutine("Flash");
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    IEnumerator Flash()
    {
        SpriteRenderer playerRender = gameObject.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(.1f);
        playerRender.enabled = !playerRender.enabled;
        if (Invincible)
            StartCoroutine("Flash");
    }

    public void OnReceiveDamage(HealthChangeEvent e)
    {
        if (Invincible)
        {
            e.Cancel();
        }
        else
        {
            Invincible = true;
            StartCoroutine("Flash");
            Invoke("ResetInvincibility", InvincibilityTime);
        }
    }
}