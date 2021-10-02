using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kiosk_sample2
{
    public partial class hot_level : Form
    {
        public string check;
        public hot_level()
        {
            InitializeComponent();
        }
        public void button1_Click(object sender, EventArgs e)  //결정 클릭
        {
            if(radioButton1.Checked == true)
            {               
                check = "순한맛";
            }
            else if(radioButton2.Checked == true)
            {                
                check = "보통맛";
            }
            else if(radioButton3.Checked == true)
            {                
                check = "매운맛";                
            }
            this.Visible = false;
        }
    }
}
