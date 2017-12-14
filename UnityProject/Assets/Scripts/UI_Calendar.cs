using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GUITexture))]
public class UI_Calendar : MonoBehaviour {
	
	public bool Enabled = true;
	public GameObject UIManager;
	public GameObject UserBlobManager;
	public List<TipData> DailyTipData = new List<TipData>(); 
	
	public GUISkin CurrentSkin;
	
	public Texture2D BackgroundBitmap;	
	public Texture2D styleCalendarButtonsBitmap;	
	public Texture2D styleCalendarButtonsActiveBitmap;	
	public Texture2D styleCalendarButtonsTodayBitmap;	
	public Texture2D style2XButtonBlueBitmap;
	public Texture2D style2XButtonBlueActiveBitmap;
	public Texture2D styleCalendarLeftArrowButtonBitmap;
	public Texture2D styleCalendarRightArrowButtonBitmap;
	public Texture2D styleCalendarWorkDayBitmap;
	
    public int selGridInt = 0;
    private string[] selStrings = new string[42];
	private System.DateTime currentDate = new System.DateTime(); 
	private string CurrentMonthString = "";
	public int AddMonths = 0;
	private int StartDay = 0;
	private int EndDay = 0;
	private int CalendarViewState = 0;
	private string CalendarViewString = "Tips";
	
	private float[] CalendarWeeklyTotal = new float[6];
	private float[] CalendarWeeklyTotalDeltaPercent = new float[6];
	private float[] CalendarDailyTotal = new float[42];
	private string[] CalendarDailyTotalString = new string[42];
	private float[] CalendarDayOfWeekTotal = new float[7];
	
	private float[] WeeklyTotalDollars = new float[6];
	private float[] WeeklyTotalDollarsDeltaPercent = new float[6];
	private float[] DailyTotalDollars = new float[42];
	private float[] DayOfWeekTotalDollars = new float[7];

	private float[] WeeklyTotalPayCheck = new float[6];
	private float[] WeeklyTotalPayCheckDeltaPercent = new float[6];
	private float[] DailyTotalPayCheck = new float[42];
	private float[] DayOfWeekTotalPayCheck = new float[7];
	
	private float[] WeeklyTotalHours = new float[6];
	private float[] WeeklyTotalHoursDeltaPercent = new float[6];
	private float[] DailyTotalHours = new float[42];
	private float[] DayOfWeekTotalHours = new float[7];

	private float[] WeeklyTotalDowns = new float[6];
	private float[] WeeklyTotalDownsDeltaPercent = new float[6];
	private float[] DailyTotalDowns = new float[42];
	private float[] DayOfWeekTotalDowns = new float[7];

	private float[] WeeklyTotalTournamentDowns = new float[6];
	private float[] WeeklyTotalTournamentDownsDeltaPercent = new float[6];
	private float[] DailyTotalTournamentDowns = new float[42];
	private float[] DayOfWeekTotalTournamentDowns = new float[7];
	
	private float[] WeeklyTotalStartTime = new float[6];
	private float[] WeeklyTotalStartTimeDeltaPercent = new float[6];
	private System.DateTime[] DailyTotalStartTime = new System.DateTime[42];
	private float[] DayOfWeekTotalStartTime = new float[7];
	

	private string[] DayLabels = new string[42];
	private string[] DailyTournamentLabels = new string[42];
	private string[] DailyPayCheckLabels = new string[42];
	private bool[] WorkDayLabels = new bool[42];
	
	private float CalendarMonthlyTotal = 0.0f;
	private float MonthlyTotalDollars = 0.0f;
	private float MonthlyTotalPayCheck = 0.0f;
	private float MonthlyTotalHours = 0.0f;
	private float MonthlyTotalDowns = 0.0f;
	private float MonthlyTotalTournamentDowns = 0.0f;
	private float MonthlyTotalStartTime = 0.0f;
	
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
	
	public Font myFont;
	public int myFontSize = 30;
	private float AspectRatioMultiplier = 0.57f;
	
	public float SwipeOffset = 0.0f;
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}	
	
	void Start()
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		DailyTipData = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData;
		
		selGridInt = selStrings.Length;
		currentDate = System.DateTime.Now;
//		print (currentDate);

		styleDailyTotals = new GUIStyle();

		AspectRatioMultiplier = ((float)Screen.width/1200 );
		
		UpdateCalendar();
	}
	
	public void GUIEnabled(bool myValue)
	{
		Enabled = myValue;
		this.GetComponent<GUITexture>().enabled = myValue;
	}

	void OnGUI() 
	{
		if(Enabled == true)
		{
			FontColor = UserBlobManager.GetComponent<UserBlobManager>().FontColor;
			ButtonColor = UserBlobManager.GetComponent<UserBlobManager>().ButtonColor;
			BackgroundColor =  UserBlobManager.GetComponent<UserBlobManager>().BackGroundColor;
			styleCalendarRightArrowButtonBitmap = UserBlobManager.GetComponent<UserBlobManager>().CalendarRightArrowButton;
			styleCalendarLeftArrowButtonBitmap = UserBlobManager.GetComponent<UserBlobManager>().CalendarLeftArrowButton;
				
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
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().CalendarBackground;
			
//			GUI.DrawTexture((new Rect(0,0,Screen.width,Screen.height)), BackgroundBitmap,ScaleMode.StretchToFill,true);

			// time and month and year		
//			GUI.Label(new Rect((Screen.width * 0.43f), (Screen.height* 0.005f), 100, 20), (UIManager.GetComponent<UI_Manager>().shit), styleBigger);
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);

			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.5f), (Screen.height* 0.045f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CurrentMonthString + " " + currentDate.Year.ToString()), style2X);

			// day of week		
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.03f), (Screen.height * 0.1f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Sun", styleBigger);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.16f), (Screen.height * 0.1f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Mon", styleBigger);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.29f), (Screen.height * 0.1f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Tue", styleBigger);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.41f), (Screen.height * 0.1f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Wed", styleBigger);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.54f), (Screen.height * 0.1f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Thu", styleBigger);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.68f), (Screen.height * 0.1f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Fri", styleBigger);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.79f), (Screen.height * 0.1f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Sat", styleBigger);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.91f), (Screen.height * 0.1f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Total", styleBigger);
			
			// grid of days		
			selGridInt = GUI.SelectionGrid(new Rect(SwipeOffset + 0, (Screen.height* 0.14f), (Screen.width * 7/8), (Screen.height/2)), selGridInt, selStrings, 7, styleCalendarButtons);
			
			var Today = System.DateTime.Now;

			for(int i = StartDay; i < (StartDay + EndDay); i++ )
			{
				var Xpos = (int)(i % 7) * (Screen.width / 8.0f)  ;
				var Ypos = (int)(((i / 7) - 1 ) * (Screen.height* 0.085f)) ;
				
				// draw today background
				if(i == (StartDay + Today.Day - 1) && currentDate.Month == Today.Month && currentDate.Year == Today.Year)
				{
					GUI.DrawTexture((new Rect(SwipeOffset + Xpos + (Screen.width * 0.002f ),Ypos + (Screen.height * 0.2215f),(Screen.width * 1/8),(Screen.height/12.5f))), styleCalendarButtonsTodayBitmap,ScaleMode.StretchToFill,true);
//					GUI.Label(new Rect(SwipeOffset + Xpos + (Screen.width * 0.08f),Ypos + (Screen.height * 0.28f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), DayLabels[i], styleWhite);
					GUI.Label(new Rect(SwipeOffset + Xpos + (Screen.width * 0.01f),Ypos + (Screen.height * 0.225f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), DayLabels[i], styleWhite);
				}
				else
				{
					if(DayLabels[i] != null)
					{
//						GUI.Label(new Rect(SwipeOffset + Xpos + (Screen.width * 0.08f),Ypos + (Screen.height * 0.28f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), DayLabels[i], style);
						GUI.Label(new Rect(SwipeOffset + Xpos + (Screen.width * 0.01f),Ypos + (Screen.height * 0.225f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), DayLabels[i], style);
					}
				}		
				
				// draw workday labels
				if(WorkDayLabels[i] == true)
				{
//					print ("draw work day");
					GUI.DrawTexture((new Rect(SwipeOffset + Xpos + (Screen.width * 0.00f ),Ypos + (Screen.height * 0.2225f),(Screen.width * 1/8),(Screen.height/13.0f))), styleCalendarWorkDayBitmap ,ScaleMode.StretchToFill,true);
				}
			}		
			
			// was the button that was pressed a vaid day
			if(selGridInt < selStrings.Length)
			{
				var ButtonPressed = (int)(selGridInt - StartDay + 1);
				if (ButtonPressed > 0 &&  ButtonPressed <= EndDay && UIManager.GetComponent<UI_Manager>().CouldBeSwipe == false)
				{
					currentDate = new System.DateTime(currentDate.Year, currentDate.Month, ButtonPressed, currentDate.Hour, currentDate.Minute, currentDate.Second);
//					print("pressed " + currentDate.Month.ToString() + " " + ButtonPressed.ToString() + " " + currentDate.Year.ToString());
					UIManager.GetComponent<UI_Manager>().SetCurrentDate(currentDate);  // pass current date to UIManager
					UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
				}
				selGridInt = selStrings.Length;
			}
			
			// month buttons		
	        if (GUI.Button(new Rect((Screen.width * 0.00f), (Screen.height* 0.0f), (Screen.width * 0.14f), (Screen.height* 0.09f)), styleCalendarLeftArrowButtonBitmap, style2XButtonBlue))
			{
				AddMonths -=1;
				UpdateCalendar();
			}
	
			if (GUI.Button(new Rect((Screen.width * 0.86f), (Screen.height* 0.0f), (Screen.width * 0.14f ), (Screen.height* 0.09f)), styleCalendarRightArrowButtonBitmap, style2XButtonBlue))
			{
				AddMonths +=1;
				UpdateCalendar();
			}
			
			// daily total labels
			for(int i = StartDay; i < (StartDay + EndDay); i++ )
			{
				var Xpos = (int)(i % 7) * (Screen.width / 8.0f)  ;
				var Ypos = (int)(((i / 7) - 1 ) * (Screen.height* 0.085f)) ;

//				if(CalendarDailyTotal[i] != 0.0f)
				if(CalendarDailyTotalString[i] != "")
				{
//					GUI.Label(new Rect(SwipeOffset + Xpos + (Screen.width * 0.01f),Ypos + (Screen.height * 0.24f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), CalendarDailyTotal[i].ToString(), styleBigger);
					GUI.Label(new Rect(SwipeOffset + Xpos + (Screen.width * 0.01f),Ypos + (Screen.height * 0.255f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), CalendarDailyTotalString[i], styleDailyTotals);
				}
				if(DailyPayCheckLabels[i] != "")
				{
					GUI.Label(new Rect(SwipeOffset + Xpos + (Screen.width * 0.085f),Ypos + (Screen.height * 0.225f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), DailyPayCheckLabels[i], styleGreen);
				}
				if(DailyTournamentLabels[i] == "$T")
				{
					GUI.Label(new Rect(SwipeOffset + Xpos + (Screen.width * 0.05f),Ypos + (Screen.height * 0.225f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), DailyTournamentLabels[i], styleGreen);
				}
				if(DailyTournamentLabels[i] == "-T")
				{
					GUI.Label(new Rect(SwipeOffset + Xpos + (Screen.width * 0.05f),Ypos + (Screen.height * 0.225f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), DailyTournamentLabels[i], styleRed);
				}
				
			}		
			
			if (CalendarViewState != 5)
			{
				// weekly total labels
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.88f), (Screen.height * 0.165f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotal[0].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.88f), (Screen.height * 0.25f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotal[1].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.88f), (Screen.height * 0.33f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotal[2].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.88f), (Screen.height * 0.415f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotal[3].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.88f), (Screen.height * 0.50f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotal[4].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.88f), (Screen.height * 0.58f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotal[5].ToString()), styleDailyTotals);
	
				// weekly total delta percent labels
				if(CalendarWeeklyTotalDeltaPercent[0] > 0)
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.20f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[0].ToString() + "%"), styleGreen);
				}
				else
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.20f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[0].ToString() + "%"), styleRed);
				}
				if(CalendarWeeklyTotalDeltaPercent[1] > 0)
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.28f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[1].ToString() + "%"), styleGreen);
				}
				else
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.28f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[1].ToString() + "%"), styleRed);
				}
				if(CalendarWeeklyTotalDeltaPercent[2] > 0)
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.37f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[2].ToString() + "%"), styleGreen);
				}
				else
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.37f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[2].ToString() + "%"), styleRed);
				}
				if(CalendarWeeklyTotalDeltaPercent[3] > 0)
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.45f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[3].ToString() + "%"), styleGreen);
				}
				else
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.45f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[3].ToString() + "%"), styleRed);
				}
				if(CalendarWeeklyTotalDeltaPercent[4] > 0)
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.53f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[4].ToString() + "%"), styleGreen);
				}
				else
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.53f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[4].ToString() + "%"), styleRed);
				}
				if(CalendarWeeklyTotalDeltaPercent[5] > 0)
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.62f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[5].ToString() + "%"), styleGreen);
				}
				else
				{
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.90f), (Screen.height * 0.62f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarWeeklyTotalDeltaPercent[5].ToString() + "%"), styleRed);
				}
				
				// day of week total labels
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.02f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarDayOfWeekTotal[0].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.15f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarDayOfWeekTotal[1].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.27f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarDayOfWeekTotal[2].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.39f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarDayOfWeekTotal[3].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.52f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarDayOfWeekTotal[4].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.65f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarDayOfWeekTotal[5].ToString()), styleDailyTotals);
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.77f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CalendarDayOfWeekTotal[6].ToString()), styleDailyTotals);
				
				// monthly total labels
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.88f), (Screen.height * 0.65f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), CalendarMonthlyTotal.ToString(), styleDailyTotals);
				
			}
			
			// view button
			GUI.Label(new Rect((Screen.width * 0.50f), (Screen.height * 0.70f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Current View:", styleBigger);
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.73f), (Screen.width * 0.50f), (Screen.height * 0.05f)) ,CalendarViewString, style2XButtonBlue))
			{
				CalendarViewState +=1;
				UpdateCalendarViewState();
			}

			// statistics button
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.79f), (Screen.width * 0.50f), (Screen.height * 0.05f)) ,"Income Statistics", style2XButtonBlue))
			{
				UIManager.GetComponent<UI_Manager>().SwitchStates("StatisticsState");
			}
			
			// expenses button
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.85f), (Screen.width * 0.50f), (Screen.height * 0.05f)) ,"Expenses", style2XButtonBlue))
			{
				UIManager.GetComponent<UI_Manager>().SwitchStates("ExpensesState");
				
			}
			
			// settings button
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.91f), (Screen.width * 0.50f), (Screen.height * 0.05f)) ,"Settings", style2XButtonBlue))
			{
				UIManager.GetComponent<UI_Manager>().SwitchStates("SettingsState");
			}
		}
    }
	
	public void ResetCalendarData()
	{
		DailyTipData = new List<TipData>();
		DailyTipData = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData;
		UpdateCalendar();
	}
	
	string DateTimeToTimeString(System.DateTime myDateTime)
	{
		var newHour = myDateTime.Hour;
		var newMinute = myDateTime.Minute.ToString();
		var newAMPM = "a";
		
		if (newHour > 12)
		{
			newHour = newHour - 12;
			newAMPM = "p";
		}
		
		if (newMinute.ToString() ==  "0")
		{
			newMinute = "00";
		}

		var newDateTimeString = newHour.ToString() + ":" + newMinute + newAMPM;
		return newDateTimeString;
	}
	
	void UpdateCalendarViewState()
	{
		if (CalendarViewState > 5)
		{
			CalendarViewState = 0;
		}		

		CalendarWeeklyTotal = new float[6];
		CalendarWeeklyTotalDeltaPercent = new float[6];
		CalendarDailyTotal = new float[42];
		CalendarDailyTotalString = new string[42];
		CalendarDayOfWeekTotal = new float[7];	
		
		switch(CalendarViewState)
		{
		case 0:
			CalendarViewString = "Tips & Tournaments";
			CalendarWeeklyTotal = WeeklyTotalDollars;
			CalendarWeeklyTotalDeltaPercent = WeeklyTotalDollarsDeltaPercent;
			CalendarDailyTotal = DailyTotalDollars;
			CalendarDayOfWeekTotal = DayOfWeekTotalDollars;	
			CalendarMonthlyTotal = MonthlyTotalDollars;
			styleDailyTotals.fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			break;
		case 1:
			CalendarViewString = "Pay Checks";
			CalendarWeeklyTotal = WeeklyTotalPayCheck;
			CalendarWeeklyTotalDeltaPercent = WeeklyTotalPayCheckDeltaPercent;
			CalendarDailyTotal = DailyTotalPayCheck;
			CalendarDayOfWeekTotal = DayOfWeekTotalPayCheck;	
			CalendarMonthlyTotal = MonthlyTotalPayCheck;
			styleDailyTotals.fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			break;
		case 2:
			CalendarViewString = "Hours Worked";
			CalendarWeeklyTotal = WeeklyTotalHours;
			CalendarWeeklyTotalDeltaPercent = WeeklyTotalHoursDeltaPercent;
			CalendarDailyTotal = DailyTotalHours;
			CalendarDayOfWeekTotal = DayOfWeekTotalHours;	
			CalendarMonthlyTotal = MonthlyTotalHours;
			styleDailyTotals.fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			break;
		case 3:
			CalendarViewString = "Live Game Downs";
			CalendarWeeklyTotal = WeeklyTotalDowns;
			CalendarWeeklyTotalDeltaPercent = WeeklyTotalDownsDeltaPercent;
			CalendarDailyTotal = DailyTotalDowns;
			CalendarDayOfWeekTotal = DayOfWeekTotalDowns;	
			CalendarMonthlyTotal = MonthlyTotalDowns;
			styleDailyTotals.fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			break;
		case 4:
			CalendarViewString = "Tournament Downs";
			CalendarWeeklyTotal = WeeklyTotalTournamentDowns;
			CalendarWeeklyTotalDeltaPercent = WeeklyTotalTournamentDownsDeltaPercent;
			CalendarDailyTotal = DailyTotalTournamentDowns;
			CalendarDayOfWeekTotal = DayOfWeekTotalTournamentDowns;	
			CalendarMonthlyTotal = MonthlyTotalTournamentDowns;
			styleDailyTotals.fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			break;
		case 5:
			CalendarViewString = "Work Schedule";
			CalendarWeeklyTotal = WeeklyTotalStartTime;
			CalendarWeeklyTotalDeltaPercent = WeeklyTotalStartTimeDeltaPercent;
//			CalendarDailyTotal = DailyTotalStartTime;
			CalendarDayOfWeekTotal = DayOfWeekTotalStartTime;	
			CalendarMonthlyTotal = MonthlyTotalStartTime;
			styleDailyTotals.fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			break;
		}

		// convert daily totals to a string	and format it
		for(int i = 0; i < CalendarDailyTotalString.Length; i++)
		{
			
			switch(CalendarViewState)
			{
			case 0:
				if(CalendarDailyTotal[i] != 0)
				{
					CalendarDailyTotalString[i] = "$" + CalendarDailyTotal[i].ToString();
				}
				else
				{
					CalendarDailyTotalString[i] = "";
				}
				break;
			case 1:
				if(CalendarDailyTotal[i] != 0)
				{
					CalendarDailyTotalString[i] = "$" + CalendarDailyTotal[i].ToString();
				}
				else
				{
					CalendarDailyTotalString[i] = "";
				}
				break;				
			case 2:
				if(CalendarDailyTotal[i] != 0)
				{
					CalendarDailyTotalString[i] = CalendarDailyTotal[i].ToString() + " h";
				}
				else
				{
					CalendarDailyTotalString[i] = "";
				}
				break;
			case 3:
				if(CalendarDailyTotal[i] != 0)
				{
					CalendarDailyTotalString[i] = CalendarDailyTotal[i].ToString() + " ld";
				}
				else
				{
					CalendarDailyTotalString[i] = "";
				}
				break;
			case 4:
				if(CalendarDailyTotal[i] != 0)
				{
					CalendarDailyTotalString[i] = CalendarDailyTotal[i].ToString() + " td";
				}
				else
				{
					CalendarDailyTotalString[i] = "";
				}
				break;				
			case 5:
				if(DailyTotalStartTime[i].Hour != 0)
				{				
					CalendarDailyTotalString[i] = DateTimeToTimeString(DailyTotalStartTime[i]);
//					CalendarDailyTotalString[i] = (DailyTotalStartTime[i].Hour.ToString() + ":" + DailyTotalStartTime[i].Minute.ToString());
				}
				else
				{
					CalendarDailyTotalString[i] = "";
				}
				break;
			}			
		}
	}

	
	public void UpdateCalendar()
	{
		selStrings = new string[42];
		DayLabels = new string[42];
		
		currentDate = System.DateTime.Now.AddMonths(AddMonths);
		switch(currentDate.Month)
		{
		case 1:
			CurrentMonthString = "January";
		break;
		case 2:
			CurrentMonthString = "February";
		break;
		case 3:
			CurrentMonthString = "March";
		break;
		case 4:
			CurrentMonthString = "April";
		break;
		case 5:
			CurrentMonthString = "May";
		break;
		case 6:
			CurrentMonthString = "June";
		break;
		case 7:
			CurrentMonthString = "July";
		break;
		case 8:
			CurrentMonthString = "August";
		break;
		case 9:
			CurrentMonthString = "September";
		break;
		case 10:
			CurrentMonthString = "October";
		break;
		case 11:
			CurrentMonthString = "November";
		break;
		case 12:
			CurrentMonthString = "December";
		break;
		
		}
		
		int delta = Convert.ToInt32(currentDate.Day);
		DateTime FirstDayOfMonth = currentDate.AddDays(-delta + 1);
//		print (currentDate);
		
		StartDay = 0;
		
		switch(FirstDayOfMonth.DayOfWeek.ToString())
		{
		case "Sunday":
			StartDay = 0;
		break;
		case "Monday":
			StartDay = 1;
		break;
		case "Tuesday":
			StartDay = 2;
		break;
		case "Wednesday":
			StartDay = 3;
		break;
		case "Thursday":
			StartDay = 4;
		break;
		case "Friday":
			StartDay = 5;
		break;
		case "Saturday":
			StartDay = 6;
		break;
		}
		
		EndDay = System.DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
		
		var count = 1;
		for(int i = StartDay; i < (StartDay + EndDay); i++ )
		{
			selStrings[i] = ""; //(count.ToString());
			DayLabels[i] = (count.ToString());
			count +=1;
		}
		selGridInt = selStrings.Length;		
		
		UpdateWorkDayLabels();
		
		UpdateDailyTotals();
		UpdateWeeklyTotals();
		UpdateWeeklyTotalsDeltaPercent();
		UpdateDayOfWeekTotals();
		UpdateMonthlyTotals();
		
		UpdateCalendarViewState();
	}

	void UpdateWorkDayLabels()
	{
		WorkDayLabels = new bool[42];
		
		for(int d = 0; d < DailyTipData.Count; d++)
		{
			// make sure data is for the current month
			if(DailyTipData[d].Date.Year == currentDate.Year && DailyTipData[d].Date.Month == currentDate.Month)
			{
				var temp = DailyTipData[d].Date.Day + StartDay - 1;
				if (DailyTipData[d].WorkDay)
				{
					WorkDayLabels[temp] = true;
				}
			}
		}
	}

	void UpdateDailyTotals()
	{
		DailyTotalDollars = new float[42];
		DailyTotalPayCheck = new float[42];
		DailyPayCheckLabels = new string[42];
		DailyTournamentLabels = new string[42];
		DailyTotalHours = new float[42];
		DailyTotalDowns = new float[42];
		DailyTotalTournamentDowns = new float[42];
		DailyTotalStartTime = new System.DateTime[42];
		
		for(int d = 0; d < DailyTipData.Count; d++)
		{
			// make sure data is for the current month
			if(DailyTipData[d].Date.Year == currentDate.Year && DailyTipData[d].Date.Month == currentDate.Month)
			{
				var temp = DailyTipData[d].Date.Day + StartDay - 1;
				DailyTotalDollars[temp] += DailyTipData[d].TipAmount;
//				DailyTotalDollars[temp] += DailyTipData[d].PayCheckAmount; // add paycheck to daily amount
				DailyTotalDollars[temp] -= DailyTipData[d].TipOut;
				DailyTotalPayCheck[temp] += DailyTipData[d].PayCheckAmount;
	
				if(DailyTipData[d].PayCheckAmount != 0)
				{
					DailyPayCheckLabels[temp] = " P";
				}
				
				if (DailyTipData[d].NumberOfTournamentDowns != 0 || DailyTipData[d].TournamentDownAmount != 0 )
				{
					DailyTournamentLabels[temp] = "-T";
				}	
				if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )
				{
					DailyTournamentLabels[temp] = "$T";
					DailyTotalDollars[temp] += (DailyTipData[d].NumberOfTournamentDowns * DailyTipData[d].TournamentDownAmount);
				}
				
				
				
				DailyTotalHours[temp] += DailyTipData[d].HoursWorked;
				DailyTotalDowns[temp] += DailyTipData[d].NumberOfDowns;
				DailyTotalTournamentDowns[temp] += DailyTipData[d].NumberOfTournamentDowns;
				
				
				// find the earliest non 0 start time
				if(System.Convert.ToDouble(DailyTipData[d].WorkDayStartTime.Hour) >= System.Convert.ToDouble(DailyTotalStartTime[temp].Hour) )
				{
					if( System.Convert.ToDouble(DailyTotalStartTime[temp].Hour) == 0 )
					{
						DailyTotalStartTime[temp] = DailyTipData[d].WorkDayStartTime;
					}
				}
				else
				{
					DailyTotalStartTime[temp] = DailyTipData[d].WorkDayStartTime;
				}
			}
		}
	}
	
	void UpdateDayOfWeekTotals()
	{
		DayOfWeekTotalDollars = new float[7];
		DayOfWeekTotalPayCheck = new float[7];
		DayOfWeekTotalHours = new float[7];
		DayOfWeekTotalDowns = new float[7];
		DayOfWeekTotalTournamentDowns = new float[7];
		DayOfWeekTotalStartTime = new float[7];
		
		for(int d = 0; d < DailyTipData.Count; d++)
		{
			// make sure data is for the current month
			if(DailyTipData[d].Date.Year == currentDate.Year && DailyTipData[d].Date.Month == currentDate.Month)
			{
				// Sunday				
				for(int i = 0; i < 42; i+=7 )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						DayOfWeekTotalDollars[0] += DailyTipData[d].TipAmount;
//						DayOfWeekTotalDollars[0] += DailyTipData[d].PayCheckAmount; // add paycheck to daily amount
						DayOfWeekTotalDollars[0] -= DailyTipData[d].TipOut;
						DayOfWeekTotalPayCheck[0] += DailyTipData[d].PayCheckAmount;

						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )							
						{
							DayOfWeekTotalDollars[0] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}	
						DayOfWeekTotalHours[0] += DailyTipData[d].HoursWorked;
						DayOfWeekTotalDowns[0] += DailyTipData[d].NumberOfDowns;
						DayOfWeekTotalTournamentDowns[0] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}
				// Monday			
				for(int i = 1; i < 42; i+=7 )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						DayOfWeekTotalDollars[1] += DailyTipData[d].TipAmount;
//						DayOfWeekTotalDollars[1] += DailyTipData[d].PayCheckAmount; // add paycheck to daily amount
						DayOfWeekTotalDollars[1] -= DailyTipData[d].TipOut;
						DayOfWeekTotalPayCheck[1] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							DayOfWeekTotalDollars[1] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}	
						DayOfWeekTotalHours[1] += DailyTipData[d].HoursWorked;
						DayOfWeekTotalDowns[1] += DailyTipData[d].NumberOfDowns;
						DayOfWeekTotalTournamentDowns[1] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}				
				// Tuesday			
				for(int i = 2; i < 42; i+=7 )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						DayOfWeekTotalDollars[2] += DailyTipData[d].TipAmount;
//						DayOfWeekTotalDollars[2] += DailyTipData[d].PayCheckAmount; // add paycheck to daily amount
						DayOfWeekTotalDollars[2] -= DailyTipData[d].TipOut;
						DayOfWeekTotalPayCheck[2] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							DayOfWeekTotalDollars[2] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}						
						DayOfWeekTotalHours[2] += DailyTipData[d].HoursWorked;
						DayOfWeekTotalDowns[2] += DailyTipData[d].NumberOfDowns;
						DayOfWeekTotalTournamentDowns[2] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}				
				// Wednesday			
				for(int i = 3; i < 42; i+=7 )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						DayOfWeekTotalDollars[3] += DailyTipData[d].TipAmount;
//						DayOfWeekTotalDollars[3] += DailyTipData[d].PayCheckAmount; // add paycheck to daily amount
						DayOfWeekTotalDollars[3] -= DailyTipData[d].TipOut;
						DayOfWeekTotalPayCheck[3] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							DayOfWeekTotalDollars[3] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}		
						DayOfWeekTotalHours[3] += DailyTipData[d].HoursWorked;
						DayOfWeekTotalDowns[3] += DailyTipData[d].NumberOfDowns;
						DayOfWeekTotalTournamentDowns[3] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}				
				// Thursday			
				for(int i = 4; i < 42; i+=7 )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						DayOfWeekTotalDollars[4] += DailyTipData[d].TipAmount;
//						DayOfWeekTotalDollars[4] += DailyTipData[d].PayCheckAmount; // add paycheck to daily amount
						DayOfWeekTotalDollars[4] -= DailyTipData[d].TipOut;
						DayOfWeekTotalPayCheck[4] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							DayOfWeekTotalDollars[4] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}		
						DayOfWeekTotalHours[4] += DailyTipData[d].HoursWorked;
						DayOfWeekTotalDowns[4] += DailyTipData[d].NumberOfDowns;
						DayOfWeekTotalTournamentDowns[4] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}				
				// Friday			
				for(int i = 5; i < 42; i+=7 )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						DayOfWeekTotalDollars[5] += DailyTipData[d].TipAmount;
//						DayOfWeekTotalDollars[5] += DailyTipData[d].PayCheckAmount; // add paycheck to daily amount
						DayOfWeekTotalDollars[5] -= DailyTipData[d].TipOut;
						DayOfWeekTotalPayCheck[5] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							DayOfWeekTotalDollars[5] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}		
						DayOfWeekTotalHours[5] += DailyTipData[d].HoursWorked;
						DayOfWeekTotalDowns[5] += DailyTipData[d].NumberOfDowns;
						DayOfWeekTotalTournamentDowns[5] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}				
				// Saturday			
				for(int i = 6; i < 42; i+=7 )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						DayOfWeekTotalDollars[6] += DailyTipData[d].TipAmount;
//						DayOfWeekTotalDollars[6] += DailyTipData[d].PayCheckAmount; // add paycheck to daily amount
						DayOfWeekTotalDollars[6] -= DailyTipData[d].TipOut;
						DayOfWeekTotalPayCheck[6] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							DayOfWeekTotalDollars[6] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}		
						DayOfWeekTotalHours[6] += DailyTipData[d].HoursWorked;
						DayOfWeekTotalDowns[6] += DailyTipData[d].NumberOfDowns;
						DayOfWeekTotalTournamentDowns[6] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}				
			}			
		}
	}
	
	void UpdateWeeklyTotals()
	{
		WeeklyTotalDollars = new float[6];
		WeeklyTotalPayCheck = new float[6];
		WeeklyTotalHours = new float[6];
		WeeklyTotalDowns = new float[6];
		WeeklyTotalTournamentDowns = new float[6];
		WeeklyTotalStartTime = new float[6];
		
		for(int d = 0; d < DailyTipData.Count; d++)
		{
			// make sure data is for the current month
			if(DailyTipData[d].Date.Year == currentDate.Year && DailyTipData[d].Date.Month == currentDate.Month)
			{
				// week 1				
				for(int i = 0; i < 7; i++ )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						WeeklyTotalDollars[0] += DailyTipData[d].TipAmount;
//						WeeklyTotalDollars[0] += DailyTipData[d].PayCheckAmount;	// add paycheck to daily amount
						WeeklyTotalDollars[0] -= DailyTipData[d].TipOut;
						WeeklyTotalPayCheck[0] += DailyTipData[d].PayCheckAmount;

						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							WeeklyTotalDollars[0] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}						
						WeeklyTotalHours[0] += DailyTipData[d].HoursWorked;
						WeeklyTotalDowns[0] += DailyTipData[d].NumberOfDowns;
						WeeklyTotalTournamentDowns[0] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}
				// week 2				
				for(int i = 7; i < 14; i++ )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						WeeklyTotalDollars[1] += DailyTipData[d].TipAmount;
//						WeeklyTotalDollars[1] += DailyTipData[d].PayCheckAmount;	// add paycheck to daily amount
						WeeklyTotalDollars[1] -= DailyTipData[d].TipOut;
						WeeklyTotalPayCheck[1] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							WeeklyTotalDollars[1] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}	
						WeeklyTotalHours[1] += DailyTipData[d].HoursWorked;
						WeeklyTotalDowns[1] += DailyTipData[d].NumberOfDowns;
						WeeklyTotalTournamentDowns[1] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}
				// week 3				
				for(int i = 14; i < 21; i++ )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						WeeklyTotalDollars[2] += DailyTipData[d].TipAmount;
//						WeeklyTotalDollars[2] += DailyTipData[d].PayCheckAmount;	// add paycheck to daily amount
						WeeklyTotalDollars[2] -= DailyTipData[d].TipOut;
						WeeklyTotalPayCheck[2] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							WeeklyTotalDollars[2] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}						
						WeeklyTotalHours[2] += DailyTipData[d].HoursWorked;
						WeeklyTotalDowns[2] += DailyTipData[d].NumberOfDowns;
						WeeklyTotalTournamentDowns[2] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}
				// week 4				
				for(int i = 21; i < 28; i++ )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						WeeklyTotalDollars[3] += DailyTipData[d].TipAmount;
//						WeeklyTotalDollars[3] += DailyTipData[d].PayCheckAmount;	// add paycheck to daily amount
						WeeklyTotalDollars[3] -= DailyTipData[d].TipOut;
						WeeklyTotalPayCheck[3] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							WeeklyTotalDollars[3] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}	
						WeeklyTotalHours[3] += DailyTipData[d].HoursWorked;
						WeeklyTotalDowns[3] += DailyTipData[d].NumberOfDowns;
						WeeklyTotalTournamentDowns[3] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}
				// week 5				
				for(int i = 28; i < 35; i++ )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						WeeklyTotalDollars[4] += DailyTipData[d].TipAmount;
//						WeeklyTotalDollars[4] += DailyTipData[d].PayCheckAmount;	// add paycheck to daily amount
						WeeklyTotalDollars[4] -= DailyTipData[d].TipOut;
						WeeklyTotalPayCheck[4] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							WeeklyTotalDollars[4] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}	
						WeeklyTotalHours[4] += DailyTipData[d].HoursWorked;
						WeeklyTotalDowns[4] += DailyTipData[d].NumberOfDowns;
						WeeklyTotalTournamentDowns[4] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}	
				// week 6				
				for(int i = 35; i < 42; i++ )
				{
					if( System.Convert.ToInt32(DayLabels[i]) == DailyTipData[d].Date.Day )
					{
						WeeklyTotalDollars[5] += DailyTipData[d].TipAmount;
//						WeeklyTotalDollars[5] += DailyTipData[d].PayCheckAmount;	// add paycheck to daily amount
						WeeklyTotalDollars[5] -= DailyTipData[d].TipOut;
						WeeklyTotalPayCheck[5] += DailyTipData[d].PayCheckAmount;
						
						if (DailyTipData[d].NumberOfTournamentDowns != 0 && DailyTipData[d].TournamentDownAmount != 0 )	
						{
							WeeklyTotalDollars[5] += (DailyTipData[d].TournamentDownAmount * DailyTipData[d].NumberOfTournamentDowns);
						}	
						WeeklyTotalHours[5] += DailyTipData[d].HoursWorked;
						WeeklyTotalDowns[5] += DailyTipData[d].NumberOfDowns;
						WeeklyTotalTournamentDowns[5] += DailyTipData[d].NumberOfTournamentDowns;
					}
				}					
			}
		}
	}

	void UpdateWeeklyTotalsDeltaPercent()
	{
		for(int i = 1; i < 6; i++ )
		{
			if( WeeklyTotalDollars[i] != 0 && WeeklyTotalDollars[i-1] != 0)
			{
				WeeklyTotalDollarsDeltaPercent[i] = Mathf.Round(-100 * (1 - (WeeklyTotalDollars[i] /  WeeklyTotalDollars[i-1])));
				WeeklyTotalPayCheckDeltaPercent[i] = Mathf.Round(-100 * (1 - (WeeklyTotalPayCheck[i] /  WeeklyTotalPayCheck[i-1])));
				WeeklyTotalHoursDeltaPercent[i] = Mathf.Round(-100 * (1 - (WeeklyTotalHours[i] /  WeeklyTotalHours[i-1])));
				WeeklyTotalDownsDeltaPercent[i] = Mathf.Round(-100 * (1 - (WeeklyTotalDowns[i] /  WeeklyTotalDowns[i-1])));
				WeeklyTotalTournamentDownsDeltaPercent[i] = Mathf.Round(-100 * (1 - (WeeklyTotalTournamentDowns[i] /  WeeklyTotalTournamentDowns[i-1])));
			}
			else
			{
				WeeklyTotalDollarsDeltaPercent[i] = 0.0f;
				WeeklyTotalPayCheckDeltaPercent[i] = 0.0f;
				WeeklyTotalHoursDeltaPercent[i] = 0.0f;
				WeeklyTotalDownsDeltaPercent[i] = 0.0f;
				WeeklyTotalTournamentDownsDeltaPercent[i] = 0.0f;
			}
			WeeklyTotalStartTimeDeltaPercent[i] = 0.0f;
		}	
	}
	
	void UpdateMonthlyTotals()
	{
		MonthlyTotalDollars = 0.0f;
		MonthlyTotalPayCheck = 0.0f;
		MonthlyTotalHours = 0.0f;
		MonthlyTotalDowns = 0.0f;
		MonthlyTotalTournamentDowns = 0.0f;
		MonthlyTotalStartTime = 0.0f;
		
		for(int i = 0; i < WeeklyTotalDollars.Length ; i++ )
		{
			MonthlyTotalDollars += WeeklyTotalDollars[i];
			MonthlyTotalPayCheck += WeeklyTotalPayCheck[i];
			MonthlyTotalHours += WeeklyTotalHours[i];
			MonthlyTotalDowns += WeeklyTotalDowns[i];
			MonthlyTotalTournamentDowns += WeeklyTotalTournamentDowns[i];
		}
	}
	
	void Update()
	{
		if (Enabled == true)
		{		
			if(UIManager.GetComponent<UI_Manager>().CouldBeSwipe && Mathf.Abs(UIManager.GetComponent<UI_Manager>().SwipeDist) > UIManager.GetComponent<UI_Manager>().MinSwipeDist)
			{
				SwipeOffset = UIManager.GetComponent<UI_Manager>().SwipeDist;
			}
			else
			{
				SwipeOffset = 0.0f;
			}
		}
	}
}
