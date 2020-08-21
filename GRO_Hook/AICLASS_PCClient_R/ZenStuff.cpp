#include "stdafx.h"
#include <Windows.h>
#include <stdio.h>
#include "detours.h"

DWORD (__fastcall* org_ZEN_VM_Exec)(void*, void*, DWORD*);


FILE* fp;
DWORD baseAddress;
char* zenLogFile = "zenlog.txt";
char zenLogBuffer[1024];

struct ZENScript
{
  DWORD pVMT;
  BYTE gap4[144];
  DWORD dword94;
  DWORD OpcPointer;
  DWORD OpcSize;
  BYTE flags;
  DWORD CImpl;
};

void VM_Log(char* c)
{	
	fprintf(fp, c);
}

void VM_Log_Format(const char* format, ...)
{
  va_list argptr;
  va_start(argptr, format);
  vsprintf(zenLogBuffer, format, argptr);
  va_end(argptr);
  VM_Log(zenLogBuffer);
}

DWORD __fastcall ZEN_VM_Exec(void* THIS, void* EDX, DWORD* script)
{
	ZENScript* zen = (ZENScript*)script;
	if(zen->flags & 0x11)
	{
		VM_Log_Format("Exec Compiled Script @%08X \n\0", (zen->CImpl - baseAddress + 0x10000000));
	}
	else
	{
		VM_Log("Exec Interpreted Script:");
		for(DWORD i = 0; i < zen->OpcSize; i++)
		{
			if((i % 16) == 0)
				VM_Log("\n");
			VM_Log_Format("%02X \0", ((BYTE*)zen->OpcPointer)[i]);
		}
		VM_Log("\n");
	}
	return org_ZEN_VM_Exec(THIS, EDX, script);
}

void ZEN_Init(DWORD addr)
{
	fp = fopen (zenLogFile, "w");
	fclose(fp);
	fp = fopen(zenLogFile, "a+");
	baseAddress = addr;
	org_ZEN_VM_Exec = (DWORD(__fastcall*) (void*,void*, DWORD*)) DetourFunction((PBYTE)(baseAddress + 0x228710),(PBYTE)ZEN_VM_Exec);
}