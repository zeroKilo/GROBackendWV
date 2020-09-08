#include "Header.h"
#include "defines.h";
char buffer[1024];
char buffer2[1024];
char buffer3[1024];

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
DWORD (__fastcall* org_FIR_FireEvent)(void*, void*);
void (__fastcall* org_AI_EntityPlayer_UpdateWarning)(void*, void*);

typedef DWORD(__fastcall* BUSEVENTHANDLER)(void*, void*, AIEvent*);
typedef DWORD(__fastcall* EVENTHANDLER)(void*, void*, DWORD, AIEvent*);

BYTE preJump[] = {0x68, 0x00, 0x00, 0x00, 0x00, 0x68, 0x00, 0x00, 0x00, 0x00, 0xC3}; //push 0, push 0, ret

#define MAXHANDLER	100
DWORD orgEventHandler		[MAXHANDLER];
DWORD orgEventHandlerW		[MAXHANDLER];
DWORD orgBusEventHandler	[MAXHANDLER];
char* handlerName			[MAXHANDLER];
DWORD nHandler = 0;
DWORD caller = 0;
DWORD handler = 0;
DWORD baseAddressAI = 0;
DWORD baseAddressRDV = 0;
DWORD moduleSizeAI = 0;
DWORD moduleSizeRDV = 0;
DWORD temp = 0;

__declspec(naked) void GetCallerAndHandler()
{
	_asm
	{
		//Extract Handler ID from Stack
		mov eax,[esp + 0x10];
		mov handler, eax;
		//Extract Return address from Stack
		mov eax,[esp + 0x14];
		mov caller, eax;
		//save return address
		pop eax
		mov temp, eax
		//push stack arguments down, 3x
		mov eax, [esp+8]
		mov [esp+0xC], eax
		mov eax, [esp+4]
		mov [esp+8], eax
		mov eax, [esp]
		mov [esp+4], eax
		//fix esp
		pop eax;
		//fix ebp
		add ebp, 4
		//restore return address
		mov eax, temp
		push eax
		ret
	}
}

char* getCallerString()
{
	if(caller >= baseAddressAI && caller < baseAddressAI + 0x782000)
	{
		caller -= baseAddressAI;
		caller += 0x10000000;
		sprintf(buffer3,"AI  : 0x%08X\0", caller);
	}
	else if(caller >= baseAddressRDV && caller < baseAddressRDV + 0x71C000)
	{
		caller -= baseAddressRDV;
		caller += 0x10000000;
		sprintf(buffer3,"RDV : 0x%08X\0", caller);
	}
	else
		sprintf(buffer3,"UNK : 0x%08X\0", caller);
	return buffer3;
}

DWORD __fastcall BusEventHandler(void* THIS, void* EDX, AIEvent* e)
{	
	GetCallerAndHandler();
	sprintf(buffer2,"AIDLL::%s::OnBusEvent\0", handlerName[handler]);
	sprintf(buffer,"%s -> %-48s (\"%s\", 0x%08X, 0x%08X, 0x%08X, 0x%08X)\n\0", getCallerString(), buffer2, modelNames[e->modelID], e->eventID, e->param1, e->param2, e->unk1);
	Log(buffer);
	return ((BUSEVENTHANDLER)orgBusEventHandler[handler])(THIS, EDX, e);
}

DWORD __fastcall EventHandler(void* THIS, void* EDX, DWORD unk1, AIEvent* e)
{	
	GetCallerAndHandler();
	sprintf(buffer2,"AIDLL::%s::OnEvent\0", handlerName[handler]);
	sprintf(buffer,"%s -> %-48s (0x%08X, %s)\n\0", getCallerString(), buffer2, unk1, e->eventID);
	Log(buffer);
	return ((EVENTHANDLER)orgEventHandler[handler])(THIS, EDX, unk1, e);
}

DWORD __fastcall EventHandlerW(void* THIS, void* EDX, DWORD unk1, AIEvent* e)
{	
	GetCallerAndHandler();
	sprintf(buffer2,"AIDLL::%s::OnEventW\0", handlerName[handler]);
	sprintf(buffer,"%s -> %-48s (0x%08X, %s)\n\0", getCallerString(), buffer2, unk1, e->eventID);
	Log(buffer);
	return ((EVENTHANDLER)orgEventHandlerW[handler])(THIS, EDX, unk1, e);
}

void AddHandler(DWORD* pVMT, char* name)
{
	if(nHandler >= MAXHANDLER)
	{
		sprintf(buffer,"Cant add handler %s\n\0", name);
		Log(buffer);
		return;
	}
	handlerName[nHandler] = name;
	DWORD old;
	VirtualProtect(pVMT,36,PAGE_EXECUTE_READWRITE,&old);
	//detouring OnBusEvent
	orgBusEventHandler[nHandler] = pVMT[0];
	BYTE* trampolineStub = (BYTE*)calloc(11, 1);
	memcpy((void*)trampolineStub, (const void*)preJump, 11);
	*(DWORD*)(&trampolineStub[1]) = nHandler;
	*(DWORD*)(&trampolineStub[6]) = (DWORD)&BusEventHandler;
	pVMT[0] = (DWORD)trampolineStub;
	//detouring OnEvent
	orgEventHandler[nHandler] = pVMT[6];
	trampolineStub = (BYTE*)calloc(11, 1);
	memcpy((void*)trampolineStub, (const void*)preJump, 11);
	*(DWORD*)(&trampolineStub[1]) = nHandler;
	*(DWORD*)(&trampolineStub[6]) = (DWORD)&EventHandler;
	pVMT[6] = (DWORD)trampolineStub;
	//detouring OnEventW
	orgEventHandlerW[nHandler] = pVMT[7];
	trampolineStub = (BYTE*)calloc(11, 1);
	memcpy((void*)trampolineStub, (const void*)preJump, 11);
	*(DWORD*)(&trampolineStub[1]) = nHandler;
	*(DWORD*)(&trampolineStub[6]) = (DWORD)&EventHandlerW;
	pVMT[7] = (DWORD)trampolineStub;
	sprintf(buffer,"Replaced handlers OnBusEvent(0x%08X), OnEvent(0x%08X), OnEventW(0x%08X) for %s (pVMT=0x%08X)\n\0", 
		orgBusEventHandler[nHandler] - baseAddressAI + 0x10000000, 
		orgEventHandler[nHandler] - baseAddressAI + 0x10000000, 
		orgEventHandlerW[nHandler] - baseAddressAI + 0x10000000, 
		name,
		pVMT);
	Log(buffer);
	nHandler++;
}

__declspec(naked) void GetCaller()
{
	_asm
	{
		push eax;
		mov eax,[esp + 0xC];
		mov caller, eax;
		pop eax
		ret;
	}
}

__declspec(naked) void GetCaller2()
{
	_asm
	{
		push eax
		mov eax, [esp+0x10];
		mov caller, eax;
		pop eax
		ret;
	}
}

__declspec(naked) void GetCaller3()
{
	_asm
	{
		push eax
		mov eax, [esp+0x14];
		mov caller, eax;
		pop eax
		ret;
	}
}

DWORD __cdecl FIR_SendEvent(int unk, char* name)
{
	GetCaller();
	sprintf(buffer,"%s -> AIDLL::FIR_SendEvent                             (0x%08X, \"%s\")\n\0", getCallerString(), unk, name);
	Log(buffer);
	return org_FIR_SendEvent(unk, name);
}

DWORD __cdecl FIR_SetVariableString(int unk, char* name, char* value)
{
	GetCaller();
	sprintf(buffer,"%s -> AIDLL::FIR_SetVariableString                     (0x%08X, \"%s\", \"%s\")\n\0", getCallerString(), unk, name, value);
	Log(buffer);
	return org_FIR_SetVariableString(unk, name, value);
}

DWORD __cdecl FIR_SetVariableUniString(int unk, char* name, wchar_t* value)
{
	GetCaller();
	sprintf(buffer,"%s -> AIDLL::FIR_SetVariableUniString                  (0x%08X, \"%s\", \"%S\")\n\0", getCallerString(), unk, name, value);
	Log(buffer);
	return org_FIR_SetVariableUniString(unk, name, value);
}

DWORD __cdecl FIR_SetVariableBool(int unk, char* name, bool value)
{
	GetCaller2();
	sprintf(buffer,"%s -> AIDLL::FIR_SetVariableBool                       (0x%08X, \"%s\", \"%s\")\n\0", getCallerString(), unk, name, value ? "TRUE" : "FALSE");
	Log(buffer);
	return org_FIR_SetVariableBool(unk, name, value);
}

DWORD __cdecl FIR_SetVariableInt(int unk, char* name, int value)
{
	GetCaller();
	sprintf(buffer,"%s -> AIDLL::FIR_SetVariableInt                        (0x%08X, \"%s\", 0x%08X)\n\0", getCallerString(), unk, name, value);
	Log(buffer);
	return org_FIR_SetVariableInt(unk, name, value);
}

DWORD __cdecl FIR_SetVariableFloat(int unk, char* name, float value)
{
	GetCaller();
	sprintf(buffer,"%s -> AIDLL::FIR_SetVariableFloat                      (0x%08X, \"%s\", %f)\n\0", getCallerString(), unk, name, value);
	Log(buffer);
	return org_FIR_SetVariableFloat(unk, name, value);
}

DWORD __cdecl FIR_LoadPackage(int unk)
{
	GetCaller();
	sprintf(buffer,"%s -> AIDLL::FIR_LoadPackage                           (0x%08X)\n\0", getCallerString(), unk);
	Log(buffer);
	return org_FIR_LoadPackage(unk);
}

DWORD __cdecl FIR_UnloadPackage(int unk)
{
	GetCaller();
	sprintf(buffer,"%s -> AIDLL::FIR_UnloadPackage                         (0x%08X)\n\0", getCallerString(), unk);
	Log(buffer);
	return org_FIR_UnloadPackage(unk);
}

DWORD __cdecl FIR_GetPackageKeyFromBank(int unk1, int unk2)
{
	GetCaller();
	sprintf(buffer,"%s -> AIDLL::FIR_GetPackageKeyFromBank                 (0x%08X, 0x%08X)\n\0", getCallerString(), unk1, unk2);
	Log(buffer);
	return org_FIR_GetPackageKeyFromBank(unk1, unk2);
}

void __cdecl FIR_GetASDataManager()
{
	GetCaller();
	sprintf(buffer,"%s -> AIDLL::FIR_GetASDataManager                      ()\n\0", getCallerString());
	Log(buffer);
	org_FIR_GetASDataManager();
}

DWORD __fastcall UI_DispatchEvent(EventCaller* THIS, void* EDX, int a1, int a2, int a3)
{	
	GetCaller3();
	sprintf(buffer,"%s -> AIDLL::UI_DispatchEvent                          (\"%s\", 0x%08X, 0x%08X, 0x%08X)\n\0", getCallerString(), modelNames[THIS->modelID], a1, a2, a3);
	Log(buffer);
	return org_UI_DispatchEvent(THIS, EDX, a1, a2, a3);
}

DWORD __fastcall FIR_FireEvent(void* THIS, void* EDX)
{	
	GetCaller3();
	sprintf(buffer,"%s -> AIDLL::FIR_FireEvent                             ()\n\0", getCallerString());
	Log(buffer);
	return org_FIR_FireEvent(THIS, EDX);
}

DWORD playerAddress = 0;

DWORD WINAPI StepStepStep(LPVOID lpvParam)
{
	Sleep(10000);
	DWORD* stanceFlags = (DWORD*)(playerAddress + 0x824);
	while(true)
	{
		Sleep(1000);
		*stanceFlags |= 0x40000;
	}
	return 0;
}

float* __cdecl GetVelocity(DWORD a1, DWORD a2)
{
	float result[3];
	result[0] = 0;
	result[1] = 0;
	result[2] = 100;
	return result;
}

void __fastcall AI_EntityPlayer_UpdateWarning(void* THIS, void* EDX)
{
	if(playerAddress == 0)
	{
		playerAddress = (DWORD)THIS;
		sprintf(buffer,"PlayerAddress=%08X\n\0", playerAddress);		
		FILE* fp = fopen("_playerAddress.txt", "w");
		fprintf(fp, buffer);
		fclose(fp);
		//StartThread(StepStepStep);
	}
	org_AI_EntityPlayer_UpdateWarning(THIS, EDX);
}
