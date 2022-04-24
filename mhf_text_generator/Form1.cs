using System.Text;
using System.Drawing;
using System.Linq;

namespace mhf_text_generator
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }
            
        private void button1_Click(object sender, EventArgs e)
        {
            int total = richTextBoxInput.Text.Length;
            var listdata = new List<byte>();
            Color prevcolor = Color.White;
            float prevsize = 0f;
            HorizontalAlignment prevpos = HorizontalAlignment.Left;

            bool needtoaddbyte = true;

            //<BODY><SIZE_0><C_1>123<BR><BODY>AAA<BR><BODY>BBB
            //<BR><BR><BODY><SIZE_0><C_1>123<BR><BODY>FFF
            //<BR><BODY>123<BR><BODY>FFF
            //< BR >< BR >< BODY > 123 < BR >< BODY > FFF

            for (int i = 0; i < total; i++)
            {
                richTextBoxInput.Select(i, 1);                          //set slection from fisrt word
                Color col = richTextBoxInput.SelectionColor;         //get color
                Font font = richTextBoxInput.SelectionFont;         //get font data
                float textsize = richTextBoxInput.SelectionFont.Size;       //get size
                string str = richTextBoxInput.SelectedText;                  //get single string
                HorizontalAlignment pos = richTextBoxInput.SelectionAlignment;

                if (str == "\n")
                {
                    //new line  
                    listdata.AddRange(new byte[] { 60, 66, 82, 62 });   //br
                    richTextBoxInput.Select(i + 1, 1);
                    if (richTextBoxInput.SelectedText == "\n")
                    {
                        needtoaddbyte = false;
                    }
                    else
                    {
                        listdata.AddRange(new byte[] { 60, 66, 79, 68, 89, 62 });       //body
                    }
                    richTextBoxInput.Select(i, 1);
                }
                else
                {
                    needtoaddbyte = true;

                }

                if (textsize == prevsize)
                {

                }
                else
                {
                    listdata.AddRange(GetSize(textsize));
                    prevsize = textsize;
                }

                if (col == prevcolor)
                {

                }
                else
                {
                    listdata.AddRange(GetColor(col));
                    prevcolor = col;
                }

                if (pos == prevpos)
                {

                }
                else
                {
                    if (pos == HorizontalAlignment.Center)
                    {
                        listdata.AddRange(new byte[] { 60, 67, 69, 78, 84, 69, 82, 62 });
                    }
                    else if (pos == HorizontalAlignment.Left)
                    {
                        listdata.AddRange(new byte[] { 60, 76, 69, 70, 84, 62 });
                    }

                    prevpos = pos;
                }

                if (needtoaddbyte)
                {
                    if (i == 0)
                    {
                        listdata.AddRange(new byte[] { 60, 66, 79, 68, 89, 62 });
                    }

                    if (str != "\n")
                    {
                        listdata.AddRange(Encoding.Default.GetBytes(str));
                    }
                }

            }

            byte[] le = { 00, 00 };
            le[0] = BitConverter.GetBytes(listdata.Count)[1];
            le[1] = BitConverter.GetBytes(listdata.Count)[0];
            listdata.InsertRange(0, le);

            if (checkBox2.Checked)
            {
                string res = "";
                for (int t = 0; t < listdata.Count; t++)
                {
                    if (t == listdata.Count - 1)
                    {
                        res = res + "0x" + listdata[t].ToString("X");
                    }
                    else
                    {
                        res = res + "0x" + listdata[t].ToString("X") + ", ";
                    }
                }

                richTextBox1.Text = res;
            }
            else
            {
                
                richTextBox1.Text = BitConverter.ToString(listdata.ToArray()).Replace("-", "");
            }
        }

        byte[] GetSize(float sizee)
        {
            string name = "";
            string siz = "";
            switch(sizee)
            {
                case 11:        //2
                    siz = (2).ToString();
                    break;
                case 13:        //3
                    siz = (3).ToString();
                    break;
                case 15:        //4
                    siz = (4).ToString();
                    break;
                case 19:
                    siz = (5).ToString();
                    break;
                case 21:
                    siz = (6).ToString();
                    break;
                case 23:
                    siz = (7).ToString();
                    break;
                case 27:        //8
                    siz = (8).ToString();
                    break;
                case 29:        //8
                    siz = (9).ToString();
                    break;
                default:
                    siz = (2).ToString();
                    break;


            }
            name = "<SIZE_" + siz + ">";
            byte[] bf = Encoding.Default.GetBytes(name);
            return bf;
        }

        byte[] GetColor(Color cl)
        {
            string name = "";
            string colr = "";
            switch (cl.Name)
            {
                case "Gray":
                    colr = "0";
                    break;
                case "Red":
                    colr = "1";
                    break;
                case "Lime":
                    colr = "2";
                    break;
                case "Yellow":
                    colr = "3";
                    break;
                case "Aqua":
                    colr = "4";
                    break;
                case "Fuchsia":
                    colr = "5";
                    break;
                case "White":
                    colr = "6";
                    break;
            }
            name = "<C_" + colr + ">";
            byte[] bf = Encoding.Default.GetBytes(name);
            return bf;
        }

        private void comColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color color = Color.White;
            switch (comColor.SelectedIndex)
            {
                case 0:
                    color = Color.Gray;
                    break;
                case 1:
                    color = Color.Red;
                    break;
                case 2:
                    color = Color.Lime;
                    break;
                case 3:
                    color = Color.Yellow;
                    break;
                case 4:
                    color = Color.Aqua;
                    break;
                case 5:
                    color = Color.Fuchsia;
                    break;
                case 6:
                    color = Color.White;
                    break;
            }
            richTextBoxInput.SelectionColor = color;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comColor.SelectedIndex = 6;           
            comSize.SelectedIndex = 0;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }



        private void comSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            float size = CalcSize(comSize.SelectedIndex);

            Font char_font = new Font(richTextBoxInput.Font.FontFamily, size, FontStyle.Bold);
            richTextBoxInput.SelectionFont = char_font;

        }

        int CalcSize(int num)
        {
            switch (num)
            {
                case 0:
                    num = 11;
                    break;
                case 1:
                    num = 13;
                    break;
                case 2:
                    num = 15;
                    break;
                case 3:
                    num = 19;
                    break;
                case 4:
                    num = 21;
                    break;
                case 5:
                    num = 23;
                    break;
                case 6:
                    num = 27;
                    break;
                case 7:
                    num = 29;
                    break;
                default:
                    num = 11;
                    break;

            }


                return num;
        }

        bool iscenter = false;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (!iscenter)
            //{
            //    richTextBoxInput.SelectionAlignment = HorizontalAlignment.Center;
            //    iscenter = true;
            //}
            //else
            //{
            //    richTextBoxInput.SelectionAlignment = HorizontalAlignment.Left;
            //    iscenter = false;
            //}
            richTextBoxInput.SelectionAlignment = HorizontalAlignment.Center;
            checkBox3.Checked = false;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBoxInput.Clear();
            richTextBox1.Clear();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            richTextBoxInput.SelectionAlignment = HorizontalAlignment.Left;
            checkBox1.Checked = false;
        }
    }
}