using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{

	public float manaPoints = 100f;
	public float maxManaPoints = 100f;	

    public float manaRegenPoints = 1f;

    private Image mpBar;

    private int nextUpdate=1;


    void Start()
    {
        
        mpBar = GameObject.Find("MPbar").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time>=nextUpdate){
             nextUpdate=Mathf.FloorToInt(Time.time)+1;

            if (manaPoints < maxManaPoints) {
                manaPoints += manaRegenPoints;
            }
         }

        mpBar.fillAmount = manaPoints / maxManaPoints;

    }


    public bool ConsumeMana(float amount)
	{	
        if (manaPoints > amount) {
            manaPoints = manaPoints - amount;	
            return true;
        } else {
            return false;
        }
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
