using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PLC
{
    public partial class Form1 : Form
    {
        SerialPort sPort = new SerialPort();
        public Form1()
        {
            InitializeComponent();
            //textBox6.Text = "输入需要写入的数据，最多隔四位用英文逗号隔开，出现其他符号会出错";
            this.init();
        }
public void init()
        {
            
            comboBox7.Items.AddRange(SerialPort.GetPortNames());
            if (comboBox7.Items.Count > 0)
            {
                comboBox7.SelectedIndex = 0;
            }
            comboBox4.SelectedIndex = 2;
            comboBox5.SelectedIndex = 1;
            comboBox6.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

      
private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (!sPort.IsOpen)
                {
                    MessageBox.Show("串口未打开！");
                    return;
                }
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择读写！");
                    return;
                }
                if (textBox4.Text.Trim() == String.Empty)
                {
                    MessageBox.Show("请输入位数！");
                    return;
                }
                if (Convert.ToInt32(textBox4.Text) % 2 != 0)
                {
                    MessageBox.Show("位数只能为偶数位！");
                    return;
                }
                /*这里是我之前那个plc通信协议解析，你可以不用看*/
                int a = Convert.ToInt32(textBox1.Text);//地址转二进制数
                int b = a * 2 + 4096;
                string str = Convert.ToString(b, 16).ToUpper();//转十六进制数
                String str2 = BitConverter.ToString(Encoding.Default.GetBytes(str)).Replace("-", "");//转ascii码
                string str1;
                string str3;
                string str4 = "02";               
                string str5;
                string str7 = "03'";
                string str14 = "null";
                int weishu = Convert.ToInt32(textBox4.Text);//读取位数
                str5 = Convert.ToString(weishu, 16).ToUpper();
                if (weishu < 10)
                {
                    str5 = "0" + textBox4.Text;
                }
                if (weishu > 64)
                {
                    MessageBox.Show("位数超过64的限制");
                    return;
                }
                String str6 = BitConverter.ToString(Encoding.Default.GetBytes(str5)).Replace("-", "");//转ascii码
                
                //如果是读
                if (comboBox1.SelectedIndex == 0) {
                    str1 = "30";
                    str3 = str1 + str2 + str6 + str7;  //将已转好的连接到一起
                long sum = 0;
                foreach (Match match in Regex.Matches(str3, @"\d{2}"))//将连接好的字符串两位两位截取转十进制数相加
                {
                    long s = Convert.ToInt64(match.Value, 16);
                    sum += s;
                }
                string str8 = Convert.ToString(sum, 16).ToUpper();//转十六进制字符串
                string str9 = str8.Substring(str8.Length - 1);//从倒数第一位截取字符串
                String str11 = BitConverter.ToString(Encoding.Default.GetBytes(str9)).Replace("-", "");
                string str10 = str8.Substring(str8.Length - 2, str8.Length - 2);//从倒数第二位截取字符串
                String str12 = BitConverter.ToString(Encoding.Default.GetBytes(str10)).Replace("-", "");
                string str13 = str4 + str3 + str12 + str11;
                str14 = str13.Replace("'", "");//这里字符串相加会出现一个‘号不知道原因，只能替换                
                }

       if (comboBox1.SelectedIndex == 1)//如果是写
                {

                    if (textBox6.Text.Trim() == String.Empty)
                    {
                        MessageBox.Show("请输入需要写入的数据");
                        return;
                    }
                   
                    string str18=null;
                    string str16 = textBox6.Text;
                    string str20=null;
                    string str22=null;
                    string[] arr = str16.Split(',');
                    string[] array2 = new string[4];
                    //int c = Convert.ToInt32(textBox1.Text);//地址转二进制数
                    //int d = c * 2 + 4096;
                    //string str17 = Convert.ToString(b, 16).ToUpper();//转十六进制数
               for (int i = 0; i < Regex.Matches(str16, @",").Count + 1; i++)
                    {
                        if (arr[i].Length > 4)
                        {
                            MessageBox.Show("最多隔四位，必须用逗号隔开");
                            return;

                        }
                        if (arr[i].Length < 4)//补充0操作
                                {
                                 for (int j = 0; j < 4 - arr[i].Length; j++)
                                    {
                                          str18 += "0";
                                    }
                                }
                        arr[i] = str18 + arr[i];
                        str = null;
                        for (int j = 0; j < 2; j++)
                        {  
                            array2[j] = arr[i].Substring(j * 2, 2);//按两位分割成字符串数组
                            str20 = array2[1] + array2[0];//必然只有两位
                        }
                          String str21 = BitConverter.ToString(Encoding.Default.GetBytes(str20)).Replace("-", "");
                          str22 += str21;
                        }
                            str1 = "31";
                            string str27 = str1 + str2 + str6 + str22 + str7; //将已转好的连接到一起
                       
                        long sum = 0;
                        foreach (Match match in Regex.Matches(str27, @"\d{2}"))//将连接好的字符串两位两位截取转十进制数相加
                        {
                            long s = Convert.ToInt64(match.Value, 16);
                            sum += s;
                        }
                        string str39 = null;
                        string str23 = Convert.ToString(sum, 16).ToUpper();//转十六进制字符串
                        str39 = str23;
                        string str24 = str23.Substring(str23.Length - 1);//从倒数第一位截取字符串
                        String str11 = BitConverter.ToString(Encoding.Default.GetBytes(str24)).Replace("-", "");//转ascii            
                        string str25 = str23.Substring(str23.Length - 2, str23.Length - 2);//从倒数第二位截取字符串
                        String str12 = BitConverter.ToString(Encoding.Default.GetBytes(str25)).Replace("-", "");
                        string str26 = str4 + str27 + str12 + str11;
                        str14 = str26.Replace("'", "");//这里字符串相加会出现一个‘号不知道原因，只能替换                                      
            
                 } 
                //解析出来的指令发送  
                string result = Regex.Replace(str14, @"(\d{2})", "$0 ");//把解析出来的指令用空格两两分开 
                textBox3.Text = result;
                str14 = str14.Replace(" ", "");
                byte[] returnBytes = new byte[str14.Length / 2];//将字符串转化为字节数组发送
                for (int i = 0; i < returnBytes.Length; i++)
                {
                    returnBytes[i] = Convert.ToByte(str14.Substring(i * 2, 2), 16);
                }
                sPort.Write(returnBytes, 0, returnBytes.Length);
                MessageBox.Show("发送成功");
            }         
            catch (Exception ex)
            {                
               MessageBox.Show(ex.Message);
            }
        }      
private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择读取数据格式！");
                    return;
                }
                if (sPort.BytesToRead == 0)//读取返回的数据，为零则没发
                {
                    MessageBox.Show("请先发送数据！");
                    return;
                }
              
                byte[] ReDatas=new byte[sPort.BytesToRead];
                sPort.Read(ReDatas,0,ReDatas.Length);
                this.AddData(ReDatas);
                if (textBox5.Text == "15" && comboBox1.SelectedIndex == 0)
                {
                    textBox2.Text = "15";
                    MessageBox.Show("指令错误!");
                    return;
                }               
                if(textBox5.Text=="15"&&comboBox1.SelectedIndex==1){
                    textBox2.Text = "15";
                    MessageBox.Show("接收错误!");
                    return;
                }
                if (textBox5.Text == "06" && comboBox1.SelectedIndex == 1)
                {
                    textBox2.Text = "06";
                    MessageBox.Show("接收成功!");
                    return;
                }   
                textBox2.Text = textBox5.Text;//这个时候textbox5读出来显示的是字节数组
                textBox2.Text = textBox2.Text.Replace(" ", "");               
                string str1 = textBox2.Text.Remove(0, 2);
                string str4 = str1.Remove(str1.Length - 6, 6);                                
                byte[] buff = new byte[str4.Length / 2];
                int index = 0;
                for (int i = 0; i < str4.Length; i += 2)
                {
                    buff[index] = Convert.ToByte(str4.Substring(i, 2), 16);
                    ++index;
                }
                string str3 = Encoding.Default.GetString(buff);//这段是为了把去了头尾的指令两个两个转化为ascii码对应的字符
                string[] array = new string[str3.Length];
                string[] array1 = new string[4];//因为array是按4位4位切割的，所以长度必然为4
                string result = null;
                string str = null;
                string str7 = null;
                for (int i = 0; i < str3.Length / 4; i++)
                {
                    array[i] = str3.Substring(i * 4, 4);//按四位四位把字符串切为字符串数组
                    for (int j = 0; j < 2; j++)
                    {
                        array1[j] = array[i].Substring(j * 2, 2);//按两位分割成字符串数组
                        str = array1[1] + array1[0];//必然只有两位
                    }
                    string str8 = str + ",";
                    str7 += str8;
                    long a = Convert.ToInt64(str, 16);                    
                    string str6 = a.ToString() + ",";//把数据用，分开
                    result += str6;                  
                }
                if (comboBox2.SelectedIndex == 0 && comboBox2.SelectedIndex != -1)//判断读写，增加30 31
                {
                    textBox2.Text = str7;
                }
                else
                {
                    textBox2.Text = result;
                }
                                              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           }
        /*这里是读取数据，因为我这个plc返回来的是字节数组，一般都是字节数组*/
            public void AddData(byte[] data)
            {
                if (textBox5.Text != null)
                {
                    textBox5.Text = null;
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", data[i]);
                }
                AddContent(sb.ToString().ToUpper().Trim());
            }
            private void AddContent(string content)
            {
                if (textBox5.Text.Length > 0)
                {
                    textBox5.AppendText("\r\n");
                }
                textBox5.AppendText(content);
            }
private void button3_Click(object sender, EventArgs e)//开
        {
            try
            {
                if (sPort.IsOpen == true)
                {
                    sPort.Close();
                }
                sPort.PortName = comboBox7.SelectedItem.ToString();
                
                if (comboBox4.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择数据位");
                }                
                if (comboBox4.SelectedIndex == 0)
                {
                    sPort.DataBits = 5;
                }
                if (comboBox4.SelectedIndex == 1)
                {
                    sPort.DataBits = 6;
                }
                if (comboBox4.SelectedIndex == 2)
                {
                    sPort.DataBits = 7;
                }
                if (comboBox4.SelectedIndex == 3)
                {
                    sPort.DataBits = 8;
                }
               
                if(comboBox5.SelectedIndex==0){
                    sPort.Parity = Parity.None;
                }
                if (comboBox5.SelectedIndex == 1)
                {
                    sPort.Parity = Parity.Even;
                }
                if (comboBox5.SelectedIndex == 2)
                {
                    sPort.Parity = Parity.Odd;
                }
                if (comboBox5.SelectedIndex == 3)
                {
                    sPort.Parity = Parity.Mark;
                }
                if (comboBox5.SelectedIndex == 4)
                {
                    sPort.Parity = Parity.Space;
                }
             
               if (comboBox6.SelectedIndex == 0)
               {
                   sPort.StopBits = StopBits.One;
               }
               if (comboBox6.SelectedIndex == 1)
               {
                   sPort.StopBits = StopBits.Two;
               }              
                sPort.Open();//开
                if (sPort.IsOpen == true)
                {
                    MessageBox.Show("串口打开成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
          }
    
private void button4_Click(object sender, EventArgs e)//关
        {
            sPort.Close();
            MessageBox.Show("串口已关闭");
        }      
private void textBox4_KeyPress(object sender, KeyPressEventArgs e)//位数栏只能输入数字
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }       
        }

public void clearData_Click(object sender, EventArgs e)//清空数据
        {
            
            textBox4.Text = null;
            textBox1.Text = null;
            textBox6.Text = null;
            textBox3.Text = null;
        }

private void textBox6_MouseClick(object sender, MouseEventArgs e)
          {
    
           }

private void button5_Click(object sender, EventArgs e)
            {
                textBox5.Text = null;
                textBox2.Text = null;
                
            }

        }

  }



