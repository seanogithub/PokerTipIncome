using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class UI_BackgroundColorPicker : MonoBehaviour {
	
	public bool Enabled = true;

	public GameObject UIManager;
	public GameObject UserBlobManager;
	
	public Vector4 PosAndSizePercent = new Vector4(0.1f, 0.1f, 0.8f, 0.8f);
	
    public Texture2D colorPicker;
    public Texture2D colorPickerClear;
    public int ImageWidth = 512;
    public int ImageHeight = 512;
	public Color ColorPicked;

	public Texture2D BackgroundBitmap;
	public Texture2D BackgroundLineBitmap;
	
	public Texture2D style2XButtonBlueActiveBitmap;
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
	
	private float AspectRatioMultiplier = 0.57f;
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	
	void Start()
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");

	}

	public void GUIEnabled(bool myValue)
	{
//		style2XButtonBlue.normal.textColor = UserBlobManager.GetComponent<UserBlobManager>().BackGroundColor;
//		ColorPicked = UserBlobManager.GetComponent<UserBlobManager>().BackGroundColor;
		Enabled = myValue;
		this.GetComponent<GUITexture>().enabled = myValue;
	}
	
    void OnGUI()
	{
		if(Enabled == true)
		{
			FontColor = UserBlobManager.GetComponent<UserBlobManager>().FontColor;
			ButtonColor = UserBlobManager.GetComponent<UserBlobManager>().ButtonColor;
//			BackgroundColor =  UserBlobManager.GetComponent<UserBlobManager>().BackGroundColor;
			BackgroundColor = ColorPicked;
			
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
			GetComponent<GUITexture>().color = ColorPicked;
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().EnterDataBackground;

			// time and month and year		
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);
			
//			if (GUI.RepeatButton(new Rect(0, 500, ImageWidth, ImageHeight), colorPicker)) 
	        if (GUI.RepeatButton(new Rect((Screen.width * PosAndSizePercent.x), (Screen.height * PosAndSizePercent.y), (Screen.width * PosAndSizePercent.z), (Screen.width * PosAndSizePercent.z)), colorPickerClear)) 
			{
	            Vector2 pickpos = Event.current.mousePosition;
	            int aaa = Convert.ToInt32((pickpos.x - (Screen.width * PosAndSizePercent.x)) / (Screen.width * PosAndSizePercent.z) * colorPicker.width );
	            int bbb = Convert.ToInt32((pickpos.y - (Screen.height * PosAndSizePercent.y)) / (Screen.width * PosAndSizePercent.z) * colorPicker.height);
	            Color col = colorPicker.GetPixel(aaa,colorPicker.height - bbb);
	 			
				ColorPicked = col;
//				print (ColorPicked.ToString());
	        }
			
			// color picker texture
			GUI.DrawTexture(new Rect((Screen.width * PosAndSizePercent.x), (Screen.height * PosAndSizePercent.y), (Screen.width * PosAndSizePercent.z), (Screen.width * PosAndSizePercent.z)), colorPicker, ScaleMode.StretchToFill, true);
			
			// OK Button
			GUI.SetNextControlName ("OKButton");
			if (GUI.Button( new Rect((Screen.width * 0.2f), (Screen.height * 0.90f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"OK", style2XButtonBlue))
			{
				UserBlobManager.GetComponent<UserBlobManager>().BackGroundColor = ColorPicked;
				UserBlobManager.GetComponent<UserBlobManager>().ChangeCurrentSkin();
				UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
				UIManager.GetComponent<UI_Manager>().SwitchStates("ColorSettingsState");
			}
			
			// Cancel Button
			GUI.SetNextControlName ("CancelButton");
			if (GUI.Button( new Rect((Screen.width * 0.6f), (Screen.height * 0.90f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"Cancel", style2XButtonBlue))
			{
				UIManager.GetComponent<UI_Manager>().SwitchStates("ColorSettingsState");
			}			
		}
    }
}