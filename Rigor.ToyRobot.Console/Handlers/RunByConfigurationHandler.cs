using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Console.Handlers
{
    public class RunByConfigurationHandler: HandlerBase
    {
        private string _errMsg;

        private string _configurationFilePath;

        private static string SOURCE_FILE_SWITCH = "F";


        public RunByConfigurationHandler() : base()
        {



        }

        public override string CommandArgs
        {
            get
            {
                return "-RunConfiguration";
            }
        }

        /// <summary>
        /// Gets the command arguments description.
        /// </summary>
        /// <value>
        /// The command arguments description.
        /// </value>
        public override string CommandArgsDescription
        {
            get
            {
                var description = "Runs the Toy Robot Challenge using a pre-defined configuration." +
                    "\nPlease ensure configuration settings file is available and set.";

                var options = "/" + SOURCE_FILE_SWITCH + "\t\tProvide the full path to the configuration file.\n";


                return description + options;
            }
        }

        /// <summary>
        /// Gets the get error message.
        /// </summary>
        /// <value>
        /// The get error message.
        /// </value>
        public override string GetErrorMessage
        {
            get
            {
                return _errMsg;
            }
        }

        /// <summary>
        /// Gets the handler unique identifier.
        /// </summary>
        /// <value>
        /// The handler unique identifier.
        /// </value>
        public override Guid HandlerGuid
        {
            get
            {
                return new Guid("{942416D1-5A34-41B3-A021-FB31C24ABF84}");
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public override void ExecuteCommand()
        {
            try
            {
                ProcessParams();
                ProcessFile();

            }
            catch (Exception e)
            {
                _errMsg = e.Message;


                throw e;
            }
        }


        #region Private functions

        /// <summary>
        /// Processes the Video configuration XML file.
        /// </summary>        
        private void ProcessFile()
        {
            try
            {
                //var itemList = _globalSettings.ItemList;

                //var _availableConverters = VideoConverterBase.GetAvailableFileConverters();

                //for (int i = 0; i < itemList.Count; i++)
                //{
                //    int entry = i + 1;

                //    ConfigurationFileStore c = itemList[i];
                //    using (VideoExportConfiguration vec = new VideoExportConfiguration())
                //    {
                //        vec.SetConfig(c);

                //        vec.CameraGuid = itemList[i].CameraGuid;
                //        vec.ConverterGuid = itemList[i].ConverterGuid != Guid.Empty ? itemList[i].ConverterGuid : _globalSettings.ConverterGuid;
                //        vec.TimingInfo = itemList[i].TimingInfo;
                //        vec.OnReportProgress += OnReportProgessChanged;
                //        vec.OnOperationCompleted += OnOperationCompleted;
                //        vec.ExportFailed += OnExportFailed;

                //        Log.Info(913, "Creating camera from GUID -> {0}", true, c.CameraGuid);

                //        Camera camera = _scDriver.GetCameraFromGuid(c.CameraGuid);

                //        Log.Info(913, "Selected Camera -> {0} ({1})", false, camera?.Name, camera?.Guid);

                //        if (camera == null)
                //        {
                //            SetMessage(this, new ConsoleHandlerEventArgs(this, "\tERROR: Invalid Camera found at entry " + entry, false));
                //        }
                //        else
                //        {
                //            Log.Info(913, "Initialising VideoExportConfiguration object", true);

                //            vec.Initialise(camera, _scDriver);

                //            var selectedConverter = _availableConverters.Where(x => x.ConverterGUID == vec.ConverterGuid).FirstOrDefault();
                //            vec.SelectedConverter = selectedConverter != null ? selectedConverter : _availableConverters[0];

                //            Log.Info(913, "SelectedConverter -> {0}", true, vec.SelectedConverter.ToString());

                //            SetMessage(this, new ConsoleHandlerEventArgs(this, String.Format("\tExporting from Camera {0} - {1}. Converter: {2}", entry, camera.Name, vec.SelectedConverter.ToString()), false));

                //            vec.StartExport();

                //            Log.Info(913, "Export Started", false);

                //            while (vec.IsExportRunning | vec.IsMP4ConverterRunning)
                //            {
                //                System.Threading.Thread.Sleep(1000);
                //            }

                //            itemList[i] = new ConfigurationFileStore(vec);
                //        }

                //    }

                //    System.Threading.Thread.Sleep(1000);

                //}

                ////save the updated file: 
                //try
                //{
                //    Log.Info(913, "Setting the last exported frame time in config file.", true);

                //    _globalSettings.ItemList = itemList;

                //    ConfigurationFileStore.SaveState(_configurationFilePath, _globalSettings, _scDriver);

                //    Log.Info(913, "Changes applied", true);
                //}
                //catch (Exception ex)
                //{

                //    _errMsg = String.Format("Unable to save the configuration due to an error. {0}", ex.Message);

                //    SetMessage(this, new ConsoleHandlerEventArgs(this, _errMsg, false));

                //    Log.Error(913, ex, _errMsg);
                //}
            }
            catch (Exception ex)
            {

                //_errMsg = String.Format("Unable to process the configuration due to an error. {0}", ex.Message);

                //Log.Error(913, ex, _errMsg);

                //throw new Exception(_errMsg);

            }
        }

        


       
        /// <summary>
        /// Processes the parameters.
        /// </summary>
        /// <exception cref="System.Exception">
        /// </exception>
        private void ProcessParams()
        {
            try
            {

                _configurationFilePath = "";

                bool pass = true;
                if (CommandParams == null)
                {

                    pass = false;
                }
                else if (CommandParams.Length < 1)
                {
                    pass = false;
                }

                if (!pass)
                {
                    _errMsg = "Invalid parameters applied to this command.";

                    throw new Exception(_errMsg);
                }

                string str = "";
                for (int i = 0; i < CommandParams.Length; i++)
                {
                    str += CommandParams[i] + " ";
                }


                string[] spl = str.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < spl.Length; i++)
                {
                    try
                    {

                        if (spl[i].ToUpper().StartsWith(SOURCE_FILE_SWITCH) && spl[i].ToString().ElementAt(1) == ' ')
                        {
                            _configurationFilePath = spl[i].Remove(0, SOURCE_FILE_SWITCH.Length);


                            FileInfo sFileInfo = new FileInfo(_configurationFilePath);

                            if (!sFileInfo.Exists)
                            {
                                _errMsg = "Invalid source file name provided: " + _configurationFilePath;

                                throw new Exception(_errMsg);
                            }
                        }
                    }
                    catch (Exception d)
                    {
                        _errMsg = "Invalid parameters provided.";

                        throw d;
                    }
                }

                SetMessage(this, new EventArgs.HandlerEventArgs(this, "Checking config file...", false));


                //pass = ConfigurationFileStore.RecallFile(_configurationFilePath, out _globalSettings);

                //if (!pass)
                //{
                //    throw new Exception("Unable to parse the given file due to an error");
                //}

                //if (_globalSettings.ItemList == null)
                //{
                //    throw new Exception("Invalid configuration - Camera entity list not found.");
                //}
                //else if (_globalSettings.ItemList.Count == 0)
                //{
                //    throw new Exception("Invalid configuration - Camera entity list is empty.");
                //}

                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                   
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~RunByConfigurationHandler()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }


        #endregion

        #endregion

    }
}
