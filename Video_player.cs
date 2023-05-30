using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Video_Player
{
    //клас, що містить інформацію про відеофайли
    public class VideoInfo
    {
        public string[] Files { get; set; } //назви файлів
        public string[] Paths { get; set; } //шляхи до файлів
        public bool HasError { get; set; } //наявність помилки
        public string ErrorMessage { get; set; } //повідомлення про помилку
    }

    //клас для обробки файлів і додавання їх в плейлист 
    public static class FileProcessing
    {
        public static void ProcessSelectedFiles(string[] files, ListBox trackList)
        {
            foreach (string file in files)
            {
                trackList.Items.Add(file);
            }
        }
    }

    //клас для роботи з відтворенням відео
    public class VideoPlayer
    {
        public VideoInfo videoInfo;
        
        //метод відтворення відео
        public void PlayVideo(string filePath, AxWMPLib.AxWindowsMediaPlayer player)
        {
            if (player != null)
            {
                player.URL = filePath;
                player.Ctlcontrols.play();
            }
        }


        //метод відкриття відеофайлів і повернення інформації про них
        public VideoInfo OpenVideoFiles()
        {
            VideoInfo videoInfo = new VideoInfo();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Video Files|*.mp4;*.avi;*.mkv|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                videoInfo.Files = openFileDialog.SafeFileNames;
                videoInfo.Paths = openFileDialog.FileNames;
            }
            else
            {
                videoInfo.HasError = true;
                videoInfo.ErrorMessage = "No files were selected.";
            }

            return videoInfo;
        }

        
        //метод призупинення відео
        public void Pause(AxWMPLib.AxWindowsMediaPlayer player)
        {
            if (player != null && player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                player.Ctlcontrols.pause();
            }
        }

        //метод повної зупинки відео
        public void Stop(AxWMPLib.AxWindowsMediaPlayer player)
        {
            if (player != null && player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                player.Ctlcontrols.stop();
                player.URL = string.Empty;
                player.Visible = true;
            }
        }
        
        //метод для премотування відео до вказаної позиції
        public void SetPosition(int position, AxWMPLib.AxWindowsMediaPlayer player)
        {
            if (player != null)
            {
                player.Ctlcontrols.currentPosition = position;
            }
        }


        //метод регулювання звуку відео
        public void SetVolume(int volume, AxWMPLib.AxWindowsMediaPlayer player)
        {
            if (player != null)
            {
                player.settings.volume = volume;
            }
        }
    }
}
