using UnityEngine;
using System.Collections;

public class UI_PopUpYesNoDialogBox : MonoBehaviour {

	public GameObject UIManager;
	public GameObject UserBlobManager;
	
	public Component Sender;
//	public Texture2D PopUpBackgroundBitmap;
	public Texture2D styleCalendarButtonsBitmap;
	public Texture2D styleCalendarButtonsActiveBitmap;
	public Texture2D style2XButtonBlueBitmap;
	public Texture2D style2XButtonBlueActiveBitmap;
		
	public Vector2 PopUpPosition = new Vector2(0.2f, 0.25f);
	public Vector2 PopUpSize = new Vector2(0.625f, 0.40f);
	public string Message = "Message";
	public float DestroyTime = 1.5f;
	
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

	private GUIStyle stylePopUp;

	public Color FontColor;
	public Color ButtonColor;
	public Color BackgroundColor;
	
	public Font myFont;
	public int myFontSize = 30;
//	private float AspectRatioMultiplier = 0.57f;
	
	// Use this for initialization
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		
	}
	
	// Update is called once per frame
	void OnGUI () 
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
		
		stylePopUp = new GUIStyle(styleEnterDataButtons);
//		style.fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 3.0f);
		stylePopUp.fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
		stylePopUp.alignment = TextAnchor.MiddleCenter;
		stylePopUp.normal.textColor = Color.black;
		
		var windowRect = GUI.Window(0, (new Rect((Screen.width * PopUpPosition.x ), (Screen.width * PopUpPosition.y), (Screen.width * PopUpSize.x) ,((float)Screen.height * PopUpSize.y))), DoMyWindow, Message, stylePopUp);		
/*		
		// background image
		GUI.DrawTexture((new Rect((Screen.width * PopUpPosition.x ), (Screen.width * PopUpPosition.y), (Screen.width * PopUpSize.x) ,((float)Screen.height * PopUpSize.y))), PopUpBackgroundBitmap,ScaleMode.StretchToFill,true);
		GUI.Label(new Rect((Screen.width * PopUpPosition.x ), (Screen.width * PopUpPosition.y), 100, 20), Message, style);
*/

		
	}
    void DoMyWindow(int windowID) 
	{
		// ok button
		if (GUI.Button( new Rect((Screen.width * 0.2f) - (Screen.width * PopUpPosition.x), (Screen.height * 0.50f) - (Screen.height * PopUpPosition.y), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"OK", style2XButtonBlue))
		{
			Sender.SendMessage("YesNoDialog", "ok");
			Destroy(this.gameObject);
		}
		
		// cancel button
		if (GUI.Button( new Rect((Screen.width * 0.6f) - (Screen.width * PopUpPosition.x), (Screen.height * 0.50f)  - (Screen.height * PopUpPosition.y), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"Cancel", style2XButtonBlue))
		{
			Sender.SendMessage("YesNoDialog", "cancel");
			Destroy(this.gameObject);
		}
		
//        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World", style2XButtonBlue ))
//            print("Got a click");
    }	
	
}
