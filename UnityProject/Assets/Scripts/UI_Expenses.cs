using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class UI_Expenses : MonoBehaviour {

	public bool Enabled = true;

	public GameObject UIManager;
	public GameObject UserBlobManager;
		
	public string PopUpYesNoDialogState = "";
	private bool PopUpActive = false;
	
	public Texture2D BackgroundBitmap;
	public Texture2D styleCalendarButtonsBitmap;	
	public Texture2D styleCalendarButtonsActiveBitmap;	
	public Texture2D styleCalendarButtonsTodayBitmap;	
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
	
	public float HourlyWageAmount = 0;
	public float TaxPercentage = 0;
	public int NumberOfWorkingDaysPerMonth = 0;
	public string UsePayCheckDataString = "";
	
	private float AspectRatioMultiplier = 0.57f;
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		
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
			
			NumberOfWorkingDaysPerMonth = UserBlobManager.GetComponent<UserBlobManager>().UserAppData.NumberOfWorkingDaysPerMonth;			
			HourlyWageAmount = UserBlobManager.GetComponent<UserBlobManager>().UserAppData.HourlyWageAmount;
			TaxPercentage = UserBlobManager.GetComponent<UserBlobManager>().UserAppData.TaxPercentage;
			
			// background image
			GetComponent<GUITexture>().color = BackgroundColor;
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().EnterDataBackground;			

			// time and month and year		
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);
			
			// monthly expenses
			if (GUI.Button( new Rect((Screen.width * 0.10f), (Screen.height * 0.10f), (Screen.width * 0.80f), (Screen.height * 0.075f)) ,"Monthly Expenses", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("MonthlyExpensesState");
				}
			}	
			
			// days worked per month
			GUI.Label(new Rect((Screen.width * 0.15f), (Screen.height * 0.25f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Expected Number of", style2XBlack);
			GUI.Label(new Rect((Screen.width * 0.15f), (Screen.height * 0.28f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Work Days Per Month", style2XBlack);
			if(GUI.Button(new Rect((Screen.width * 0.60f), (Screen.height * 0.25f), (Screen.width * 0.25f), (Screen.height * 0.05f)), NumberOfWorkingDaysPerMonth.ToString(), styleEnterDataButtons))
			{
				if(PopUpActive == false)
				{
					UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDaysWorkedPerMonthState");
				}
			}	
			
/*			
			// use pay check
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.27f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Pay Check or Hourly Wage", style2XBlack);
			
			if(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePayCheckData)
			{
				UsePayCheckDataString = "Pay Check";
			}
			else
			{
				UsePayCheckDataString = "Hourly Wage";
			}
			
			if(GUI.Button(new Rect((Screen.width * 0.60f), (Screen.height * 0.27f), (Screen.width * 0.35f), (Screen.height * 0.05f)), UsePayCheckDataString, styleEnterDataButtons))
			{
				if(PopUpActive == false)
				{
					UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePayCheckData = !UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePayCheckData;
					if(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePayCheckData)
					{
						UsePayCheckDataString = "Pay Check";
					}
					else
					{
						UsePayCheckDataString = "Hourly Wage";
					}
				}
			}	
			
			// hourly wage amount
			
			if(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePayCheckData == false)
			{
			
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.34f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Hourly Wage", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.60f), (Screen.height * 0.34f), (Screen.width * 0.25f), (Screen.height * 0.05f)), HourlyWageAmount.ToString(), styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						UIManager.GetComponent<UI_Manager>().SwitchStates("EnterHourlyWageAmountState");
					}
				}	
			}
			
			// tax percentage
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.41f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Tax Percent", style2XBlack);
			if(GUI.Button(new Rect((Screen.width * 0.60f), (Screen.height * 0.41f), (Screen.width * 0.25f), (Screen.height * 0.05f)), TaxPercentage.ToString(), styleEnterDataButtons))
			{
				if(PopUpActive == false)
				{
//					PopUpMessage("Feature is coming soon.");
					UIManager.GetComponent<UI_Manager>().SwitchStates("EnterTaxPercentageState");
				}
			}	
*/
			
			// income/expense report
			if (GUI.Button( new Rect((Screen.width * 0.10f), (Screen.height * 0.35f), (Screen.width * 0.80f), (Screen.height * 0.075f)) ,"Monthly Income/Expense Report", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("MonthlySavingsReportState");
				}
			}	
			
/*			
			if (GUI.Button( new Rect((Screen.width * 0.10f), (Screen.height * 0.60f), (Screen.width * 0.80f), (Screen.height * 0.075f)) ,"Daily Expenses", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					PopUpMessage("Feature is coming soon.");
//					UIManager.GetComponent<UI_Manager>().SwitchStates("CalendarState");
				}
			}	
*/
			// back button
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.90f), (Screen.width * 0.5f), (Screen.height * 0.075f)) ,"Back", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("CalendarState");
				}
			}				
		}
	}

	void PopUpMessage(string myMessage)
	{
		var temp = Resources.Load("UI/UI_PopUpDialogBox_Prefab") as GameObject;
		var myPopUp = GameObject.Instantiate(temp, (new Vector3(0.0f, 0.0f , 1.0f)), Quaternion.identity ) as GameObject;
		myPopUp.GetComponent<UI_PopUpDialogBox>().Message = myMessage; 
	}	
	
}
