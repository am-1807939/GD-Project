using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour {
	
	public enum deathAction {loadLevelWhenDead,doNothingWhenDead};
	
	public float healthPoints = 100f;
	public float respawnHealthPoints = 100f;		//base health points
	
	public int numberOfLives = 1;					//lives and variables for respawning
	public bool isAlive = true;	

	public GameObject explosionPrefab;
	
	public deathAction onLivesGone = deathAction.doNothingWhenDead;
	
	public string LevelToLoad = "";
	
	private Vector3 respawnPosition;
	private Quaternion respawnRotation;

    private int nextUpdate=1;
	public float healthRegenPoints = 1f;

	private Image hpBar;
	

	// Use this for initialization
	void Start () 
	{
		// store initial position as respawn location
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;
		
		if (LevelToLoad=="") // default to current scene 
		{
			// LevelToLoad = Application.loadedLevelName;
			Scene scene = SceneManager.GetActiveScene();
			LevelToLoad = scene.name;

		}

		hpBar = GameObject.Find("HPbar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (healthPoints <= 0) {				// if the object is 'dead'
			numberOfLives--;					// decrement # of lives, update lives GUI
			
			if (explosionPrefab!=null) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}
			
			if (numberOfLives > 0) { // respawn
				transform.position = respawnPosition;	// reset the player to respawn position
				transform.rotation = respawnRotation;
				healthPoints = respawnHealthPoints;	// give the player full health again
			} else { // here is where you do stuff once ALL lives are gone)
				isAlive = false;
				
				switch(onLivesGone)
				{
				case deathAction.loadLevelWhenDead:
					// Application.LoadLevel (LevelToLoad);
					SceneManager.LoadScene(LevelToLoad, LoadSceneMode.Single);
					break;
				case deathAction.doNothingWhenDead:
					// do nothing, death must be handled in another way elsewhere
					break;
				}
				Destroy(gameObject);


				// Death animation -- not working properly
				// Animator anim = GetComponent<CharacterSwitch>().getActiveModel().GetComponent<Animator>();
				// anim.SetTrigger("death");	
				// Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + 2f);
				// foreach(Collider c in GetComponents<Collider> ()) {
				// c.enabled = false; 
				// }
				// enabled = false;
			}
		}

		if(Time.time>=nextUpdate){
             nextUpdate=Mathf.FloorToInt(Time.time)+1;

            if (healthPoints < respawnHealthPoints) {
                ApplyHeal(healthRegenPoints);
            }
         }

		hpBar.fillAmount = healthPoints / respawnHealthPoints;

	}
	
	public void ApplyDamage(float amount)
	{	
		healthPoints = healthPoints - amount;
		GetComponent<CharacterSwitch>().getActiveModel().GetComponent<Animator>().SetTrigger("isHit");	
	}
	
	public void ApplyHeal(float amount)
	{
		healthPoints = healthPoints + amount;
		if (healthPoints > respawnHealthPoints)
        {
            healthPoints = respawnHealthPoints;
        }
	}

	public void ApplyBonusLife(int amount)
	{
		numberOfLives = numberOfLives + amount;
	}
	
	public void updateRespawn(Vector3 newRespawnPosition, Quaternion newRespawnRotation) {
		respawnPosition = newRespawnPosition;
		respawnRotation = newRespawnRotation;
	}
}
