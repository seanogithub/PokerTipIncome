using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GUITexture))]
public class UI_MonthlyExpenses : MonoBehaviour {

	public bool Enabled = true;
	
	public List<ExpenseData> MonthlyExpenseData = new List<ExpenseData>();
	private System.DateTime currentDate = new System.DateTime(); 
	private string CurrentMonthString = "";
	private int AddMonths = 0;
	public int CurrentMonthlyExpenseIndex = 0;
	public bool MonthyExpensesFound = false;
	public float MonthyExpenseTotal = 0.0f;
	public int myExpenseNameIndex = 0;
	public int myExpenseAmountIndex = 0;
	
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
	private GUIStyle style2XButtonBudget;

	public Color FontColor;
	public Color ButtonColor;
	public Color BackgroundColor;
	
	private float AspectRatioMultiplier = 0.57f;
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
			
		UpdateMonthlyExpenses();
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
			
			// needed to make this extras style to left justify the butons on this screen			
			style2XButtonBudget = new GUIStyle(style2XButtonBlue);
			style2XButtonBudget.alignment = TextAnchor.MiddleLeft;
			
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
				UpdateMonthlyExpenses();
			}
	
			if (GUI.Button(new Rect((Screen.width * 0.86f), (Screen.height* 0.0f), (Screen.width * 0.14f ), (Screen.height* 0.09f)), styleCalendarRightArrowButtonBitmap, style2XButtonBlue))
			{
				AddMonths +=1;
				UpdateMonthlyExpenses();
			}
			
			// del button
			if(MonthyExpensesFound == true && MonthlyExpenseData.Count > 0)
			{
				if (GUI.Button( new Rect((Screen.width * 0.12f), (Screen.height * 0.02f), (Screen.width * 0.15f), (Screen.height * 0.05f)) ,"Del", style2XButtonBlue))
				{
					if(PopUpActive == false)
					{				
						PopUpYesNoDialog("Are you sure you want to delete this data?", this.gameObject.GetComponent<UI_MonthlyExpenses>(), "DeleteMonltyExpenseData");
	//					RemoveData();
					}
				}
			}
			
			
			// MonthlyExpenseData has no entries
			if(MonthyExpensesFound == true && MonthlyExpenseData.Count > 0)
//			if(MonthlyExpenseData.Count > 0)
			{
				// has at least one entry
				if( MonthlyExpenseData[CurrentMonthlyExpenseIndex] == null)
				{
					if (GUI.Button(new Rect((Screen.width * 0.20f), (Screen.height* 0.40f), (Screen.width * 0.60f ), (Screen.height* 0.09f)), "Add Monthly Expenses", style2XButtonBlue))
					{
						if(PopUpActive == false)
						{					
							AddNewBudget();
							print ("add new budget");	
						}
					}				
				}
				else
				{
					for(int i = 0; i < 15 ; i++ )
					{
						if (GUI.Button( new Rect((Screen.width * 0.05f), (Screen.height * (i * 0.05f)) + (Screen.height * 0.08f), (Screen.width * 0.65f), (Screen.height * 0.05f)) , MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseName[i].ToString(), style2XButtonBudget))
						{
							if(PopUpActive == false)
							{			
								myExpenseNameIndex = i;
								UIManager.GetComponent<UI_Manager>().SwitchStates("PickerExpenseNameState");
							}
						}				
					}
					for(int i = 0; i < 15 ; i++ )
					{
						if (GUI.Button( new Rect((Screen.width * 0.75f), (Screen.height * (i * 0.05f)) + (Screen.height * 0.08f), (Screen.width * 0.20f), (Screen.height * 0.05f)) , "$ " + MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseAmount[i].ToString(), style2XButtonBudget))
						{
							if(PopUpActive == false)
							{				
								myExpenseAmountIndex = i;
								UIManager.GetComponent<UI_Manager>().SwitchStates("EnterExpenseAmountState");
							}
						}				
					}
					
					GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.85f), 100, 20), "Total", styleBigger);
					GUI.Label(new Rect((Screen.width * 0.75f), (Screen.height* 0.85f), 100, 20), (" $ " + MonthyExpenseTotal.ToString()), styleBigger);
					
				}	
			}
			else
			{
				if (GUI.Button(new Rect((Screen.width * 0.20f), (Screen.height* 0.40f), (Screen.width * 0.60f ), (Screen.height* 0.09f)), "Add Monthly Expenses", style2XButtonBlue))
				{
					if(PopUpActive == false)
					{					
						AddNewBudget();
						print ("add new budget");	
					}
				}				
			}
			
			
			// back button
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.90f), (Screen.width * 0.5f), (Screen.height * 0.075f)) ,"Back", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{	
					PassDataOut();			
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
	
	void AddNewBudget()
	{
		var tempExpenseData = new ExpenseData();
		
		tempExpenseData.Date = currentDate;
				
		tempExpenseData.ExpenseName[0] = "Mortgage";
		tempExpenseData.ExpenseName[1] = "Car";
		tempExpenseData.ExpenseName[2] = "Car Insurance";
		tempExpenseData.ExpenseName[3] = "Gas";
		tempExpenseData.ExpenseName[4] = "Food";
		tempExpenseData.ExpenseName[5] = "Phone";
		tempExpenseData.ExpenseName[6] = "Cable";
		tempExpenseData.ExpenseName[7] = "Water";
		tempExpenseData.ExpenseName[8] = "Power";
		tempExpenseData.ExpenseName[9] = "Utilites";
		tempExpenseData.ExpenseName[10] = "Credit Card 1";
		tempExpenseData.ExpenseName[11] = "Credit Card 2";
		tempExpenseData.ExpenseName[12] = "Credit Card 3";
		tempExpenseData.ExpenseName[13] = "Other";
		tempExpenseData.ExpenseName[14] = "Misc";
		
		tempExpenseData.ExpenseAmount[0] = 1500.0f;
		tempExpenseData.ExpenseAmount[1] = 250.0f;
		tempExpenseData.ExpenseAmount[2] = 150.0f;
		tempExpenseData.ExpenseAmount[3] = 400.0f;
		tempExpenseData.ExpenseAmount[4] = 75.0f;
		tempExpenseData.ExpenseAmount[5] = 125.0f;
		tempExpenseData.ExpenseAmount[6] = 50.0f;
		tempExpenseData.ExpenseAmount[7] = 100.0f;
		tempExpenseData.ExpenseAmount[8] = 0.0f;
		tempExpenseData.ExpenseAmount[9] = 0.0f;
		tempExpenseData.ExpenseAmount[10] = 0.0f;
		tempExpenseData.ExpenseAmount[11] = 0.0f;
		tempExpenseData.ExpenseAmount[12] = 0.0f;
		tempExpenseData.ExpenseAmount[13] = 0.0f;
		tempExpenseData.ExpenseAmount[14] = 0.0f;
		
		MonthlyExpenseData.Add(tempExpenseData);
		
		UpdateMonthlyExpenses();

		UserBlobManager.GetComponent<UserBlobManager>().MonthlyExpenseData = MonthlyExpenseData;
		UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
	}
	
	public void UpdateMonthlyExpenses()
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
		
	}
	
	public void PassDataIn()
	{
//		AddMonths = 0;
		MonthlyExpenseData = new List<ExpenseData>();
		MonthlyExpenseData = UserBlobManager.GetComponent<UserBlobManager>().MonthlyExpenseData;
		UpdateMonthlyExpenses();
	}

	public void PassDataOut()
	{
		UserBlobManager.GetComponent<UserBlobManager>().MonthlyExpenseData = MonthlyExpenseData;
		UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
	}
	
	void RemoveData()
	{
		MonthlyExpenseData.TrimExcess();
		MonthlyExpenseData.RemoveAt(CurrentMonthlyExpenseIndex);
		UpdateMonthlyExpenses();
		UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
	}
	
	void PopUpMessage(string myMessage)
	{
		var temp = Resources.Load("UI/UI_PopUpDialogBox_Prefab") as GameObject;
		var myPopUp = GameObject.Instantiate(temp, (new Vector3(0.0f, 0.0f , 1.0f)), Quaternion.identity ) as GameObject;
		myPopUp.GetComponent<UI_PopUpDialogBox>().Message = myMessage; 
	}		

	void PopUpYesNoDialog(string myMessage, Component mySender, string myState)
	{
		var temp = Resources.Load("UI/UI_PopUpYesNoDialogBox_Prefab") as GameObject;
		var myPopUpYesNo = GameObject.Instantiate(temp, (new Vector3(0.0f, 0.0f , 1.0f)), Quaternion.identity ) as GameObject;
		myPopUpYesNo.GetComponent<UI_PopUpYesNoDialogBox>().Message = myMessage; 
		myPopUpYesNo.GetComponent<UI_PopUpYesNoDialogBox>().Sender = mySender; 
		PopUpYesNoDialogState = myState;
		PopUpActive = true;
	}
		
	public void YesNoDialog(string DialogState)
	{
		switch(PopUpYesNoDialogState)
		{
		case "DeleteMonltyExpenseData":
			if(DialogState == "ok")
			{
				RemoveData();
			}
			if(DialogState == "cancel")
			{
				print ("DeleteMonltyExpenseData cancel");
			}
			break;			
		}
		PopUpYesNoDialogState = "";	
		PopUpActive = false;
	}	
}
