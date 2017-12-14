using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppData  {

	public Color FontColor = new Color( 0.0f, 0.0f, 0.0f, 1.0f);  // Black
	public Color ButtonColor = new Color( 0.37f, 0.63f, 1.0f, 1.0f);  // Light Blue
	public Color BackGroundColor = new Color( 0.50f, 0.50f, 0.50f, 1.0f);  // Light Gray
	public float HourlyWageAmount = 8.50f;
	public float TaxPercentage = 20.0f;
	public int NumberOfWorkingDaysPerMonth = 20;
	public bool UsePayCheckData = true;
	public bool UsePassCode = false;
	public string PassCode = "";
	public float PassCodeTimer = 120.0f;	
	public List<string> JobNameList = new List<string>();
	public List<string> TournamentNameList = new List<string>(); 
	public List<string> ExpenseNameList = new List<string>(); 
	public string SkinName = "iOS7";
	
}
