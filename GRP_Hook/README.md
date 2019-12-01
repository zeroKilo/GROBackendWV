# Dll to hook functions

this proxy dll gets loaded in place of AICLASS_PCClient_R.dll, rename the original dll to "AICLASS_PCClient_R_org.dll" and then insert the resulting dll from this project.

this allows to run your own code in the dll main before the ai dll is loaded. this can be used to hook functions like here the Fire Script event functions