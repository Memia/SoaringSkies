using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	[Header("PLAYER STATS")]
	[Header("Energy")]
	public float curEnergy;
	public float maxEnergy = 100;
	public int decreaseEnergySpeed;
	public int increaseEnergySpeed;
	public Image energyBar;

	// Decrease energy
	PlayerMovment playerMovement;

	void Start()
	{
		curEnergy = maxEnergy;
		energyBar = GameObject.Find("Energy_Fill").GetComponent<Image>();
		playerMovement = GetComponent<PlayerMovment>();
	}
	
	void Update()
	{
		Energy();
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Boost")
		{
			curEnergy += increaseEnergySpeed * Time.deltaTime;
		}
	}
	void Energy()
	{
		energyBar.fillAmount = curEnergy / 100;

		// Over the limit
		if (curEnergy >= maxEnergy)
		{
			curEnergy = maxEnergy;
		}

		// Under the limit
		if (curEnergy <= 0)
		{
			curEnergy = 0;

			// Cannot Soar anymore
			OopsNoEnergy();

			// If run out of energy in air
			if (!playerMovement.IsGrounded)
	        {
				// After 3 seconds camera will freeze and player will fall really fast... to his end
			}
		}

		// Decrease energy bar when soaring
		if (playerMovement.soaring)
		{
			curEnergy -= decreaseEnergySpeed * Time.deltaTime;
		}
	}
	void OopsNoEnergy()
	{
		playerMovement.soaring = false;
		// Player will stop gliding
	}
}
