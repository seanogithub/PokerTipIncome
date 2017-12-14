using UnityEngine;
using System;
using System.Collections;

public class TipData  {
	
	public string JobName = "";
	public System.DateTime Date = new System.DateTime();
	public float TipAmount = 0.0f;
	public int NumberOfDowns = 0;
	public float HoursWorked = 0.0f;
	public bool TournamentDowns = false;
	public string TournamentName = "";
	public float TournamentDownAmount = 0.0f;
	public int NumberOfTournamentDowns = 0;
	public float TipOut = 0.0f;
	public string Notes = "";
	public bool WorkDay = false;
	public System.DateTime WorkDayStartTime = new System.DateTime();
	public float PayCheckAmount = 0;
	
}
