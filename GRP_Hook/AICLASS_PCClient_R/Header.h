#include "detours.h"
#include <stdlib.h> 
extern char* logFilename;

void OpenConsole();
bool IsThisExeName(wchar_t* name);
bool FileExists(char* str);
void StartThread(LPTHREAD_START_ROUTINE func);
void ClearFile(char* str);
void LogToFile(char* str);
void Log(char* str);

void DetourMain();