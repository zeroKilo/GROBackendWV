#include "Header.h"
char buffer[1024];

struct AIEvent
{
  DWORD modelID;
  DWORD eventID;
  DWORD param1;
  DWORD param2;
  DWORD unk1;
};

struct EventCaller
{
  DWORD *pVMT;
  DWORD modelID;
};


DWORD (__cdecl* org_FIR_SendEvent)(int, char*);
DWORD (__cdecl* org_FIR_SetVariableString)(int, char*, char*);
DWORD (__cdecl* org_FIR_SetVariableUniString)(int, char*, wchar_t*);
DWORD (__cdecl* org_FIR_SetVariableBool)(int, char*, bool);
DWORD (__cdecl* org_FIR_SetVariableInt)(int, char*, int);
DWORD (__cdecl* org_FIR_SetVariableFloat)(int, char*, float);
DWORD (__cdecl* org_FIR_LoadPackage)(int);
DWORD (__cdecl* org_FIR_UnloadPackage)(int);
DWORD (__cdecl* org_FIR_GetPackageKeyFromBank)(int, int);
DWORD (__cdecl* org_FIR_GetASDataManager)();

DWORD (__fastcall* org_UI_DispatchEvent)(EventCaller*, void*, int, int, int);
DWORD (__fastcall* org_UI_HandleEvent)(void*, void*, AIEvent*);

DWORD caller = 0;
DWORD baseAddress = 0;

__declspec(naked) void GetCaller()
{
	_asm
	{
		push eax;
		mov eax,[esp + 0xC];
		sub eax, baseAddress;
		add eax, 0x10000000;
		mov caller, eax;
		pop eax
		ret;
	}
}

DWORD __cdecl FIR_SendEvent(int unk, char* name)
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_SendEvent              (0x%08X, \"%s\")\n\0", caller, unk, name);
	Log(buffer);
	return org_FIR_SendEvent(unk, name);
}

DWORD __cdecl FIR_SetVariableString(int unk, char* name, char* value)
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_SetVariableString      (0x%08X, \"%s\", \"%s\")\n\0", caller, unk, name, value);
	Log(buffer);
	return org_FIR_SetVariableString(unk, name, value);
}

DWORD __cdecl FIR_SetVariableUniString(int unk, char* name, wchar_t* value)
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_SetVariableUniString   (0x%08X, \"%s\", \"%S\")\n\0", caller, unk, name, value);
	Log(buffer);
	return org_FIR_SetVariableUniString(unk, name, value);
}

DWORD __cdecl FIR_SetVariableBool(int unk, char* name, bool value)
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_SetVariableBool        (0x%08X, \"%s\", \"%s\")\n\0", caller, unk, name, value ? "TRUE" : "FALSE");
	Log(buffer);
	return org_FIR_SetVariableBool(unk, name, value);
}

DWORD __cdecl FIR_SetVariableInt(int unk, char* name, int value)
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_SetVariableInt         (0x%08X, \"%s\", 0x%08X)\n\0", caller, unk, name, value);
	Log(buffer);
	return org_FIR_SetVariableInt(unk, name, value);
}

DWORD __cdecl FIR_SetVariableFloat(int unk, char* name, float value)
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_SetVariableFloat       (0x%08X, \"%s\", %f)\n\0", caller, unk, name, value);
	Log(buffer);
	return org_FIR_SetVariableFloat(unk, name, value);
}

DWORD __cdecl FIR_LoadPackage(int unk)
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_LoadPackage            (0x%08X)\n\0", caller, unk);
	Log(buffer);
	return org_FIR_LoadPackage(unk);
}

DWORD __cdecl FIR_UnloadPackage(int unk)
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_UnloadPackage          (0x%08X)\n\0", caller, unk);
	Log(buffer);
	return org_FIR_UnloadPackage(unk);
}

DWORD __cdecl FIR_GetPackageKeyFromBank(int unk1, int unk2)
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_GetPackageKeyFromBank  (0x%08X, 0x%08X)\n\0", caller, unk1, unk2);
	Log(buffer);
	return org_FIR_GetPackageKeyFromBank(unk1, unk2);
}

void __cdecl FIR_GetASDataManager()
{
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::FIR_GetASDataManager       ()\n\0", caller);
	Log(buffer);
	org_FIR_GetASDataManager();
}

DWORD __fastcall UI_DispatchEvent(EventCaller* THIS, void* EDX, int a1, int a2, int a3)
{	
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::UI_DispatchEvent           (0x%08X, 0x%08X, 0x%08X, 0x%08X)\n\0", caller, THIS->modelID, a1, a2, a3);
	Log(buffer);
	return org_UI_DispatchEvent(THIS, EDX, a1, a2, a3);
}

DWORD __fastcall UI_HandleEvent(void* THIS, void* EDX, AIEvent* e)
{	
	GetCaller();
	sprintf(buffer,"0x%08X -> AIDLL::UI_HandleEvent             (0x%08X, 0x%08X, 0x%08X, 0x%08X, 0x%08X)\n\0", caller, e->modelID, e->eventID, e->param1, e->param2, e->unk1);
	Log(buffer);
	return org_UI_HandleEvent(THIS, EDX, e);
}