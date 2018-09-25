using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public Text scoreText;
	public float curScore;


	void Start()
	{
		scoreText = GameObject.Find("timerText").GetComponent<Text>();
	}

	void Update()
	{
		scoreText.text = curScore.ToString("N");
		curScore += Time.deltaTime;
	}
}
