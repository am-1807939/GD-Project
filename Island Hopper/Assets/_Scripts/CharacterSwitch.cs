using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject wizard;
    public GameObject knight;
    private ParticleSystem smoke;
    public Transform mainCam;
    public AudioSource switchAudioSrc;
    void Start()
    {
        smoke = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            float counter = 0;
            StartCoroutine(SmokeScreen());
            var yRotation = mainCam.eulerAngles.y;
            if (wizard.activeInHierarchy == true)
            {
                wizard.SetActive(false);
                knight.SetActive(true);
                knight.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
                knight.transform.Rotate(knight.transform.eulerAngles.x, yRotation, knight.transform.eulerAngles.z);
            }
            else
            {
                wizard.SetActive(true);
                knight.SetActive(false);
                wizard.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
                wizard.transform.Rotate(wizard.transform.eulerAngles.x, yRotation, wizard.transform.eulerAngles.z);
            }

            float waitTime = 1;
            while (counter < waitTime)
            {
                counter += Time.deltaTime;
                
            }
        }
    }

    private IEnumerator SmokeScreen()
    {
        ParticleSystem.EmissionModule smokeEmission = smoke.emission;
        switchAudioSrc.Play();
        smokeEmission.enabled = true;
        yield return new WaitForSeconds(0.4f);
        smokeEmission.enabled = false;
    }

    public GameObject getActiveModel() {
        if (wizard.activeInHierarchy == true)
        {
            return wizard;
        }
        else {
            return knight;
        }
    }

}
