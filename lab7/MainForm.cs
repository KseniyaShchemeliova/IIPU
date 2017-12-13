using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;
using IMAPI2.Interop;
using IMAPI2.MediaItem;


namespace BurnMedia
{

    public partial class MainForm : Form
    {
        private const string ClientName = "BurnMedia";

        Int64 _totalDiscSize;

        private bool _isBurning;
        private bool _isFormatting;
        private IMAPI_BURN_VERIFICATION_LEVEL _verificationLevel = 
            IMAPI_BURN_VERIFICATION_LEVEL.IMAPI_BURN_VERIFICATION_NONE; // = 0; No burn verification.
        private bool _closeMedia;
        private bool _ejectMedia;

        private BurnData _burnData = new BurnData();

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //
            // Determine the current recording devices (определить)
            //
            MsftDiscMaster2 discMaster = null;
            try
            {
                discMaster = new MsftDiscMaster2();  //для перечсления сд и двд

                if (!discMaster.IsSupportedEnvironment) //Извлекает значение, определяющее, содержит ли среда одно или несколько оптических устройств, а контекст выполнения имеет разрешение на доступ к устройствам.
                    return;
                foreach (string uniqueRecorderId in discMaster) 
                {
                    var discRecorder2 = new MsftDiscRecorder2();	//физ устройство, интервейс для получения инфы о сд
                    discRecorder2.InitializeDiscRecorder(uniqueRecorderId); //Связывает объект с указанным дисковым устройством.

                    devicesComboBox.Items.Add(discRecorder2);
                }
                if (devicesComboBox.Items.Count > 0)
                {
                    devicesComboBox.SelectedIndex = 0;
                }
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message,
                    string.Format("Error:{0} - Please install IMAPI2", ex.ErrorCode),
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            finally
            {
                if (discMaster != null)
                {
                    Marshal.ReleaseComObject(discMaster);  //Уменьшает счетчик ссылок оболочки Runtime Callable Wrapper (RCW), связанной с указанным COM-объектом.
                }
            }


            //
            // Create the volume label based on the current date (метка тома по дате)
            //
            DateTime now = DateTime.Now;
            textBoxLabel.Text = now.Year + "_" + now.Month + "_" + now.Day;

            labelStatusText.Text = string.Empty;

            UpdateCapacity();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //
            // Release the disc recorder items
            //
            foreach (MsftDiscRecorder2 discRecorder2 in devicesComboBox.Items)
            {
                if (discRecorder2 != null)
                {
                    Marshal.ReleaseComObject(discRecorder2);
                }
            }
        }



        #region Device ComboBox
        /// <summary>
        /// Selected a new device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devicesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (devicesComboBox.SelectedIndex == -1)
            {
                return;
            }

            var discRecorder =
                (IDiscRecorder2)devicesComboBox.Items[devicesComboBox.SelectedIndex];

            supportedMediaLabel.Text = string.Empty;

            //
            // Verify recorder is supported
            //
            IDiscFormat2Data discFormatData = null; //инетрфейс для запис потока данных
            try
            {
                discFormatData = new MsftDiscFormat2Data();
                if (!discFormatData.IsRecorderSupported(discRecorder))
                {
                    MessageBox.Show("Recorder not supported", ClientName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                StringBuilder supportedMediaTypes = new StringBuilder();
                foreach (IMAPI_PROFILE_TYPE profileType in discRecorder.SupportedProfiles) //Определяет значения для возможных профилей устройства CD и DVD. Профиль определяет тип носителя и функции, поддерживаемые устройством.
                {
                    string profileName = GetProfileTypeString(profileType);

                    if (string.IsNullOrEmpty(profileName))
                        continue;

                    if (supportedMediaTypes.Length > 0)
                        supportedMediaTypes.Append(", ");
                    supportedMediaTypes.Append(profileName);
                }

                supportedMediaLabel.Text = supportedMediaTypes.ToString();
            }
            catch (COMException)
            {
                supportedMediaLabel.Text = "Error getting supported types";
            }
            finally
            {
                if (discFormatData != null)
                {
                    Marshal.ReleaseComObject(discFormatData);
                }
            }
        }

        /// <summary>
        /// converts an IMAPI_MEDIA_PHYSICAL_TYPE to it's string
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        private static string GetMediaTypeString(IMAPI_MEDIA_PHYSICAL_TYPE mediaType)
        {
            switch (mediaType) //Определяет значения для известных в настоящее время типов носителей, поддерживаемых IMAPI.
            {
                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_UNKNOWN:
                default:
                    return "Unknown Media Type";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_CDROM:
                    return "CD-ROM";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_CDR:
                    return "CD-R";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_CDRW:
                    return "CD-RW";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DVDROM:
                    return "DVD ROM";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DVDRAM:
                    return "DVD-RAM";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DVDPLUSR:
                    return "DVD+R";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DVDPLUSRW:
                    return "DVD+RW";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DVDPLUSR_DUALLAYER:
                    return "DVD+R Dual Layer";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DVDDASHR:
                    return "DVD-R";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DVDDASHRW:
                    return "DVD-RW";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DVDDASHR_DUALLAYER:
                    return "DVD-R Dual Layer";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DISK:
                    return "random-access writes";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DVDPLUSRW_DUALLAYER:
                    return "DVD+RW DL";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_HDDVDROM:
                    return "HD DVD-ROM";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_HDDVDR:
                    return "HD DVD-R";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_HDDVDRAM:
                    return "HD DVD-RAM";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_BDROM:
                    return "Blu-ray DVD (BD-ROM)";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_BDR:
                    return "Blu-ray media";

                case IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_BDRE:
                    return "Blu-ray Rewritable media";
            }
        }

        /// <summary>
        /// converts an IMAPI_PROFILE_TYPE to it's string
        /// </summary>
        /// <param name="profileType"></param>
        /// <returns></returns>
        static string GetProfileTypeString(IMAPI_PROFILE_TYPE profileType) //Определяет значения для возможных профилей устройства CD и DVD.
        {
            switch (profileType)
            {
                default:
                    return string.Empty;

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_CD_RECORDABLE:
                    return "CD-R";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_CD_REWRITABLE:
                    return "CD-RW";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVDROM:
                    return "DVD ROM";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_DASH_RECORDABLE:
                    return "DVD-R";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_RAM:
                    return "DVD-RAM";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_PLUS_R:
                    return "DVD+R";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_PLUS_RW:
                    return "DVD+RW";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_PLUS_R_DUAL:
                    return "DVD+R Dual Layer";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_DASH_REWRITABLE:
                    return "DVD-RW";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_DASH_RW_SEQUENTIAL:
                    return "DVD-RW Sequential";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_DASH_R_DUAL_SEQUENTIAL:
                    return "DVD-R DL Sequential";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_DASH_R_DUAL_LAYER_JUMP:
                    return "DVD-R Dual Layer";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_DVD_PLUS_RW_DUAL:
                    return "DVD+RW DL";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_HD_DVD_ROM:
                    return "HD DVD-ROM";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_HD_DVD_RECORDABLE:
                    return "HD DVD-R";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_HD_DVD_RAM:
                    return "HD DVD-RAM";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_BD_ROM:
                    return "Blu-ray DVD (BD-ROM)";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_BD_R_SEQUENTIAL:
                    return "Blu-ray media Sequential";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_BD_R_RANDOM_RECORDING:
                    return "Blu-ray media";

                case IMAPI_PROFILE_TYPE.IMAPI_PROFILE_TYPE_BD_REWRITABLE:
                    return "Blu-ray Rewritable media";
            }
        }

        /// <summary>
        /// Provides the display string for an IDiscRecorder2 object in the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devicesComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            IDiscRecorder2 discRecorder2 = (IDiscRecorder2)e.ListItem; // физ устр (дисковод)
            string devicePaths = string.Empty;
            string volumePath = (string)discRecorder2.VolumePathNames.GetValue(0); //Извлекает буквы дисков и точки монтирования NTFS для устройства
            foreach (string volPath in discRecorder2.VolumePathNames)
            {
                if (!string.IsNullOrEmpty(devicePaths))
                {
                    devicePaths += ",";
                }
                devicePaths += volumePath;
            }

            e.Value = string.Format("{0} [{1}]", devicePaths, discRecorder2.ProductId //Извлекает идентификатор продукта устройства.
	    );
        }
        #endregion


        #region Media Size

        private void buttonDetectMedia_Click(object sender, EventArgs e)
        {
            if (devicesComboBox.SelectedIndex == -1)
            {
                return;
            }

            var discRecorder =
                (IDiscRecorder2)devicesComboBox.Items[devicesComboBox.SelectedIndex];

            MsftFileSystemImage fileSystemImage = null; 
	   //Используйте этот интерфейс для создания образа файловой системы, установки параметра сеанса и импорта или экспорта изображения.
	//Иерархия каталогов файловой системы создается путем добавления каталогов и файлов в корневые или дочерние каталоги.
            MsftDiscFormat2Data discFormatData = null; // интерфейс для записи  потока данных

            try
            {
                //
                // Create and initialize the IDiscFormat2Data
                //
                discFormatData = new MsftDiscFormat2Data();
                if (!discFormatData.IsCurrentMediaSupported(discRecorder)) //проверка поддерживается ли устр
                {
                    labelMediaType.Text = "Media not supported!";
                    _totalDiscSize = 0;
                    return;
                }
                else
                {
                    //
                    // Get the media type in the recorder
                    //
                    discFormatData.Recorder = discRecorder;
                    IMAPI_MEDIA_PHYSICAL_TYPE mediaType = discFormatData.CurrentPhysicalMediaType; //Извлекает тип носителя в дисковом устройстве.
                    labelMediaType.Text = GetMediaTypeString(mediaType);

                    //
                    // Create a file system and select the media type
                    //
                    fileSystemImage = new MsftFileSystemImage();
                    fileSystemImage.ChooseImageDefaultsForMediaType(mediaType); //Устанавливает типы файловой системы по умолчанию и размер изображения на основе указанного типа носителя

                    //
                    // See if there are other recorded sessions on the disc
                    //
                    if (!discFormatData.MediaHeuristicallyBlank) //Попытки определить, является ли носитель пустым, используя эвристику (в основном для дисков DVD + RW и DVD-RAM).
                    {
                        fileSystemImage.MultisessionInterfaces = discFormatData.MultisessionInterfaces; //Извлекает список мультисессионных интерфейсов для оптических носителей.
                        fileSystemImage.ImportFileSystem();//Импортирует файловую систему по умолчанию на текущий диск.
                    }

                    Int64 freeMediaBlocks = fileSystemImage.FreeMediaBlocks; //Устанавливает максимальное количество блоков, доступных для изображения.
                    _totalDiscSize = 2048 * freeMediaBlocks;
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(this, exception.Message, "Detect Media Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (discFormatData != null)
                {
                    Marshal.ReleaseComObject(discFormatData);
                }

                if (fileSystemImage != null)
                {
                    Marshal.ReleaseComObject(fileSystemImage);
                }
            }


            UpdateCapacity();
        }

        /// <summary>
        /// Updates the capacity progressbar
        /// </summary>
        private void UpdateCapacity()
        {
            //
            // Get the text for the Max Size
            //
            if (_totalDiscSize == 0)
            {
                labelTotalSize.Text = "0MB";
                return;
            }
            
            labelTotalSize.Text = _totalDiscSize < 1000000000 ?
                string.Format("{0}MB", _totalDiscSize / 1000000) :
                string.Format("{0:F2}GB", (float)_totalDiscSize / 1000000000.0);

            //
            // Calculate the size of the files
            //
            Int64 totalMediaSize = 0;
            foreach (IMediaItem mediaItem in listBoxFiles.Items) //один з файлов которые запсываются на диск
            {
                totalMediaSize += mediaItem.SizeOnDisc;
            }

            if (totalMediaSize == 0)
            {
                progressBarCapacity.Value = 0;
                progressBarCapacity.ForeColor = SystemColors.Highlight; //Цвет переднего плана для отображения текста в данном элементе управления.
            }
            else
            {
                var percent = (int)((totalMediaSize * 100) / _totalDiscSize);
                if (percent > 100)
                {
                    progressBarCapacity.Value = 100;
                    progressBarCapacity.ForeColor = Color.Red;
                }
                else
                {
                    progressBarCapacity.Value = percent;
                    progressBarCapacity.ForeColor = SystemColors.Highlight;
                }
            }
        }

        #endregion


        #region Burn Media Process

        /// <summary>
        /// User clicked the "Burn" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBurn_Click(object sender, EventArgs e)
        {
            if (devicesComboBox.SelectedIndex == -1)
            {
                return;
            }

            if (_isBurning)
            {
                buttonBurn.Enabled = false;
                backgroundBurnWorker.CancelAsync();//Запрашивает отмену отложенной фоновой операции.
            }
            else
            {
                _isBurning = true;
                _closeMedia = checkBoxCloseMedia.Checked;
                _ejectMedia = checkBoxEject.Checked;

                EnableBurnUI(false); // enable disbale ui

                var discRecorder =
                    (IDiscRecorder2)devicesComboBox.Items[devicesComboBox.SelectedIndex];
                _burnData.uniqueRecorderId = discRecorder.ActiveDiscRecorder; //Получает уникальный идентификатор, используемый для инициализации дискового устройства.

                backgroundBurnWorker.RunWorkerAsync(_burnData);  //Запускает выполнение фоновой операции.
            }
        }

        /// <summary>
        /// The thread that does the burning of the media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundBurnWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MsftDiscRecorder2 discRecorder = null;
            MsftDiscFormat2Data discFormatData = null;

            try
            {
                //
                // Create and initialize the IDiscRecorder2 object
                //
                discRecorder = new MsftDiscRecorder2();
                var burnData = (BurnData)e.Argument;
                discRecorder.InitializeDiscRecorder(burnData.uniqueRecorderId); //Связывает объект с указанным дисковым устройством.

                //
                // Create and initialize the IDiscFormat2Data
                //
                discFormatData = new MsftDiscFormat2Data
                    {
                        Recorder = discRecorder,
                        ClientName = ClientName,
                        ForceMediaToBeClosed = _closeMedia
                    };

                //
                // Set the verification level
                //
                var burnVerification = (IBurnVerification)discFormatData;//Используйте этот интерфейс с IDiscFormat2Data или IDiscFormat2TrackAtOnce, чтобы получить или установить свойство уровня проверки записи, которое диктует, как сжечь носители проверены на целостность после операции записи.
                burnVerification.BurnVerificationLevel = _verificationLevel; //Получает текущий уровень проверки записи.

                //
                // Check if media is blank, (for RW media)
                //
                object[] multisessionInterfaces = null;
                if (!discFormatData.MediaHeuristicallyBlank) ////Попытки определить, является ли носитель пустым, используя эвристику (в основном для дисков DVD + RW и DVD-RAM).
                {
                    multisessionInterfaces = discFormatData.MultisessionInterfaces; //Получает список доступных многосеансовых интерфейсов.
                }

                //
                // Create the file system
                //
                IStream fileSystem; //объекты входного потока могут считывать и интерпретировать ввод последовательностей символов. Для выполнения этих операций ввода предусмотрены специальные элементы (см. Функции ниже).
                if (!CreateMediaFileSystem(discRecorder, multisessionInterfaces, out fileSystem))
                {
                    e.Result = -1;
                    return;
                }

                //
                // add the Update event handler
                //
                discFormatData.Update += discFormatData_Update;

                //
                // Write the data here
                //
                try
                {
                    discFormatData.Write(fileSystem);
                    e.Result = 0;
                }
                catch (COMException ex)
                {
                    e.Result = ex.ErrorCode;
                    MessageBox.Show(ex.Message, "IDiscFormat2Data.Write failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    if (fileSystem != null)
                    {
                        Marshal.FinalReleaseComObject(fileSystem); //Освобождает все ссылки на Runtime Callable Wrapper (времени выполнения RCW), установив число ссылок равным 0.
                    }
                }

                //
                // remove the Update event handler
                //
                discFormatData.Update -= discFormatData_Update;

                if (_ejectMedia)
                {
                    discRecorder.EjectMedia();
                }
            }
            catch (COMException exception)
            {
                //
                // If anything happens during the format, show the message
                //
                MessageBox.Show(exception.Message);
                e.Result = exception.ErrorCode;
            }
            finally
            {
                if (discRecorder != null)
                {
                    Marshal.ReleaseComObject(discRecorder);
                }

                if (discFormatData != null)
                {
                    Marshal.ReleaseComObject(discFormatData);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="progress"></param>
        void discFormatData_Update([In, MarshalAs(UnmanagedType.IDispatch)] object sender, [In, MarshalAs(UnmanagedType.IDispatch)] object progress)
        {
            //
            // Check if we've cancelled
            //
            if (backgroundBurnWorker.CancellationPending) //Возвращает значение, указывающее, запросило ли приложение отмену фоновой операции.
            {
                var format2Data = (IDiscFormat2Data)sender;
                format2Data.CancelWrite();
                return;
            }

            var eventArgs = (IDiscFormat2DataEventArgs)progress;

            _burnData.task = BURN_MEDIA_TASK.BURN_MEDIA_TASK_WRITING;

            // IDiscFormat2DataEventArgs Interface
            _burnData.elapsedTime = eventArgs.ElapsedTime; //Получает общее время выполнения операции записи.
            _burnData.remainingTime = eventArgs.RemainingTime; //Получает оценочное оставшееся время операции записи.
            _burnData.totalTime = eventArgs.TotalTime; //Получает расчетное общее время для операции записи.

            // IWriteEngine2EventArgs Interface
            _burnData.currentAction = eventArgs.CurrentAction; //Получает текущее действие записи.
            _burnData.startLba = eventArgs.StartLba; //Получает исходный адрес логического блока (LBA) текущей операции записи.
            _burnData.sectorCount = eventArgs.SectorCount; //Извлекает количество секторов для записи на устройство в текущей операции записи.
            _burnData.lastReadLba = eventArgs.LastReadLba;//Извлекает адрес сектора, который был недавно прочитан из образа записи.
            _burnData.lastWrittenLba = eventArgs.LastWrittenLba;//Извлекает адрес сектора, который недавно был записан на устройство.
            _burnData.totalSystemBuffer = eventArgs.TotalSystemBuffer;//Извлекает размер внутреннего буфера данных, который используется для записи на диск.
            _burnData.usedSystemBuffer = eventArgs.UsedSystemBuffer;//Извлекает количество используемых байтов во внутренний буфер данных, который используется для записи на диск.
            _burnData.freeSystemBuffer = eventArgs.FreeSystemBuffer;//Извлекает количество неиспользуемых байтов во внутреннем буфере данных, который используется для записи на диск.

            //
            // Report back to the UI
            //
            backgroundBurnWorker.ReportProgress(0, _burnData); //Вызывает событие ProgressChanged.
        }

        /// <summary>
        /// Completed the "Burn" thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundBurnWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelStatusText.Text = (int)e.Result == 0 ? "Finished Burning Disc!" : "Error Burning Disc!";
            statusProgressBar.Value = 0;

            _isBurning = false;
            EnableBurnUI(true);
            buttonBurn.Enabled = true;
        }

        /// <summary>
        /// Enables/Disables the "Burn" User Interface
        /// </summary>
        /// <param name="enable"></param>
        void EnableBurnUI(bool enable)
        {
            buttonBurn.Text = enable ? "&Burn" : "&Cancel";
            buttonDetectMedia.Enabled = enable;

            devicesComboBox.Enabled = enable;
            listBoxFiles.Enabled = enable;

            buttonAddFiles.Enabled = enable;
            buttonRemoveFiles.Enabled = enable;
            checkBoxEject.Enabled = enable;
            checkBoxCloseMedia.Enabled = enable;
            textBoxLabel.Enabled = enable;
        }

        /// <summary>
        /// Event receives notification from the Burn thread of an event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundBurnWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //int percent = e.ProgressPercentage;
            var burnData = (BurnData)e.UserState;

            if (burnData.task == BURN_MEDIA_TASK.BURN_MEDIA_TASK_FILE_SYSTEM)
            {
                labelStatusText.Text = burnData.statusMessage;
            }
            else if (burnData.task == BURN_MEDIA_TASK.BURN_MEDIA_TASK_WRITING)
            {
                switch (burnData.currentAction)
                {
                    case IMAPI_FORMAT2_DATA_WRITE_ACTION.IMAPI_FORMAT2_DATA_WRITE_ACTION_VALIDATING_MEDIA:
                        labelStatusText.Text = "Validating current media...";
                        break;

                    case IMAPI_FORMAT2_DATA_WRITE_ACTION.IMAPI_FORMAT2_DATA_WRITE_ACTION_FORMATTING_MEDIA:
                        labelStatusText.Text = "Formatting media...";
                        break;

                    case IMAPI_FORMAT2_DATA_WRITE_ACTION.IMAPI_FORMAT2_DATA_WRITE_ACTION_INITIALIZING_HARDWARE:
                        labelStatusText.Text = "Initializing hardware...";
                        break;

                    case IMAPI_FORMAT2_DATA_WRITE_ACTION.IMAPI_FORMAT2_DATA_WRITE_ACTION_CALIBRATING_POWER:
                        labelStatusText.Text = "Optimizing laser intensity...";
                        break;

                    case IMAPI_FORMAT2_DATA_WRITE_ACTION.IMAPI_FORMAT2_DATA_WRITE_ACTION_WRITING_DATA:
                        long writtenSectors = burnData.lastWrittenLba - burnData.startLba;

                        if (writtenSectors > 0 && burnData.sectorCount > 0)
                        {
                            var percent = (int)((100 * writtenSectors) / burnData.sectorCount);
                            labelStatusText.Text = string.Format("Progress: {0}%", percent);
                            statusProgressBar.Value = percent;
                        }
                        else
                        {
                            labelStatusText.Text = "Progress 0%";
                            statusProgressBar.Value = 0;
                        }
                        break;

                    case IMAPI_FORMAT2_DATA_WRITE_ACTION.IMAPI_FORMAT2_DATA_WRITE_ACTION_FINALIZATION:
                        labelStatusText.Text = "Finalizing writing...";
                        break;

                    case IMAPI_FORMAT2_DATA_WRITE_ACTION.IMAPI_FORMAT2_DATA_WRITE_ACTION_COMPLETED:
                        labelStatusText.Text = "Completed!";
                        break;

                    case IMAPI_FORMAT2_DATA_WRITE_ACTION.IMAPI_FORMAT2_DATA_WRITE_ACTION_VERIFYING:
                        labelStatusText.Text = "Verifying";
                        break;
                }
            }
        }

        /// <summary>
        /// Enable the Burn Button if items in the file listbox
        /// </summary>
        private void EnableBurnButton()
        {
            buttonBurn.Enabled = (listBoxFiles.Items.Count > 0);
        }


        #endregion


        #region File System Process
        private bool CreateMediaFileSystem(IDiscRecorder2 discRecorder, object[] multisessionInterfaces, out IStream dataStream)
        {
            MsftFileSystemImage fileSystemImage = null;
            try
            {
                fileSystemImage = new MsftFileSystemImage();
                fileSystemImage.ChooseImageDefaults(discRecorder);
                fileSystemImage.FileSystemsToCreate = //Извлекает типы файловых систем для создания при создании потока результатов.
                    FsiFileSystems.FsiFileSystemJoliet | FsiFileSystems.FsiFileSystemISO9660;
                fileSystemImage.VolumeName = textBoxLabel.Text;

                fileSystemImage.Update += fileSystemImage_Update;

                //
                // If multisessions, then import previous sessions
                //
                if (multisessionInterfaces != null)
                {
                    fileSystemImage.MultisessionInterfaces = multisessionInterfaces;
                    fileSystemImage.ImportFileSystem();//Импортирует файловую систему по умолчанию на текущий диск.
                }

                //
                // Get the image root
                //
                IFsiDirectoryItem rootItem = fileSystemImage.Root;

                //
                // Add Files and Directories to File System Image
                //
                foreach (IMediaItem mediaItem in listBoxFiles.Items)
                {
                    //
                    // Check if we've cancelled
                    //
                    if (backgroundBurnWorker.CancellationPending)
                    {
                        break;
                    }

                    //
                    // Add to File System
                    //
                    mediaItem.AddToFileSystem(rootItem);
                }

                fileSystemImage.Update -= fileSystemImage_Update;

                //
                // did we cancel?
                //
                if (backgroundBurnWorker.CancellationPending)
                {
                    dataStream = null;
                    return false;
                }

                dataStream = fileSystemImage.CreateResultImage().ImageStream; //Создайте объект результата, содержащий файловую систему и данные файла.
            }
            catch (COMException exception)
            {
                MessageBox.Show(this, exception.Message, "Create File System Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataStream = null;
                return false;
            }
            finally
            {
                if (fileSystemImage != null)
                {
                    Marshal.ReleaseComObject(fileSystemImage);
                }
            }

	        return true;
        }

        /// <summary>
        /// Event Handler for File System Progress Updates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="currentFile"></param>
        /// <param name="copiedSectors"></param>
        /// <param name="totalSectors"></param>
        void fileSystemImage_Update([In, MarshalAs(UnmanagedType.IDispatch)] object sender,
            [In, MarshalAs(UnmanagedType.BStr)]string currentFile, [In] int copiedSectors, [In] int totalSectors)
        {
            var percentProgress = 0;
            if (copiedSectors > 0 && totalSectors > 0)
            {
                percentProgress = (copiedSectors * 100) / totalSectors;
            }

            if (!string.IsNullOrEmpty(currentFile))
            {
                var fileInfo = new FileInfo(currentFile);
                _burnData.statusMessage = "Adding \"" + fileInfo.Name + "\" to image...";

                //
                // report back to the ui
                //
                _burnData.task = BURN_MEDIA_TASK.BURN_MEDIA_TASK_FILE_SYSTEM;
                backgroundBurnWorker.ReportProgress(percentProgress, _burnData);
            }

        }
        #endregion


        #region Add/Remove File(s)/Folder(s)

        /// <summary>
        /// Adds a file to the burn list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var fileItem = new FileItem(openFileDialog.FileName);
                listBoxFiles.Items.Add(fileItem);

                UpdateCapacity();
                EnableBurnButton();
            }
        }

        /// <summary>
        /// User wants to remove a file or folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveFiles_Click(object sender, EventArgs e)
        {
            var mediaItem = (IMediaItem)listBoxFiles.SelectedItem;
            if (mediaItem == null)
                return;

            if (MessageBox.Show("Are you sure you want to remove \"" + mediaItem + "\"?",
                "Remove item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listBoxFiles.Items.Remove(mediaItem);

                EnableBurnButton();
                UpdateCapacity();
            }
        }

        #endregion


        #region File ListBox Events
        /// <summary>
        /// The user has selected a file or folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemoveFiles.Enabled = (listBoxFiles.SelectedIndex != -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxFiles_DrawItem(object sender, DrawItemEventArgs e)
        {
            var mediaItem = (IMediaItem)listBoxFiles.Items[e.Index];
            if (mediaItem == null)
            {
                return;
            }

            e.DrawBackground();

            if ((e.State & DrawItemState.Focus) != 0)
            {
                e.DrawFocusRectangle();
            }

            if (mediaItem.FileIconImage != null)
            {
                e.Graphics.DrawImage(mediaItem.FileIconImage, new Rectangle(4, e.Bounds.Y + 4, 16, 16));
            }

            var rectF = new RectangleF(e.Bounds.X + 24, e.Bounds.Y,
                e.Bounds.Width - 24, e.Bounds.Height);

            var font = new Font(FontFamily.GenericSansSerif, 11);

            var stringFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near,
                    Trimming = StringTrimming.EllipsisCharacter
                };

            e.Graphics.DrawString(mediaItem.ToString(), font, new SolidBrush(e.ForeColor),
                rectF, stringFormat);
        }
        #endregion

        /// <summary>
        /// Called when user selects a new tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //
            // Prevent page from changing if we're burning or formatting.
            //
            if (_isBurning || _isFormatting)
            {
                e.Cancel = true;
            }
        }
    }
}
