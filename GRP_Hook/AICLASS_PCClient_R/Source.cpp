#include "stdafx.h"
#include <Windows.h>
#include <stdio.h>
#include "HookedFunctions.h"

char* logFilename = "GRP_Event_Log.txt";

void OpenConsole()
{
	AllocConsole();
	freopen("conin$","r",stdin);
	freopen("conout$","w",stdout);
	freopen("conout$","w",stderr);
	HWND consoleHandle = GetConsoleWindow();
	MoveWindow(consoleHandle,1,1,680,480,1);
	printf("Console initialized.\n");
}

bool IsThisExeName(wchar_t* name)
{
	wchar_t szFileName[MAX_PATH + 1];
	GetModuleFileName(NULL, szFileName, MAX_PATH + 1);
	return wcsstr(szFileName, name) != NULL;
}

bool FileExists(char* str)
{
	DWORD dwAttrib = GetFileAttributesA(str);
	return (dwAttrib != INVALID_FILE_ATTRIBUTES && !(dwAttrib & FILE_ATTRIBUTE_DIRECTORY));
}

void StartThread(LPTHREAD_START_ROUTINE func)
{
	DWORD dwThreadId, dwThrdParam = 1;
	HANDLE hThread;
	hThread = CreateThread(NULL, 0, func, &dwThrdParam, 0, &dwThreadId);
}

void ClearFile(char* str)
{
	FILE* fp = fopen (str, "w");
	fclose(fp);
}

void LogToFile(char* str)
{
	FILE* fp = fopen(logFilename, "a+");
	fprintf(fp, str);
	fclose(fp);
}

void Log(char* str)
{
	printf("%s", str);
	LogToFile(str);
}

void DetourMain()
{
	char buffer[512];
	ClearFile(logFilename);
	OpenConsole();
	Log("GRP Hook made by Warranty Voider\n");
	baseAddress = (DWORD)GetModuleHandleA("AICLASS_PCClient_R_org.dll");
	sprintf(buffer,"AI DLL Base = 0x%08X\n\0", baseAddress);
	Log(buffer);
	if(!baseAddress)
	{
		Log("DLL not found, exit...\n");
		return;
	}
	org_FIR_SendEvent = (DWORD(__cdecl*) (int,char*)) DetourFunction((PBYTE)(baseAddress + 0xF4C20),(PBYTE)FIR_SendEvent);
	Log("Hooked AIDLL::FIR_SendEvent\n");
	org_FIR_SetVariableString = (DWORD(__cdecl*) (int,char*,char*)) DetourFunction((PBYTE)(baseAddress + 0xF4DC0),(PBYTE)FIR_SetVariableString);
	Log("Hooked AIDLL::FIR_SetVariableString\n");
	org_FIR_SetVariableUniString = (DWORD(__cdecl*) (int,char*,wchar_t*)) DetourFunction((PBYTE)(baseAddress + 0xF4F70),(PBYTE)FIR_SetVariableUniString);
	Log("Hooked AIDLL::FIR_SetVariableUniString\n");
	org_FIR_SetVariableBool = (DWORD(__cdecl*) (int,char*,bool)) DetourFunction((PBYTE)(baseAddress + 0xF4E50),(PBYTE)FIR_SetVariableBool);
	Log("Hooked AIDLL::FIR_SetVariableBool\n");
	org_FIR_SetVariableInt = (DWORD(__cdecl*) (int,char*,int)) DetourFunction((PBYTE)(baseAddress + 0xF4EE0),(PBYTE)FIR_SetVariableInt);
	Log("Hooked AIDLL::FIR_SetVariableInt\n");
	org_FIR_SetVariableFloat = (DWORD(__cdecl*) (int,char*,float)) DetourFunction((PBYTE)(baseAddress + 0xF4B90),(PBYTE)FIR_SetVariableFloat);
	Log("Hooked AIDLL::FIR_SetVariableFloat\n");
	org_FIR_LoadPackage = (DWORD(__cdecl*) (int)) DetourFunction((PBYTE)(baseAddress + 0xF4CB0),(PBYTE)FIR_LoadPackage);
	Log("Hooked AIDLL::FIR_LoadPackage\n");
	org_FIR_UnloadPackage = (DWORD(__cdecl*) (int)) DetourFunction((PBYTE)(baseAddress + 0xF4D40),(PBYTE)FIR_UnloadPackage);
	Log("Hooked AIDLL::FIR_UnloadPackage\n");
	org_FIR_GetPackageKeyFromBank = (DWORD(__cdecl*) (int,int)) DetourFunction((PBYTE)(baseAddress + 0xF5000),(PBYTE)FIR_GetPackageKeyFromBank);
	Log("Hooked AIDLL::FIR_GetPackageKeyFromBank\n");
	org_FIR_GetASDataManager = (DWORD(__cdecl*) ()) DetourFunction((PBYTE)(baseAddress + 0x10B590),(PBYTE)FIR_GetASDataManager);
	Log("Hooked AIDLL::FIR_GetASDataManager\n");
	org_UI_DispatchEvent = (DWORD(__fastcall*) (EventCaller*,void*,int,int,int)) DetourFunction((PBYTE)(baseAddress + 0x15E350),(PBYTE)UI_DispatchEvent);
	Log("Hooked AIDLL::UI_DispatchEvent\n");
	org_FIR_FireEvent = (DWORD(__fastcall*) (void*,void*)) DetourFunction((PBYTE)(baseAddress + 0x1079D0),(PBYTE)FIR_FireEvent);
	Log("Hooked AIDLL::FIR_FireEvent\n");
	org_AI_GlobalUI_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x109930),(PBYTE)AI_GlobalUI_OnBusEvent);
	Log("Hooked AIDLL::AI_GlobalUI::OnBusEvent\n");
	org_AI_Login_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x137070),(PBYTE)AI_Login_OnBusEvent);
	Log("Hooked AIDLL::AI_Login::OnBusEvent\n");
	org_AI_FriendList_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x115860),(PBYTE)AI_FriendList_OnBusEvent);
	Log("Hooked AIDLL::AI_FriendList::OnBusEvent\n");
	org_AI_NetworkManager_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x10AE0),(PBYTE)AI_NetworkManager_OnBusEvent);
	Log("Hooked AIDLL::AI_NetworkManager::OnBusEvent\n");
	org_AI_HudChatPanel_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x113550),(PBYTE)AI_HudChatPanel_OnBusEvent);
	Log("Hooked AIDLL::AI_HudChatPanel::OnBusEvent\n");
	org_AI_PartyWidget_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x118550),(PBYTE)AI_PartyWidget_OnBusEvent);
	Log("Hooked AIDLL::AI_PartyWidget::OnBusEvent\n");
	org_AI_InGameScore_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x126BE0),(PBYTE)AI_InGameScore_OnBusEvent);
	Log("Hooked AIDLL::AI_InGameScore::OnBusEvent\n");
	org_AI_InGameAchievement_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x126FF0),(PBYTE)AI_InGameAchievement_OnBusEvent);
	Log("Hooked AIDLL::AI_InGameAchievement::OnBusEvent\n");
	org_AI_IgnoreList_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x146EF0),(PBYTE)AI_IgnoreList_OnBusEvent);
	Log("Hooked AIDLL::AI_IgnoreList::OnBusEvent\n");
	org_AI_GameSessionModel_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x158C00),(PBYTE)AI_GameSessionModel_OnBusEvent);
	Log("Hooked AIDLL::AI_GameSessionModel::OnBusEvent\n");
	org_WebBrowserManager_OnBusEvent = (DWORD(__fastcall*) (void*,void*,AIEvent*)) DetourFunction((PBYTE)(baseAddress + 0x1F0B10),(PBYTE)WebBrowserManager_OnBusEvent);
	Log("Hooked AIDLL::WebBrowserManager::OnBusEvent\n");
}