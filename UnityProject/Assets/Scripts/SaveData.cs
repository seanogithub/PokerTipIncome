using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveData  {
	
	public UserData UserAccountData = new UserData();
	public AppData UserAppData = new AppData();
	public List<TipData> DailyTipData = new List<TipData>(); 
	public List<ExpenseData> MonthlyExpenseData = new List<ExpenseData>();
	
	public SaveData()
	{
		UserAccountData = new UserData();
		UserAppData = new AppData();
		DailyTipData = new List<TipData>(); 
		MonthlyExpenseData = new List<ExpenseData>();
	}

}
