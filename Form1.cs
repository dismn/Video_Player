using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Video_Player
{
    public partial class Form1 : Form
    {
        public VideoPlayer videoPlayer;
        public Form1()
        {
            InitializeComponent();
            videoPlayer = new VideoPlayer();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            VideoInfo videoInfo = videoPlayer.OpenVideoFiles();
            if (videoInfo != null)
            {
                FileProcessing.ProcessSelectedFiles(videoInfo.Files, trackList);
                videoPlayer.videoInfo = videoInfo;
            }
        }

        private void trackList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {

            int selectedIndex = trackList.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < videoPlayer.videoInfo.Paths.Length)
            {
                string selectedFilePath = videoPlayer.videoInfo.Paths[selectedIndex];
                videoPlayer.PlayVideo(selectedFilePath, player);
            }
            timer1.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            videoPlayer.Pause(player);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            videoPlayer.Stop(player);
        }

        private void tbVolume_Scroll(object sender, EventArgs e)
        {
            int volume = tbVolume.Value;
            videoPlayer.SetVolume(volume, player);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                timeline.Maximum = (int)player.Ctlcontrols.currentItem.duration;
                timeline.Value = (int)player.Ctlcontrols.currentPosition;
            }
            lblStart.Text = player.Ctlcontrols.currentPositionString;
            lblEnd.Text = player.Ctlcontrols.currentItem.durationString.ToString();
        }

        private void timeline_ValueChanged(object sender, EventArgs e)
        {
            int selectedPosition = timeline.Value;
            videoPlayer.SetPosition(selectedPosition, player);
        }
    }
}
