using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        int championsnum = 0;
        string[] champions = { "Aatrox",
                                "Ahri",
                                "Akali",
                                "Alistar",
								"Amumu",
								"Anivia",
								"Annie",
								"Ashe",
								"Blitzcrank",
								"Brand",
								"Braum",
								"Caitlyn",
								"Cassiopeia",
								"Chogath",
								"Corki",
								"Darius",
								"Diana",
								"Draven",
								"DrMundo",
								"Elise",
								"Evelynn",
								"Ezreal",
								"Fiddlesticks",
								"Fiora",
								"Fizz",
								"Galio",
								"Gangplank",
								"Garen",
								"Gragas",
								"Graves",
								"Hecarim",
								"Heimerdinger",
								"Irelia",
								"Janna",
								"JarvanIV",
								"Jax",
								"Jayce",
								"Jinx",
								"Karma",
								"Karthus",
								"Kassadin",
								"Katarina",
								"Kayle",
								"Kennen",
								"KhaZix",
								"KogMaw",
								"LeBlanc",
								"LeeSin",
								"Leona",
								"Lissandra",
								"Lucian",
								"Lulu",
								"Lux",
								"Malphite",
								"Malzahar",
								"Maokai",
								"MasterYi",
								"MissFortune",
								"MonkeyKing",
								"Mordekaiser",
								"Morgana",
								"Nami",
								"Nasus",
								"Nautilus",
								"Nidalee",
								"Nocturne",
								"Nunu",
								"Olaf",
								"Orianna",
								"Pantheon",
								"Poppy",
								"Quinn",
								"Rammus",
								"Renekton",
								"Rengar",
								"Riven",
								"Rumble",
								"Ryze",
								"Sejuani",
								"Shaco",
								"Shen",
								"Shyvana",
								"Singed",
								"Sion",
								"Sivir",
								"Skarner",
								"Sona",
								"Soraka",
								"Swain",
								"Syndra",
								"Talon",
								"Taric",
								"Teemo",
								"Thresh",
								"Tristana",
								"Trundle",
								"Tryndamere",
								"TwistedFate",
								"Twitch",
								"Udyr",
								"Urgot",
								"Varus",
								"Vayne",
								"Veigar",
								"VelKoz",
								"Vi",
								"Viktor",
								"Vladimir",
								"Volibear",
								"Warwick",
								"Xerath",
								"XinZhao",
								"Yasuo",
								"Yorick",
								"Zac",
								"Zed",
								"Ziggs",
								"Zilean",
								"Zyra"
							 };
        int skinnummax;
        int skinnum = 0;
        string gamepath; //LoL安裝路徑
        string summoner = "stop"; //判斷啟用-停用，控制按鈕顯示用
        string[] summonernames = { }; //召喚師名稱
        int[] summonernamesswitch = { }; //召喚師啟用-停用
        int summonernamesnum = 0; //召喚師陣列中序號
        string OSType = ""; //作業系統版本
        UTF8Encoding utf8 = new UTF8Encoding(false); 
        
        public Form1()
        {
            InitializeComponent();
            textBox1.Hide();
            button14.Hide();

            //判斷作業系统版本
            Version ver = System.Environment.OSVersion.Version;
            //Major主版本號, Minor副版本號
            if (ver.Major == 5 && ver.Minor == 0)
            {
                OSType = "Windows 2000";
            }
            else if (ver.Major == 5 && ver.Minor == 1)
            {
                OSType = "Windows XP";
            }
            else if (ver.Major == 5 && ver.Minor == 2)
            {
                OSType = "Windows 2003";
            }
            else if (ver.Major == 6 && ver.Minor == 0)
            {
                OSType = "Windows Vista";
            }
            else if (ver.Major == 6 && ver.Minor == 1)
            {
                OSType = "Windows7";
            }
            else if (ver.Major == 6 && ver.Minor == 2)
            {
                OSType = "Windows8";
            }
            else if (ver.Major == 6 && ver.Minor == 3)
            {
                OSType = "Windows8.1";
            }
            else
            {
                OSType = "未知";
            }

            if (OSType == "Windows XP")
            {
                string allusersgxp = @"C:\Documents and Settings\All Users\Application Data\GarenaMessenger\app.ini";
                gamepath = @"C:\Program Files\GarenaLoLTW\GameData";
                //讀取 app.ini ，取gamepath
                StreamReader stmRdr = new StreamReader(allusersgxp); 
                string appini = stmRdr.ReadLine();
                while (appini != null)
                {
                    if (appini.IndexOf(@"cafe_LoLTW=") != -1)
                    {
                        gamepath = appini.Substring(appini.IndexOf(@"cafe_LoLTW=") + @"cafe_LoLTW=".Length); //預設為@"C:\Program Files\GarenaLoLTW\GameData"
                    }
                    appini = stmRdr.ReadLine();
                }
            }
            else {
                //取 gamepath
                
                //取使用者名稱
                string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string[] userinfo = username.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                username = userinfo[1];
                
                //取 Users 路徑(去掉使用者名稱)
                string allusers = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string[] allusersg = allusers.Split(new string[] { username }, StringSplitOptions.RemoveEmptyEntries);
                
                gamepath = @"C:\Program Files (x86)\GarenaLoLTW\GameData";
                //讀取 app.ini ，取gamepath
                //一行一行的取讀取文字檔內的資料  System.Text.Encoding.Default才不會讀到亂碼
                StreamReader stmRdr = new StreamReader(allusersg[0] + @"All Users\GarenaMessenger\app.ini"); //預設為@"C:\Users\All Users\GarenaMessenger\app.ini"
                string appini = stmRdr.ReadLine();
                while (appini != null)
                {
                    if (appini.IndexOf(@"cafe_LoLTW=") != -1)
                    {
                        gamepath = appini.Substring(appini.IndexOf(@"cafe_LoLTW=") + @"cafe_LoLTW=".Length); //預設為@"C:\Program Files (x86)\GarenaLoLTW\GameData"
                    }
                    appini = stmRdr.ReadLine();
                }
            }
            
            //判斷檔案是否存在(是否已安裝skinID.ini與dinput8.dll)
            if (System.IO.File.Exists(gamepath + @"\Apps\LoLTW\Game\skinID.ini") && System.IO.File.Exists(gamepath + @"\Apps\LoLTW\Game\dinput8.dll"))
            {
                button11.Hide();

				comboBox1.Text = champions[championsnum];
				label1.Text = champions[championsnum];
				label2.Text = "Skin編號：" + championsnum;
				Image pic = Image.FromFile(gamepath + @"\Apps\LoLTW\Air\assets\images\champions\" + champions[championsnum] + "_" + skinnum + ".jpg");
				this.pictureBox1.BackgroundImage = pic;

				
				//一行一行的取讀取文字檔內的資料  System.Text.Encoding.Default才不會讀到亂碼
                StreamReader stmRdr2 = new StreamReader(gamepath + @"\Apps\LoLTW\Game\skinID.ini"); //讀取skinID.ini
				string skinini = stmRdr2.ReadLine();
				while (skinini != null)
				{
					if (skinini.IndexOf(@"[SummonerName]") != -1)
					{
						summoner = "start";
					}
					if (skinini.IndexOf(@"[Champions]") != -1) {
						summoner = "stop";
					}
					if(summoner=="start"){
						if (skinini.IndexOf(@" = 0") != -1)
						{
							// 調整陣列的大小
							System.Array.Resize(ref summonernames, summonernames.Length + 1);
							// 指定新的陣列值
							summonernames[summonernames.Length - 1] = skinini.Substring( 0, skinini.IndexOf(@" = 0")); //讀取召喚師名稱
							// 調整陣列的大小
							System.Array.Resize(ref summonernamesswitch, summonernamesswitch.Length + 1);
							// 指定新的陣列值
							summonernamesswitch[summonernamesswitch.Length - 1] = 0; //讀取開關值 0
						}
						if (skinini.IndexOf(@" = 1") != -1)
						{
							// 調整陣列的大小
							System.Array.Resize(ref summonernames, summonernames.Length + 1);
							// 指定新的陣列值
							summonernames[summonernames.Length - 1] = skinini.Substring( 0, skinini.IndexOf(@" = 1")); //讀取召喚師名稱
							// 調整陣列的大小
							System.Array.Resize(ref summonernamesswitch, summonernamesswitch.Length + 1);
							// 指定新的陣列值
							summonernamesswitch[summonernamesswitch.Length - 1] = 1; //讀取開關值 1
						}
					}
					skinini = stmRdr2.ReadLine();
				}
				stmRdr2.Close();
				for (int i = 0; i < summonernames.Length; i++)
				{
					comboBox2.Items.Add(summonernames[i]);
				}
				comboBox2.Text = summonernames[0];
                textBox1.Text = summonernames[0];

				label7.Text = "啟用中";
				label8.Text = "停用中";
				if (summonernamesswitch[0] == 1)
				{
					button8.Hide();
					button9.Show();
					label7.Show();
					label8.Hide();
				}
				if (summonernamesswitch[0] == 0)
				{
                    label6.Show(); //召喚師
                    comboBox2.Show(); //召喚師下拉選單
                    button8.Show(); //啟用
                    label8.Show(); //停用中
                    button10.Show(); //新增
                    button13.Show(); //修改
                    button12.Show(); //反安裝

                    button9.Hide(); //停用
					label7.Hide(); //啟用中
                    button1.Hide(); //套用造型
                    button2.Hide(); //英雄←
                    button3.Hide(); //英雄→
                    button4.Hide(); //造型→
                    button5.Hide(); //造型←
                    
                    button11.Hide(); //安裝
                    
                    button14.Hide(); //確定
                    label1.Hide(); //角色名稱
                    label2.Hide(); //造型編號
                    label3.Hide(); //已套用
                    //label4.Hide(); //用途：..
                    //label5.Hide(); //powered ...
                    
                    pictureBox1.Hide(); //造型圖
                    comboBox1.Hide(); //英雄下拉選單
                    textBox1.Hide(); //輸入召喚師名稱
				}
			}
            else
            {
                button1.Hide(); //套用造型
                button2.Hide(); //英雄←
                button3.Hide(); //英雄→
                button4.Hide(); //造型→
                button5.Hide(); //造型←
                button8.Hide(); //啟用
                button9.Hide(); //停用
                button10.Hide(); //新增
                button11.Show(); //安裝
                button12.Hide(); //反安裝
                button13.Hide(); //修改
                button14.Hide(); //確定
                label1.Hide(); //角色名稱
                label2.Hide(); //造型編號
                label3.Hide(); //已套用
                //label4.Hide(); //用途：..
                //label5.Hide(); //powered ...
                label6.Hide(); //召喚師
                label7.Hide(); //啟用中
                label8.Hide(); //停用中
                pictureBox1.Hide(); //造型圖
                comboBox1.Hide(); //英雄下拉選單
                comboBox2.Hide(); //召喚師下拉選單
                textBox1.Hide(); //輸入召喚師名稱
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string championskin;
            championskin = champions[championsnum] + " = " + skinnum;

            //取得英雄的skin數量
            DirectoryInfo di = new DirectoryInfo(gamepath + @"\Apps\LoLTW\Air\assets\images\champions");
            FileInfo[] files = di.GetFiles(champions[championsnum] + "_*.jpg"); //篩選檔名
            FileInfo[] files2 = di.GetFiles(champions[championsnum] + "_Splash_*.jpg"); //篩選檔名
            skinnummax = (files.Length - files2.Length - 1); //取得個數
            Array.Clear(files, 0, files.Length);
            Array.Clear(files2, 0, files2.Length);

            //for (int i = 0; i < skinnummax[championsnum]+1; i++)
            for (int i = 0; i < skinnummax+1; i++)
            {
                string str;
                using (StreamReader sr = new StreamReader(gamepath + @"\Apps\LoLTW\Game\skinID.ini")) //路徑
                {
                    str = sr.ReadToEnd();
                }
                str = str.Replace(champions[championsnum] + " = " + i, championskin);
                using (StreamWriter sw = new StreamWriter(gamepath + @"\Apps\LoLTW\Game\skinID.ini"))
                {
                    sw.Write(str);
                }
            }
            label3.Text = "已套用";
            label3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (championsnum == 0)
            {
                championsnum = champions.Length -1 ;
            }
            else {
                championsnum = championsnum - 1;
            }
            label1.Text = champions[championsnum];
            comboBox1.Text = champions[championsnum];
            skinnum = 0;
            Image pic = Image.FromFile(gamepath + @"\Apps\LoLTW\Air\assets\images\champions\" + champions[championsnum] + "_" + skinnum + ".jpg");
            this.pictureBox1.BackgroundImage = pic;
            label2.Text = "Skin編號：0";
            label3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (championsnum == champions.Length -1 )
            {
                championsnum = 0;
            }
            else
            {
                championsnum = championsnum + 1; 
            }
            
            label1.Text = champions[championsnum];
            comboBox1.Text = champions[championsnum];
            skinnum = 0;
            Image pic = Image.FromFile(gamepath + @"\Apps\LoLTW\Air\assets\images\champions\" + champions[championsnum] + "_" + skinnum + ".jpg");
            this.pictureBox1.BackgroundImage = pic;
            label2.Text = "Skin編號：0";
            label3.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //取得英雄的skin數量
            DirectoryInfo di = new DirectoryInfo(gamepath + @"\Apps\LoLTW\Air\assets\images\champions");
            FileInfo[] files = di.GetFiles(champions[championsnum] + "_*.jpg"); //篩選檔名
            FileInfo[] files2 = di.GetFiles(champions[championsnum] + "_Splash_*.jpg"); //篩選檔名
            skinnummax = (files.Length - files2.Length - 1); //取得個數
            Array.Clear(files, 0, files.Length);
            Array.Clear(files2, 0, files2.Length);

            if (skinnum == 0)
            {
                skinnum = skinnummax;
            }
            else
            {
                skinnum = skinnum - 1;
            }
            Image pic = Image.FromFile(gamepath + @"\Apps\LoLTW\Air\assets\images\champions\" + champions[championsnum] + "_" + skinnum + ".jpg");
            this.pictureBox1.BackgroundImage = pic;
            label2.Text = "Skin編號：" + skinnum;
            label3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //取得英雄的skin數量
            DirectoryInfo di = new DirectoryInfo(gamepath + @"\Apps\LoLTW\Air\assets\images\champions");
            FileInfo[] files = di.GetFiles(champions[championsnum] + "_*.jpg"); //篩選檔名
            FileInfo[] files2 = di.GetFiles(champions[championsnum] + "_Splash_*.jpg"); //篩選檔名
            skinnummax = (files.Length - files2.Length - 1); //取得個數
            Array.Clear(files, 0, files.Length);
            Array.Clear(files2, 0, files2.Length);
            
            if (skinnum == skinnummax)
            {
                skinnum = 0;
            }
            else
            {
                skinnum = skinnum + 1;
            }
            Image pic = Image.FromFile(gamepath + @"\Apps\LoLTW\Air\assets\images\champions\" + champions[championsnum] + "_" + skinnum + ".jpg");
            this.pictureBox1.BackgroundImage = pic;
            label2.Text = "Skin編號：" + skinnum;
            label3.Text = "";
            label4.Text = skinnummax.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OSType == "Windows XP")
            {
                string allusersgxp = @"C:\Documents and Settings\All Users\Application Data\GarenaMessenger\app.ini";
                gamepath = @"C:\Program Files\GarenaLoLTW\GameData";
                //讀取 app.ini ，取gamepath
                StreamReader stmRdr = new StreamReader(allusersgxp);
                string appini = stmRdr.ReadLine();
                while (appini != null)
                {
                    if (appini.IndexOf(@"cafe_LoLTW=") != -1)
                    {
                        gamepath = appini.Substring(appini.IndexOf(@"cafe_LoLTW=") + @"cafe_LoLTW=".Length); //預設為@"C:\Program Files\GarenaLoLTW\GameData"
                    }
                    appini = stmRdr.ReadLine();
                }
            }
            else
            {
                //取 gamepath

                //取使用者名稱
                string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string[] userinfo = username.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                username = userinfo[1];

                //取 Users 路徑(去掉使用者名稱)
                string allusers = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string[] allusersg = allusers.Split(new string[] { username }, StringSplitOptions.RemoveEmptyEntries);

                gamepath = @"C:\Program Files (x86)\GarenaLoLTW\GameData";
                //讀取 app.ini ，取gamepath
                //一行一行的取讀取文字檔內的資料  System.Text.Encoding.Default才不會讀到亂碼
                StreamReader stmRdr = new StreamReader(allusersg[0] + @"All Users\GarenaMessenger\app.ini"); //預設為@"C:\Users\All Users\GarenaMessenger\app.ini"
                string appini = stmRdr.ReadLine();
                while (appini != null)
                {
                    if (appini.IndexOf(@"cafe_LoLTW=") != -1)
                    {
                        gamepath = appini.Substring(appini.IndexOf(@"cafe_LoLTW=") + @"cafe_LoLTW=".Length); //預設為@"C:\Program Files (x86)\GarenaLoLTW\GameData"
                    }
                    appini = stmRdr.ReadLine();
                }
                stmRdr.Close();
            }
           
            championsnum = Array.IndexOf(champions, comboBox1.Text);
            label1.Text = champions[championsnum];
            skinnum = 0;
            Image pic = Image.FromFile(gamepath + @"\Apps\LoLTW\Air\assets\images\champions\" + champions[championsnum] + "_" + skinnum + ".jpg");
            this.pictureBox1.BackgroundImage = pic;
            label2.Text = "Skin編號：0";
            label3.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            summonernamesnum = Array.IndexOf(summonernames, comboBox2.Text);
            if (summonernamesswitch[summonernamesnum] == 1)
            {
                button8.Hide(); //啟用
                label8.Hide(); //停用中

                label6.Show(); //召喚師
                comboBox2.Show(); //召喚師下拉選單
                button10.Show(); //新增
                button13.Show(); //修改
                button12.Show(); //反安裝

                button9.Show(); //停用
                label7.Show(); //啟用中
                button1.Show(); //套用造型
                button2.Show(); //英雄←
                button3.Show(); //英雄→
                button4.Show(); //造型→
                button5.Show(); //造型←

                button11.Hide(); //安裝

                button14.Hide(); //確定
                label1.Show(); //角色名稱
                label2.Show(); //造型編號
                label3.Hide(); //已套用
                //label4.Hide(); //用途：..
                //label5.Hide(); //powered ...
                pictureBox1.Show(); //造型圖
                comboBox1.Show(); //英雄下拉選單
                textBox1.Hide(); //輸入召喚師名稱
            }
            if (summonernamesswitch[summonernamesnum] == 0)
            {
                label6.Show(); //召喚師
                comboBox2.Show(); //召喚師下拉選單
                button8.Show(); //啟用
                label8.Show(); //停用中
                button10.Show(); //新增
                button13.Show(); //修改
                button12.Show(); //反安裝

                button9.Hide(); //停用
                label7.Hide(); //啟用中
                button1.Hide(); //套用造型
                button2.Hide(); //英雄←
                button3.Hide(); //英雄→
                button4.Hide(); //造型→
                button5.Hide(); //造型←

                button11.Hide(); //安裝

                button14.Hide(); //確定
                label1.Hide(); //角色名稱
                label2.Hide(); //造型編號
                label3.Hide(); //已套用
                //label4.Hide(); //用途：..
                //label5.Hide(); //powered ...

                pictureBox1.Hide(); //造型圖
                comboBox1.Hide(); //英雄下拉選單
                textBox1.Hide(); //輸入召喚師名稱
            }
            textBox1.Text = comboBox2.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //召喚師-啟用
            string stron;
            using (StreamReader sron = new StreamReader(gamepath + @"\Apps\LoLTW\Game\skinID.ini")) //路徑
			{
                stron = sron.ReadToEnd();
			}
            stron = stron.Replace(summonernames[summonernamesnum] + " = 0", summonernames[summonernamesnum] + " = 1");
            using (StreamWriter swon = new StreamWriter(gamepath + @"\Apps\LoLTW\Game\skinID.ini"))
			{
                swon.Write(stron, false, utf8);
			}
            summonernamesswitch[summonernamesnum] = 1;
            button8.Hide(); //啟用
            label8.Hide(); //停用中
            
            label6.Show(); //召喚師
            comboBox2.Show(); //召喚師下拉選單
            button10.Show(); //新增
            button13.Show(); //修改
            button12.Show(); //反安裝

            button9.Show(); //停用
            label7.Show(); //啟用中
            button1.Show(); //套用造型
            button2.Show(); //英雄←
            button3.Show(); //英雄→
            button4.Show(); //造型→
            button5.Show(); //造型←

            button11.Hide(); //安裝

            button14.Hide(); //確定
            label1.Show(); //角色名稱
            label2.Show(); //造型編號
            label3.Hide(); //已套用
            //label4.Hide(); //用途：..
            //label5.Hide(); //powered ...
            pictureBox1.Show(); //造型圖
            comboBox1.Show(); //英雄下拉選單
            textBox1.Hide(); //輸入召喚師名稱
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //召喚師-停用
            string stroff;
            using (StreamReader sroff = new StreamReader(gamepath + @"\Apps\LoLTW\Game\skinID.ini")) //路徑
			{
                stroff = sroff.ReadToEnd();
			}
            stroff = stroff.Replace(summonernames[summonernamesnum] + " = 1", summonernames[summonernamesnum] + " = 0");
			using (StreamWriter sw = new StreamWriter(gamepath + @"\Apps\LoLTW\Game\skinID.ini"))
			{
                sw.Write(stroff, false, utf8);
			}
            summonernamesswitch[summonernamesnum] = 0;
            label6.Show(); //召喚師
            comboBox2.Show(); //召喚師下拉選單
            button8.Show(); //啟用
            label8.Show(); //停用中
            button10.Show(); //新增
            button13.Show(); //修改
            button12.Show(); //反安裝

            button9.Hide(); //停用
            label7.Hide(); //啟用中
            button1.Hide(); //套用造型
            button2.Hide(); //英雄←
            button3.Hide(); //英雄→
            button4.Hide(); //造型→
            button5.Hide(); //造型←

            button11.Hide(); //安裝

            button14.Hide(); //確定
            label1.Hide(); //角色名稱
            label2.Hide(); //造型編號
            label3.Hide(); //已套用
            //label4.Hide(); //用途：..
            //label5.Hide(); //powered ...

            pictureBox1.Hide(); //造型圖
            comboBox1.Hide(); //英雄下拉選單
            textBox1.Hide(); //輸入召喚師名稱
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Process.Start(gamepath + @"\Apps\LoLTW\Game\skinID.ini"); //開啟skinID.ini
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // To copy a file to another location and  
            // overwrite the destination file if it already exists. 
            System.IO.File.Copy(System.Environment.CurrentDirectory + @"\skinID.ini", gamepath + @"\Apps\LoLTW\Game\skinID.ini", true);
            System.IO.File.Copy(System.Environment.CurrentDirectory + @"\dinput8.dll", gamepath + @"\Apps\LoLTW\Game\dinput8.dll", true);
            Application.Restart();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            File.Delete(gamepath + @"\Apps\LoLTW\Game\skinID.ini");
            File.Delete(gamepath + @"\Apps\LoLTW\Game\dinput8.dll");
            Application.Exit();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            comboBox2.Hide();
            textBox1.Show();
            button14.Show();
            button10.Hide();
            button13.Hide();
            label7.Hide();
            label8.Hide();
            button8.Hide();
            button9.Hide();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //修改召喚師名稱
            string stroff;
            using (StreamReader sroff = new StreamReader(gamepath + @"\Apps\LoLTW\Game\skinID.ini")) //路徑
            {
                stroff = sroff.ReadToEnd();
            }
            stroff = stroff.Replace(summonernames[summonernamesnum] + " =", textBox1.Text + " =");
            using (StreamWriter sw = new StreamWriter(gamepath + @"\Apps\LoLTW\Game\skinID.ini"))
            {
                sw.Write(stroff, false, utf8);
            }
            Application.Restart();
        }
    }
}
