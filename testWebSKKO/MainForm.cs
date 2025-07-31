using testWebSKKO.Models;
using testWebSKKO.Services;
using System.Diagnostics;
using testWebSKKO.Services;

namespace testWebSKKO
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnGenerateAndSign_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() != DialogResult.OK) return;

            string outputFolder = folderDialog.SelectedPath;
            string jsonPath = Path.Combine(outputFolder, "document.json");

            Document doc = DocumentGenerator.Create();
            string json = JsonHelper.Canonicalize(doc);
            File.WriteAllText(jsonPath, json);

            string signedPath = jsonPath + ".p7s";
            string avestExe = Path.Combine(
                Environment.Is64BitOperatingSystem ?
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) :
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                @"Avest\AvPCM_nces\AvCmUt4.exe");

            string args = $"-s \"{jsonPath}\" -T -m1 -M -o \"{signedPath}\"";

            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = avestExe;
                proc.StartInfo.Arguments = args;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.WaitForExit();

                MessageBox.Show($"✅ Успешно!\nJSON: {jsonPath}\nПодпись: {signedPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
