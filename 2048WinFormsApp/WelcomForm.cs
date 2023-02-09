using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048WinFormsApp
{
    public partial class WelcomForm : Form
    {
        public List<RadioButton> radioButtons;

        public WelcomForm()
        {
            InitializeComponent();
            radioButtons = new List<RadioButton>
            {
                radioButton1, radioButton2, radioButton3, radioButton4
            };

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Close();    
        }

        private void WelcomForm_Load(object sender, EventArgs e)
        {

        }
    }
}
