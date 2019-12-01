// Folgender ifdef-Block ist die Standardmethode zum Erstellen von Makros, die das Exportieren 
// aus einer DLL vereinfachen. Alle Dateien in dieser DLL werden mit dem AICLASS_PCCLIENT_R_EXPORTS-Symbol
// (in der Befehlszeile definiert) kompiliert. Dieses Symbol darf für kein Projekt definiert werden,
// das diese DLL verwendet. Alle anderen Projekte, deren Quelldateien diese Datei beinhalten, erkennen 
// AICLASS_PCCLIENT_R_API-Funktionen als aus einer DLL importiert, während die DLL
// mit diesem Makro definierte Symbole als exportiert ansieht.
#ifdef AICLASS_PCCLIENT_R_EXPORTS
#define AICLASS_PCCLIENT_R_API __declspec(dllexport)
#else
#define AICLASS_PCCLIENT_R_API __declspec(dllimport)
#endif

// Diese Klasse wird aus AICLASS_PCClient_R.dll exportiert.
class AICLASS_PCCLIENT_R_API CAICLASS_PCClient_R {
public:
	CAICLASS_PCClient_R(void);
	// TODO: Hier die Methoden hinzufügen.
};

extern AICLASS_PCCLIENT_R_API int nAICLASS_PCClient_R;

AICLASS_PCCLIENT_R_API int fnAICLASS_PCClient_R(void);
