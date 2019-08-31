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
    public partial class AddAddressForm : Form
    {
        public AddAddressForm()
        {
            InitializeComponent();
        }


        public string CompanyCity
        {
            get { return addCompanyCity.Text; }
        }

        public string CompanyState
        {
            get { return addCompanyState.Text; }
        }

        public string CompanyZip
        {
            get { return addCompanyZip.Text; }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }

}
