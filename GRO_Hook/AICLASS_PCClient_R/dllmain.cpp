#include "stdafx.h"
#include <windows.h>
#include <stdio.h>
#include <tchar.h>
#include "Header.h"
#pragma pack(1)

LPCWSTR dllName = _T(".\\AICLASS_PCClient_R_org.dll");

extern "C" BOOL WINAPI DllMain(HINSTANCE hInst,DWORD reason,LPVOID)
{
	static HINSTANCE hL;
	if (reason == DLL_PROCESS_ATTACH)
	{
		hL = GetModuleHandle(dllName);
		if(!hL)
		{
			hL = LoadLibrary(dllName);
			if (!hL) return false;
		}
		if(!FileExists("firstrun"))
			ClearFile("firstrun");
		else
		{
			DeleteFileA("firstrun");
			DetourMain();
		}
	}
	return TRUE;
}

#pragma comment(linker, "/export:AI_BootClassDLL=AICLASS_PCClient_R_org.AI_BootClassDLL")


