using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarmchemCallLog
{
    public partial class SearchForm : Form
    {

        public SearchForm()
        {
            InitializeComponent();
            searchValue.Select();
        }

        public string SearchValue
        {
            get { return searchValue.Text; }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
