using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace MusicLibGeneratorGUI {
    public partial class Form1 : Form {
        //TODO:
        //IN MAIN PROGRAM: ADD METHOD TO GET ALL mp3 FILES IN MULTI SONG SETS, LIKE JUMP TRAINING

        #region Overhead
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {           
            ReadPathsFromFile();
            UI_OptionsHandler(iniRadio.Checked ? mType.ini : mType.Simple);
            defaultRootPath = osuPathEntry.Text.Trim() == "" ? "C:\\" : osuPathEntry.Text;
            defaultTargetPath = targetPathEntry.Text.Trim() == "" ? "C:\\" : targetPathEntry.Text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            
        }
        #endregion

        #region Main
        bool isRunning = false;
        Process proc = new Process();
        private void MainProcess() {
            try {
                string cb = dupecheck_Checkbox.Checked ? "true" : "false";
                string mType = iniRadio.Checked ? "-i" : "-s";

                
                string pathA = "\"" + osuPathEntry.Text + "\\\\\"";
                string pathB = "\"" + targetPathEntry.Text + "\\\\\"";

                string logging = logging_checkbox.Checked ? "true" : "false";
                

                //string pathA = osuPathEntry.Text;
                //string pathB = targetPathEntry.Text;

                Console.WriteLine(pathA);
                Console.WriteLine(pathB);

                proc.StartInfo.FileName = "MusicLibGenerator.exe";
                proc.StartInfo.Arguments =
                    pathA + " " + pathB + " " +
                    cb + " " + 
                    logging;
                proc.Start();
                proc.WaitForExit();
                if (notableErrs.Contains((uint)proc.ExitCode)) {
                    ShowErrDialog((ExitCodes)proc.ExitCode);
                }
            }

            catch (Win32Exception e) {
                ShowErrDialog(ExitCodes.MainExeMissing, e);
            }

        }
        #endregion

        #region GUI
        private void ControlToggler(bool Enabled) {
            foreach (Control c in this.Controls) {
                if (c.Name == "start_button") { continue; }
                c.Enabled = Enabled;
            }
        }

        enum GUIStatusSetting : byte {
            Idle = 0,
            Running = 1
        }

        enum mType : byte {
            Simple = 0,
            ini = 1
        }

        private void GUIStatus(GUIStatusSetting g) {
            switch (g) {
                case GUIStatusSetting.Idle:
                ControlToggler(true);
                start_button.Text = "Start";
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                break;
                case GUIStatusSetting.Running:
                ControlToggler(false);
                start_button.Text = "Stop";
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.Value = 30;
                break;
            }
        }

        private void UI_OptionsHandler(mType m) {
            //switch (m) {
            //    case mType.Simple:
            //        dupecheck_Checkbox.Enabled = true;
            //        break;
            //    case mType.ini:
            //        dupecheck_Checkbox.Enabled = false;
            //        break;
            //}
        }
        #endregion

        #region ErrCode
        List<uint> supressedErrs = new List<uint>() {
            0,          //Success
            4294967295  //-1 / 0xFFFFFF | Occurrs on button_close
        };

        List<uint> notableErrs = new List<uint>() {
            1,
            2,
            3,
            4,
            101,
            102,
        };
        List<uint> winErrs = new List<uint>() {
            0xc000013a
        };

        enum ExitCodes : uint {
            Success = 0,
            LoggedErr = 1,
            IncorrectAmountArgs = 2,
            DupecheckArgUnresolved = 3,
            InvalidSongPath = 4,
            LoggingArgUnresolved = 6,
            //GUI Proj Specific
            MainExeMissing = 101,
            ConfigError = 102,
            //Windows Error
            AbruptProcessExit = 0xc000013a //WIN_58
        }

        private void ShowErrDialog(ExitCodes exit, Exception EXCEPT = null) {
            string e = "";
            switch (exit) {
                case ExitCodes.LoggedErr: e = "An unknown error has occurred, see ErrLog.log for details."; break;
                case ExitCodes.InvalidSongPath: e = "Song path was invalid, please enter a valid song path ending in \\osu!\\Songs"; break;
                case ExitCodes.IncorrectAmountArgs:
                case ExitCodes.LoggingArgUnresolved:
                case ExitCodes.DupecheckArgUnresolved: e = "This shouldn't be possible, what have you done???"; break;
                //
                case ExitCodes.MainExeMissing: e = "MusicLibGenerator.exe was not found"; break;
                case ExitCodes.ConfigError: e = "Config file read incorrectly"; break;
                
            }

            if(e == "" && EXCEPT != null) { e = EXCEPT.ToString(); }
            MessageBox.Show($"Error {((int)exit)} - {exit}: " + e, "Error!", MessageBoxButtons.OK);
        }

        private void ShowErrDialog(uint exit, Exception EXCEPT = null) {
            string e = "";
            switch (exit) {
                case 0xc000013a: e = "Main process window was closed unexpectedly"; break;
            }

            if (e == "" && EXCEPT != null) { e = EXCEPT.ToString(); }
            MessageBox.Show($"Error 0x{(exit.ToString("X").ToLower())}: " + e, "Error!", MessageBoxButtons.OK);
        }
        #endregion

        #region EventHandlers
        private async void start_button_Click(object sender, EventArgs e) {

            if (!isRunning) {
                GUIStatus(GUIStatusSetting.Running);
                isRunning = true;
                await Task.Run(() =>
                   MainProcess()
                    );
                isRunning = false;
                GUIStatus(GUIStatusSetting.Idle);
            }
            else {
                proc.Kill();
                isRunning = false;
                GUIStatus(GUIStatusSetting.Idle);
            }
        }

        public static string defaultRootPath = "";
        public static string defaultTargetPath = "";
        private void osuPathButton_Click(object sender, EventArgs e) {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = defaultRootPath;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                osuPathEntry.Text = dialog.FileName;
            }
            
        }

        private void button1_Click(object sender, EventArgs e) {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = defaultTargetPath;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                targetPathEntry.Text = dialog.FileName;
                
            }
        }

        private void osuPathEntry_TextChanged(object sender, EventArgs e) {
            WriteSettingsToFile();
        }

        private void targetPathEntry_TextChanged(object sender, EventArgs e) {
            WriteSettingsToFile();
        }

        private void dupecheck_Checkbox_CheckedChanged(object sender, EventArgs e) {
            WriteSettingsToFile();
        }

        private void iniRadio_CheckedChanged(object sender, EventArgs e) {
            WriteSettingsToFile();
            UI_OptionsHandler(iniRadio.Checked ? mType.ini : mType.Simple);
        }

        private void folderRadio_CheckedChanged(object sender, EventArgs e) {
            WriteSettingsToFile();
            UI_OptionsHandler(iniRadio.Checked ? mType.ini : mType.Simple);
        }

        private void multiSetButton_CheckedChanged(object sender, EventArgs e) {
            WriteSettingsToFile();
            UI_OptionsHandler(iniRadio.Checked ? mType.ini : mType.Simple);
        }
        #endregion

        #region FileHandling
        bool startUp = true;
        private void WriteSettingsToFile() {
            if (startUp) { return; }
            string cb = dupecheck_Checkbox.Checked ? "true" : "false";
            string lo = logging_checkbox.Checked ? "true" : "false";
            string mTypeStr = iniRadio.Checked ? "ini" : "simple";
            using (StreamWriter sw = new StreamWriter("savedConfig.cfg", false)){
                sw.WriteLine("songPath=" + osuPathEntry.Text);
                sw.WriteLine("targetDir=" + targetPathEntry.Text);
                sw.WriteLine("dupechecking=" + cb);
                sw.WriteLine("mType=" + mTypeStr);
                sw.WriteLine("logging=" + lo);
            }
        }

        private void ReadPathsFromFile() {
            if (!File.Exists("savedConfig.cfg")) {
                File.Create("savedConfig.cfg");
                WriteSettingsToFile();
            }
            else {
                bool dontThrow = false;
                try {
                    using (StreamReader sr = new StreamReader("savedConfig.cfg")) {
                        string[] SA = new string[5];
                        for(int i = 0; i < SA.Length; i++){ 
                            SA[i] = sr.ReadLine();
                        }
                        if (SA[0].Split('=').Length == 2) {
                            osuPathEntry.Text = SA[0].Split('=')[1];
                        }
                        if (SA[1].Split('=').Length == 2) {
                            targetPathEntry.Text = SA[1].Split('=')[1];
                        }
                        if (SA[2].Split('=').Length == 2) {
                            dupecheck_Checkbox.Checked = SA[2].Split('=')[1] == "true";
                        }
                        if (SA[3].Split('=').Length == 2) {
                            iniRadio.Checked = SA[3].Split('=')[1] == "ini";
                            //folderRadio.Checked = !iniRadio.Checked;
                        }
                        if (SA[4].Split('=').Length == 2)
                        {
                            logging_checkbox.Checked = SA[4].Split('=')[1] == "true";
                        }
                        
                    }
                }
                catch (NullReferenceException) { dontThrow = true; }
                catch (Exception e) {
                    if (!dontThrow)
                    {
                        ShowErrDialog(ExitCodes.ConfigError, e);
                    }
                }
            }
            startUp = false;
        }

        #endregion

        
    }
}
