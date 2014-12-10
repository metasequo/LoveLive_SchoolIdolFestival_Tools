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

namespace ノート重要度計算機
{
    public partial class Form1 : Form
    {
        static bool Record = false;
        static bool Event = false;

        public Form1()
        {
            InitializeComponent();
        }

        // 文字の出現回数をカウント
        public int CountCharPlus(string s, string c){
            return s.Length - s.Replace(c, "").Length;
        }
        public int CountCharMinus(string s, string c){
            return s.Length - s.Replace(c, "-").Length;
        }

        //それぞれのコントロールを選択
        public Label Labeltab1(int i){
            return (Label)(tabPage1.Controls["label" + i.ToString()]);
        }
        public Label Labeltab2(int i){
            return (Label)(tabPage2.Controls["label" + i.ToString()]);
        }
        public Label Labeltab3(int i){
            return (Label)(tabPage3.Controls["label" + i.ToString()]);
        }

        public TextBox TextBoxtab1(int i){
            return (TextBox)(tabPage1.Controls["textBox" + i.ToString()]);
        }
        public TextBox TextBoxtab3(int i){
            return (TextBox)(tabPage3.Controls["textBox" + i.ToString()]);
        }

        public Button Buttontab1(int i){
            return (Button)(tabPage1.Controls["button" + i.ToString()]);
        }
        public Button Buttontab2(int i){
            return (Button)(tabPage2.Controls["button" + i.ToString()]);
        }
        public Button Buttontab3(int i){
            return (Button)(tabPage3.Controls["button" + i.ToString()]);
        }

        public NumericUpDown NumericUpDowntab1(int i){
            return (NumericUpDown)(tabPage1.Controls["numericUpDown" + i.ToString()]);
        }
        public NumericUpDown NumericUpDowntab2(int i){
            return (NumericUpDown)(tabPage2.Controls["numericUpDown" + i.ToString()]);
        }
        public NumericUpDown NumericUpDowntab3(int i){
            return (NumericUpDown)(tabPage3.Controls["numericUpDown" + i.ToString()]);
        }
        
//
//各ノード重要度計算機
//
//ファイル選択ボタン
        private void button1_Click(object sender, EventArgs e)
        {

//      OpenFileDialog コンポーネントを使用してファイルをストリームとして開く
            openFileDialog1.Filter = "テキスト ファイル|*.txt";
            openFileDialog1.Title = "譜面データファイルを選択してください。";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int plus, minus;
                System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName, System.Text.Encoding.GetEncoding("shift_jis"));
                string s = sr.ReadToEnd();
                for (int i = 0; i < 9; i++){
                    plus = CountCharPlus(s, (i + 1).ToString());
                    minus = CountCharMinus(s, (-1 - i).ToString());
                    plus -= minus;
                    NumericUpDowntab1(i + 23).Value = plus;
                    NumericUpDowntab1(i + 32).Value = minus;
                }

                sr.Close();
            }
            label1.Text = "ファイルが選択されました。";
            textBox1.Text = openFileDialog1.FileName;
        }

//計算ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            double tan = 0.0, naga = 0.0, most = 0.0;
            double[] important = new double[9];
            int i, j;
            for (i = 0; i < 9; i++)
            {
                tan = (Double)NumericUpDowntab1(i + 23).Value;
                naga = (Double)NumericUpDowntab1(i + 32).Value;
                important[i] = tan + naga * 1.25;
                Labeltab1(i + 14).Text = important[i].ToString();
            }
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++) if (most < important[j]) most = important[j];
                for (j = 0; j < 9; j++)
                {
                    if (most == important[j])
                    {
                        Buttontab1(j + 4).Text = (i + 1).ToString();
                        important[j] = 0.0;
                        most = -1.0;
                        break;
                    }
                }
            }

        }

//保存ボタン
        private void button13_Click(object sender, EventArgs e)
        {
//      SpenFileDialog コンポーネントを使用してファイルをストリームとして開く
            saveFileDialog1.Filter = "テキスト ファイル|*.txt";
            saveFileDialog1.Title = "結果を保存";
            saveFileDialog1.FileName = Path.GetFileName(textBox1.Text);

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int i, j;
                double most = 0.0;
                double[] important = new double[9];
                for (i = 0; i < 9; i++)
                {
                    important[i] = Double.Parse(Labeltab1(i + 14).Text);
                }
                System.IO.StreamWriter sw = new
                    System.IO.StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write("重要度ランキング\r\n順位\tボタン\t重要度\r\n");
                for (i = 0; i < 9; i++)
                {
                    for (j = 0; j < 9; j++) if (most < important[j]) most = important[j];
                    for (j = 0; j < 9; j++)
                    {
                        if (most == important[j])
                        {
                            sw.Write("{0}位:\t{1}\t{2}\r\n", i + 1, j + 1, important[j]);
                            important[j] = 0.0;
                            most = -1.0;
                            break;
                        }
                    }
                }
                sw.Close();
            }
        }

//終了ボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

//OpenFileDialog
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

//
//Rankup計算機
//
//Rankup計算ボタン
        private void button14_Click(object sender, EventArgs e)
        {
            int maxEXP, nowEXP, needEXP, Item = 0, needItem = 0, mem, LP;
            int i = 0, j = 0, k = 0, o = 0, p = 0, q = 0;
            DateTime dt1 = DateTime.Now;
            maxEXP = (int)numericUpDown1.Value;
            nowEXP = (int)numericUpDown2.Value;
            LP = (int)numericUpDown3.Value;
            needEXP = maxEXP - nowEXP;
            mem = needEXP;
            

            if (checkBox4.Checked == true)
            {
                Event = true;
                Item = (int)numericUpDown41.Value;
            }
            if (checkBox4.Checked == false)
            {
                Event = false;
                Item = 0;
            }

            while (needEXP > 0){
                needEXP -= 26;
                i++;
            }
            label36.Text = i.ToString() + "回でランクアップ";
            label37.Text = (i * 10).ToString() + "LP";
            label38.Text = ((i * 10 - LP) * 6).ToString() + "分";
            label39.Text = (((float)i * 10 - LP) * 6 / 60).ToString() + "時間";

            i = 0;
            needEXP = mem;
            while (needEXP > 0){
                needEXP -= 46;
                i++;
            }
            label40.Text = i.ToString() + "回でランクアップ";
            label41.Text = (i * 15).ToString() + "LP";
            label42.Text = ((i * 15 - LP) * 6).ToString() + "分";
            label43.Text = (((float)i * 15 - LP) * 6 / 60).ToString() + "時間";

            i = 0;
            needEXP = mem;
            while (needEXP > 0){
                needEXP -= 83;
                i++;
            }
            label44.Text = i.ToString() + "回でランクアップ";
            label45.Text = (i * 25).ToString() + "LP";
            label46.Text = ((i * 25 - LP) * 6).ToString() + "分";
            label47.Text = ((((float)i * 25 - LP) * 6 / 60)).ToString() + "時間";

            i = 0;
            needEXP = mem;
            while (needEXP > 0){
                if (Event == true && Item >= 30)
                {
                    if (checkBox3.Checked == true && Item >= 75 && needEXP >= 47 || (checkBox2.Checked == false && checkBox3.Checked == true))
                    {
                        needEXP -= 83;
                        needItem += 75;
                        Item -= 75;
                        o++;
                    }
                    else if (checkBox2.Checked == true && Item >= 45 && needEXP >= 27 || (checkBox1.Checked == false && checkBox2.Checked == true)){
                        needEXP -= 46;
                        needItem += 45;
                        Item -= 45;
                        p++;
                    }
                    else if (checkBox1.Checked == true && Item >= 30)
                    {
                        needEXP -= 26;
                        needItem += 30;
                        Item -= 30;
                        q++;
                    }
                    else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false)
                    {
                        needEXP -= 46;
                        needItem += 45;
                        Item -= 45;
                        p++;
                    }
                }
                else if (Event == false || (Event == true && Item < 30))
                {
                    if (checkBox3.Checked == true && needEXP >= 47 || (needEXP >= 27 && checkBox2.Checked == false && checkBox3.Checked == true))
                    {
                        needEXP -= 83;
                        if (Event == true) Item += 27;
                        i++;
                    }
                    else if (checkBox2.Checked == true && needEXP >= 27 || (checkBox1.Checked == false && checkBox2.Checked == true))
                    {
                        needEXP -= 46;
                        if (Event == true) Item += 16;
                        j++;
                    }
                    else if (checkBox1.Checked == true)
                    {
                        needEXP -= 26;
                        if (Event == true) Item += 10;
                        k++;
                    }
                    else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false)
                    {
                        needEXP -= 83;
                        if (Event == true) Item += 27;
                        i++;
                    }
                }

            }
            label52.Text = k.ToString() + "回";
            label53.Text = j.ToString() + "回";
            label54.Text = i.ToString() + "回";
            if (checkBox4.Checked == true)
            {
                label52.Text += " + イベント楽曲 " + q.ToString() + "回";
                label53.Text += " + イベント楽曲 " + p.ToString() + "回";
                label54.Text += " + イベント楽曲 " + o.ToString() + "回";
            }
            label57.Text = (k * 10 + j * 15 + i * 25).ToString() + "LP";
            label58.Text = (((k * 10 + j * 15 + i * 25) - LP) * 6).ToString() + "分";
            label59.Text = ((((float)k * 10 + j * 15 + i * 25) - LP) * 6 / 60).ToString() + "時間";
            label50.Text = needItem.ToString() + "個";

            TimeSpan ts1 = new TimeSpan(0, 0, (((k * 10 + j * 15 + i * 25) - LP) * 6), 0);
            DateTime dt2 = dt1 + ts1;
            label51.Text = dt2.Year + "年" + dt2.Month + "月" + dt2.Day + "日" + dt2.Hour + "時" + dt2.Minute + "分頃";

        }

//保存ボタン
        private void button24_Click(object sender, EventArgs e)
        {
//          SpenFileDialog コンポーネントを使用してファイルをストリームとして開く
            saveFileDialog1.Filter = "テキスト ファイル|*.txt";
            saveFileDialog1.Title = "結果を保存";
            saveFileDialog1.FileName = "Rankup.txt";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int i;
                System.IO.StreamWriter sw = new
                    System.IO.StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write("現在" + numericUpDown3.Value.ToString() + "LPあり、");
                sw.Write("現在の" + numericUpDown2.Value.ToString() + "EXPから" + numericUpDown1.Value.ToString());
                sw.Write("EXPまで貯めるためには･･･\r\n\r\n");
                for (i = 0; i < 3; i++)
                {
                    if (i == 0) sw.Write("NORMALのみ:");
                    if (i == 1) sw.Write("HARDのみ:");
                    if (i == 2) sw.Write("EXPERTのみ:");
                    sw.Write(Labeltab2(i * 4 + 36).Text);
                    sw.Write("\r\n必要LP:\t" + Labeltab2(i * 4 + 37).Text);
                    sw.Write("\r\nLP回復時間:\t" + Labeltab2(i * 4 + 38).Text + "\r\n\t\t" + Labeltab2(i * 4 + 39).Text + "\r\n\r\n");
                }
                sw.Write("最低限のLPでランクアップするには\r\nEASY:\t" + label52.Text + "\r\nNORMA\t" + label53.Text + "\r\nEXPERT\t" + label54.Text +
                    "\r\n必要LP:" + label57.Text + "\r\nLP回復時間:\t" + label58.Text + "\r\n\t\t" + label59.Text +
                    "\r\n\r\n※NORMAL及びHARDでは、「夏色えがおで1,2,Jump!」以降の曲をプレイした場合での計算です。\r\n");

                sw.Close();
            }
        }

//終了ボタン
        private void button25_Click(object sender, EventArgs e)
        {
            this.Close();
        }

//
//InputFileCreator
//
//記録開始ボタン
        private void button28_Click(object sender, EventArgs e)
        {
            if (Record == false){
                Record = true;
                Labeltab3(81).Text = "●REC";
                Buttontab3(28).Text = "記録終了";
            }
            else{
                Record = false;
                Labeltab3(81).Text = "";
                Buttontab3(28).Text = "記録開始";
                numericUpDown22.Value = 0;
                for (int i=0; i < 9; i++)
                {
                    numericUpDown22.Value += NumericUpDowntab3(i * 2 + 4).Value;
                    numericUpDown22.Value += NumericUpDowntab3(i * 2 + 5).Value;
                }
            }
        }

//押したキーによって加算
        private void Record_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            int sum, i;
            if(Record){
                switch (e.KeyData){
                    case Keys.D1:
                        if (numericUpDown4.Value < 100) numericUpDown4.Value += 1;
                        break;
                    case Keys.Q:
                        if (numericUpDown5.Value < 100) numericUpDown5.Value += 1;
                        break;
                    case Keys.D2:
                        if (numericUpDown6.Value < 100) numericUpDown6.Value += 1;
                        break;
                    case Keys.W:
                        if (numericUpDown7.Value < 100) numericUpDown7.Value += 1;
                        break;
                    case Keys.D3:
                        if (numericUpDown8.Value < 100) numericUpDown8.Value += 1;
                        break;
                    case Keys.E:
                        if (numericUpDown9.Value < 100) numericUpDown9.Value += 1;
                        break;
                    case Keys.D4:
                        if (numericUpDown10.Value < 100) numericUpDown10.Value += 1;
                        break;
                    case Keys.R:
                        if (numericUpDown11.Value < 100) numericUpDown11.Value += 1;
                        break;
                    case Keys.D5:
                        if (numericUpDown12.Value < 100) numericUpDown12.Value += 1;
                        break;
                    case Keys.T:
                        if (numericUpDown13.Value < 100) numericUpDown13.Value += 1;
                        break;
                    case Keys.D6:
                        if (numericUpDown14.Value < 100) numericUpDown14.Value += 1;
                        break;
                    case Keys.Y:
                        if (numericUpDown15.Value < 100) numericUpDown15.Value += 1;
                        break;
                    case Keys.D7:
                        if (numericUpDown16.Value < 100) numericUpDown16.Value += 1;
                        break;
                    case Keys.U:
                        if (numericUpDown17.Value < 100) numericUpDown17.Value += 1;
                        break;
                    case Keys.D8:
                        if (numericUpDown18.Value < 100) numericUpDown18.Value += 1;
                        break;
                    case Keys.I:
                        if (numericUpDown19.Value < 100) numericUpDown19.Value += 1;
                        break;
                    case Keys.D9:
                        if (numericUpDown20.Value < 100) numericUpDown20.Value += 1;
                        break;
                    case Keys.O:
                        if (numericUpDown21.Value < 100) numericUpDown21.Value += 1;
                        break;
                }
                sum = 0;
                for (i = 0; i < 9; i++)
                {
                    sum += (int)NumericUpDowntab3(i * 2 + 4).Value;
                    sum += (int)NumericUpDowntab3(i * 2 + 5).Value;
                }
                numericUpDown22.Value = sum;
            }
        }
        
//更新ボタン
        private void button30_Click(object sender, EventArgs e)
        {
            int sum = 0, i;
            for (i = 0; i < 9; i++)
            {
                sum += (int)NumericUpDowntab3(i * 2 + 4).Value;
                sum += (int)NumericUpDowntab3(i * 2 + 5).Value;
            }
            numericUpDown22.Value = sum;
        }

//リセットボタン
        private void button29_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                NumericUpDowntab3(i * 2 + 4).Value = 0;
                NumericUpDowntab3(i * 2 + 5).Value = 0;
            }
            NumericUpDowntab3(22).Value = 0;
        }

//保存ボタン
        private void button26_Click(object sender, EventArgs e)
        {
            numericUpDown22.Value = 0;
            for (int i = 0; i < 9; i++)
            {
                numericUpDown22.Value += NumericUpDowntab3(i * 2 + 4).Value;
                numericUpDown22.Value += NumericUpDowntab3(i * 2 + 5).Value;
            }

            saveFileDialog1.Filter = "テキスト ファイル|*.txt";
            saveFileDialog1.Title = "結果を保存";
            saveFileDialog1.FileName = textBox20.Text + " - " + comboBox1.Text;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int i, j;
                System.IO.StreamWriter sw = new
                    System.IO.StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write(numericUpDown22.Value.ToString() + "\r\n");
                for (i = 0; i < 9; i++)
                {
                    for (j = 0; j < NumericUpDowntab3(2 * i + 4).Value; j++){
                        sw.Write((i + 1) + "\r\n");
                    }
                    for (j = 0; j < NumericUpDowntab3(2 * i + 5).Value; j++){
                        sw.Write("-" + (i + 1) + "\r\n");
                    }
                }
                sw.Close();
            }
        }

//終了ボタン
        private void button27_Click(object sender, EventArgs e)
        {
            this.Close();
        }



//
//メドレーフェスティバル
//

//計算ボタン
        private void button33_Click(object sender, EventArgs e)
        {
            double EventPt, Exp;
            int difficulty, trac;
            int[,] basic = new int [,] {{241, 500, 777},
                                        {126, 262, 408},
                                        {72, 150, 234},
                                        {31, 64, 99}};
            int[] playLP = new int[] { 20, 12, 8, 4 };
            int[] nextRankExp = new int[] {6, 6, 8, 10, 13,
                16, 20, 24, 28, 34, 39, 46, 52, 60, 68, 76,
                85, 94, 104, 115, 125, 197, 149, 162, 174,
                188, 203, 217, 232, 247, 264, 281, 298};
            int maxEXP, nowEXP, needEXP;
            int maxEventPt, nowEventPt, needEventPt;
            int nowLP, needLP;
            nowLP = (int)numericUpDown48.Value;
            int RankTimes = 0, EventTimes = 0;
            DateTime dt1 = DateTime.Now, dt2;

            trac = (int)numericUpDown42.Value;

            switch (comboBox2.Text)
            {
                case "EXPERT":
                    difficulty = 0;
                    Exp = 83;
                    break;
                case "HARD":
                    difficulty = 1;
                    Exp = 46;
                    break;
                case "NORMAL":
                    difficulty = 2;
                    Exp = 26;
                    break;
                case "EASY":
                    difficulty = 3;
                    Exp = 12;
                    break;
                default:
                    difficulty = 0;
                    Exp = 83;
                    break;
            }

            EventPt = basic[difficulty, trac - 1];
            Exp *= trac;

            switch(comboBox3.Text)
            {
                case "S":
                    EventPt *= 1.2;
                    break;
                case "A":
                    EventPt *= 1.15;
                    break;
                case "B":
                    EventPt *= 1.1;
                    break;
                case "C":
                    EventPt *= 1.05;
                    break;
            }

            switch (comboBox4.Text)
            {
                case "S":
                    EventPt *= 1.08;
                    break;
                case "A":
                    EventPt *= 1.06;
                    break;
                case "B":
                    EventPt *= 1.04;
                    break;
                case "C":
                    EventPt *= 1.02;
                    break;
            }

            if (checkBox5.Checked == true)
            {
                EventPt *= 1.1;
            }

            if (checkBox6.Checked == true)
            {
                Exp *= 1.1;
            }

            label93.Text = Math.Round(EventPt, MidpointRounding.AwayFromZero).ToString() + "Pt";
            label94.Text = Math.Round(Exp, MidpointRounding.AwayFromZero).ToString() + "Exp";

//ランクアップ計算
            if (checkBox7.Checked == true)
            {
                maxEXP = (int)numericUpDown43.Value;
                nowEXP = (int)numericUpDown44.Value;
                needEXP = maxEXP - nowEXP;

                while (needEXP > 0)
                {
                    needEXP -= (int)Math.Round(Exp, MidpointRounding.AwayFromZero);
                    ++RankTimes;
                }

                needLP = RankTimes * playLP[difficulty] * trac;

                label104.Text = RankTimes.ToString() + "回";
                label106.Text = needLP.ToString() + "LP";
                label108.Text = ((needLP - nowLP) * 6).ToString() + "分";
                label109.Text = (((float)needLP - nowLP) * 6 / 60).ToString() + "時間";

                TimeSpan ts1 = new TimeSpan(0, 0, ((needLP - nowLP) * 6), 0);
                dt2 = dt1 + ts1;
                label110.Text = dt2.Year + "年" + dt2.Month + "月" + dt2.Day + "日" + dt2.Hour + "時" + dt2.Minute + "分頃";
            }

//イベントポイント計算
            if (checkBox8.Checked == true)
            {
                maxEventPt = (int)numericUpDown45.Value;
                nowEventPt = (int)numericUpDown46.Value;
                needEventPt = maxEventPt - nowEventPt;

                while (needEventPt > 0)
                {
                    needEventPt -= (int)Math.Round(EventPt, MidpointRounding.AwayFromZero);
                    ++EventTimes;
                }

                needLP = EventTimes * playLP[difficulty] * trac;

                label113.Text = EventTimes.ToString() + "回";
                label115.Text = needLP.ToString() + "LP";
                label117.Text = ((needLP - nowLP) * 6).ToString() + "分";
                label118.Text = (((float)needLP - nowLP) * 6 / 60).ToString() + "時間";

                TimeSpan ts1 = new TimeSpan(0, 0, ((needLP - nowLP) * 6), 0);
                dt2 = dt1 + ts1;
                label119.Text = dt2.Year + "年" + dt2.Month + "月" + dt2.Day + "日" + dt2.Hour + "時" + dt2.Minute + "分頃";
            }

//ランクアップを考慮する
            if (checkBox9.Checked == true)
            {
                int eventExp;
                double doubleExp;
                int targetExp;
                int nextRank, nowRank;
                int healLP;
                int needTime;
                int playNum = 0;
                
                nowRank = (int)numericUpDown47.Value;
                nextRank = nowRank + 1;
                needLP = EventTimes * playLP[difficulty] * trac;
                needTime = (needLP - nowLP) * 6;
                targetExp = (int)numericUpDown43.Value - (int)numericUpDown44.Value;
                eventExp = EventTimes * (int)Math.Round(Exp, MidpointRounding.AwayFromZero);

                while (eventExp > targetExp)
                {

                    if (nextRank > 33)
                    {
                        doubleExp = 34.45 * nextRank - 551;
                        if (nextRank <= 100)
                        {
                            doubleExp /= 2;
                        }
                        targetExp = (int)Math.Round(doubleExp, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        targetExp = nextRankExp[nextRank - 1];
                    }

                    eventExp -= targetExp;
                    nowRank++;
                    nextRank++;
                    playNum++;

                    if (nowRank < 300)
                    {
                        healLP = nowRank / 2 + 25;
                    }
                    else
                    {
                        healLP = (nowRank - 300) / 3 + 175;
                    }

                    needTime -= healLP * 6;
                }


                label122.Text = (nowRank).ToString();
                label124.Text = needTime.ToString() + "分";
                TimeSpan ts1 = new TimeSpan(0, 0, needTime, 0);
                dt2 = dt1 + ts1;
                label125.Text = dt2.Year + "年" + dt2.Month + "月" + dt2.Day + "日" + dt2.Hour + "時" + dt2.Minute + "分頃";


            }

        }

//ランクアップを考慮するとき、ランクアップ計算とイベントポイント計算にチェックを入れる
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                checkBox7.Checked = true;
                checkBox8.Checked = true;
            }
        }


//終了ボタン
        private void button32_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //保存ボタン
        private void button31_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "テキスト ファイル|*.txt";
            saveFileDialog1.Title = "結果を保存";
            saveFileDialog1.FileName = "MedleyFestival.txt";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int i;
                System.IO.StreamWriter sw = new
                    System.IO.StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write("プレイスタイル\n");
                sw.Write("難易度:" + comboBox2.Text + "\r\n");
                sw.Write("曲数:" + numericUpDown42.Value.ToString() + "\r\n");
                sw.Write("スコアランク:" + comboBox3.Text + "\r\n");
                sw.Write("コンボランク:" + comboBox4.Text + "\r\n\r\n");

                sw.Write("1プレイで獲得できるポイントは\r\n");
                sw.Write("獲得イベントポイント:" + label93.Text + "\r\n");
                sw.Write("獲得経験値:" + label94.Text + "\r\n");

                if (checkBox7.Checked == true)
                {
                    sw.Write("\r\n現在の" + numericUpDown43.Value.ToString() + "EXPから");
                    sw.Write(numericUpDown44.Value.ToString() + "EXPまで貯めるには\r\n");
                    sw.Write("プレイ回数:" + label104.Text);
                    sw.Write("\r\n必要LP" + label106.Text);
                    sw.Write("\r\n回復時間:" + label108.Text);
                    sw.Write("\r\n" + label109.Text);
                    sw.Write("\r\n目安は" + label110.Text + "です。\r\n");
                }

                if (checkBox8.Checked == true)
                {
                    sw.Write("\r\n現在の" + numericUpDown45.Value.ToString() + "ポイントから");
                    sw.Write(numericUpDown46.Value.ToString() + "ポイントまで貯めるには\r\n");
                    sw.Write("プレイ回数:" + label104.Text);
                    sw.Write("\r\n必要LP" + label106.Text);
                    sw.Write("\r\n回復時間:" + label108.Text);
                    sw.Write("\r\n" + label109.Text);
                    sw.Write("\r\n目安は" + label110.Text + "です。\r\n");
                }

                sw.Close();
            }
        }



    }
}
