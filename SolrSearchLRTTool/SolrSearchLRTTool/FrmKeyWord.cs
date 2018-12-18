using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolrSearchLRTTool
{
    public partial class FrmKeyWord : Form
    {
        public FrmKeyWord()
        {
            InitializeComponent();
        }

        private void btnCreateKeyWord_Click(object sender, EventArgs e)
        {
            this.txtResult.Text = this.txtKey.Text.ReplaceALLByKeyword();
            this.txtResult.Refresh();
        }
    }
}
