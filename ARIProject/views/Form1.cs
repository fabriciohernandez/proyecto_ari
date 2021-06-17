using ARIProject.controllers;
using ARIProject.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARIProject
{
    public partial class Form1 : Form
    {
        private string fileType;
        private List<Client> clients = new List<Client>();
        string[] fileLines;


        public Form1()
        {
            InitializeComponent();
        }

        private void cmbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResultTitle.Text = "Resultado " + cmbFileType.Text;
        }

        private void btnOriginSearchRoute_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files| *.txt;*.xml;*.json";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog.FileName;
                if (selectedFile != null)
                {
                    //Clean all fields
                    ClearAllFields();
                    //If json or xml selected we need to hide cmb file type, because the result is txt
                    var fileType = Tools.GetFileType(selectedFile);
                    if (fileType.Equals("xml") || fileType.Equals("json"))
                    {
                        cmbFileType.Enabled = false;
                        cmbFileType.Text = "TXT";
                    }

                    txtOriginRoute.Text = selectedFile;
                    fileLines = File.ReadAllLines(selectedFile);
                    for (int i = 0; i < fileLines.Length; i++)
                    {
                        fileContent.Text = fileContent.Text + fileLines[i] + "\n";
                    }
                }
            }
        }

        private void btnDestinySearchRoute_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = openFileDialog.SelectedPath;
                if (selectedPath != null)
                {
                    txtDestinyRoute.Text = selectedPath;
                }
            }
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            //Only for UI
            RestoreTextColors();

            //Validating Entrys
            if (ValidateEntrys())
            {
                switch (Tools.GetFileType(txtOriginRoute.Text))
                {
                    case "txt":
                        //Verify file to generate
                        if (cmbFileType.SelectedIndex == 0)
                        {
                            GenerateJSON();
                        }
                        else
                        {
                            GenerateXML();
                        }

                        break;
                    case "json":
                        GenerateTxtByJson();
                        break;
                    case "xml":
                        GenerateTxtByXml();
                        break;
                    default:
                        MessageBox.Show("Archivo seleccionado no soportado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

            }
            else
            {
                //Show error
                MessageBox.Show("Por favor complete todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResaltErrorsFields();
            }
        }

        private void GenerateJSON()
        {
            clients.Clear();
            for (int i = 0; i < fileLines.Length; i++)
            {
                var att = fileLines[i].Split(cmbDeli.Text);
                clients.Add(new Client(att[0], att[1], att[2], att[3], att[4], att[5]));

            }
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            var json = "[";
            foreach (Client client in clients)
            {
                json = json + ",\n" + JsonSerializer.Serialize(client, options);
            }
            rTxtResult.Text = json + "\n ]";
        }

        private void GenerateXML()
        {
            //TO DO
            MessageBox.Show("Pendiente de implementacion GenerateXML", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerateTxtByJson()
        {
            //TO DO
            MessageBox.Show("Pendiente de implementacion GenerateTxtByJson", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerateTxtByXml()
        {
            //TO DO
            MessageBox.Show("Pendiente de implementacion GenerateTxtByXml", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateEntrys()
        {
            return txtOriginRoute.Text.Length != 0
                && txtDestinyRoute.Text.Length != 0
                && cmbFileType.Text.Length != 0
                && cmbDeli.Text.Length != 0
                && txtKey.Text.Length != 0;
        }

        private void ResaltErrorsFields()
        {
            if (txtOriginRoute.Text.Length == 0)
            {
                lblTitleOriginRoute.ForeColor = Color.Red;
            }
            if (txtDestinyRoute.Text.Length == 0)
            {
                lblTitleDestinyRoute.ForeColor = Color.Red;
            }
            if (cmbFileType.Text.Length == 0)
            {
                lblTitleFileType.ForeColor = Color.Red;
            }
            if (cmbDeli.Text.Length == 0)
            {
                lblTitleDeli.ForeColor = Color.Red;
            }
            if (txtKey.Text.Length == 0)
            {
                lblTitleKey.ForeColor = Color.Red;
            }

        }

        private void RestoreTextColors()
        {
            lblTitleOriginRoute.ForeColor = Color.Black;
            lblTitleDestinyRoute.ForeColor = Color.Black;
            lblTitleFileType.ForeColor = Color.Black;
            lblTitleDeli.ForeColor = Color.Black;
            lblTitleKey.ForeColor = Color.Black;
        }

        private void ClearAllFields()
        {
            fileContent.Clear();
            txtDestinyRoute.Clear();
            cmbDeli.Text = "";
            cmbFileType.Text = "";
            txtKey.Clear();
            cmbFileType.Enabled = true;
        }


    }
}
