using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stamina : MonoBehaviour
{
    public PlayerMove player;

    public Slider staminaBar;
    private int maxStamina = 100;
    private float currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static Stamina instance;

    private void Awake()
    {
        instance=this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentStamina>1)
            {
                player.Move(currentStamina);
                UseStamina(0.4f);
            }
            else
            {
                player.Move(currentStamina);
            }
        }
        else
        {
            player.Move(currentStamina);
            staminaBar.value = currentStamina;
        }
    }

    public void UseStamina(float amount)
    {
        if (currentStamina-amount>=0)
        {
            currentStamina-=amount;
            staminaBar.value = currentStamina;
        }

        if (regen!=null)
        {
            StopCoroutine(regen);
        }

        regen = StartCoroutine(RegenStamina());
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while (currentStamina< maxStamina)
        {
            currentStamina += maxStamina/100;
            staminaBar.value =currentStamina;
            yield return regenTick;
        }
    }
}
