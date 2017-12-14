using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GUITexture))]
public class UI_Statistics : MonoBehaviour {

	public bool Enabled = true;

	public List<TipData> DailyTipData = new List<TipData>(); 
	
	public List<float> DailyTotalDollars = new List<float>();
	private List<float> DailyHoursWorked = new List<float>();
	private List<float> DailyDownsWorked = new List<float>();

	public List<float>	WeeklyTotalDollars = new List<float>();
	private List<float>	WeeklyHoursWorked = new List<float>();
	private List<float>	WeeklyDownsWorked = new List<float>();
	
	public List<float> MonthlyTotalDollars = new List<float>();
	private List<float> MonthlyHoursWorked = new List<float>();
	private List<float> MonthlyDownsWorked = new List<float>();
	
	private float[] DayOfWeekTipAmount = new float[7];
	
//	private System.DateTime currentDate = new System.DateTime(); 
	
	public GameObject UIManager;
	public GameObject UserBlobManager;
		
	public Texture2D BackgroundBitmap;
	public Texture2D style2XButtonBlueBitmap;
	public Texture2D style2XButtonBlueActiveBitmap;

	private GUIStyle style;
	private GUIStyle styleWhite;
	private GUIStyle styleRed;
	private GUIStyle styleGreen;
	private GUIStyle style2X;
	private GUIStyle style2XButton;
	private GUIStyle style2XButtonBlue;
	private GUIStyle styleBigger;
	private GUIStyle styleDailyTotals;
	private GUIStyle styleCalendarButtons;
	private GUIStyle styleEnterDataButtons;
	private GUIStyle style2XBlack;

	public Color FontColor;
	public Color ButtonColor;
	public Color BackgroundColor;
	
	private float AspectRatioMultiplier = 0.57f;

	// stats
	private float AverageTipAmountPerDown = 0.0f;
	private float AverageTipAmountPerHour = 0.0f;
	private float AverageTipAmountPerDay = 0.0f;
	private float AverageTipAmountPerWeek = 0.0f;
	private float AverageTipAmountPerMonth = 0.0f;
	private float AverageHoursPerWeek = 0.0f;
	private float AverageHoursPerMonth = 0.0f;
	private float AverageDownsPerDay = 0.0f;
	private float AverageDownsPerWeek = 0.0f;
	private float AverageDownsPerMonth = 0.0f;
	private string BestDayToWork = "no data";
	private string WorstDayToWork = "no data";
	private float TotalTipAmountThisWeek = 0.0f;
	private float TotalTipAmountThisMonth = 0.0f;
	private float TotalTipAmountThisYear = 0.0f;
	
	private int ZeroValueDays = 0;
	private int ZeroValueWeek = 0;

	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	
	// Use this for initialization
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		DailyTipData = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData;
		
		AspectRatioMultiplier = ((float)Screen.width/1200 );
		
	}

	public void GUIEnabled(bool myValue)
	{
		Enabled = myValue;
		this.GetComponent<GUITexture>().enabled = myValue;
	}
	
	void OnGUI()
	{
		if (Enabled == true)
		{
			FontColor = UserBlobManager.GetComponent<UserBlobManager>().FontColor;
			ButtonColor = UserBlobManager.GetComponent<UserBlobManager>().ButtonColor;
			BackgroundColor =  UserBlobManager.GetComponent<UserBlobManager>().BackGroundColor;
			
			style = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[0];
			styleWhite = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[1];
			styleRed = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[2];
			styleGreen = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[3];
			style2X = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[4];
			style2XButton = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[5];
			style2XButtonBlue = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[6];
			styleBigger = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[7];
			styleDailyTotals = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[8];
			styleCalendarButtons = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[9];
			styleEnterDataButtons = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[10];
			style2XBlack = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[11];
			
			AspectRatioMultiplier = ((float)Screen.width/1200 );
			
			// background image
			GetComponent<GUITexture>().color = BackgroundColor;
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().EnterDataBackground;

			// time and month and year		
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);

			
			// data labels
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.10f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Tips Per Down", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.15f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Tips Per Hour", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.20f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Tips Per Day", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.25f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Tips Per Week", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.30f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Tips Per Month", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.35f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Hours Per Week", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.40f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Hours Per Month", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.45f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Downs Per Day", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.50f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Downs Per Week", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.55f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Average Downs Per Month", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.60f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Best day to work", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Worst day to work", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.70f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Total Tips this Week", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.75f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Total Tips this Month", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.80f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Total Tips this Year", style2XBlack);
			
			// statistics values
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.10f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), ("$ " + AverageTipAmountPerDown.ToString()), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.15f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), ("$ " + AverageTipAmountPerHour.ToString()), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.20f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), ("$ " + AverageTipAmountPerDay.ToString()), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.25f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), ("$ " + AverageTipAmountPerWeek.ToString()), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.30f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), ("$ " + AverageTipAmountPerMonth.ToString()), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.35f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (AverageHoursPerWeek.ToString() + " hours"), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.40f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (AverageHoursPerMonth.ToString() + " hours"), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.45f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (AverageDownsPerDay.ToString() + " downs"), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.50f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (AverageDownsPerWeek.ToString() + " downs"), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.55f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (AverageDownsPerMonth.ToString() + " downs"), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.60f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (BestDayToWork.ToString()), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (WorstDayToWork.ToString()), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.70f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), ("$ " + TotalTipAmountThisWeek.ToString()), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.75f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), ("$ " + TotalTipAmountThisMonth.ToString()), style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.60f), (Screen.height * 0.80f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), ("$ " + TotalTipAmountThisYear.ToString()), style2XBlack);
			
			// back button
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.90f), (Screen.width * 0.5f), (Screen.height * 0.075f)) ,"Back", style2XButtonBlue))
			{
				UIManager.GetComponent<UI_Manager>().SwitchStates("CalendarState");
			}			
		}
	}

	public void ResetStatisticsData()
	{
		DailyTipData = new List<TipData>();
		DailyTipData = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData;
		UpdateStatistics();
	}
	
	public void UpdateStatistics()
	{
		DailyTipData = new List<TipData>(); 
		DailyTipData = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData;
		DailyTipData.Sort( delegate(TipData DTD1, TipData DTD2) { return DTD1.Date.CompareTo(DTD2.Date); });

/*		
		for (int i = 0; i < DailyTipData.Count; i++)
		{
			print (i.ToString() + " " + DailyTipData[i].Date.ToString() + " " + DailyTipData[i].TipAmount.ToString());
		}		
*/		
		// calculate stats
		SumDailyValues();
		SumWeeklyValues();
		SumMonthlyValues();
		
		CalculateAverageTipsPerDown();
		CalculateAverageTipsPerDay();
		CalculateAverageDollarsPerHour();
		CalculateAverageDownsPerDay();

		CalculateAverageTipsPerWeek();
		CalculateAverageHoursPerWeek();
		CalculateAverageDownsPerWeek();
		
		CalculateAverageTipsPerMonth();
		CalculateAverageHoursPerMonth();
		CalculateAverageDownsPerMonth();
		
		CalculateBestWorstDayToWork();

		CalculateWeeklyTotals();
		CalculateMonthlyTotals();
		CalculateYearTotals();
	}
	
	void CheckZeroValueDay(float myValue)
	{
		if (myValue == 0)
		{
			ZeroValueDays +=1;
		}
	}

	void CheckZeroValueWeek(float myValue)
	{
		if (myValue == 0)
		{
			ZeroValueWeek +=1;
		}
	}
	
	void SumDailyValues()
	{
		var AddLastEntry = true;
		// add up tips for each day and put them into a list
		DailyTotalDollars = new List<float>();
		DailyHoursWorked = new List<float>();
		DailyDownsWorked = new List<float>();
		DayOfWeekTipAmount = new float[7];
		ZeroValueDays = 0;
		
		var CurrentTipAmount = 0.0f;
		var CurrentHoursWorked = 0.0f;
		var CurrentDowns = 0.0f;

		for(int i = 0; i < DailyTipData.Count - 1; i++)
		{
			CurrentHoursWorked = DailyTipData[i].HoursWorked;
			CurrentTipAmount = DailyTipData[i].TipAmount - DailyTipData[i].TipOut;
			CurrentDowns = (DailyTipData[i].NumberOfDowns + DailyTipData[i].NumberOfTournamentDowns);
			CurrentTipAmount += (DailyTipData[i].TournamentDownAmount * DailyTipData[i].NumberOfTournamentDowns);
			
			if(DailyTipData[i].Date.Year == DailyTipData[i+1].Date.Year && 
				DailyTipData[i].Date.Month == DailyTipData[i+1].Date.Month &&
				DailyTipData[i].Date.Day == DailyTipData[i+1].Date.Day)
			{
				i++;
				for(int d = i; d < DailyTipData.Count; d++)
				{
					if(DailyTipData[i].Date.Year == DailyTipData[d].Date.Year &&
						DailyTipData[i].Date.Month == DailyTipData[d].Date.Month &&
						DailyTipData[i].Date.Day == DailyTipData[d].Date.Day)
					{
						CurrentTipAmount += DailyTipData[d].TipAmount;
						CurrentTipAmount -= DailyTipData[d].TipOut;
						CurrentHoursWorked += DailyTipData[d].HoursWorked;
						CurrentDowns += (DailyTipData[d].NumberOfDowns + DailyTipData[d].NumberOfTournamentDowns);
						CurrentTipAmount += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
					}
					else
					{
						i = d - 1 ;
						d = DailyTipData.Count;
					}
					// hit end of entries
					if (d == DailyTipData.Count - 1)
					{
						i = d;
						AddLastEntry = false;
					}
				}
			}
			
			CheckZeroValueDay(CurrentTipAmount);
			DailyTotalDollars.Add(CurrentTipAmount);
			DailyHoursWorked.Add(CurrentHoursWorked);
			DailyDownsWorked.Add(CurrentDowns);
			AddSumDayOfWeek(DailyTipData[i].Date, CurrentTipAmount);
		}
		
		// this handles the last entry in the list
		if(DailyTipData.Count > 1 &&
			DailyTipData[DailyTipData.Count -1].Date.Year == DailyTipData[DailyTipData.Count -2].Date.Year &&
			DailyTipData[DailyTipData.Count -1].Date.Month == DailyTipData[DailyTipData.Count -2].Date.Month &&
			DailyTipData[DailyTipData.Count -1].Date.Day == DailyTipData[DailyTipData.Count -2].Date.Day &&
			AddLastEntry)
		{
			DailyTotalDollars[DailyTotalDollars.Count-1] += DailyTipData[DailyTipData.Count -1].TipAmount;
			DailyTotalDollars[DailyTotalDollars.Count-1] -= DailyTipData[DailyTipData.Count -1].TipOut;
			DailyHoursWorked[DailyHoursWorked.Count-1] += DailyTipData[DailyTipData.Count -1].HoursWorked;
			DailyDownsWorked[DailyDownsWorked.Count-1] += (DailyTipData[DailyTipData.Count -1].NumberOfDowns + DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns);
			DailyTotalDollars[DailyTotalDollars.Count-1] += (DailyTipData[DailyTipData.Count -1].TournamentDownAmount * DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns);
			AddSumDayOfWeek(DailyTipData[DailyTotalDollars.Count-1].Date, DailyTipData[DailyTipData.Count -1].TipAmount - DailyTipData[DailyTipData.Count -1].TipOut);
		}
		else
		{
			if(DailyTipData.Count > 1 &&
				AddLastEntry)
			{			
				CurrentTipAmount = DailyTipData[DailyTipData.Count -1].TipAmount - DailyTipData[DailyTipData.Count -1].TipOut;
				CurrentHoursWorked = DailyTipData[DailyTipData.Count -1].HoursWorked;
				CurrentDowns = DailyTipData[DailyTipData.Count -1].NumberOfDowns + DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns;
				CurrentTipAmount += (DailyTipData[DailyTipData.Count -1].TournamentDownAmount * DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns);

				CheckZeroValueDay(CurrentTipAmount);
				DailyTotalDollars.Add(CurrentTipAmount);
				DailyHoursWorked.Add(CurrentHoursWorked);
				DailyDownsWorked.Add(CurrentDowns);
				AddSumDayOfWeek(DailyTipData[DailyTotalDollars.Count-1].Date, CurrentTipAmount);
			}
		}
		
		// this handles if there is only one entry
		if(DailyTipData.Count == 1)
		{
			CurrentTipAmount = DailyTipData[0].TipAmount - DailyTipData[0].TipOut;
			CurrentHoursWorked = DailyTipData[0].HoursWorked;
			CurrentDowns = DailyTipData[0].NumberOfDowns + DailyTipData[0].NumberOfTournamentDowns;
			CurrentTipAmount += (DailyTipData[0].TournamentDownAmount * DailyTipData[0].NumberOfTournamentDowns);

			CheckZeroValueDay(CurrentTipAmount);
			DailyTotalDollars.Add(CurrentTipAmount);
			DailyHoursWorked.Add(CurrentHoursWorked);
			DailyDownsWorked.Add(CurrentDowns);
			AddSumDayOfWeek(DailyTipData[DailyTotalDollars.Count-1].Date, CurrentTipAmount);
		}
	}
	
	void AddSumDayOfWeek(System.DateTime myDate, float TipAmount)
	{
		switch(myDate.DayOfWeek.ToString())
		{
		case "Sunday":
			DayOfWeekTipAmount[0] += TipAmount;
			break;
		case "Monday":
			DayOfWeekTipAmount[1] += TipAmount;
		break;
		case "Tuesday":
			DayOfWeekTipAmount[2] += TipAmount;
		break;
		case "Wednesday":
			DayOfWeekTipAmount[3] += TipAmount;
		break;
		case "Thursday":
			DayOfWeekTipAmount[4] += TipAmount;
		break;
		case "Friday":
			DayOfWeekTipAmount[5] += TipAmount;
		break;
		case "Saturday":
			DayOfWeekTipAmount[6] += TipAmount;
		break;			
		}
	}
	
	void SumWeeklyValues()
	{
		var AddLastEntry = true;
		// add up tips for each day and put them into a list
		WeeklyTotalDollars = new List<float>();
		WeeklyHoursWorked = new List<float>();
		WeeklyDownsWorked = new List<float>();	
		ZeroValueWeek = 0;
		
		var CurrentTipAmount = 0.0f;
		var CurrentHoursWorked = 0.0f;
		var CurrentDowns = 0.0f;
		
		System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.CurrentCulture;
		int	WeekNum = cul.Calendar.GetWeekOfYear( System.DateTime.Now.Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
		int	NextWeekNum = cul.Calendar.GetWeekOfYear( System.DateTime.Now.Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
		
		// this handles more than 1 entry
		if(DailyTipData.Count > 1)
		{
			for(int i = 0; i < DailyTipData.Count - 1; i++)
			{
				WeekNum = cul.Calendar.GetWeekOfYear( DailyTipData[i].Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
				NextWeekNum = cul.Calendar.GetWeekOfYear( DailyTipData[i+1].Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
	
				CurrentTipAmount = DailyTipData[i].TipAmount - DailyTipData[i].TipOut;
				CurrentHoursWorked = DailyTipData[i].HoursWorked;
				CurrentDowns = DailyTipData[i].NumberOfDowns + DailyTipData[i].NumberOfTournamentDowns;
				CurrentTipAmount += (DailyTipData[i].TournamentDownAmount * DailyTipData[i].NumberOfTournamentDowns);
				
				if(DailyTipData[i].Date.Year == DailyTipData[i+1].Date.Year && 
					WeekNum == NextWeekNum)
				{
					i++;
					for(int d = i; d < DailyTipData.Count; d++)
					{
						NextWeekNum = cul.Calendar.GetWeekOfYear( DailyTipData[d].Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
											
						if(DailyTipData[i].Date.Year == DailyTipData[d].Date.Year &&
							WeekNum == NextWeekNum)
						{
							CurrentTipAmount += DailyTipData[d].TipAmount;
							CurrentTipAmount -= DailyTipData[d].TipOut;
							CurrentHoursWorked += DailyTipData[d].HoursWorked;
							CurrentDowns += (DailyTipData[d].NumberOfDowns + DailyTipData[d].NumberOfTournamentDowns);
							CurrentTipAmount += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
							
						}
						else
						{
							i = d - 1 ;
							d = DailyTipData.Count;
						}
						// hit end of entries
						if (d == DailyTipData.Count - 1)
						{
							i = d;
							AddLastEntry = false;
						}
					}				
				}
				CheckZeroValueWeek(CurrentTipAmount);
				WeeklyTotalDollars.Add(CurrentTipAmount);
				WeeklyHoursWorked.Add(CurrentHoursWorked);
				WeeklyDownsWorked.Add(CurrentDowns);			
			}	
			
			// this handles the last entry in the list
			WeekNum = cul.Calendar.GetWeekOfYear( DailyTipData[DailyTipData.Count -2].Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
			NextWeekNum = cul.Calendar.GetWeekOfYear( DailyTipData[DailyTipData.Count -1].Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
			
			if(DailyTipData.Count > 1 &&
				DailyTipData[DailyTipData.Count -1].Date.Year == DailyTipData[DailyTipData.Count -2].Date.Year &&
				WeekNum == NextWeekNum && 
				AddLastEntry)
			{
				WeeklyTotalDollars[MonthlyTotalDollars.Count-1] += DailyTipData[DailyTipData.Count -1].TipAmount;
				WeeklyTotalDollars[MonthlyTotalDollars.Count-1] -= DailyTipData[DailyTipData.Count -1].TipOut;
				WeeklyHoursWorked[MonthlyHoursWorked.Count-1] += DailyTipData[DailyTipData.Count -1].HoursWorked;
				WeeklyDownsWorked[MonthlyDownsWorked.Count-1] += (DailyTipData[DailyTipData.Count -1].NumberOfDowns + DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns);
				WeeklyTotalDollars[MonthlyTotalDollars.Count-1] += (DailyTipData[DailyTipData.Count -1].TournamentDownAmount * DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns);
			}
			else
			{
				if(DailyTipData.Count > 1 &&
					AddLastEntry)
				{
					CurrentTipAmount = DailyTipData[DailyTipData.Count -1].TipAmount - DailyTipData[DailyTipData.Count -1].TipOut;
					CurrentHoursWorked = DailyTipData[DailyTipData.Count -1].HoursWorked;
					CurrentDowns = DailyTipData[DailyTipData.Count -1].NumberOfDowns + DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns;
					CurrentTipAmount += (DailyTipData[DailyTipData.Count -1].TournamentDownAmount * DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns);
					
					CheckZeroValueWeek(CurrentTipAmount);					
					WeeklyTotalDollars.Add(CurrentTipAmount);
					WeeklyHoursWorked.Add(CurrentHoursWorked);
					WeeklyDownsWorked.Add(CurrentDowns);
				}
			}
		}

		// this handles if there is only one entry
		if(DailyTipData.Count == 1)
		{
			CurrentTipAmount = DailyTipData[0].TipAmount - DailyTipData[0].TipOut;
			CurrentHoursWorked = DailyTipData[0].HoursWorked;
			CurrentDowns = DailyTipData[0].NumberOfDowns + DailyTipData[0].NumberOfTournamentDowns;
			CurrentTipAmount += (DailyTipData[0].TournamentDownAmount * DailyTipData[0].NumberOfTournamentDowns);
			
			CheckZeroValueWeek(CurrentTipAmount);
			WeeklyTotalDollars.Add(CurrentTipAmount);
			WeeklyHoursWorked.Add(CurrentHoursWorked);
			WeeklyDownsWorked.Add(CurrentDowns);
		}		
		
		for (int i = 0; i < WeeklyTotalDollars.Count; i++)
		{
//			print (i.ToString() + " " + WeeklyTotalDollars[i].ToString() );
		}		
	}
	
	void SumMonthlyValues()
	{
		var AddLastEntry = true;
		// add up tips for each day and put them into a list
		MonthlyTotalDollars = new List<float>();
		MonthlyHoursWorked = new List<float>();
		MonthlyDownsWorked = new List<float>();
		
		var CurrentTipAmount = 0.0f;
		var CurrentHoursWorked = 0.0f;
		var CurrentDowns = 0.0f;

		for(int i = 0; i < DailyTipData.Count - 1; i++)
		{
			CurrentTipAmount = DailyTipData[i].TipAmount - DailyTipData[i].TipOut;
			CurrentHoursWorked = DailyTipData[i].HoursWorked;
			CurrentDowns = DailyTipData[i].NumberOfDowns + DailyTipData[i].NumberOfTournamentDowns;
			CurrentTipAmount += (DailyTipData[i].TournamentDownAmount * DailyTipData[i].NumberOfTournamentDowns);
			
			if(DailyTipData[i].Date.Year == DailyTipData[i+1].Date.Year && 
				DailyTipData[i].Date.Month == DailyTipData[i+1].Date.Month)
			{
				i++;
				for(int d = i; d < DailyTipData.Count; d++)
				{
					if(DailyTipData[i].Date.Year == DailyTipData[d].Date.Year &&
						DailyTipData[i].Date.Month == DailyTipData[d].Date.Month)
					{
						CurrentTipAmount += DailyTipData[d].TipAmount;
						CurrentTipAmount -= DailyTipData[d].TipOut;
						CurrentHoursWorked += DailyTipData[d].HoursWorked;
						CurrentDowns += (DailyTipData[d].NumberOfDowns + DailyTipData[d].NumberOfTournamentDowns);
						CurrentTipAmount += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
					}
					else
					{
						i = d - 1 ;
						d = DailyTipData.Count;
					}
					// hit end of entries
					if (d == DailyTipData.Count - 1)
					{
						i = d;
						AddLastEntry = false;
					}
				}
			}
			MonthlyTotalDollars.Add(CurrentTipAmount);
			MonthlyHoursWorked.Add(CurrentHoursWorked);
			MonthlyDownsWorked.Add(CurrentDowns);
		}
		
		// this handles the last entry in the list
		if(DailyTipData.Count > 1 &&
			DailyTipData[DailyTipData.Count -1].Date.Year == DailyTipData[DailyTipData.Count -2].Date.Year &&
			DailyTipData[DailyTipData.Count -1].Date.Month == DailyTipData[DailyTipData.Count -2].Date.Month && 
			AddLastEntry)
		{
			MonthlyTotalDollars[MonthlyTotalDollars.Count-1] += DailyTipData[DailyTipData.Count -1].TipAmount;
			MonthlyTotalDollars[MonthlyTotalDollars.Count-1] -= DailyTipData[DailyTipData.Count -1].TipOut;
			MonthlyHoursWorked[MonthlyHoursWorked.Count-1] += DailyTipData[DailyTipData.Count -1].HoursWorked;
			MonthlyDownsWorked[MonthlyDownsWorked.Count-1] += (DailyTipData[DailyTipData.Count -1].NumberOfDowns + DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns);
			MonthlyTotalDollars[MonthlyTotalDollars.Count-1] += (DailyTipData[DailyTipData.Count -1].TournamentDownAmount * DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns);
		}
		else
		{
			if(DailyTipData.Count > 1 &&
				AddLastEntry)
			{
				CurrentTipAmount = DailyTipData[DailyTipData.Count -1].TipAmount - DailyTipData[DailyTipData.Count -1].TipOut;
				CurrentHoursWorked = DailyTipData[DailyTipData.Count -1].HoursWorked;
				CurrentDowns = DailyTipData[DailyTipData.Count -1].NumberOfDowns + DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns;
				CurrentTipAmount += (DailyTipData[DailyTipData.Count -1].TournamentDownAmount * DailyTipData[DailyTipData.Count -1].NumberOfTournamentDowns);
				
				MonthlyTotalDollars.Add(CurrentTipAmount);
				MonthlyHoursWorked.Add(CurrentHoursWorked);
				MonthlyDownsWorked.Add(CurrentDowns);
			}
		}
		
		// this handles if there is only one entry
		if(DailyTipData.Count == 1)
		{
			CurrentTipAmount = DailyTipData[0].TipAmount - DailyTipData[0].TipOut;
			CurrentHoursWorked = DailyTipData[0].HoursWorked;
			CurrentDowns = DailyTipData[0].NumberOfDowns + DailyTipData[0].NumberOfTournamentDowns;
			CurrentTipAmount += (DailyTipData[0].TournamentDownAmount * DailyTipData[0].NumberOfTournamentDowns);
			
			MonthlyTotalDollars.Add(CurrentTipAmount);
			MonthlyHoursWorked.Add(CurrentHoursWorked);
			MonthlyDownsWorked.Add(CurrentDowns);
		}

		for (int i = 0; i < MonthlyTotalDollars.Count; i++)
		{
//			print (i.ToString() + " " + MonthlyTotalDollars[i].ToString() );
		}		
	}	
	
//-------------------------	
// Day calculations
//-------------------------	

	void CalculateAverageTipsPerDown()
	{
		var TotalTips = 0.0f;
		var TotalDowns = 0.0f;
		for (int i = 0; i < DailyTotalDollars.Count; i++)
		{
			TotalTips += DailyTotalDollars[i];
			TotalDowns += DailyDownsWorked[i];
		}
		if(DailyTotalDollars.Count != 0)
		{
			AverageTipAmountPerDown = (float)Math.Round((TotalTips / TotalDowns) , 2);
		}
		else
		{
			AverageTipAmountPerDay = 0.0f;
		}
	}
	
	void CalculateAverageTipsPerDay()
	{
		var TotalTips = 0.0f;
		
		for (int i = 0; i < DailyTotalDollars.Count; i++)
		{
			TotalTips += DailyTotalDollars[i];
		}
		
		if((DailyTotalDollars.Count - ZeroValueDays) != 0)
		{
			AverageTipAmountPerDay = (float)Math.Round((TotalTips / (DailyTotalDollars.Count - ZeroValueDays)) , 2);
		}
		else
		{
			AverageTipAmountPerDay = 0.0f;
		}
	}

	void CalculateAverageDollarsPerHour()
	{
		var TotalTips = 0.0f;
		var TotalHours = 0.0f;
		if(DailyTotalDollars.Count == DailyHoursWorked.Count)
		{
			for (int i = 0; i < DailyTotalDollars.Count; i++)
			{
				TotalTips += DailyTotalDollars[i];
				TotalHours += DailyHoursWorked[i];
			}
		}
		if(TotalHours != 0)
		{
			AverageTipAmountPerHour =  (float)Math.Round((TotalTips / TotalHours), 2);
		}
		else
		{
			AverageTipAmountPerHour = 0.0f;
		}
	}

	void CalculateAverageDownsPerDay()
	{
		var TotalDowns = 0.0f;
		for (int i = 0; i < DailyDownsWorked.Count; i++)
		{
			TotalDowns += DailyDownsWorked[i];
		}
		if((DailyDownsWorked.Count - ZeroValueDays) != 0)
		{
			AverageDownsPerDay = (float)Math.Round((TotalDowns / (DailyDownsWorked.Count - ZeroValueDays)) , 2);
		}
		else
		{
			AverageDownsPerDay = 0.0f;
		}
	}	

//-------------------------	
// Week calculations
//-------------------------	
	
	
	void CalculateAverageTipsPerWeek()
	{
		var TotalTips = 0.0f;
		for (int i = 0; i < WeeklyTotalDollars.Count; i++)
		{
			TotalTips += WeeklyTotalDollars[i];
		}
		if((WeeklyTotalDollars.Count - ZeroValueWeek) != 0)
		{
			AverageTipAmountPerWeek = (float)Math.Round((TotalTips / (WeeklyTotalDollars.Count - ZeroValueWeek)) , 2);
		}
		else
		{
			AverageTipAmountPerWeek = 0.0f;
		}
	}	

	void CalculateAverageHoursPerWeek()
	{
		var TotalHours = 0.0f;
		for (int i = 0; i < WeeklyHoursWorked.Count; i++)
		{
			TotalHours += WeeklyHoursWorked[i];
		}
		if((WeeklyHoursWorked.Count - ZeroValueWeek) != 0)
		{
			AverageHoursPerWeek = (float)Math.Round((TotalHours / (WeeklyHoursWorked.Count - ZeroValueWeek)) , 2);
		}
		else
		{
			AverageHoursPerWeek = 0.0f;
		}
	}

	void CalculateAverageDownsPerWeek()
	{
		var TotalDowns = 0.0f;
		for (int i = 0; i < WeeklyDownsWorked.Count; i++)
		{
			TotalDowns += WeeklyDownsWorked[i];
		}
		if((WeeklyDownsWorked.Count - ZeroValueWeek) != 0)
		{
			AverageDownsPerWeek = (float)Math.Round((TotalDowns / (WeeklyDownsWorked.Count - ZeroValueWeek)) , 2);
		}
		else
		{
			AverageDownsPerWeek = 0.0f;
		}
	}		
	
//-------------------------	
// month calculations
//-------------------------	
	
	void CalculateAverageTipsPerMonth()
	{
		var TotalTips = 0.0f;
		for (int i = 0; i < MonthlyTotalDollars.Count; i++)
		{
			TotalTips += MonthlyTotalDollars[i];
		}
		if(MonthlyTotalDollars.Count != 0)
		{
			AverageTipAmountPerMonth = (float)Math.Round((TotalTips / MonthlyTotalDollars.Count) , 2);
		}
		else
		{
			AverageTipAmountPerMonth = 0.0f;
		}
	}	

	void CalculateAverageHoursPerMonth()
	{
		var TotalHours = 0.0f;
		for (int i = 0; i < MonthlyHoursWorked.Count; i++)
		{
			TotalHours += MonthlyHoursWorked[i];
		}
		if(MonthlyHoursWorked.Count != 0)
		{
			AverageHoursPerMonth = (float)Math.Round((TotalHours / MonthlyHoursWorked.Count) , 2);
		}
		else
		{
			AverageHoursPerMonth = 0.0f;
		}
	}	
	
	void CalculateAverageDownsPerMonth()
	{
		var TotalDowns = 0.0f;
		for (int i = 0; i < MonthlyDownsWorked.Count; i++)
		{
			TotalDowns += MonthlyDownsWorked[i];
		}
		if(MonthlyDownsWorked.Count != 0)
		{
			AverageDownsPerMonth = (float)Math.Round((TotalDowns / MonthlyDownsWorked.Count) , 2);
		}
		else
		{
			AverageDownsPerMonth = 0.0f;
		}
	}		
	
	void CalculateWeeklyTotals()
	{
		var CurrentTipAmount = 0.0f;
		var CurrentHoursWorked = 0.0f;
		var CurrentDowns = 0.0f;

		System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.CurrentCulture;
		int	WeekNumCurrent = cul.Calendar.GetWeekOfYear( System.DateTime.Now.Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
		int	WeekNumData = cul.Calendar.GetWeekOfYear( System.DateTime.Now.Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
		
		for(int i = 0; i < DailyTipData.Count ; i++)
		{
			WeekNumCurrent = cul.Calendar.GetWeekOfYear( System.DateTime.Now.Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
			WeekNumData = cul.Calendar.GetWeekOfYear( DailyTipData[i].Date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);

			if(DailyTipData[i].Date.Year == System.DateTime.Now.Year && 
				WeekNumCurrent == WeekNumData)
			{
				CurrentTipAmount += DailyTipData[i].TipAmount;
				CurrentTipAmount -= DailyTipData[i].TipOut;
				CurrentHoursWorked += DailyTipData[i].HoursWorked;
				CurrentDowns += (DailyTipData[i].NumberOfDowns + DailyTipData[i].NumberOfTournamentDowns);
				CurrentTipAmount += (DailyTipData[i].TournamentDownAmount * DailyTipData[i].NumberOfTournamentDowns);
			}
		}	
		TotalTipAmountThisWeek = CurrentTipAmount;
	}	
	
	void CalculateMonthlyTotals()
	{
		var CurrentTipAmount = 0.0f;
		var CurrentHoursWorked = 0.0f;
		var CurrentDowns = 0.0f;

		for(int i = 0; i < DailyTipData.Count ; i++)
		{
			if(DailyTipData[i].Date.Year == System.DateTime.Now.Year && 
				DailyTipData[i].Date.Month == System.DateTime.Now.Month)
			{
				CurrentTipAmount += DailyTipData[i].TipAmount;
				CurrentTipAmount -= DailyTipData[i].TipOut;
				CurrentHoursWorked += DailyTipData[i].HoursWorked;
				CurrentDowns += (DailyTipData[i].NumberOfDowns + DailyTipData[i].NumberOfTournamentDowns);
				CurrentTipAmount += (DailyTipData[i].TournamentDownAmount * DailyTipData[i].NumberOfTournamentDowns);
			}
		}	
		TotalTipAmountThisMonth = CurrentTipAmount;
	}
	
	void CalculateYearTotals()
	{
		var CurrentTipAmount = 0.0f;
		var CurrentHoursWorked = 0.0f;
		var CurrentDowns = 0.0f;

		for(int i = 0; i < DailyTipData.Count ; i++)
		{
			if(DailyTipData[i].Date.Year == System.DateTime.Now.Year)
			{
				CurrentTipAmount += DailyTipData[i].TipAmount;
				CurrentTipAmount -= DailyTipData[i].TipOut;
				CurrentHoursWorked += DailyTipData[i].HoursWorked;
				CurrentDowns += (DailyTipData[i].NumberOfDowns + DailyTipData[i].NumberOfTournamentDowns);
				CurrentTipAmount += (DailyTipData[i].TournamentDownAmount * DailyTipData[i].NumberOfTournamentDowns);
			}
		}	
		TotalTipAmountThisYear = CurrentTipAmount;
	}
	
	void CalculateBestWorstDayToWork()
	{
		var MinValue = 999999999.0f;
		var MaxValue = 0.0f;
		var CurrentMaxIndex = 0;
		var CurrentMinIndex = 0;
		
		for(int i = 0; i < DayOfWeekTipAmount.Length ; i++)
		{
			if (DayOfWeekTipAmount[i] > MaxValue)
			{
				MaxValue = DayOfWeekTipAmount[i];
				CurrentMaxIndex = i;
			}
			if (DayOfWeekTipAmount[i] < MinValue && DayOfWeekTipAmount[i] != 0)
			{
				MinValue = DayOfWeekTipAmount[i];
				CurrentMinIndex = i;
			}
		}
		
		switch(CurrentMinIndex.ToString())
		{
		case "0":
			WorstDayToWork = "Sunday";
			break;
		case "1":
			WorstDayToWork = "Monday";
		break;
		case "2":
			WorstDayToWork = "Tuesday";
		break;
		case "3":
			WorstDayToWork = "Wednesday";
		break;
		case "4":
			WorstDayToWork = "Thursday";
		break;
		case "5":
			WorstDayToWork = "Friday";
		break;
		case "6":
			WorstDayToWork = "Saturday";
		break;	
		}
			
		switch(CurrentMaxIndex.ToString())
		{
		case "0":
			BestDayToWork = "Sunday";
			break;
		case "1":
			BestDayToWork = "Monday";
		break;
		case "2":
			BestDayToWork = "Tuesday";
		break;
		case "3":
			BestDayToWork = "Wednesday";
		break;
		case "4":
			BestDayToWork = "Thursday";
		break;
		case "5":
			BestDayToWork = "Friday";
		break;
		case "6":
			BestDayToWork = "Saturday";
		break;				
		}	
	}
}
