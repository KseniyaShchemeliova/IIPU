using System;
using System.Collections.Generic;
using System.Linq;
using IMAPI2;

namespace CDBurn
{
    public class BurnManager
    {
        public delegate void UpdateBurnHandler(FormatWriteUpdateEventArgs e);
        public event UpdateBurnHandler UpdateBurn;

        private readonly ImageMaster _imageMaster = new ImageMaster();//класс для работы с дисками

        public List<DiscRecorder> RecordersList { get; }//записыватели дисков

        public BurnManager()
        {
            RecordersList = _imageMaster.Recorders.ToList();
            _imageMaster.FormatWriteUpdate += ImageMasterFormatWriteUpdate;
        }

        private void ImageMasterFormatWriteUpdate(ImageMaster sender, FormatWriteUpdateEventArgs e)
        {
            UpdateBurn?.Invoke(e);//сказать делегату или и делай
        }

        public Disc GetDiscInfo(string recordersInfo)
        {
            try
            {
                _imageMaster.Recorders.SelectedIndex = _imageMaster.Recorders.ToList().FindIndex(x =>
                    x.VolumePath.Equals(recordersInfo));//совпадает ли путь с названием диска
                _imageMaster.LoadRecorder();
                _imageMaster.LoadMedia();//загрузить инфу о нужном записывателе
                return new Disc()
                {
                    Type = _imageMaster.Media,
                    State = _imageMaster.MediaStates.Any(x => x == MediaState.Blank)
                        ? MediaState.Blank//занят
                        : MediaState.Unknown,//неизвестно
                    Size = _imageMaster.MediaCapacity
                };
            }
            catch (Exception)
            {
                return new Disc()
                {
                    Type = PhysicalMedia.Unknown,
                    State = MediaState.Unknown,
                    Size = 0
                };
            }
        }

        public bool DiskIsAviable()//доступен ли выбранный диск
        {
            try
            {
                _imageMaster.LoadMedia();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetFilesToBurning(List<FileNode> files)
        {
            _imageMaster.Nodes.Clear();
            _imageMaster.Nodes.AddRange(files);
        }

        public void Burn()
        {
            _imageMaster.WriteImage(BurnVerificationLevel.None, false, false);//уровень приоритета
        }
    }
}
