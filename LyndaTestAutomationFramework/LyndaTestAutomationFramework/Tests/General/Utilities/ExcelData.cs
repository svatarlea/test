/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/15/2012
 * Time: 11:16 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Ranorex;

namespace Tests.General.Utilities
{
	/// <summary>
	/// Description of ExcelData.
	/// </summary>
	public static class ExcelData
	{
		public static void Write(string strPath, int introw, int intcol, params string[] strdata)
		{
				
			    Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelWb;
                Excel.Worksheet excelWs;
                object misValue = System.Reflection.Missing.Value;
                
                try
                {
                
                	excelApp.DisplayAlerts = false;
                	excelWb = excelApp.Workbooks.Open(strPath,misValue,false,misValue,misValue,misValue,misValue,misValue, misValue, true, misValue, misValue, misValue);
                	excelWs = (Excel.Worksheet)excelWb.ActiveSheet;
                	excelApp.Visible = false;
                	for( int i=0; i < strdata.Length; i++)
                	{
                		excelWs.Cells[introw, intcol+i] = strdata[i];
                	}
                	excelWs.SaveAs(strPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                	excelApp.DisplayAlerts = true;
                	excelApp.Quit();
                	
                }
                catch(Exception e)
                {
                	Report.Log(ReportLevel.Error, e.ToString());
                }
                finally
                {
                	if (excelApp != null)
                		excelApp.Quit();
                }
		}
		
		public static object[,] Read(string strPath, string startcell, string endcell)
		{
			    Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelWb;
                Excel.Worksheet excelWs;
                Excel.Range excelRg;
                object misValue = System.Reflection.Missing.Value;
                object[,] results = null;
                try
                {
                	excelApp.DisplayAlerts = false;
                	excelWb = excelApp.Workbooks.Open(strPath,misValue,false,misValue,misValue,misValue,misValue,misValue, misValue, true, misValue, misValue, misValue);
                	excelWs = (Excel.Worksheet)excelWb.ActiveSheet;
                	excelRg = excelWs.Range[startcell,endcell];
                	excelApp.Visible = false;
                	results = (object[,])excelRg.Value;
                	
                	
                }
			    catch(Exception e)
                {
                	Report.Log(ReportLevel.Error, e.ToString());
                }
                finally
                {
                	if (excelApp != null)
                		excelApp.Quit();
                	
                }
                
                return results;
		}
	}
}
