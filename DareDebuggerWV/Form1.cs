using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace DareDebuggerWV
{
    public partial class Form1 : Form
    {
        private TcpListener server;
        private readonly object _sync = new object();
        private bool _exit = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log.box = rtb1;
            new Thread(tServer).Start();
            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.Items.AddRange(new string[] { 
                "AddPartialDirectories",
                "AddSoundObjectTypeCommand",
                "AtomicObjectLoadedCallbackRegistrationCommand",
                "BeginPreviewAtomicObject",
                "BusParameterCommand",
                "CancelEditing",
                "CancelOverride",
                "CancelOverrideReverb",
                "ChangeVolumeMTTChannelTrackCommand",
                "ChangeVolumeObjectTypeCommand",
                "ChangeVolumeRequestCommand",
                "CheckConnection",
                "CompletionNotificationCommand",
                "CreateClientBufferCommand",
                "CreateDestroyEffectChainCommand",
                "DeadSoundRequestCallbackRegistrationCommand",
                "DsqFastForward",
                "DsqGotoNextMarker",
                "DsqGotoNextSendSoundRequest",
                "DsqPlay",
                "DsqStop",
                "EnableRemoteLogger",
                "EnableSpyObjectManager",
                "EnableSpyRTVariableListManager",
                "EndPreviewAtomicObject",
                "FlushQueueClientBufferCommand",
                "ForceFullAtomicPrefetchCommand",
                "GetActivePresetList",
                "GetAtomicObjectList",
                "GetBusList",
                "GetCompressorValues",
                "GetDareVersion",
                "GetDbgObjectList",
                "GetEnvironmentInfo",
                "GetHLPList",
                "GetMasterDirectory",
                "GetMicroList",
                "GetProjectDataVersion",
                "GetRTVariableList",
                "GetTitleGuid",
                "GetVoiceList",
                "InitDataLoadCommand",
                "InitializeEnvironment",
                "IsEventValidFor3DMTTFollowerCommand",
                "KillAllSoundObjects",
                "KillSoundObject",
                "KillSoundObjectCommand",
                "LoadAtomicObjectCommand",
                "LoadUnloadPackageCommand",
                "MethodCallSequenceBase",
                "MethodCallSequence|_DT1|const char*",
                "MethodCallSequence|_DT1|PTR",
                "MethodCallSequence|_DT1|S32",
                "MethodCallSequence|_DT1|U32",
                "MethodCallSequence|_DT2|PTR|const char*",
                "MethodCallSequence|_DT2|PTR|S32",
                "MethodCallSequence|_DT2|PTR|U32",
                "MethodCallSequence|_DT2|PTR|Volume",
                "MethodCallSequence|_DT2|S32|Float32",
                "MethodCallSequence|_DT2|S32|PositionInfo",
                "MethodCallSequence|_DT3|PTR|PTR|S32",
                "MethodCallSequence|_DT3|PTR|Volume|Volume",
                "MethodCallSequence|_DT4|PTR|PTR|U32|S32",
                "MethodCallSequence|_DT4|PTR|PTR|Volume|Volume",
                "MicCommand",
                "ModifyObjectRefCountCommand",
                "MuteBus",
                "MuteBusConnectedVoices",
                "MuteBusDryChannels",
                "NotifySoundObjectDestruction",
                "OverrideFloat",
                "OverrideReverb",
                "OverrideS32",
                "PauseObjectTypeCommand",
                "PlayEventRequest",
                "PlayResourceRequest",
                "PlayWaveSourceRequest",
                "PlayWavFile",
                "PrefetchAtomicObjectCommand",
                "PresetCommand",
                "QueueDataClientBufferCommand",
                "QueuedPauseResumeCmd",
                "ReloadAtomicObject",
                "ResetBusTree",
                "ResetVoiceUseStatistics",
                "ResumeObjectTypeCommand",
                "RetRolloffbackRegistrationCommand",
                "RTPCCommand",
                "SampleResourceCallbackRegistrationCommand",
                "SendDPSPData",
                "SendSoundRequestCommand",
                "SendSynthesisCommand",
                "SequenceStreamMarker",
                "SetActiveMic",
                "SetBusEffectChainParametersCommand",
                "SetBusIdSoundObjectTypeCommand",
                "SetConnectionTimeout",
                "SetCurrentLanguage",
                "SetCurrentLanguageCommand",
                "SetDLCProjectSettingsAtomicIds",
                "SetDynamicRequestCommand",
                "SetEffectChainCommand",
                "SetEffectParamCommand",
                "SetEffectStateCommand",
                "SetInaudibleDistanceCommand",
                "SetObjectData",
                "SetObjectFloat",
                "SetObjectS32",
                "SetOverrideFloat",
                "SetOverrideS32",
                "SetParamClientBufferCommand",
                "SetPlayStateCommand",
                "SetRandomVolumeOverrideMode",
                "SetRetSoundObjectTypeCommand",
                "SetRetSynthSoundObjectTypeCommand",
                "SetSoundDopplerFactorCommand",
                "SetSoundPosRequestCommand",
                "SetSpeakerSetup",
                "SetSynthExcitationType",
                "SND_td_pfn_bMultitrack3DRetRolloffFactorSequencer",
                "SND_td_pfn_bRetRolloffSequencer",
                "SND_td_pfn_bRetSoundMultiLayerActivationFlagSequencer",
                "SND_td_pfn_fRetSoundMultiLayerParameterSequencer",
                "SND_td_pfn_hRetSoundSwitchSequencer",
                "SND_td_pfn_vRetSoundMicroVectorSequencer",
                "SND_td_pfn_vRetSoundMultitrack3DOccObstSequencer",
                "SND_td_pfn_vRetSoundMultitrack3DPosSequencer",
                "SND_td_pfn_vRetSoundOccObstSequencer",
                "SND_td_pfn_vRetSoundVectorSequencer",
                "SND_td_pfn_vRetSoundWiimoteSpeakerSequencer",
                "SND_td_pfn_vSynthImpactCBSequencer",
                "SND_td_pfn_vSynthRollingCBSequencer",
                "SND_td_pfn_vSynthScrapingCBSequencer",
                "SoloBus",
                "SpeakerSetupCommand",
                "StopAtomicObjects",
                "StopEventRequest",
                "StopResourceRequest",
                "StopWaveSourceRequest",
                "SynchronizeCommand",
                "TerminatorCommand",
                "TypeInfobackRegistrationCommand",
                "UninitDataLoadSoundCommand",
                "Uninitialize",
                "UnloadAtomicObjectCommand",
                "UnmuteAllBuses",
                "UnmuteBus",
                "UnmuteBusConnectedVoices",
                "UnmuteBusDryChannels",
                "UnsoloAllBuses",
                "UnsoloBus",
                "UpdateCommand",
                "WaveMarkerCallbackRegistrationCommand"
            });
            toolStripComboBox1.SelectedIndex = 31;
        }

        private void StopServer()
        {
            if (server != null)
            {
                lock (_sync)
                {
                    _exit = true;
                }
                server.Stop();
                server = null;
                Thread.Sleep(100);
            }
        }

        public static void WriteU32(Stream s, uint v)
        {
            s.WriteByte((byte)(v >> 24));
            s.WriteByte((byte)(v >> 16));
            s.WriteByte((byte)(v >> 8));
            s.WriteByte((byte)v);
        }

        private void tServer(object obj)
        {
            ushort portHost = Convert.ToUInt16(toolStripTextBox2.Text);
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), portHost);
            server.Start();
            lock (_sync)
            {
                _exit = false;
            }
            Log.WriteLine("Started Listening on port " + portHost);
            while (true)
            {
                lock (_sync)
                {
                    if (_exit)
                        break;
                }
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    Log.WriteLine("Client connected");
                    NetworkStream ns = client.GetStream();
                    Recv(ns);
                    client.Close();
                }
                catch { }
            }
            Log.WriteLine("Stopped Listening");
        }

        private byte[] Recv(NetworkStream ns)
        {
            Thread.Sleep(1000);
            MemoryStream m = new MemoryStream();
            while (ns.DataAvailable)
                m.WriteByte((byte)ns.ReadByte());
            return m.ToArray();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            StopServer();
            new Thread(tServer).Start();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                }
                ushort portHost = Convert.ToUInt16(toolStripTextBox2.Text);
                ushort portGame = Convert.ToUInt16(toolStripTextBox1.Text);
                TcpClient client = new TcpClient();
                Log.WriteLine("Connecting to " + portGame);
                client.Connect("127.0.0.1", portGame);
                Log.WriteLine("Connected");
                NetworkStream ns = client.GetStream();                
                string s = portHost + " 1 VersionWV 7";
                while (s.Length < 32)
                    s += " ";
                byte[] buff = Encoding.ASCII.GetBytes(s);
                MemoryStream m = new MemoryStream();
                m.Write(buff, 0, buff.Length);
                string data = toolStripTextBox3.Text;
                WriteU32(m, (uint)(data.Length + 9));
                WriteU32(m, (uint)(data.Length + 1));
                foreach (char c in data)
                    m.WriteByte((byte)c);
                m.WriteByte(0);
                buff = m.ToArray();
                ns.Write(buff, 0, buff.Length);
                ns.Flush();
                byte[] result = Recv(ns);
                hb1.ByteProvider = new DynamicByteProvider(result);
                client.Close();
                Log.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Log.WriteLine("Error : " + ex.Message);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = toolStripComboBox1.SelectedItem.ToString();
        }
    }
}
