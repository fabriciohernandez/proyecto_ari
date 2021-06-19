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
using System.Xml;

namespace ARIProject
{
    public partial class Form1 : Form
    {
        private List<Client> clients = new List<Client>();
        string[] fileLines;

        public Form1()
        {
            InitializeComponent();
        }

        private void cmbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResultTitle.Text = "Resultado " + cmbFileType.Text;

            var fileType = Tools.GetFileType(txtOriginRoute.Text);
            if (fileType.Equals("json") && cmbFileType.Text.Equals("TXT"))
            {
                txtKey.Text = "No necesaria";
                txtKey.Enabled = false;
                cmbDeli.Text = "";
                cmbDeli.Enabled = true;
            }
            else if (fileType.Equals("json") && cmbFileType.Text.Equals("JSON"))
            {
                cmbDeli.Text = "No necesaria";
                cmbDeli.Enabled = false;
                txtKey.Clear();
                txtKey.Enabled = true;
            }
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
                    rTxtResult.Clear();
                    var fileType = Tools.GetFileType(selectedFile);
                    if (fileType.Equals("txt"))
                    {
                        cmbFileType.Items.Remove("TXT");
                        cmbFileType.Items.Remove("JSON");
                    }
                    else if (fileType.Equals("xml"))
                    {
                        cmbFileType.Text = "TXT";
                        cmbFileType.Enabled = false;
                    }
                    else if (fileType.Equals("json"))
                    {
                        cmbFileType.Items.Remove("XML");
                        cmbFileType.Items.Remove("JWT");
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
                        if (cmbFileType.Text.Equals("JWT"))
                        {
                            GenerateJWT();
                        }
                        else if (cmbFileType.Text.Equals("XML"))
                        {
                            GenerateXML();
                        }
                        break;
                    case "json":
                        if (cmbFileType.Text.Equals("JSON"))
                        {
                            GenerateJSONByJwT();
                        }
                        if (cmbFileType.Text.Equals("TXT"))
                        {
                            GenerateTxtByJson();
                        }
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

                MessageBox.Show("Archivo generado exitosamente y se ha guardado en: " + txtDestinyRoute.Text, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Lo sentimos, ha ocurrido un error inesperado al tratar de generar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateJWT()
        {
            try
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
                }
                else
                    File.WriteAllText(path, json);

                MessageBox.Show("Archivo generado exitosamente y se ha guardado en: " + txtDestinyRoute.Text, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {

                MessageBox.Show("Ha ocurrido un error al tratar de generar el JWT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
            {
                clients.Clear();
                clients = new List<Client>();
                for (int i = 0; i < fileLines.Length; i++)
                {
                    var att = fileLines[i].Split(cmbDeli.Text);
                    clients.Add(new Client(att[0], att[1], att[2], att[3], att[4], att[5]));

                }

                XmlTextWriter textWriter = new XmlTextWriter(txtDestinyRoute.Text + "/XMLGenerated.xml", null);
                textWriter.WriteStartDocument();
                textWriter.WriteStartElement("clientes");
                foreach (var item in clients)
                {
                    textWriter.WriteStartElement("cliente");
                    textWriter.WriteStartElement("documento");
                    textWriter.WriteString(item.documento);
                    textWriter.WriteEndElement();
                    textWriter.WriteStartElement("primer-nombre");
                    textWriter.WriteString(item.primer_nombre);
                    textWriter.WriteEndElement();
                    textWriter.WriteStartElement("apellido");
                    textWriter.WriteString(item.apellido);
                    textWriter.WriteEndElement();
                    textWriter.WriteStartElement("credit-card");
                    textWriter.WriteString(Vigenere.VigenereEncode(item.credit_card, txtKey.Text));
                    textWriter.WriteEndElement();
                    textWriter.WriteStartElement("tipo");
                    textWriter.WriteString(item.tipo);
                    textWriter.WriteEndElement();
                    textWriter.WriteStartElement("telefono");
                    textWriter.WriteString(item.telefono);
                    textWriter.WriteEndElement();
                    textWriter.WriteEndElement();
                }
                textWriter.WriteEndDocument();
                textWriter.Close();

                fileLines = File.ReadAllLines(txtDestinyRoute.Text + "/XMLGenerated.xml");
                for (int i = 0; i < fileLines.Length; i++)
                {
                    rTxtResult.Text = rTxtResult.Text + fileLines[i] + "\n";
                }

                MessageBox.Show("Arhivo XML generado exitosamente.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error al tratar de generar el archivo XML.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

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
            try
            {
                clients.Clear();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;
                string toTxt = "";
                using (XmlReader reader = XmlReader.Create(txtOriginRoute.Text))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                          
                            case XmlNodeType.Text:
                                toTxt += reader.Value + cmbDeli.Text;
                                break;
                            case XmlNodeType.EndElement:
                                if (reader.Name.Equals("cliente"))
                                {
                                    toTxt += "/";
                                }
                                break;
                        }
                    }
                }
                var clientsString = toTxt.Split("/");
                for (int i = 0; i < clientsString.Length-1; i++)
                {
                    clientsString[i] = clientsString[i].Substring(0, clientsString[i].Length - 2);
                }

                //convert to client object 
                for (int i = 0; i < clientsString.Length - 1; i++)
                {
                    var att = clientsString[i].Split(cmbDeli.Text);
                    clients.Add(new Client(att[0], att[1], att[2], att[3], att[4], att[5]));
                }


                //Creating file
                txtDestinyRoute.Text += "/GeneratedXmlToTxt.txt";
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
                    foreach (var element in clients)
                    {
                 
                        String clientText = element.documento
                                + cmbDeli.Text
                                + element.primer_nombre
                                + cmbDeli.Text
                                + element.apellido
                                + cmbDeli.Text
                                + Vigenere.Decode(element.credit_card, txtKey.Text)
                                + cmbDeli.Text
                                + element.tipo
                                + cmbDeli.Text
                                + element.telefono
                                + "\n";

                        Byte[] text = new UTF8Encoding(true).GetBytes(clientText);
                        fs.Write(text, 0, text.Length);

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
                MessageBox.Show("Ha ocurrido un errro al tratar de generar un txt a partir de un xml.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

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
            cmbDeli.Enabled = true;
            txtKey.Enabled = true;

            cmbFileType.Items.Clear();
            cmbFileType.Items.Add("JSON");
            cmbFileType.Items.Add("JWT");
            cmbFileType.Items.Add("TXT");
            cmbFileType.Items.Add("XML");
        }      
    }
}
