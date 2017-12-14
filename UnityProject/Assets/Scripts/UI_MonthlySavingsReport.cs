using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GUITexture))]
public class UI_MonthlySavingsReport : MonoBehaviour {

	public bool Enabled = true;
	
	public List<TipData> DailyTipData = new List<TipData>(); 
	public List<ExpenseData> MonthlyExpenseData = new List<ExpenseData>();
	private System.DateTime currentDate = new System.DateTime(); 
	private string CurrentMonthString = "";
	public int AddMonths = 0;
	public int CurrentMonthlyExpenseIndex = 0;
	public bool MonthyExpensesFound = false;
	public float MonthyExpenseTotal = 0.0f;
	public float TotalTournamentAmountThisMonth = 0.0f;
	public float TotalTipAmountThisMonth = 0.0f;
	public float  TotalPayCheckAmountThisMonth	= 0.0f;
	public float TotalWageAmountThisMonth = 0.0f;
	public float TotalIncomeAmountThisMonth = 0.0f;
	public float TotalSavingsThisMonth = 0.0f;
	public float TotalTaxesThisMonth = 0.0f;
	
	public float BreakEvenAverageDollarsPerDay = 0.0f;
	
	private string TaxesLabelString = "";
	
	public GameObject UIManager;
	public GameObject UserBlobManager;
		
	public string PopUpYesNoDialogState = "";
	private bool PopUpActive = false;
	
	public Texture2D BackgroundBitmap;
	public Texture2D style2XButtonBlueBitmap;
	public Texture2D style2XButtonBlueActiveBitmap;
	public Texture2D styleCalendarLeftArrowButtonBitmap;
	public Texture2D styleCalendarRightArrowButtonBitmap;
	
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
	
	public float SwipeOffset = 0.0f;
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		
		UpdateMonthlySavingsReport();
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
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().EnterDataBackground;

			// time and month and year		
			GUI.Label(new Rect((Screen.width * 0.50f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);
			GUI.Label(new Rect((Screen.width * 0.50f), (Screen.height* 0.045f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (CurrentMonthString + " " + currentDate.Year.ToString()), style2X);
			
			// month buttons		
	        if (GUI.Button(new Rect((Screen.width * 0.00f), (Screen.height* 0.0f), (Screen.width * 0.14f), (Screen.height* 0.09f)), styleCalendarLeftArrowButtonBitmap, style2XButtonBlue))
			{
				AddMonths -=1;
				UpdateMonthlySavingsReport();
			}
	
			if (GUI.Button(new Rect((Screen.width * 0.86f), (Screen.height* 0.0f), (Screen.width * 0.14f ), (Screen.height* 0.09f)), styleCalendarRightArrowButtonBitmap, style2XButtonBlue))
			{
				AddMonths +=1;
				UpdateMonthlySavingsReport();
			}
			
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.05f), (Screen.height* 0.10f), 100, 20), "INCOME", style2XBlack);
			
			// live game income
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.15f), (Screen.height* 0.15f), 100, 20), "Live Game Income", style2XBlack);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.75f), (Screen.height* 0.15f), 100, 20), (" $ " + TotalTipAmountThisMonth.ToString()), style2XBlack);

			// tournament income
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.15f), (Screen.height* 0.20f), 100, 20), "Tournament Income", style2XBlack);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.75f), (Screen.height* 0.20f), 100, 20), (" $ " + TotalTournamentAmountThisMonth.ToString()), style2XBlack);

			// pay check income
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.15f), (Screen.height* 0.25f), 100, 20), "Pay Check Income", style2XBlack);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.75f), (Screen.height* 0.25f), 100, 20), (" $ " + TotalPayCheckAmountThisMonth.ToString()), style2XBlack);

/*			
			// wage income
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.15f), (Screen.height* 0.25f), 100, 20), "Hourly Wage Income", styleBigger);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.75f), (Screen.height* 0.25f), 100, 20), (" $ " + TotalWageAmountThisMonth.ToString()), styleBigger);
			
			// taxes
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.15f), (Screen.height* 0.30f), 100, 20), TaxesLabelString, styleBigger);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.75f), (Screen.height* 0.30f), 100, 20), ("-$ " + TotalTaxesThisMonth.ToString()), styleBigger);
*/
			
			// total income
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.30f), (Screen.height* 0.30f), 100, 20), "Total Monthly Income", style2XBlack);
			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.75f), (Screen.height* 0.30f), 100, 20), (" $ " + TotalIncomeAmountThisMonth.ToString()), style2XBlack);

			GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.05f), (Screen.height* 0.45f), 100, 20), "EXPENSES", style2XBlack);
						
			// MonthlyExpenseData has no entries
			if(MonthyExpensesFound == true && MonthlyExpenseData.Count > 0)
//			if(MonthlyExpenseData.Count > 0)
			{
				// expenses has at least one entry
				if( MonthlyExpenseData[CurrentMonthlyExpenseIndex] == null)
				{
					// has 0 entries
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.25f), (Screen.height* 0.60f), 100, 20), "error", styleBigger);
				}
				else
				{
					
					// expenses
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.15f), (Screen.height* 0.50f), 100, 20), "Monthly Expenses", style2XBlack);
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.75f), (Screen.height* 0.50f), 100, 20), (" $ " + MonthyExpenseTotal.ToString()), style2XBlack);

					// total expenses
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.30f), (Screen.height* 0.55f), 100, 20), "Total Monthly Expenses", style2XBlack);
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.75f), (Screen.height* 0.55f), 100, 20), (" $ " + MonthyExpenseTotal.ToString()), style2XBlack);

/*					
					// savings
					GUI.Label(new Rect((Screen.width * 0.15f), (Screen.height* 0.60f), 100, 20), "Total Monthly Savings", styleBigger);
					if (TotalSavingsThisMonth >= 0)
					{
						GUI.Label(new Rect((Screen.width * 0.75f), (Screen.height* 0.60f), 100, 20), (" $ " + TotalSavingsThisMonth.ToString()), styleBiggerGreen);
					}
					else
					{
						GUI.Label(new Rect((Screen.width * 0.75f), (Screen.height* 0.60f), 100, 20), (" $ " + TotalSavingsThisMonth.ToString()), styleBiggerRed);
					}
*/					
					// $ per day to cover expenses
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.05f), (Screen.height* 0.675f), 100, 20), "Dollars per Day to Cover Expenses", style2XBlack);
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.05f), (Screen.height* 0.70f), 100, 20), ("at " +UserBlobManager.GetComponent<UserBlobManager>().UserAppData.NumberOfWorkingDaysPerMonth.ToString() + " Work Days per Month"), style2XBlack);
					GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.75f), (Screen.height* 0.70f), 100, 20), (" $ " + BreakEvenAverageDollarsPerDay.ToString()), style2XBlack);
					
/*					
					GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height* 0.75f), 100, 20), "*Days Worked This Month", styleBigger);
					GUI.Label(new Rect((Screen.width * 0.75f), (Screen.height* 0.75f), 100, 20), ("3 of 20"), styleBigger);

					GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height* 0.80f), 100, 20), "*Percent of Expenses Covered", styleBigger);
					GUI.Label(new Rect((Screen.width * 0.75f), (Screen.height* 0.80f), 100, 20), ("15%"), styleBigger);
*/					
				}	
			}
			else
			{
				GUI.Label(new Rect(SwipeOffset + (Screen.width * 0.25f), (Screen.height* 0.55f), 100, 20), "No expenses added for this month", styleBigger);
			}
			
			
			// back button
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.90f), (Screen.width * 0.5f), (Screen.height * 0.075f)) ,"Back", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{	
					UIManager.GetComponent<UI_Manager>().SwitchStates("ExpensesState");
				}
			}				
		}
	}
	
	void FindMonthlyBudgetIndex()
	{
		MonthyExpensesFound = false;
		for(int i = 0; i < MonthlyExpenseData.Count ; i++ )
		{
			if(MonthlyExpenseData[i].Date.Month == currentDate.Month && 
				MonthlyExpenseData[i].Date.Year == currentDate.Year )
			{
				CurrentMonthlyExpenseIndex = i;
				MonthyExpensesFound = true;
			}
		}
		if(CurrentMonthlyExpenseIndex > MonthlyExpenseData.Count)
		{
			CurrentMonthlyExpenseIndex = 0;
		}
	}
	
	void CalculateMonthlyTotals()
	{
		var CurrentTipAmount = 0.0f;
		var CurrentTournamentAmount = 0.0f;
		var CurrentHoursWorked = 0.0f;
		var CurrentPayCheckAmount = 0.0f;
		TotalTipAmountThisMonth = 0.0f;
		
		for(int i = 0; i < DailyTipData.Count ; i++)
		{
//			if(DailyTipData[i].Date.Year == System.DateTime.Now.Year && 
//				DailyTipData[i].Date.Month == System.DateTime.Now.Month)
			if(DailyTipData[i].Date.Year == currentDate.Year && 
				DailyTipData[i].Date.Month == currentDate.Month)
			{
				CurrentTipAmount += DailyTipData[i].TipAmount;
				CurrentTipAmount -= DailyTipData[i].TipOut;
				CurrentTournamentAmount += (DailyTipData[i].TournamentDownAmount * DailyTipData[i].NumberOfTournamentDowns);
				CurrentPayCheckAmount += DailyTipData[i].PayCheckAmount;
				CurrentHoursWorked += DailyTipData[i].HoursWorked;
			}
		}	
		
		TotalTipAmountThisMonth = CurrentTipAmount;
		TotalTournamentAmountThisMonth = CurrentTournamentAmount;
		TotalPayCheckAmountThisMonth = CurrentPayCheckAmount;
//		TotalWageAmountThisMonth = CurrentHoursWorked * UserBlobManager.GetComponent<UserBlobManager>().UserAppData.HourlyWageAmount;
		TotalIncomeAmountThisMonth = TotalTipAmountThisMonth + TotalTournamentAmountThisMonth + CurrentPayCheckAmount;
		
//		TaxesLabelString = "Taxes at " + UserBlobManager.GetComponent<UserBlobManager>().UserAppData.TaxPercentage.ToString() + "%";
//		TotalTaxesThisMonth = TotalIncomeAmountThisMonth * (UserBlobManager.GetComponent<UserBlobManager>().UserAppData.TaxPercentage / 100);
//		TotalIncomeAmountThisMonth -= TotalTaxesThisMonth;
		
		BreakEvenAverageDollarsPerDay = (float)Math.Round((MonthyExpenseTotal / UserBlobManager.GetComponent<UserBlobManager>().UserAppData.NumberOfWorkingDaysPerMonth),2);
	}	
	
	public void UpdateMonthlySavingsReport()
	{
		
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

		FindMonthlyBudgetIndex();	
		
		// sum the total budget
		MonthyExpenseTotal = 0.0f;
		if(CurrentMonthlyExpenseIndex <  MonthlyExpenseData.Count)
		{
			for(int i = 0; i < 15; i++)
			{
				MonthyExpenseTotal += MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseAmount[i];
			}
		}
		MonthyExpenseTotal = (float)Math.Round(MonthyExpenseTotal,2);
		
		CalculateMonthlyTotals();
		
		TotalSavingsThisMonth = TotalTipAmountThisMonth - MonthyExpenseTotal;
	}

	public void PassDataIn()
	{
		DailyTipData = new List<TipData>(); 
		DailyTipData = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData;
		DailyTipData.Sort( delegate(TipData DTD1, TipData DTD2) { return DTD1.Date.CompareTo(DTD2.Date); });

//		AddMonths = 0;
		MonthlyExpenseData = new List<ExpenseData>();
		MonthlyExpenseData = UserBlobManager.GetComponent<UserBlobManager>().MonthlyExpenseData;

		UpdateMonthlySavingsReport();
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
