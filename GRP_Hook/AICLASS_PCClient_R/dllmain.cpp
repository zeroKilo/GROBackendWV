#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <tchar.h>
#include "Header.h"
#pragma pack(1)



extern "C" BOOL WINAPI DllMain(HINSTANCE hInst,DWORD reason,LPVOID)
{
	static HINSTANCE hL;
	if (reason == DLL_PROCESS_ATTACH)
	{
		hL = LoadLibrary(_T(".\\AICLASS_PCClient_R_org.dll"));
		if (!hL) return false;
		if(!FileExists("firstrun"))
			ClearFile("firstrun");
		else
		{
			DeleteFileA("firstrun");
			DetourMain();
		}
	}
	if (reason == DLL_PROCESS_DETACH)
		FreeLibrary(hL);
	return TRUE;
}

#pragma comment(linker, "/export:AI_BootClassDLL=AICLASS_PCClient_R_org.AI_BootClassDLL")


