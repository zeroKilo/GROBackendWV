#include "stdafx.h"
#include <Windows.h>
#include <stdio.h>
#include "HookedFunctions.h"
#include "ZenStuff.h"

char* logFilename = "GRO_Event_Log.txt";

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

void DetourFireFunctions();
void DetourEventHandlerFunctions();

void WriteByte(DWORD address, BYTE b)
{	
	DWORD old;
	VirtualProtect((LPVOID)address, 1, PAGE_EXECUTE_READWRITE, &old);
	*(BYTE*)address = b;
}

void WriteBuffer(DWORD address, BYTE* buff, int len)
{
	for(int i = 0; i < len; i++)
		WriteByte(address + i, buff[i]);
}

void EnableDebugScreen1()
{
	BYTE patch[] = {0x90, 0x90};
	WriteBuffer(baseAddressAI + 0x3C384, patch, 2);
	Log("Patched position for DebugScreen 1\n");
}
void EnableDebugScreen2()
{
	BYTE patch[] = {0x90, 0x90, 0x90, 0x90, 0x90, 0x90};
	WriteBuffer(baseAddressAI + 0x1B3716, patch, 2);
	WriteBuffer(baseAddressAI + 0x1B372C, patch, 2);
	WriteBuffer(baseAddressAI + 0x1BBABC, patch, 6);
	WriteBuffer(baseAddressAI + 0x1BBAD0, patch, 6);
	Log("Patched position for DebugScreen 2\n");
}
void EnableDebugScreen3()
{
	BYTE patch[] = {0x90, 0x90, 0x90, 0x90, 0x90, 0x90};
	WriteBuffer(baseAddressAI + 0x3BC57, patch, 6);
	Log("Patched position for DebugScreen 3\n");
}
void EnableDebugScreen4()
{
	BYTE patch[] = {0x90, 0x90};
	WriteBuffer(baseAddressAI + 0x1EC256, patch, 2);
	Log("Patched position for DebugScreen 4\n");
}
void EnableDebugScreen5()
{
	BYTE patch[] = {0x90, 0x90};
	WriteBuffer(baseAddressAI + 0xA798E, patch, 2);
	Log("Patched position for DebugScreen 5\n");
}

void Patch1()
{
	BYTE patch[] = {0xC3};
	WriteBuffer(baseAddressAI + 0xECC30, patch, 1);
	Log("Patched crash position 1\n");
}

void Patch2()
{
	BYTE patch[] = {0xC3};
	WriteBuffer(baseAddressAI + 0x1A2FB0, patch, 1);
	Log("Patched crash position 2\n");
}

void Patch3()
{
	BYTE patch[] = {0x90, 0x90};
	WriteBuffer(baseAddressAI + 0xA735F, patch, 2);
	Log("Patched keyboard input\n");
}

void Patch4()
{
	BYTE patch[] = {0xB0, 0x01, 0xC3, 0xCC, 0xCC, 0xCC, 0xCC};//mov al, 1h; ret
	WriteBuffer(baseAddressAI + 0x2A560, patch, 7);
	Log("Patched cDNAManager::bCanSendEvent\n");
}

void Patch5()
{
	BYTE patch[] = {0x90, 0x90, 0x90, 0x90, 0x90, 0x90};
	WriteBuffer(baseAddressAI + 0xD1B55, patch, 6);
	Log("Patched AI_EntityPlayer::InitEntity server check\n");
}

void Patch6()
{
	BYTE patch[] = {0xC3, 0xCC};//return;
	WriteBuffer(baseAddressAI + 0x1D0CB0, patch, 2);
}

void CreateServerPatch()
{
	/*
	push edi
	nop x53
	*/
	BYTE push_edi[] = { 0x57, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90,
						0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 
						0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 
						0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 
						0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90,
						0x90, 0x90, 0x90, 0x90 };
	WriteBuffer(baseAddressAI + 0xE9C0, push_edi, 54);

	/*
	pop edi
	nop x6
	*/
	//relies on ZF=0 state!
	BYTE pop_edi[] = { 0x5F, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
	WriteBuffer(baseAddressAI + 0xE9FB, pop_edi, 7);

	/*
	nop x8
	lea ecx, ds:0x0044CDC0
	call ecx
	*/
	BYTE patch[] = {0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90,
					0x8D, 0x0D, 0xC0, 0xCD, 0x44, 0x00,
					0xFF, 0xD1 };
	WriteBuffer(baseAddressAI + 0x116EE, patch, 16);
	Log("The glorious server patch applied\n");
}

void OnPeerConnectionPatch()
{
	//nop x52
	BYTE patch[] = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 
					0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 
					0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 
					0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 
					0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90,
					0x90, 0x90 };
	WriteBuffer(baseAddressAI + 0x11326, patch, 52);
	Log("AI_NetworkManager::OnPeerConnection assertion patched\n");
}

void IsServerPatch()
{
	BYTE patch[] = { 0xB0, 0x01 };
	WriteBuffer(baseAddressAI + 0x341B, patch, 2);
	Log("Patched AI_NetworkManager::IsServer\n");
}

void GetPeerIndexPatch()
{
	BYTE patch[] = { 0x01 };
	WriteBuffer(baseAddressAI + 0x1036F, patch, 1);
	Log("Patched AI_NetworkManager::GetPeerIndex");
}


void ExportPlayerAddress()
{
	org_AI_EntityPlayer_UpdateWarning = (void(__fastcall*) (void*,void*)) DetourFunction((PBYTE)(baseAddressAI + 0xC86F0),(PBYTE)AI_EntityPlayer_UpdateWarning);
	Log("Hooked AI_EntityPlayer::UpdateWarning\n");
}

void ReplaceVelocity()
{
	DetourFunction((PBYTE)(baseAddressAI + 0x79DC0),(PBYTE)GetVelocity);
	Log("Replaced GetVelocity\n");
}

void DetourMain()
{
	char buffer[512];
	if(FileExists("_debug_output_"))
	OpenConsole();
	ClearFile(logFilename);
	Log("GRO Hook made by Warranty Voider\n");
	baseAddressAI = (DWORD)GetModuleHandleA("AICLASS_PCClient_R_org.dll");
	if(!baseAddressAI)
	{
		Log("AI DLL not found, exit...\n");
		return;
	}
	sprintf(buffer,"AI  DLL Base = 0x%08X\n\0", baseAddressAI);
	Log(buffer);
	baseAddressRDV = (DWORD)GetModuleHandleA("RDVDLL.dll");
	if(!baseAddressRDV)
	{
		Log("RDV DLL not found, exit...\n");
		return;
	}
	sprintf(buffer,"RDV DLL Base = 0x%08X\n\0", baseAddressRDV);
	Log(buffer);
	DetourFireFunctions();
	DetourEventHandlerFunctions();
	//EnableDebugScreen1();
	//EnableDebugScreen2();
	//EnableDebugScreen3();
	//EnableDebugScreen4();
	//EnableDebugScreen5();
	Patch1();	//crash1
	Patch2();	//crash2
	Patch3();	//keyboard input
	Patch4();	//cDNAManager::bCanSendEvent
	Patch5();	//AI_EntityPlayer::InitEntity server check
	Patch6();	//cObjectHealth::SetDefaultHitPointsServer call cancel
	//CreateServerPatch();
	//OnPeerConnectionPatch();
	//IsServerPatch();
	//GetPeerIndexPatch();
	ExportPlayerAddress();
	//ReplaceVelocity();
	//ZEN_Init(baseAddressAI);
}

void DetourFireFunctions()
{
	org_FIR_SendEvent = (DWORD(__cdecl*) (int,char*)) DetourFunction((PBYTE)(baseAddressAI + 0xF4C20),(PBYTE)FIR_SendEvent);
	Log("Hooked AIDLL::FIR_SendEvent\n");
	org_FIR_SetVariableString = (DWORD(__cdecl*) (int,char*,char*)) DetourFunction((PBYTE)(baseAddressAI + 0xF4DC0),(PBYTE)FIR_SetVariableString);
	Log("Hooked AIDLL::FIR_SetVariableString\n");
	org_FIR_SetVariableUniString = (DWORD(__cdecl*) (int,char*,wchar_t*)) DetourFunction((PBYTE)(baseAddressAI + 0xF4F70),(PBYTE)FIR_SetVariableUniString);
	Log("Hooked AIDLL::FIR_SetVariableUniString\n");
	org_FIR_SetVariableBool = (DWORD(__cdecl*) (int,char*,bool)) DetourFunction((PBYTE)(baseAddressAI + 0xF4E50),(PBYTE)FIR_SetVariableBool);
	Log("Hooked AIDLL::FIR_SetVariableBool\n");
	org_FIR_SetVariableInt = (DWORD(__cdecl*) (int,char*,int)) DetourFunction((PBYTE)(baseAddressAI + 0xF4EE0),(PBYTE)FIR_SetVariableInt);
	Log("Hooked AIDLL::FIR_SetVariableInt\n");
	org_FIR_SetVariableFloat = (DWORD(__cdecl*) (int,char*,float)) DetourFunction((PBYTE)(baseAddressAI + 0xF4B90),(PBYTE)FIR_SetVariableFloat);
	Log("Hooked AIDLL::FIR_SetVariableFloat\n");
	org_FIR_LoadPackage = (DWORD(__cdecl*) (int)) DetourFunction((PBYTE)(baseAddressAI + 0xF4CB0),(PBYTE)FIR_LoadPackage);
	Log("Hooked AIDLL::FIR_LoadPackage\n");
	org_FIR_UnloadPackage = (DWORD(__cdecl*) (int)) DetourFunction((PBYTE)(baseAddressAI + 0xF4D40),(PBYTE)FIR_UnloadPackage);
	Log("Hooked AIDLL::FIR_UnloadPackage\n");
	org_FIR_GetPackageKeyFromBank = (DWORD(__cdecl*) (int,int)) DetourFunction((PBYTE)(baseAddressAI + 0xF5000),(PBYTE)FIR_GetPackageKeyFromBank);
	Log("Hooked AIDLL::FIR_GetPackageKeyFromBank\n");
	org_FIR_GetASDataManager = (DWORD(__cdecl*) ()) DetourFunction((PBYTE)(baseAddressAI + 0x10B590),(PBYTE)FIR_GetASDataManager);
	Log("Hooked AIDLL::FIR_GetASDataManager\n");
	org_UI_DispatchEvent = (DWORD(__fastcall*) (EventCaller*,void*,int,int,int)) DetourFunction((PBYTE)(baseAddressAI + 0x15E350),(PBYTE)UI_DispatchEvent);
	Log("Hooked AIDLL::UI_DispatchEvent\n");
	org_FIR_FireEvent = (DWORD(__fastcall*) (void*,void*)) DetourFunction((PBYTE)(baseAddressAI + 0x1079D0),(PBYTE)FIR_FireEvent);
	Log("Hooked AIDLL::FIR_FireEvent\n");
}

void DetourEventHandlerFunctions()
{
	AddHandler((DWORD*)(baseAddressAI + 0x2A7EEC), "AI_AbilityCustomizePopUp");
	AddHandler((DWORD*)(baseAddressAI + 0x2A4480), "AI_AchievementPage");
	AddHandler((DWORD*)(baseAddressAI + 0x2A771C), "AI_AdWidget");
	AddHandler((DWORD*)(baseAddressAI + 0x2A7864), "AI_ArmorCustomizePopUp");
	AddHandler((DWORD*)(baseAddressAI + 0x2A47E8), "AI_AvatarSelectionWidget");
	AddHandler((DWORD*)(baseAddressAI + 0x2A748C), "AI_CharacterSelectionScreen");
	AddHandler((DWORD*)(baseAddressAI + 0x2A22F8), "AI_ChatPanel");
	AddHandler((DWORD*)(baseAddressAI + 0x2A33E4), "AI_DynamicMenu");
	AddHandler((DWORD*)(baseAddressAI + 0x2A75E0), "AI_ExpiredPopUp");
	AddHandler((DWORD*)(baseAddressAI + 0x2A7674), "AI_ExpiryWidget");
	AddHandler((DWORD*)(baseAddressAI + 0x2A242C), "AI_FlyingText");
	AddHandler((DWORD*)(baseAddressAI + 0x2A2A5C), "AI_FriendList");
	AddHandler((DWORD*)(baseAddressAI + 0x2A1580), "AI_GlobalUI");
	AddHandler((DWORD*)(baseAddressAI + 0x2A7A34), "AI_HomePage");
	AddHandler((DWORD*)(baseAddressAI + 0x2A2524), "AI_HudChatPanel");
	AddHandler((DWORD*)(baseAddressAI + 0x2A7B5C), "AI_IgnoreList");
	AddHandler((DWORD*)(baseAddressAI + 0x2A4368), "AI_InGameAchievement");
	AddHandler((DWORD*)(baseAddressAI + 0x2A4400), "AI_InGameCallSign");
	AddHandler((DWORD*)(baseAddressAI + 0x2A41E4), "AI_InGameScore");
	AddHandler((DWORD*)(baseAddressAI + 0x2A3458), "AI_InGameUI");
	AddHandler((DWORD*)(baseAddressAI + 0x2A7CC4), "AI_Inbox");
	AddHandler((DWORD*)(baseAddressAI + 0x2A2C48), "AI_InviteCheckList");
	AddHandler((DWORD*)(baseAddressAI + 0x2A5EF4), "AI_LeaderboardPage");
	AddHandler((DWORD*)(baseAddressAI + 0x2A2854), "AI_LoadingChatPanel");
	AddHandler((DWORD*)(baseAddressAI + 0x2A30D8), "AI_LoadingScreen");
	AddHandler((DWORD*)(baseAddressAI + 0x2A5D58), "AI_Login");
	AddHandler((DWORD*)(baseAddressAI + 0x2A3590), "AI_MatchRewardWidget");
	AddHandler((DWORD*)(baseAddressAI + 0x2A5B50), "AI_MissionWidget");
	AddHandler((DWORD*)(baseAddressAI + 0x2A31B4), "AI_MultiplayerMenu");
	AddHandler((DWORD*)(baseAddressAI + 0x2A6484), "AI_OptionsAudioPanel");
	AddHandler((DWORD*)(baseAddressAI + 0x2A6514), "AI_OptionsGeneralPanel");
	AddHandler((DWORD*)(baseAddressAI + 0x2A65BC), "AI_OptionsKeyMappingSPPanel");
	AddHandler((DWORD*)(baseAddressAI + 0x2A6784), "AI_OptionsPage");
	AddHandler((DWORD*)(baseAddressAI + 0x2A67E8), "AI_OptionsVideoPanel");
	AddHandler((DWORD*)(baseAddressAI + 0x2A6894), "AI_OptionsVoipPanel");
	AddHandler((DWORD*)(baseAddressAI + 0x2A2F28), "AI_PartyWidget");
	AddHandler((DWORD*)(baseAddressAI + 0x2A3678), "AI_PostGameScore");
	AddHandler((DWORD*)(baseAddressAI + 0x2A3788), "AI_PostGameSummary");
	AddHandler((DWORD*)(baseAddressAI + 0x2A2FB8), "AI_PreGameLobby");
	AddHandler((DWORD*)(baseAddressAI + 0x2A4F30), "AI_ProfilePage");
	AddHandler((DWORD*)(baseAddressAI + 0x2A1940), "AI_ProfileWidget");
	AddHandler((DWORD*)(baseAddressAI + 0x2A28C8), "AI_RoomList");
	AddHandler((DWORD*)(baseAddressAI + 0x2A3038), "AI_SingleTeamPreGameLobby");
	AddHandler((DWORD*)(baseAddressAI + 0x2A801C), "AI_SocialMenuWidget");
	AddHandler((DWORD*)(baseAddressAI + 0x2A80B0), "AI_TutorialLobby");
	AddHandler((DWORD*)(baseAddressAI + 0x29FF18), "AI_UICoreManager_0");
	AddHandler((DWORD*)(baseAddressAI + 0x2A0108), "AI_UICoreManager_1");
	AddHandler((DWORD*)(baseAddressAI + 0x2A0540), "AI_UICoreManager_2");
	AddHandler((DWORD*)(baseAddressAI + 0x2A0B20), "AI_UICoreManager_3");
	AddHandler((DWORD*)(baseAddressAI + 0x2A3F28), "AI_WeaponCustomizePopUp");
}