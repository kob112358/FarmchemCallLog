﻿using System;
using System.Windows.Forms;

namespace CallLog
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

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void SearchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                SearchButton_Click(sender, e);
            }
        }
    }
}
