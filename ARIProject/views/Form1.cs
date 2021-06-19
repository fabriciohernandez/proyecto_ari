using ARIProject.controllers;
using ARIProject.models;
using Soft = Newtonsoft.Json;
using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

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
                    if (fileType.Equals("xml"))
                    {
                        cmbFileType.Enabled = false;
                        cmbFileType.SelectedIndex = 3;
                        txtKey.Enabled = false;
                        txtKey.Text = "No necesario";
                    }
                   else if(fileType.Equals("json"))
                    {
                        cmbFileType.Enabled = false;
                        cmbFileType.SelectedIndex = 0;
                        txtKey.Enabled = true;
                        cmbDeli.Enabled = false;
                        cmbDeli.Text = "No necesario";

                    }
                    else
                    {
                        cmbFileType.Enabled = true;
                        txtKey.Enabled = true;
                        txtKey.Clear();
                        cmbDeli.Enabled = true;
                        cmbDeli.SelectedItem = 0;
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
                       
                        if (cmbFileType.SelectedIndex == 1)
                        {
                            GenerateJWT();
                        }
                        else
                        {
                            GenerateXML();
                        }

                        break;
                    case "json":
                        if (cmbFileType.SelectedIndex == 0)
                        {
                            GenerateJSONByJwT();
                        }
                        else
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

        private void GenerateJSONByJwT()
        {
            try
            {
                using (StreamReader r = new StreamReader(txtOriginRoute.Text))
                {

                    var clientsData = new List<Token>();
                    string path = txtDestinyRoute.Text + @"\JSONGenerated.json";
                    string json = r.ReadToEnd();
                    MessageBox.Show(json);
                    clientsData = Soft.JsonConvert.DeserializeObject<List<Token>>(json);
                   
                    var jsonGen = "[";
                    foreach (Token token in clientsData)
                    {
                        
                        try
                        {
                            if (jsonGen != "[")
                                jsonGen = jsonGen + "\n," + VerifyToken(token.cliente);
                            else
                                jsonGen = jsonGen + "\n" + VerifyToken(token.cliente);
                        }
                        catch
                        {
                            MessageBox.Show("Clave incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    jsonGen = jsonGen + "\n]";
                    rTxtResult.Text = jsonGen;
                    if (File.Exists(path))
                    {
                        DialogResult dialogResult = MessageBox.Show("El archivo ya existe", "¿Desea reemplazarlo?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            File.WriteAllText(path, jsonGen);
                        }

                    }
                    else
                        File.WriteAllText(path, jsonGen);
                }


            }
            catch (Exception)
            {
                //MessageBox.Show("Lo sentimos, ha ocurrido un error inesperado al tratar de generar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void GenerateJWT()
        {
            clients = new List<Client>();
            string path = txtDestinyRoute.Text + @"\JsonJwtGenerated.json";
            for (int i = 0; i < fileLines.Length; i++)
            {
                var att = fileLines[i].Split(cmbDeli.Text);
                clients.Add(new Client(att[0], att[1], att[2], att[3], att[4], att[5]));

            }

            var json = "[";
            foreach (Client client in clients)
            {
                if (json != "[")
                    json = json + "\n,{\"cliente\": \"" + GenerateAccessToken(client) + "\"}";
                else
                    json = json + "\n{\"cliente\": \"" + GenerateAccessToken(client) + "\"}";
            }
            json = json + "\n]";
            rTxtResult.Text = json;
            Console.WriteLine(path);
            if (File.Exists(path))
            {
                DialogResult dialogResult = MessageBox.Show("El archivo ya existe", "Desea reemplazarlo?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    File.WriteAllText(path, json);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
                File.WriteAllText(path, json);
        }


        private string GenerateAccessToken(Client client)
        {
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Encoding.ASCII.GetBytes(txtKey.Text))
                .AddClaim("documento", client.documento)
                .AddClaim("primer-nombre", client.primer_nombre)
                .AddClaim("apellido", client.apellido)
                .AddClaim("credit-card", client.credit_card)
                .AddClaim("tipo", client.tipo)
                .AddClaim("telefono", client.telefono)
                .Encode();
        }

        private string VerifyToken(string token)
        {
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                 .WithSecret(txtKey.Text)
                 .MustVerifySignature()
                 .Decode(token);
        }


        private void GenerateXML()
        {
            //TO DO
            MessageBox.Show("Pendiente de implementacion GenerateXML", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerateTxtByJson()
        {
            try
            {
                using (StreamReader r = new StreamReader(txtOriginRoute.Text))
                {
                    string json = r.ReadToEnd();
                    clients.Clear();
                    clients = Soft.JsonConvert.DeserializeObject<List<Client>>(json);
                    txtDestinyRoute.Text += "/jsonToTxtResult.txt";
                    //Creating file
                    if (File.Exists(txtDestinyRoute.Text))
                    {
                        DialogResult dialogResult = MessageBox.Show("El archivo ya ha sido generado con anterioridad en la misma ruta ¿desea reemplazarlo?", "Advertencia", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            File.Delete(txtDestinyRoute.Text);
                        }
                        else
                        {
                            Random _random = new Random();
                            txtDestinyRoute.Text = Tools.RemoveFileExtension(txtDestinyRoute.Text);
                            txtDestinyRoute.Text += _random.Next(1, 1000) + ".txt";
                        }

                    }

                    // Create a new file     
                    using (FileStream fs = File.Create(txtDestinyRoute.Text))
                    {
                        // Add some text to file    
                        foreach (Client element in clients)
                        {
                            String clientText = element.documento
                                + cmbDeli.Text
                                + element.primer_nombre
                                + cmbDeli.Text
                                + element.apellido
                                + cmbDeli.Text
                                + element.credit_card
                                + cmbDeli.Text
                                + element.tipo
                                + cmbDeli.Text
                                + element.telefono
                                + "\n";

                            Byte[] text = new UTF8Encoding(true).GetBytes(clientText);
                            fs.Write(text, 0, text.Length);

                        }
                    }
                }
                MessageBox.Show("Archivo generado exitosamente y se ha guardado en: " + txtDestinyRoute.Text, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fileLines = File.ReadAllLines(txtDestinyRoute.Text);
                for (int i = 0; i < fileLines.Length; i++)
                {
                    rTxtResult.Text = rTxtResult.Text + fileLines[i] + "\n";
                }
                ClearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Lo sentimos, ha ocurrido un error inesperado al tratar de generar el archivo de texto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

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
