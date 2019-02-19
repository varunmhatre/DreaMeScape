using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsTextDisplay : MonoBehaviour
{
    [SerializeField] private int initialHealth;
    [SerializeField] private int initialAttack;

    [SerializeField] private bool alt;

    private int healthValue;
    private int attackValue;

    private Text healthTextAlt;
    private Text attackTextAlt;

    private TextMesh healthText;
    private TextMesh attackText;
    // Start is called before the first frame update
    void Start()
    {
        if (!alt)
        {
            healthText = gameObject.GetComponent<KeyObjectReferences>().uiHealthValueObj.GetComponent<TextMesh>();
            attackText = gameObject.GetComponent<KeyObjectReferences>().uiAttackValueObj.GetComponent<TextMesh>();
        }

        if (alt)
        {
            healthTextAlt = gameObject.GetComponent<KeyObjectReferences>().uiHealthValueObj.GetComponent<Text>();
            attackTextAlt = gameObject.GetComponent<KeyObjectReferences>().uiAttackValueObj.GetComponent<Text>();
        }


        healthValue = initialHealth;
        attackValue = initialAttack;

        if (!alt)
        {
            healthText.text = healthValue.ToString();
            attackText.text = attackValue.ToString();
        }

        if (alt)
        {
            healthTextAlt.text = healthValue.ToString();
            attackTextAlt.text = attackValue.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!alt)
        {
            healthText.text = healthValue.ToString();
            attackText.text = attackValue.ToString();
        }

        if (alt)
        {
            healthTextAlt.text = healthValue.ToString();
            attackTextAlt.text = attackValue.ToString();
        }
    }

    public void SetHealth(int val)
    {
        healthValue = val;
    }

    public void SetAttack(int val)
    {
        attackValue = val;
    }

    public int GetHealth()
    {
        return healthValue;
    }

    public int GetAttack()
    {
        return attackValue;
    }

    public int GetInitialHealth()
    {
        return initialHealth;
    }

    public int GetInitialAttack()
    {
        return initialAttack;
    }
}
