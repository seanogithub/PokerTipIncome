using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Manager : MonoBehaviour {
	
	public string UIPreviousState = "SplashScreenState";
	public string UICurrentState = "SplashScreenState";
//	public string UIPreviousState = "PasswordScreenState";
//	public string UICurrentState = "PasswordScreenState";
	public GameObject UISplashScreen;
	public GameObject UICalendar;
	public GameObject UIEnterData;
	public GameObject UIEnterAlphaNumericValue;
	public GameObject UIEnterNumericValue;
	public GameObject UIEnterJobName;
	public GameObject UIEnterTipAmount;
	public GameObject UIEnterNumberOfDowns;
	public GameObject UIEnterHoursWorked;
	public GameObject UIEnterTournamentName;
	public GameObject UIEnterTournamentDownAmount;
	public GameObject UIEnterNumberOfTournamentDowns;
	public GameObject UIEnterTipOut;
	public GameObject UIEnterNotes;
	public GameObject UIStatistics;
	public GameObject UISettings;
	public GameObject UIFontColorPicker;
	public GameObject UIButtonColorPicker;
	public GameObject UIBackgroundColorPicker;
	public GameObject UIPickerJobName;
	public GameObject UIPickerTournamentName;
	public GameObject UIExpenses;
	public GameObject UIMonthlyExpenses;
	public GameObject UIPickerExpenseName;
	public GameObject UIEnterExpenseName;
	public GameObject UIEnterExpenseAmount;
	public GameObject UIMonthlySavingsReport;
	public GameObject UIPickerWorkDayStartTime;
	public GameObject UIEnterPayCheckAmount;
	public GameObject UIEnterDaysWorkedPerMonth;
	public GameObject UIEnterTaxPercentage;
	public GameObject UIEnterHourlyWageAmount;
	public GameObject UIColorSettings;
	public GameObject UIPasswordScreen;
	public GameObject UIEnterPassCode;
	public GameObject UIImportData;
	public GameObject UIPickerSkin;

	public GameObject UserBlobManager;
	
	public bool SplashScreenState = false;
	public bool CalendarState = false;
	public bool EnterDataState = false;
	public bool EnterAlphaNumericValueState = false;
	public bool EnterNumericValueState = false;
	public bool EnterJobNameState = false;
	public bool EnterTipAmountState = false;
	public bool EnterNumberOfDownsState = false;
	public bool EnterHoursWorkedState = false;
	public bool EnterTournamentNameState = false;
	public bool EnterTournamentDownAmountState = false;
	public bool EnterNumberOfTournamentDownsState = false;
	public bool EnterTipOutState = false;
	public bool EnterNotesState = false;
	public bool StatisticsState = false;
	public bool SettingsState = false;
	public bool FontColorPickerState = false;
	public bool ButtonColorPickerState = false;
	public bool BackgroundColorPickerState = false;
	public bool PickerJobNameState = false;
	public bool PickerTournamentNameState = false;
	public bool ExpensesState = false;
	public bool MonthlyExpensesState = false;
	public bool PickerExpenseNameState = false;
	public bool EnterExpenseNameState = false;
	public bool EnterExpenseAmountState = false;
	public bool MonthlySavingsReportState = false;
	public bool PickerWorkDayStartTimeState = false;
	public bool EnterPayCheckAmountState = false;
	public bool EnterDaysWorkedPerMonthState = false;
	public bool EnterTaxPercentageState = false;
	public bool EnterHourlyWageAmountState = false;
	public bool ColorSettingsState = false;
	public bool PasswordScreenState = false;
	public bool EnterPassCodeState = false;
	public bool ImportDataState = false;
	public bool PickerSkinState = false;
	
	public bool UpdateEnteredData = false;
	public bool FindNewData = true;
	
	public List<TipData> myDailyTipData = new List<TipData>();	
	public List<ExpenseData> MonthlyExpenseData = new List<ExpenseData>();
	
	public System.DateTime currentDate = new System.DateTime();
	
	public float PassCodeTimer = 120.0f;
	public System.DateTime LastTouch = new System.DateTime();
	
	public float SwipeStartTime = 0.0f;
	public Vector2 SwipeStartPos;
	public bool CouldBeSwipe = false;
	public float SwipeComfortZone = 0.0f;
	public float MinSwipeDist = 0.0f;
	public float MaxSwipeTime = 2.0f;
	public float SwipeDist = 0.0f;
	public string shit = "";
	
	void Awake()
	{
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		UISplashScreen = GameObject.Find("UI_SplashScreen_Prefab");
		UICalendar = GameObject.Find("UI_Calendar_Prefab");
		UIEnterData = GameObject.Find("UI_EnterData_Prefab");
		UIEnterAlphaNumericValue = GameObject.Find("UI_EnterAlphaNumericValue_Prefab");
		UIEnterNumericValue = GameObject.Find("UI_EnterNumericValue_Prefab");
		UIEnterJobName = GameObject.Find("UI_EnterJobName_Prefab");
		UIEnterTipAmount = GameObject.Find("UI_EnterTipAmount_Prefab");
		UIEnterNumberOfDowns = GameObject.Find("UI_EnterNumberOfDowns_Prefab");
		UIEnterHoursWorked = GameObject.Find("UI_EnterHoursWorked_Prefab");
		UIEnterTournamentName = GameObject.Find("UI_EnterTournamentName_Prefab");
		UIEnterTournamentDownAmount = GameObject.Find("UI_EnterTournamentDownAmount_Prefab");
		UIEnterNumberOfTournamentDowns = GameObject.Find("UI_EnterNumberOfTournamentDowns_Prefab");
		UIEnterTipOut = GameObject.Find("UI_EnterTipOut_Prefab");
		UIEnterNotes = GameObject.Find("UI_EnterNotes_Prefab");
		UIStatistics = GameObject.Find("UI_Statistics_Prefab");
		UISettings = GameObject.Find("UI_Settings_Prefab");
		UIFontColorPicker = GameObject.Find("UI_FontColorPicker_Prefab");
		UIButtonColorPicker = GameObject.Find("UI_ButtonColorPicker_Prefab");
		UIBackgroundColorPicker = GameObject.Find("UI_BackgroundColorPicker_Prefab");
		UIPickerJobName = GameObject.Find("UI_PickerJobName_Prefab");
		UIPickerTournamentName = GameObject.Find("UI_PickerTournamentName_Prefab");
		UIExpenses = GameObject.Find("UI_Expenses_Prefab");
		UIMonthlyExpenses = GameObject.Find("UI_MonthlyExpenses_Prefab");
		UIPickerExpenseName = GameObject.Find("UI_PickerExpenseName_Prefab");
		UIEnterExpenseName = GameObject.Find("UI_EnterExpenseName_Prefab");
		UIEnterExpenseAmount = GameObject.Find("UI_EnterExpenseAmount_Prefab");
		UIMonthlySavingsReport = GameObject.Find("UI_MontlySavingsReport_Prefab");
		UIPickerWorkDayStartTime = GameObject.Find("UI_PickerWorkDayStartTime_Prefab");
		UIEnterPayCheckAmount = GameObject.Find("UI_EnterPayCheckAmount_Prefab");
		UIEnterDaysWorkedPerMonth = GameObject.Find("UI_EnterDaysWorkedPerMonth_Prefab");
		UIEnterTaxPercentage = GameObject.Find("UI_EnterTaxPercentage_Prefab");
		UIEnterHourlyWageAmount = GameObject.Find("UI_EnterHourlyWageAmount_Prefab");
		UIColorSettings = GameObject.Find("UI_ColorSettings_Prefab");
		UIPasswordScreen = GameObject.Find("UI_PasswordScreen_Prefab");
		UIEnterPassCode = GameObject.Find("UI_EnterPassCode_Prefab");
		UIImportData = GameObject.Find("UI_ImportData_Prefab");
		UIPickerSkin = GameObject.Find("UI_PickerSkin_Prefab");
	}
	
	// Use this for initialization
	void Start () 
	{
		myDailyTipData = new List<TipData>();
		
		SetUIState();
		PassCodeTimer = 120;// UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCodeTimer;
		MinSwipeDist = Screen.width * 0.15f;
		SwipeComfortZone = Screen.width * 0.80f;
		
	}

	public void ResetManagerData()
	{
		myDailyTipData = new List<TipData>();
		myDailyTipData = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData;
	}
	
	public void SetCurrentDate(System.DateTime newDate)
	{
		currentDate = new System.DateTime(newDate.Year, newDate.Month, newDate.Day, newDate.Hour, newDate.Minute, newDate.Second);
	}
	
	public void SwitchStates(string myNewState)
	{
		// check for valid states
		if(	myNewState == "SplashScreenState" ||
			myNewState == "CalendarState" ||
			myNewState == "EnterDataState" ||
			myNewState == "EnterAlphaNumericValue" ||
			myNewState == "EnterNumericValue" ||
			myNewState == "EnterJobName" ||
			myNewState == "EnterTipAmount" ||
			myNewState == "EnterNumberOfDowns" ||
			myNewState == "EnterHoursWorked" ||
			myNewState == "EnterTournamentName" ||			
			myNewState == "EnterTournamentDownAmount" ||
			myNewState == "EnterNumberOfTournamentDowns" ||
			myNewState == "EnterTipOut" || 
			myNewState == "EnterNotes" ||
			myNewState == "StatisticsState" ||
			myNewState == "SettingsState" ||
			myNewState == "FontColorPickerState" ||
			myNewState == "ButtonColorPickerState" ||
			myNewState == "BackgroundColorPickerState" ||
			myNewState == "PickerJobNameState" || 
			myNewState == "PickerTournamentNameState" ||
			myNewState == "ExpensesState" || 
			myNewState == "MonthlyExpensesState" ||
			myNewState == "PickerExpenseNameState" ||
			myNewState == "EnterExpenseNameState" ||
			myNewState == "EnterExpenseAmountState" ||
			myNewState == "MonthlySavingsReportState" ||
			myNewState == "PickerWorkDayStartTimeState" ||
			myNewState == "EnterPayCheckAmountState" ||
			myNewState == "EnterDaysWorkedPerMonthState" ||
			myNewState == "EnterTaxPercentageState" ||
			myNewState == "EnterHourlyWageAmountState" ||
			myNewState == "ColorSettingsState" ||
			myNewState == "PasswordScreenState" ||
			myNewState == "EnterPassCodeState" ||
			myNewState == "ImportDataState" ||
			myNewState == "PickerSkinState" 
			)
		{
			UICurrentState = myNewState;
			SetUIState();
		}
		
	}
	
	// Update is called once per frame
	void SetUIState () 
	{
		SplashScreenState = false;
		CalendarState = false;
		EnterDataState = false;
		EnterAlphaNumericValueState = false;
		EnterNumericValueState = false;
		EnterJobNameState = false;
		EnterTipAmountState = false;
		EnterNumberOfDownsState = false;
		EnterHoursWorkedState = false;
		EnterTournamentNameState = false;
		EnterTournamentDownAmountState = false;
		EnterNumberOfTournamentDownsState = false;
		EnterTipOutState = false;
		EnterNotesState = false;
		StatisticsState = false;
		SettingsState = false;
		FontColorPickerState = false;
		ButtonColorPickerState = false;
		BackgroundColorPickerState = false;
		PickerJobNameState = false;
		PickerTournamentNameState = false;
		ExpensesState = false;
		MonthlyExpensesState = false;
		PickerExpenseNameState = false;
		EnterExpenseNameState = false;
		EnterExpenseAmountState = false;
		MonthlySavingsReportState = false;
		PickerWorkDayStartTimeState = false;
		EnterPayCheckAmountState = false;
		EnterDaysWorkedPerMonthState = false;
		EnterTaxPercentageState = false;
		EnterHourlyWageAmountState = false;
		ColorSettingsState = false;
		PasswordScreenState = false;
		EnterPassCodeState = false;
		ImportDataState = false;
		PickerSkinState = false;
		
		switch(UICurrentState)
		{
		case "SplashScreenState":
			SplashScreenState = true;
			break;			
		case "CalendarState":
			CalendarState = true;
			break;
		case "EnterDataState":
			EnterDataState = true;
			break;
		case "EnterAlphaNumericValue":
			EnterAlphaNumericValueState = true;
			break;			
		case "EnterNumericValue":
			EnterNumericValueState = true;
			break;			
		case "EnterJobName":
			EnterJobNameState = true;
			break;	
		case "EnterTipAmount":
			EnterTipAmountState = true;
			break;	
		case "EnterNumberOfDowns":
			EnterNumberOfDownsState = true;
			break;	
		case "EnterHoursWorked":
			EnterHoursWorkedState = true;
			break;	
		case "EnterTournamentName":
			EnterTournamentNameState = true;
			break;	
		case "EnterTournamentDownAmount":
			EnterTournamentDownAmountState = true;
			break;	
		case "EnterNumberOfTournamentDowns":
			EnterNumberOfTournamentDownsState = true;
			break;			
		case "EnterTipOut":
			EnterTipOutState = true;
			break;		
		case "EnterNotes":
			EnterNotesState = true;
			break;			
		case "StatisticsState":
			StatisticsState = true;
			break;					
		case "SettingsState":
			SettingsState = true;
			break;	
		case "FontColorPickerState":
			FontColorPickerState = true;
			break;		
		case "ButtonColorPickerState":
			ButtonColorPickerState = true;
			break;		
		case "BackgroundColorPickerState":
			BackgroundColorPickerState = true;
			break;	
		case "PickerJobNameState":
			PickerJobNameState = true;
			break;
		case "PickerTournamentNameState":
			PickerTournamentNameState = true;
			break;		
		case "ExpensesState":
			ExpensesState = true;
			break;	
		case "MonthlyExpensesState":
			MonthlyExpensesState = true;
			break;		
		case "PickerExpenseNameState":
			PickerExpenseNameState = true;
			break;	
		case "EnterExpenseNameState":
			EnterExpenseNameState = true;
			break;		
		case "EnterExpenseAmountState":
			EnterExpenseAmountState = true;
			break;	
		case "MonthlySavingsReportState":
			MonthlySavingsReportState = true;
			break;		
		case "PickerWorkDayStartTimeState":
			PickerWorkDayStartTimeState = true;
			break;		
		case "EnterPayCheckAmountState":
			EnterPayCheckAmountState = true;
			break;
		case "EnterDaysWorkedPerMonthState":
			EnterDaysWorkedPerMonthState = true;
			break;		
		case "EnterTaxPercentageState":
			EnterTaxPercentageState = true;
			break;				
		case "EnterHourlyWageAmountState":
			EnterHourlyWageAmountState = true;
			break;		
		case "ColorSettingsState":
			ColorSettingsState = true;
			break;				
		case "PasswordScreenState":
			PasswordScreenState = true;
			break;	
		case "EnterPassCodeState":
			EnterPassCodeState = true;
			break;	
		case "ImportDataState":
			ImportDataState = true;
			break;	
		case "PickerSkinState":
			PickerSkinState = true;
			break;			
		}

		UISplashScreen.GetComponent<SplashScreen>().GUIEnabled(SplashScreenState);
		UICalendar.GetComponent<UI_Calendar>().GUIEnabled(CalendarState);
		UIEnterData.GetComponent<UI_EnterData>().GUIEnabled(EnterDataState);
		UIEnterAlphaNumericValue.GetComponent<UI_EnterAlphaNumericValue>().GUIEnabled(EnterAlphaNumericValueState);
		UIEnterNumericValue.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterNumericValueState);
		UIEnterJobName.GetComponent<UI_EnterAlphaNumericValue>().GUIEnabled(EnterJobNameState);
		UIEnterTipAmount.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterTipAmountState);
		UIEnterNumberOfDowns.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterNumberOfDownsState);
		UIEnterHoursWorked.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterHoursWorkedState);
		UIEnterTournamentName.GetComponent<UI_EnterAlphaNumericValue>().GUIEnabled(EnterTournamentNameState);
		UIEnterTournamentDownAmount.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterTournamentDownAmountState);
		UIEnterNumberOfTournamentDowns.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterNumberOfTournamentDownsState);
		UIEnterTipOut.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterTipOutState);
		UIEnterNotes.GetComponent<UI_EnterAlphaNumericValue>().GUIEnabled(EnterNotesState);
		UIStatistics.GetComponent<UI_Statistics>().GUIEnabled(StatisticsState);
		UISettings.GetComponent<UI_Settings>().GUIEnabled(SettingsState);
		UIFontColorPicker.GetComponent<UI_FontColorPicker>().GUIEnabled(FontColorPickerState);
		UIButtonColorPicker.GetComponent<UI_ButtonColorPicker>().GUIEnabled(ButtonColorPickerState);
		UIBackgroundColorPicker.GetComponent<UI_BackgroundColorPicker>().GUIEnabled(BackgroundColorPickerState);
		UIPickerJobName.GetComponent<UI_PickerJobName>().GUIEnabled(PickerJobNameState);
		UIPickerJobName.GetComponent<UI_PickerJobNameWindow>().GUIEnabled(PickerJobNameState); // need to set both scripts
		UIPickerTournamentName.GetComponent<UI_PickerTournamentName>().GUIEnabled(PickerTournamentNameState);
		UIPickerTournamentName.GetComponent<UI_PickerTournamentNameWindow>().GUIEnabled(PickerTournamentNameState); // need to set both scripts
		UIExpenses.GetComponent<UI_Expenses>().GUIEnabled(ExpensesState); 
		UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().GUIEnabled(MonthlyExpensesState);
		UIPickerExpenseName.GetComponent<UI_PickerExpenseName>().GUIEnabled(PickerExpenseNameState);
		UIPickerExpenseName.GetComponent<UI_PickerExpenseNameWindow>().GUIEnabled(PickerExpenseNameState); // need to set both scripts
		UIEnterExpenseName.GetComponent<UI_EnterAlphaNumericValue>().GUIEnabled(EnterExpenseNameState);
		UIEnterExpenseAmount.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterExpenseAmountState);
		UIMonthlySavingsReport.GetComponent<UI_MonthlySavingsReport>().GUIEnabled(MonthlySavingsReportState);
		UIPickerWorkDayStartTime.GetComponent<UI_PickerWorkDayStartTime>().GUIEnabled(PickerWorkDayStartTimeState);
		UIPickerWorkDayStartTime.GetComponent<UI_PickerWorkDayStartTimeWindow>().GUIEnabled(PickerWorkDayStartTimeState); // need to set both scripts
		UIEnterPayCheckAmount.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterPayCheckAmountState);
		UIEnterDaysWorkedPerMonth.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterDaysWorkedPerMonthState);
		UIEnterTaxPercentage.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterTaxPercentageState);
		UIEnterHourlyWageAmount.GetComponent<UI_EnterNumericValue>().GUIEnabled(EnterHourlyWageAmountState);
		UIColorSettings.GetComponent<UI_ColorSettings>().GUIEnabled(ColorSettingsState);
		UIPasswordScreen.GetComponent<UI_PasswordScreen>().GUIEnabled(PasswordScreenState);
		UIEnterPassCode.GetComponent<UI_EnterPassCode>().GUIEnabled(EnterPassCodeState);
		UIImportData.GetComponent<UI_ImportData>().GUIEnabled(ImportDataState);
		UIPickerSkin.GetComponent<UI_PickerSkin>().GUIEnabled(PickerSkinState);
		UIPickerSkin.GetComponent<UI_PickerSkinWindow>().GUIEnabled(PickerSkinState); // need to set both scripts
		
		if (SplashScreenState)
		{
			FindNewData = false;
		}		
		if (CalendarState)
		{
			FindNewData = true;
			UICalendar.GetComponent<UI_Calendar>().UpdateCalendar();
		}
		if (EnterDataState)
		{
//			print (FindNewData.ToString());
			if(FindNewData == true)
			{
				UIEnterData.GetComponent<UI_EnterData>().myDailyTipData = new List<TipData>(); 
				FindTipDataByDate(currentDate);
				UIEnterData.GetComponent<UI_EnterData>().GetNewData();
				UIEnterData.GetComponent<UI_EnterData>().myTipDataIndex = 0;
			}
			FindNewData = true;
		}
		if (EnterJobNameState)
		{
			FindNewData = false;
			UIEnterJobName.GetComponent<UI_EnterAlphaNumericValue>().PassDataIn ( "JobName");
		}
		if (EnterNumericValueState)
		{
			FindNewData = false;
			UIEnterTipAmount.GetComponent<UI_EnterNumericValue>().PassDataIn ( "TipAmount");
		}
		if (EnterTipAmountState)
		{
			FindNewData = false;
			UIEnterTipAmount.GetComponent<UI_EnterNumericValue>().PassDataIn ( "TipAmount");
		}
		if (EnterNumberOfDownsState)
		{
			FindNewData = false;
			UIEnterNumberOfDowns.GetComponent<UI_EnterNumericValue>().PassDataIn ( "NumberOfDowns");
		}
		if (EnterHoursWorkedState)
		{
			FindNewData = false;
			UIEnterHoursWorked.GetComponent<UI_EnterNumericValue>().PassDataIn ( "HoursWorked");
		}	
		if (EnterTournamentNameState)
		{
			FindNewData = false;
			UIEnterTournamentName.GetComponent<UI_EnterAlphaNumericValue>().PassDataIn ( "TournamentName");
		}
		if (EnterTournamentDownAmountState)
		{
			FindNewData = false;
			UIEnterTournamentDownAmount.GetComponent<UI_EnterNumericValue>().PassDataIn ( "TournamentDownAmount");
		}	
		if (EnterNumberOfTournamentDownsState)
		{
			FindNewData = false;
			UIEnterNumberOfTournamentDowns.GetComponent<UI_EnterNumericValue>().PassDataIn ( "NumberOfTournamentDowns");
		}		
		if (EnterTipOutState)
		{
			FindNewData = false;
			UIEnterTipOut.GetComponent<UI_EnterNumericValue>().PassDataIn ( "TipOut");
		}	
		if (EnterNotesState)
		{
			FindNewData = false;
			UIEnterNotes.GetComponent<UI_EnterAlphaNumericValue>().PassDataIn ( "Notes");
		}
		if (StatisticsState)
		{
			FindNewData = false;
			UIStatistics.GetComponent<UI_Statistics>().UpdateStatistics();
		}
		if (SettingsState)
		{
			FindNewData = false;
//			UISettings.GetComponent<UI_Settings>().PassDataIn ( "Settings");
		}
		if (FontColorPickerState)
		{
			FindNewData = false;
		}
		if (ButtonColorPickerState)
		{
			FindNewData = false;
		}
		if (BackgroundColorPickerState)
		{
			FindNewData = false;
		}
		if (PickerJobNameState)
		{
			FindNewData = false;
			UIPickerJobName.GetComponent<UI_PickerJobNameWindow>().PassDataIn("JobName");
		}
		if (PickerTournamentNameState)
		{
			FindNewData = false;
			UIPickerTournamentName.GetComponent<UI_PickerTournamentNameWindow>().PassDataIn("TournamentName");
		}
		if (ExpensesState)
		{
			FindNewData = false;
		}
		if (MonthlyExpensesState)
		{
			UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().PassDataIn();
			FindNewData = false;
		}
		if (PickerExpenseNameState)
		{
			FindNewData = false;
			UIPickerExpenseName.GetComponent<UI_PickerExpenseNameWindow>().PassDataIn("ExpenseName");
		}
		if (EnterExpenseNameState)
		{
			FindNewData = false;
			UIEnterExpenseName.GetComponent<UI_EnterAlphaNumericValue>().PassDataIn("ExpenseName");
		}
		if (EnterExpenseAmountState)
		{
			FindNewData = false;
			UIEnterExpenseAmount.GetComponent<UI_EnterNumericValue>().PassDataIn("ExpenseAmount");
		}
		if (MonthlySavingsReportState)
		{
			FindNewData = false;
			UIMonthlySavingsReport.GetComponent<UI_MonthlySavingsReport>().PassDataIn();
		}
		if (PickerWorkDayStartTimeState)
		{
			FindNewData = false;
			UIPickerWorkDayStartTime.GetComponent<UI_PickerWorkDayStartTimeWindow>().PassDataIn("WorkDayStartTime");
		}
		if (EnterPayCheckAmountState)
		{
			FindNewData = false;
			UIEnterPayCheckAmount.GetComponent<UI_EnterNumericValue>().PassDataIn("PayCheckAmount");
		}
		if (EnterDaysWorkedPerMonthState)
		{
			FindNewData = false;
			UIEnterDaysWorkedPerMonth.GetComponent<UI_EnterNumericValue>().PassDataIn("DaysWorkedPerMonth");
		}
		if (EnterTaxPercentageState)
		{
			FindNewData = false;
			UIEnterTaxPercentage.GetComponent<UI_EnterNumericValue>().PassDataIn("TaxPercentage");
		}
		if (EnterHourlyWageAmountState)
		{
			FindNewData = false;
			UIEnterHourlyWageAmount.GetComponent<UI_EnterNumericValue>().PassDataIn("HourlyWageAmount");
		}
		if (ColorSettingsState)
		{
			FindNewData = false;
		}		
		if (PasswordScreenState)
		{
			FindNewData = false;
			UIPasswordScreen.GetComponent<UI_PasswordScreen>().ClearPassCode();
		}	
		if (EnterPassCodeState)
		{
			FindNewData = false;
			UIEnterPassCode.GetComponent<UI_EnterPassCode>().PassDataIn("PassCode");
		}	
		if (ImportDataState)
		{
			FindNewData = false;
			UIImportData.GetComponent<UI_ImportData>().PassDataIn("ImportData");
		}	
		if (PickerSkinState)
		{
			FindNewData = false;
			UIPickerSkin.GetComponent<UI_PickerSkinWindow>().PassDataIn("SkinName");
		}
		
		UIPreviousState = UICurrentState;
	}
	
	void FindTipDataByDate(System.DateTime SearchDate)
	{
		UIEnterData.GetComponent<UI_EnterData>().myDailyTipData = new List<TipData>(); 
		myDailyTipData = new List<TipData>();
		for(var i = 0; i < UserBlobManager.GetComponent<UserBlobManager>().DailyTipData.Count; i++)
		{
			if( UserBlobManager.GetComponent<UserBlobManager>().DailyTipData[i].Date.Year == SearchDate.Year && 
				UserBlobManager.GetComponent<UserBlobManager>().DailyTipData[i].Date.Month == SearchDate.Month &&
				UserBlobManager.GetComponent<UserBlobManager>().DailyTipData[i].Date.Day == SearchDate.Day )
			{
				myDailyTipData.Add(UserBlobManager.GetComponent<UserBlobManager>().DailyTipData[i]);
				myDailyTipData.TrimExcess();
				
//				print (myDailyTipData[0].Date.ToString());
			}
		}
		// add new temp entry so the list isnt empty
		if(myDailyTipData.Count < 1)
		{
			var tempTipData = new TipData();
			tempTipData.JobName = "Golden Nugget";
			tempTipData.Date = SearchDate;
			tempTipData.TipAmount = 0.0f;
			tempTipData.NumberOfDowns = 0;
			tempTipData.HoursWorked = 0.0f;
			tempTipData.TournamentDowns = false;
			tempTipData.TournamentName = "";
			tempTipData.NumberOfTournamentDowns = 0;
			tempTipData.TipOut = 0.0f;
			tempTipData.Notes = "";	
			tempTipData.WorkDay = false;
			tempTipData.WorkDayStartTime =  new System.DateTime(SearchDate.Year,SearchDate.Month,SearchDate.Day,0,0,0);
			tempTipData.PayCheckAmount = 0.0f;
			
			myDailyTipData.Add(tempTipData);
//			print (" added day because the current day was not found");
		}
	}
	
	void UpdateUserBlobData()
	{
		// remove all data for that day
		for(var i = 0; i < UserBlobManager.GetComponent<UserBlobManager>().DailyTipData.Count; i++)
		{
			if( UserBlobManager.GetComponent<UserBlobManager>().DailyTipData[i].Date.Year == currentDate.Date.Year &&  //myDailyTipData[0].Date.Year && 
				UserBlobManager.GetComponent<UserBlobManager>().DailyTipData[i].Date.Month == currentDate.Date.Month && //myDailyTipData[0].Date.Month &&
				UserBlobManager.GetComponent<UserBlobManager>().DailyTipData[i].Date.Day == currentDate.Date.Day) //myDailyTipData[0].Date.Day )
			{
				UserBlobManager.GetComponent<UserBlobManager>().DailyTipData.RemoveAt(i);
				UserBlobManager.GetComponent<UserBlobManager>().DailyTipData.Capacity = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData.Count;
				UserBlobManager.GetComponent<UserBlobManager>().DailyTipData.TrimExcess();
				i = -1; // have to do this because the length of the list is changing each time an element is deleted and i will be incremented to 0 on the next loop
			}
		}

		// add updated data for that day
		for(var i = 0; i < myDailyTipData.Count; i++)
		{
			UserBlobManager.GetComponent<UserBlobManager>().DailyTipData.Add(myDailyTipData[i]);
		}		
		
		UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
		UIEnterData.GetComponent<UI_EnterData>().myDailyTipData = new List<TipData>();
		myDailyTipData = new List<TipData>();		
	}
	
	void Update()
	{

		if (UpdateEnteredData == true)
		{
			UpdateUserBlobData();
			UpdateEnteredData = false;
			UICalendar.GetComponent<UI_Calendar>().UpdateCalendar();
			myDailyTipData = new List<TipData>();
		}
		
		if (UIPreviousState != UICurrentState)
		{
			SetUIState();
		}

		
		if (Input.touchCount != 0 ||
			Input.GetMouseButtonDown(0))
		{
			LastTouch = System.DateTime.Now;
			PassCodeTimer = UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCodeTimer;
		}
		
//		PassCodeTimer -= Time.deltaTime;
//		if(PassCodeTimer < 0 )
		if(LastTouch.AddMinutes((float)(PassCodeTimer/60)) <  System.DateTime.Now && PassCodeTimer > 0)
		{
			PassCodeTimer = UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCodeTimer;
			if(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode == true &&
				UICurrentState != "SplashScreenState")
			{
				UIPasswordScreen.GetComponent<UI_PasswordScreen>().ClearPassCode();
				SwitchStates("PasswordScreenState");
			}
		}
		
// check for swipe
		if(Input.touchCount >0)
		{
			var touch = Input.touches[0];
			
			if(CouldBeSwipe)
			{
				SwipeDist = (touch.position.x - SwipeStartPos.x);
//				shit = CouldBeSwipe.ToString() + " " + SwipeDist.ToString();
			}
			shit = CouldBeSwipe.ToString() + " " + SwipeDist.ToString();
				
			
			switch(touch.phase)
			{
			case TouchPhase.Began:
//				CouldBeSwipe = true;
				SwipeStartPos = touch.position;
				SwipeStartTime = Time.time;
				break;
			case TouchPhase.Moved:
				if(Mathf.Abs( touch.position.x - SwipeStartPos.x) > SwipeComfortZone)
				{
					CouldBeSwipe = false;
				}
				else
				{
					CouldBeSwipe = true;
				}
				break;
			case TouchPhase.Stationary:
//				CouldBeSwipe = false;
				break;
			case TouchPhase.Ended:
				var SwipeTime = Time.time - SwipeStartTime;
				SwipeDist = (touch.position - SwipeStartPos).magnitude;
					
				if (CouldBeSwipe && (SwipeTime < MaxSwipeTime) && (SwipeDist > MinSwipeDist))
				{
					// do some swipey thing
					var SwipeDirection = Mathf.Sign(touch.position.x - SwipeStartPos.x);
					
					// calendar swipe
					if(SwipeDirection > 0 && UICurrentState == "CalendarState")
					{
						UICalendar.GetComponent<UI_Calendar>().AddMonths -=1;
						UICalendar.GetComponent<UI_Calendar>().UpdateCalendar();						
					}
					if(SwipeDirection < 0 && UICurrentState == "CalendarState")
					{
						UICalendar.GetComponent<UI_Calendar>().AddMonths +=1;
						UICalendar.GetComponent<UI_Calendar>().UpdateCalendar();						
					}
					
					// expense report swipe
					if(SwipeDirection > 0 && UICurrentState == "MonthlySavingsReportState")
					{
						UIMonthlySavingsReport.GetComponent<UI_MonthlySavingsReport>().AddMonths -=1;
						UIMonthlySavingsReport.GetComponent<UI_MonthlySavingsReport>().UpdateMonthlySavingsReport();						
					}
					if(SwipeDirection < 0 && UICurrentState == "MonthlySavingsReportState")
					{
						UIMonthlySavingsReport.GetComponent<UI_MonthlySavingsReport>().AddMonths +=1;
						UIMonthlySavingsReport.GetComponent<UI_MonthlySavingsReport>().UpdateMonthlySavingsReport();						
					}
					
				}
				CouldBeSwipe = false;
				break;
			}
		}
	}
}
