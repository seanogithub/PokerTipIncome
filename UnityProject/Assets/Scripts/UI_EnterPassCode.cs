using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

[RequireComponent(typeof(GUITexture))]
public class UI_EnterPassCode : MonoBehaviour {
	
	public bool Enabled = true;
	
	public GameObject UIManager;
	public GameObject UIEnterData;
	public GameObject UserBlobManager;
	public GameObject UIMonthlyExpenses;

	public Texture2D BackgroundBitmap;	
	public Texture2D styleCalendarButtonsBitmap;	
	public Texture2D styleCalendarButtonsActiveBitmap;	
	public Texture2D style2XButtonBlueBitmap;

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

	public TouchScreenKeyboard NumPadKeyBoard;
	
	public float myNumericValue;
	public string myNumericValueString;
	private string myNumericValueLabel;
	
	private string Digit_01 = "";
	private string Digit_02 = "";
	private string Digit_03 = "";
	private string Digit_04 = "";
	
	private float CheckNumberValue;
	private string PreviousmyNumericValueString;

	public int myTipDataIndex = 0;
	
	public string DataOutState = "Password";
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	
	// Use this for initialization
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UIEnterData = GameObject.Find("UI_EnterData_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		UIMonthlyExpenses = GameObject.Find("UI_MonthlyExpenses_Prefab");
		
//		NumPadKeyBoard = new TouchScreenKeyboard.Open(myNumericValueString, TouchScreenKeyboardType.NumbersAndPunctuation);

		AspectRatioMultiplier = ((float)Screen.width/1200 );

		
// need to pass this value in		
//		myNumericValue = 0.0f;
//		myNumericValueString = "";
//		myNumericValueLabel = "Tips $";
		
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
				
			styleEnterDataButtons.fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 6.0f);
			
			AspectRatioMultiplier = ((float)Screen.width/1200 );
	
			// background image
			GetComponent<GUITexture>().color = BackgroundColor;
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().EnterAlphaNumericValueBackground;
			
			// time and month and year		
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);
			
//			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.17f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), myNumericValueLabel, styleBigger);

			GUI.Label(new Rect((Screen.width * 0.30f), (Screen.height * 0.15f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Enter 4 Digit Pass Code", styleBigger);

			
#if UNITY_ANDROID || UNITY_IPHONE	
			if(GUI.Button (new Rect((Screen.width * 0.05f), (Screen.height * 0.20f), (Screen.width * 0.15f), (Screen.height * 0.10f)), Digit_01, styleEnterDataButtons))
			{
				if (TouchScreenKeyboard.visible == false )
				{
					NumPadKeyBoard = null;
					NumPadKeyBoard = new TouchScreenKeyboard(myNumericValueString, TouchScreenKeyboardType.PhonePad, true, false, false, false, "0");
				}			
			}

			if(GUI.Button (new Rect((Screen.width * 0.30f), (Screen.height * 0.20f), (Screen.width * 0.15f), (Screen.height * 0.10f)), Digit_02, styleEnterDataButtons))
			{
				if (TouchScreenKeyboard.visible == false )
				{
					NumPadKeyBoard = null;
					NumPadKeyBoard = new TouchScreenKeyboard(myNumericValueString, TouchScreenKeyboardType.PhonePad, true, false, false, false, "0");
				}			
			}

			if(GUI.Button (new Rect((Screen.width * 0.55f), (Screen.height * 0.20f), (Screen.width * 0.15f), (Screen.height * 0.10f)), Digit_03, styleEnterDataButtons))
			{
				if (TouchScreenKeyboard.visible == false )
				{
					NumPadKeyBoard = null;
					NumPadKeyBoard = new TouchScreenKeyboard(myNumericValueString, TouchScreenKeyboardType.PhonePad, true, false, false, false, "0");
				}			
			}
			
			if(GUI.Button (new Rect((Screen.width * 0.80f), (Screen.height * 0.20f), (Screen.width * 0.15f), (Screen.height * 0.10f)), Digit_04, styleEnterDataButtons))
			{
				if (TouchScreenKeyboard.visible == false )
				{
					NumPadKeyBoard = null;
					NumPadKeyBoard = new TouchScreenKeyboard(myNumericValueString, TouchScreenKeyboardType.PhonePad, true, false, false, false, "0");
				}			
			}

			myNumericValueString = NumPadKeyBoard.text;
			myNumericValueString = Regex.Replace(myNumericValueString, @"[^0-9.]", "");
			// only use first 4 numbers
			if (myNumericValueString.Length > 4)
			{
				myNumericValueString = myNumericValueString.Substring(0,4);
			}
			
			if (myNumericValueString != "")
			{
				
				CheckNumberValue = 0.0f;
				if(float.TryParse(myNumericValueString, System.Globalization.NumberStyles.Currency ,new CultureInfo("en-US"), out CheckNumberValue))
				{
					myNumericValueString = CheckNumberValue.ToString();
					PreviousmyNumericValueString = myNumericValueString;
				}
				else
				{
					myNumericValueString = PreviousmyNumericValueString;
					NumPadKeyBoard.text = PreviousmyNumericValueString;
				}
				
			}
			PreviousmyNumericValueString = myNumericValueString;			
	
#elif UNITY_EDITOR || UNITY_STANDALONE
			myNumericValueString = GUI.TextField (new Rect((Screen.width * 0.4f), (Screen.height * 0.17f), (Screen.width * 0.5f), (Screen.height * 0.05f)), myNumericValueString, 10, styleEnterDataButtons);
			myNumericValueString = Regex.Replace(myNumericValueString, @"[^0-9.]", "");
			
			if (myNumericValueString != "")
			{
				CheckNumberValue = 0.0f;
				if(float.TryParse(myNumericValueString, System.Globalization.NumberStyles.Currency ,new CultureInfo("en-US"), out CheckNumberValue))
				{
					myNumericValueString = CheckNumberValue.ToString();
					PreviousmyNumericValueString = myNumericValueString;
				}
				else
				{
					myNumericValueString = PreviousmyNumericValueString;
//					NumPadKeyBoard.text = PreviousmyNumericValueString;
				}
			}			
			PreviousmyNumericValueString = myNumericValueString;
#endif
	
			// ok button
			GUI.SetNextControlName ("OKButton");
			if (GUI.Button( new Rect((Screen.width * 0.2f), (Screen.height * 0.35f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"OK", style2XButtonBlue))
			{
				PassDataOut();
			}
			
			// cancel button
			GUI.SetNextControlName ("CancelButton");
			if (GUI.Button( new Rect((Screen.width * 0.6f), (Screen.height * 0.35f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"Cancel", style2XButtonBlue))
			{
//				CheckNumberValue = 1234;
				DontPassDataOut();
			}
		}
	}
	
	
	public void ClearPassCode()
	{
		myNumericValueString = "";
		if(NumPadKeyBoard != null)
		{
			NumPadKeyBoard.text = myNumericValueString;
		}
	}
	
	void Update()
	{
		if(Enabled == true)
		{
			if (NumPadKeyBoard == null )
			{
				NumPadKeyBoard = new TouchScreenKeyboard(myNumericValueString, TouchScreenKeyboardType.NumbersAndPunctuation, true, false, false, false, myNumericValueString );
			}
			
			if(myNumericValueString.Length > 0)
			{
				Digit_01 = myNumericValueString.Substring(0,1);
			}
			else
			{
				Digit_01 = "";
			}	
			
			if(myNumericValueString.Length > 1)
			{
				Digit_02 = myNumericValueString.Substring(1,1);
			}
			else
			{
				Digit_02 = "";
			}			

			if(myNumericValueString.Length > 2)
			{
				Digit_03 = myNumericValueString.Substring(2,1);
			}
			else
			{
				Digit_03 = "";
			}			

			if(myNumericValueString.Length > 3)
			{
				Digit_04 = myNumericValueString.Substring(3,1);
			}
			else
			{
				Digit_04 = "";
			}			
			
			
		}
	}
	

	
	public void PassDataIn( string DataName )
	{
		myTipDataIndex = UIEnterData.GetComponent<UI_EnterData>().myTipDataIndex;
		
		switch(DataName)
		{
		case "PassCode":
			if(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCode == "")
			{
				myNumericValue = 1234.0f; //(float)System.Convert.ToInt32("");
			}
			else
			{
				myNumericValue = (float)System.Convert.ToInt32(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCode);
			}
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Tips $";
			DataOutState = DataName;
			break;
		}
	}
	
	public void DontPassDataOut()
	{
		switch(DataOutState)
		{
		case "PassCode":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("SettingsState");
			break;
		}		
	}
	
	public void PassDataOut()
	{
		switch(DataOutState)
		{
		case "PassCode":
			if(CheckNumberValue.ToString().Length == 4)
			{
				UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCode = CheckNumberValue.ToString();
				NumPadKeyBoard = null;
				UIManager.GetComponent<UI_Manager>().SwitchStates("SettingsState");
			}
			else
			{
				PopUpMessage("Incorrect Pass Code");
			}
			break;
		}
	}
	
	void PopUpMessage(string myMessage)
	{
		var temp = Resources.Load("UI/UI_PopUpDialogBox_Prefab") as GameObject;
		var myPopUp = GameObject.Instantiate(temp, (new Vector3(0.0f, 0.0f , 1.0f)), Quaternion.identity ) as GameObject;
		myPopUp.GetComponent<UI_PopUpDialogBox>().Message = myMessage; 
	}	
}
