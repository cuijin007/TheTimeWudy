using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace TimeMDev
{
    class MPlayer
    {
        public delegate void  playerRateAction(double time,double totalTime);
        public bool isPlaying = true;
        Thread mplayerPlay;
        string moviePath;
        Process mplayer;
        public bool getPositionMark = true;
        public double totalTime=60*60*4;
        public playerRateAction DplayerRateAction;
        double nowTime;
        int wid;
        int playStep;//开始的步骤,0：读取总时间，1：开始播放
        int PlayStep 
        {
            get 
            {
                return this.playStep;
            }
            set
            {
                playStep = value;
                if (playStep == 0)
                {
                    this.totalTime = 0;
                }
            }
        }
        string appPath;
        public  MPlayer(int wid)
        {
            this.wid = wid;//输出窗口
            this.appPath = System.AppDomain.CurrentDomain.BaseDirectory;
        }
        public void SetMoviePath(string moviePath)
        {
            this.moviePath=moviePath;
        }
        public void Clear()
        {
            if(mplayer!=null)
            {
                try
                {
                    this.mplayer.Kill();
                }
                catch
                {
 
                }
            }
           
        }
        public void StartPlay(string moviePath)
        {
            this.moviePath=moviePath;
            if (mplayer == null)
            {
                this.mplayer = new Process();
                this.StartPlayInit();
            }
            else
            {
                try
                {
                    this.totalTime = 0;
                    this.LoadMovie(this.moviePath);
                }
                catch (Exception ee)
                {
 
                }
                
            }
            if (this.mplayerPlay == null)
            {
                this.mplayerPlay = new Thread(new ThreadStart(this.GetNowPostion));
                this.mplayerPlay.Start();
            }
            else
            {
                
            }
        }
        private void StartPlayInit()
        {
            if (this.mplayer != null)
            {
                mplayer.StartInfo.FileName = appPath + @"mplayer\mplayer.exe";
                mplayer.StartInfo.CreateNoWindow = true;
                mplayer.StartInfo.UseShellExecute = false;
                mplayer.StartInfo.ErrorDialog = false;
                mplayer.StartInfo.RedirectStandardInput = true;
                mplayer.StartInfo.RedirectStandardOutput = true;
                mplayer.StartInfo.RedirectStandardError = true;
                this.mplayer.OutputDataReceived += new DataReceivedEventHandler(mplayer_OutputDataReceived);

                this.InitMovie();

                mplayer.StartInfo.Arguments = string.Format("-slave -quiet -idle  -noautosub -v -vo gl -wid {1} \"{0}\"", this.moviePath, this.wid);
                mplayer.Start();
                mplayer.BeginErrorReadLine();
                mplayer.BeginOutputReadLine();
            }
            
            
        }
        void  mplayer_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string line = e.Data;
            if (line != null)
            {
                if (line.StartsWith("ANS_TIME_POSITION="))
                {
                    line = line.Replace("ANS_TIME_POSITION=", "");
                    double time = Double.Parse(line);
                    if (this.DplayerRateAction != null)
                    {
                        this.nowTime = time;
                        this.DplayerRateAction(time, this.totalTime);
                    }
                    else
                    {
                        this.nowTime = time;
                    }
                }
                if (line.StartsWith("ANS_LENGTH="))
                {
                    line = line.Replace("ANS_LENGTH=", "");
                    this.totalTime = Double.Parse(line);
                }
                if(line.StartsWith("ID_LENGTH"))
                {
                    line=line.Replace("ID_LENGTH=","");
                    this.totalTime = Double.Parse(line);
                }
            }
        }
        private void GetNowPostion()
        {
            while (this.getPositionMark)
            {
                if (this.totalTime == 0)
                {
                    this.GetTotalTime();
                }
                if (this.mplayer != null)
                {
                    this.mplayer.StandardInput.WriteLine("get_time_pos");
                    this.mplayer.StandardInput.Flush();
                    Thread.Sleep(100);
                }
            }
        }
        /// <summary>
        /// 在指定位置播放，绝对位置
        /// </summary>
        /// <param name="position"></param>
        public void SeekPosition(double position)
        {
            if (this.mplayer != null)
            {
                this.mplayer.StandardInput.WriteLine("seek" + " " + position + " " + "2");
                //this.mplayer.StandardInput.WriteLine(" ss " + position);
                this.mplayer.StandardInput.Flush();
            }
        }
        /// <summary>
        /// 获取总时间
        /// </summary>
       public void GetTotalTime()
        {
            if (this.mplayer != null)
            {
                this.mplayer.StandardInput.WriteLine("get_time_length");
                this.mplayer.StandardInput.Flush();
                Thread.Sleep(200);
            }
        }
       public void Pause()
        {
            if (mplayer != null)
            {
                this.mplayer.StandardInput.WriteLine("pause");
                this.mplayer.StandardInput.Flush();
                if (this.isPlaying)
                {
                    this.isPlaying = false;
                }
                else
                {
                    this.isPlaying = true;
                }
            }
        }

       public void Forward(double time)
       {
           if(this.nowTime+time<this.totalTime)
           this.SeekPosition(this.nowTime + time);
       }
       public void BackWard(double time)
       {
           if ((this.nowTime - time) > 0)
           {
               this.SeekPosition(this.nowTime - time);
           }
           else
           {
               this.SeekPosition(0);
           }
       }
       public void InitMovie()
       {
           System.Diagnostics.Process handle = new System.Diagnostics.Process();

           handle.StartInfo.UseShellExecute = false;
           handle.StartInfo.CreateNoWindow = true;
           handle.StartInfo.RedirectStandardOutput = true;
           handle.StartInfo.RedirectStandardError = true;

           handle.StartInfo.FileName =this.appPath+ @"mplayer\mplayer.exe";
           handle.StartInfo.Arguments = string.Format("-loop 1 -identify -ao null -vo null -frames 0 {0} \"{1}\"", 1, this.moviePath);
           handle.Start();
           string line = "";
           StringReader strReader = new StringReader(handle.StandardOutput.ReadToEnd());

           while ((line = strReader.ReadLine()) != null)
           //while (handle.HasExited == false)
           {

               if (line.Trim() == "")
               {
                   continue;
               }
               int position = line.IndexOf("ID_");
               if (position == -1)
               {
                   continue;
               }
               line = line.Substring(position);
               if (line.StartsWith("ID_VIDEO_BITRATE"))
               {
                   
               }
               if (line.StartsWith("ID_VIDEO_WIDTH"))
               {
                  
               }
               if (line.StartsWith("ID_VIDEO_HEIGHT"))
               {
                  
               }
               if (line.StartsWith("ID_VIDEO_ASPECT"))
               {
                 
               }
               if (line.StartsWith("ID_VIDEO_FPS"))
               {
                 
               }
               if (line.StartsWith("ID_AUDIO_BITRATE"))
               {
                   
               }
               if (line.StartsWith("ID_AUDIO_RATE"))
               {
                  
               }
               if (line.StartsWith("ID_LENGTH"))
               {
                   line=line.Replace("ID_LENGTH=","");
                   this.totalTime = Double.Parse(line); 
               }
               if (line.StartsWith("ID_VIDEO_ID"))
               {
                  
               }
               if (line.StartsWith("ID_AUDIO_ID"))
               {
                  
               }
           }

           handle.WaitForExit();
           handle.Close();

         
       }
       public void LoadTimeLine(string timeLinePath)
       {
           this.mplayer.StandardInput.WriteLine("sub_load "+timeLinePath);
           this.mplayer.StandardInput.Flush();
       }
       public void LoadMovie(string path)
       {
           this.moviePath = path;
           string LoadCommand = @"" + string.Format("loadfile \"{0}\"", path);
           this.mplayer.StandardInput.WriteLine(LoadCommand);
           this.mplayer.StandardInput.Flush();
       }
        /// <summary>
        /// 首次启动
        /// </summary>
        /// <param name="path"></param>
       public void FristTimeStart(string path)
       {
           if (this.mplayer == null)
           {
               //this.mplayer = new Process();
           }
           else
           {
               try
               {
                   mplayer.Kill();
               }
               catch
               {
                    
               }
           }
           this.mplayer = new Process();
           //2013-4-12 增加路径
           mplayer.StartInfo.FileName =this.appPath+ @"mplayer\mplayer.exe";
           mplayer.StartInfo.Arguments = string.Format("-slave -quiet \"{0}\"",path);
           mplayer.Start();
       }
    }
    
}
