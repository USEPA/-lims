﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using LimsServer.Entities;
using PluginBase;

namespace LIMSDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select input file";
            if (ofd.ShowDialog() == DialogResult.OK)
                txtInput.Text = ofd.FileName;            
            
        }

        private void templateDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //foreach (DataGridViewRow row in templateDataGridView.Rows)
            //    row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProcessorManager procMgr = new ProcessorManager();
            string basePath = Assembly.GetExecutingAssembly().Location;
            string baseFolder = Path.GetDirectoryName(basePath);
            string procPaths = Path.Combine(baseFolder, "app_files\\processors");
            var lstProc = procMgr.GetProcessors(procPaths);
            comboBox1.DataSource = lstProc;
            comboBox1.DisplayMember = "Name";
            //comboBox1.ValueMember = "Processor";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessorDTO proc = comboBox1.Items[comboBox1.SelectedIndex] as ProcessorDTO;
            txtID.Text = proc.UniqueId;
            txtName.Text = proc.Name;
            txtDesc.Text = proc.Description;
            txtFileType.Text = proc.InstrumentFileType;
            
            //txtPath.Text = proc.;

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Running";
            ProcessorDTO proc = comboBox1.Items[comboBox1.SelectedIndex] as ProcessorDTO;
            txtID.Text = proc.UniqueId;
            txtName.Text = proc.Name;
            txtDesc.Text = proc.Description;
            txtFileType.Text = proc.InstrumentFileType;            

            ProcessorManager procMgr = new ProcessorManager();
            //string output = @"E:\lims\LIMSDesktop\bin\Debug\netcoreapp3.0\Processors\Output\file.csv";
            //string procPaths = @"E:\lims\lims_server\app_files\processors";
            DataTableResponseMessage dtRespMsg = procMgr.ExecuteProcessor(proc.Path, txtID.Text, txtInput.Text);

            if (string.IsNullOrWhiteSpace(dtRespMsg.Message))
                lblMessage.Text = "Success";
            else
                lblMessage.Text = dtRespMsg.Message;

            if (!string.IsNullOrWhiteSpace(dtRespMsg.LogMessage))
                LogMessage(dtRespMsg.LogMessage);
            else

            templateDataGridView.DataSource = dtRespMsg.TemplateData;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sfd = new SaveFileDialog();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Save output file location";
            fbd.ShowNewFolderButton = true;
            string fName = Path.GetFileNameWithoutExtension(txtInput.Text);
            
            //sfd.FileName = fName;
            //sfd.Filter = @"Excel Files (*.xlsx)|*.xlsx";
            DialogResult dr = fbd.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            
            ProcessorManager procMgr = new ProcessorManager();
            DataTable dt = templateDataGridView.DataSource as DataTable;
            //string dir = Path.GetDirectoryName(sfd.FileName);

            //string outPath = Path.Combine(sfd.FileName, "output");
            DirectoryInfo di = new DirectoryInfo(fbd.SelectedPath);
            if (!di.Exists)
                di.Create();

            procMgr.WriteTemplateOutputFile(fbd.SelectedPath, dt);
        }

        private void LogMessage(string message)
        {
            string logPath = "";
            try
            {
                string loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
                FileInfo fi = new FileInfo(loc);
                DirectoryInfo di = fi.Directory;
                if (!Directory.Exists(fi.Directory.FullName))
                    Directory.CreateDirectory(fi.Directory.FullName);

                logPath = Path.Combine(di.FullName, "logs", "lims_desktop.log");

                File.AppendAllText(logPath, message);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error writing to log file. " + ex.Message);
            }

            return;
        }
    }
}
