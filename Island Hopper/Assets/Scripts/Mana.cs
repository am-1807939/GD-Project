using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{

	public float manaPoints = 100f;
	public float maxManaPoints = 100f;	

    private Image mpBar;


    void Start()
    {
        
        mpBar = GameObject.Find("MPbar").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
        mpBar.fillAmount = manaPoints / maxManaPoints;

    }


    public void ConsumeMana(float amount)
	{	
		manaPoints = manaPoints - amount;	
	}
	
	public void RefillMana(float amount)
	{
		manaPoints = manaPoints + amount;
        if (manaPoints > maxManaPoints)
        {
            manaPoints = maxManaPoints;
        }
	}

}
