# Ghost Recon Online Backend by Warranty Voider

This is an experimental implementation of the Quazal packet protocol suite to emulate a backend for GRO.

That includes an RDV matchmaking server and a dedicated game server, both based on Pretty Reliable UDP (PRUDP).

## Config

To make the game use this backend make sure following lines are set in `yeti.ini`:

```
OnlineConfigServiceUrl=127.0.0.1
OnlineConfigKey=23ad683803a0457cabce83f905811dbc
OnlineAccessKey=8dtRv2oj
```

## Projects

- DareDebuggerWV - tool to interface the daredebug port of the game
- DTBReaderWV - converts .dtb files to .csv
- GROBackendWV - experimental backend for GRO
- GRODedicatedServerWV - experimental DS for GRO
- GROExplorerWV - tool to browse the yeti.big file for game content
- GROMemoryToolWV - tool to browse various structures like lists and trees in memory
- GRO_Hook - proxy dll for easy code injection, hooks currently fire script event functions
- NamespaceParserWV - extracts custom RTTI information found in different exe and dll

## Demos

[![Alt text](https://img.youtube.com/vi/7Gix54amKxk/0.jpg)](https://www.youtube.com/watch?v=7Gix54amKxk)


[![Alt text](https://img.youtube.com/vi/_MaOtB4U2RM/0.jpg)](https://www.youtube.com/watch?v=_MaOtB4U2RM)


[![Alt text](https://img.youtube.com/vi/TL2OUyEL0xw/0.jpg)](https://www.youtube.com/watch?v=TL2OUyEL0xw)


[![Alt text](https://img.youtube.com/vi/YsnXCel8Nso/0.jpg)](https://www.youtube.com/watch?v=YsnXCel8Nso)


[![Alt text](https://img.youtube.com/vi/3CwcyioQDR0/0.jpg)](https://www.youtube.com/watch?v=3CwcyioQDR0)


[![Alt text](https://img.youtube.com/vi/FMV0ZmOaO60/0.jpg)](https://www.youtube.com/watch?v=FMV0ZmOaO60)

## Credits

Mimak - For doing alot of the reversing

LifeCoder - For helping with reversing

Kinnay - For the helpful wiki and the predone RE work

## Game files

Compatible with GRO build from March 12th, 2012:
- `AICLASS_PCClient_R.dll` - version 38.215.0.0
- `Yeti_Release.exe` - version 1.0.0.1
